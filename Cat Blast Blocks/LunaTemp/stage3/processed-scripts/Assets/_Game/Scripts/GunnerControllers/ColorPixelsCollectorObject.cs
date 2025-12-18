using System.Collections.Generic;
using UnityEngine;
using System;
// Custom ReadOnly attribute if not available in the project

public class ColorPixelsCollectorObject : CollectorMachanicObjectBase
{
    #region PROPERTIES

    public Transform RabbitRootTransform;
    public Transform RabbitRotateTransform;
    public CollectorVisualHandler VisualHandler;
    public CollectorAnimation CollectorAnimator;

    public State CurrentState = State.Normal;

    [Header("Movement")]
    [Tooltip("Movement handler script for spline movement")]
    public CachedTransformPathMover MovementHandle;
    public float NormalSpeed = 10;
    public float AbsoluteWinSpeed = 10;

    [Header("Target Grid")]
    [Tooltip("Current target grid object")]
    public PaintingGridObject CurrentGrid;

    [Header("Shooting Mechanics")]
    [Tooltip("Max number of bullets")]
    public int BulletCapacity = 10;

    [Tooltip("Bullets left")]
    public int BulletLeft;

    [Header("Color Matching")]
    [Tooltip("Pixel's color code that this object can shoot. Color of this object based on pixel's color")]
    public string CollectorColor = "WhiteDefault";

    [Header("Detection Settings")]
    [Tooltip("Distance threshold to detect and shoot nearby pixels")]
    public float DetectionRadius = 0.5f;

    [Header("CONFIG ATTRIBUTES")]
    public bool IsLocked;
    public bool IsHidden;
    public List<int> ConnectedCollectorsIDs; // original index of other collectors that connected to this collector

    [Header("Runtime Data")]
    public bool IsCollectorActive = true;
    public Vector3 CurrentTargetPosition;
    public MovementDirection currentMovementDirection = MovementDirection.Unknown;
    private Vector3 previousMovementDirection = Vector3.zero;
    public Color CurrentColor;

    // List of possible targets around the grid outline
    public PaintingPixel[] possibleTargets;

    [SerializeField] private BulletDisplayHandler bulletDisplayHandler;
    private int possibleTargetsCount;
    
    // Reuse list to avoid allocations in UpdatePossibleTargets
    private List<PaintingPixel> tempOutlinePixelsList = new List<PaintingPixel>();

    // Cache các biến để tối ưu hiệu năng CPU và RAM
    private Vector3 cachedRotation = new Vector3(0, -90, 0);
    private PaintingPixel[] cachedTargets;
    private int maxTargetCapacity = 0;

    [Header("ABSOLUTE WIN")]
    public int AbsoluteCheckRate = 5;
    private int currentFrameCount = 0;

    private bool dead = false;
    public bool IsHided = false;
    private bool hasRotated = false;
    public enum State
    {
        Normal,
        AbsoluteWin
    }

    // Property to check if collector is still available (has bullets left)
    public bool Available
    {
        get
        {
            return BulletLeft > 0;
        }
    }

    // Variables to track which columns/rows have been processed in the current movement direction
    private HashSet<int> processedHorizontalPositions = new HashSet<int>(); // For tracking rows when moving horizontally
    private HashSet<int> processedVerticalPositions = new HashSet<int>();   // For tracking columns when moving vertically

    public Action OnCompleteAllColorPixels = null;
    #endregion

    #region UNITY CORE
    private void Awake()
    {
        RegisterEvents();
    }
    private void Start()
    {
        InitializeCollector();
    }
    private void OnDisable()
    {
        UnregisterEvents();
    }
    private void OnDestroy()
    {
        UnregisterEvents();
    }

    [Header("OPTIMIZATION")]
    [Tooltip("Number of frames to skip between checks")]
    public int CheckIntervalFrames = 5;
    [Tooltip("Minimum distance moved before checking again")]
    public float MinMoveDistance = 0.1f;
    
    private int _frameCounter = 0;
    private Vector3 _lastCheckPosition;
    private float _minMoveDistanceSqr;

    private void Update()
    {
        if (!IsCollectorActive || BulletLeft <= 0) return;

        if (CurrentState == State.AbsoluteWin)
        {
            currentFrameCount++;
            if (currentFrameCount >= AbsoluteCheckRate)
            {
                CurrentGrid.UpdateOutlinePixels();
                UpdatePossibleTargets();
                currentFrameCount = 0;
            }
        }

        // Check for movement direction change to reset processed positions
        CheckMovementDirectionChange();

        // OPTIMIZED: Throttle checks
        _frameCounter++;
        if (_frameCounter >= CheckIntervalFrames)
        {
            // Check distance moved
            float distSqr = (RabbitRootTransform.position - _lastCheckPosition).sqrMagnitude;
            if (distSqr >= _minMoveDistanceSqr)
            {
                CheckAndDestroyNearbyPixels();
                _frameCounter = 0;
                _lastCheckPosition = RabbitRootTransform.position;
            }
        }
    }

    #endregion

    #region MAIN

    #region _events

    private void RegisterEvents()
    {
        GameplayEventsManager.OnGridObjectChanged += OnGridPixelsChanged;
        GameplayEventsManager.OnPaintingInitializeDone += OnGridPaintingObjectInitialized;
    }

    private void UnregisterEvents()
    {
        GameplayEventsManager.OnGridObjectChanged -= OnGridPixelsChanged;
        GameplayEventsManager.OnPaintingInitializeDone -= OnGridPaintingObjectInitialized;
    }

    // Callback for when grid pixels change
    private void OnGridPixelsChanged(PaintingGridObject changedGrid)
    {
        // Only update if this collector is targeting the changed grid and is active
        if (changedGrid == CurrentGrid && IsCollectorActive)
        {
            UpdatePossibleTargets();
        }
    }

    private void OnGridPaintingObjectInitialized(PaintingGridObject _gridObject)
    {
        CurrentGrid = _gridObject;
        InitializeCollector();
    }

    public void OnAbsoluteWin(bool isMoving)
    {
        CurrentState = State.AbsoluteWin;
        if (isMoving)
        {
            SetStatusAttributes();
        }
    }

    // Public method to activate/deactivate the collector
    public void SetCollectorActive(bool active)
    {
        SetActiveCollector(active);

        if (MovementHandle != null)
        {
            if (active)
            {
                MovementHandle.StartMovement();

                // Update movement direction after activation
                if (MovementHandle.IsPathValid())
                {
                    Vector3 initialDirection = MovementHandle.GetTangentAtTF(MovementHandle.currentTF);
                    previousMovementDirection = initialDirection;
                    currentMovementDirection = DetermineMovementDirection(initialDirection);
                }

                SetStatusAttributes();
            }
            else
            {
                MovementHandle.StopMovement();
                // Clear processed positions when deactivating
                processedHorizontalPositions.Clear();
                processedVerticalPositions.Clear();
            }
        }
    }

    public void SetStatusAttributes()
    {
        if (CurrentState == State.AbsoluteWin)
        {
            bool connectedOnBelt = !IsCollectorActive && !IsHided;
            if (IsCollectorActive || connectedOnBelt)
            {
                VisualHandler?.StartTrailHighSpeed();
                MovementHandle?.SetSpeed(AbsoluteWinSpeed);
                MovementHandle?.ChangeMovementType(CachedTransformPathMover.MovementType.Loop);
            }
        }
        else
        {
            VisualHandler?.StopTrailHighSpeed();
            MovementHandle?.SetSpeed(NormalSpeed);
            MovementHandle?.ChangeMovementType(CachedTransformPathMover.MovementType.Clamp);
        }
    }

    public void Unlock()
    {
        IsLocked = false;
        ApplyLockedState();
    }

    public void OnStartMove()
    {
        hasRotated = false;
    }

    public void Reveal()
    {
        IsHidden = false;
        VisualHandler.SetHiddenState(false, false);
        GameplayEventsManager.OnCollectorRevealed?.Invoke(this);
    }

    #endregion

    #region _initialize
    private void InitializeCollector()
    {
        BulletLeft = BulletCapacity;
        bulletDisplayHandler?.UpdateBulletDisplay(BulletLeft);

        if (MovementHandle != null)
        {
            //MovementHandle.StartMovement();
            // Initialize the previous movement direction
            if (MovementHandle.IsPathValid())
            {
                Vector3 initialDirection = MovementHandle.GetTangentAtTF(MovementHandle.currentTF);
                previousMovementDirection = initialDirection;
                currentMovementDirection = DetermineMovementDirection(initialDirection);
            }
        }
        CurrentState = State.Normal;
        VisualHandler?.StopTrailHighSpeed();
        MovementHandle?.SetSpeed(NormalSpeed);
        MovementHandle?.ChangeMovementType(CachedTransformPathMover.MovementType.Clamp);
        UpdatePossibleTargets();
        
        // Initialize optimization variables
        _minMoveDistanceSqr = MinMoveDistance * MinMoveDistance;
        _lastCheckPosition = RabbitRootTransform.position;
        _frameCounter = UnityEngine.Random.Range(0, CheckIntervalFrames); // Randomize start to distribute load
    }

    public void SetActiveCollector(bool active)
    {
        IsCollectorActive = active;
        if (IsCollectorActive) UpdatePossibleTargets();
    }
    #endregion

    #region _moving
    // Method to reset processed positions when movement direction changes
    private void CheckMovementDirectionChange()
    {
        if (MovementHandle.IsPathValid())
        {
            Vector3 rawMovementDirection = MovementHandle.GetTangentAtTF(MovementHandle.currentTF);

            // Only consider significant direction changes (not minor fluctuations)
            if (rawMovementDirection != Vector3.zero)
            {
                // Determine the new movement direction based on the tangent vector
                MovementDirection newMovementDirection = DetermineMovementDirection(rawMovementDirection);

                // Check if the primary axis of movement has changed
                bool wasHorizontal = IsHorizontalDirection(currentMovementDirection);
                bool isHorizontal = IsHorizontalDirection(newMovementDirection);

                // If movement direction has changed significantly (from horizontal to vertical or vice versa)
                if (wasHorizontal != isHorizontal)
                {
                    // Reset the appropriate processed positions
                    if (wasHorizontal)
                    {
                        processedHorizontalPositions.Clear(); // Clear row tracking when switching from horizontal to vertical
                    }
                    else
                    {
                        processedVerticalPositions.Clear();   // Clear column tracking when switching from vertical to horizontal
                    }
                }

                // Update the current movement direction
                currentMovementDirection = newMovementDirection;
                previousMovementDirection = rawMovementDirection;
            }
        }
    }

    // Helper method to determine movement direction from tangent vector
    private MovementDirection DetermineMovementDirection([Bridge.Ref] Vector3 tangent)
    {
        // Check if movement is primarily horizontal or vertical
        // Using direct comparison to avoid Mathf.Abs calls which can be slightly slower
        float absX = tangent.x >= 0 ? tangent.x : -tangent.x;
        float absZ = tangent.z >= 0 ? tangent.z : -tangent.z;
        
        if (absX > absZ)
        {
            // Horizontal movement
            if (tangent.x > 0)
            {
                return MovementDirection.HorizontalRightToLeft;  // Moving in positive X direction (right to left)
            }
            else
            {
                return MovementDirection.HorizontalLeftToRight;  // Moving in negative X direction (left to right)
            }
        }
        else
        {
            // Vertical movement
            if (tangent.z > 0)
            {
                return MovementDirection.VerticalTopToBottom;    // Moving in positive Z direction (top to bottom)
            }
            else
            {
                return MovementDirection.VerticalBottomToTop;    // Moving in negative Z direction (bottom to top)
            }
        }
    }

    // Helper method to check if a direction is horizontal
    private bool IsHorizontalDirection(MovementDirection direction)
    {
        return direction == MovementDirection.HorizontalLeftToRight ||
               direction == MovementDirection.HorizontalRightToLeft;
    }

    // Helper method to check if a direction is vertical
    private bool IsVerticalDirection(MovementDirection direction)
    {
        return direction == MovementDirection.VerticalBottomToTop ||
               direction == MovementDirection.VerticalTopToBottom;
    }
    #endregion

    #region _action
    // Method to update possible targets based on the current grid state
    public void UpdatePossibleTargets()
    {
        if (CurrentGrid != null)
        {
            // Get all outline pixels with the matching color code
            List<PaintingPixel> outlinePixelsWithColor = CurrentGrid.SelectOutlinePixelsWithColor(CollectorColor);

            if (outlinePixelsWithColor != null && outlinePixelsWithColor.Count > 0)
            {
                // Chỉ resize when cần thiết để tránh allocation thường xuyên
                if (cachedTargets == null || outlinePixelsWithColor.Count > maxTargetCapacity)
                {
                    maxTargetCapacity = Mathf.NextPowerOfTwo(outlinePixelsWithColor.Count);
                    cachedTargets = new PaintingPixel[maxTargetCapacity];
                }
                
                // Copy elements to the cached array
                outlinePixelsWithColor.CopyTo(cachedTargets, 0);
                possibleTargetsCount = outlinePixelsWithColor.Count;

                // Cập nhật reference để tương thích with code hiện tại
                possibleTargets = cachedTargets;
            }
            else
            {
                possibleTargets = new PaintingPixel[0];
                possibleTargetsCount = 0;
            }
        }
    }
    // Method to check and destroy nearby pixels

    private void OnRunOutOfBulletDestroyed()
    {
        if (!dead)
        {
            dead = true;
            SetActiveCollector(false);
            OnCompleteAllColorPixels?.Invoke();
            bulletDisplayHandler?.SetUpdateText(false);
            return;
        }
    }

    private void CheckAndDestroyNearbyPixels()
    {
        if (dead) return;
        if (BulletLeft <= 0 && !dead)
        {
            dead = true;
            SetActiveCollector(false);
            OnCompleteAllColorPixels?.Invoke();
            bulletDisplayHandler?.SetUpdateText(false);
            return;
        }

        // Determine the current movement direction using the tangent vector
        bool hasValidMovementDirection = currentMovementDirection != MovementDirection.Unknown;
        if (!hasValidMovementDirection) return;

        // Cache hướng di chuyển để tránh gọi method nhiều lần
        bool isHorizontalDirection = IsHorizontalDirection(currentMovementDirection);
        bool isVerticalDirection = !isHorizontalDirection;

        // Cache frequently accessed values to reduce property access
        Vector3 collectorPosition = RabbitRootTransform.position;
        float detectionRadiusSqr = DetectionRadius * DetectionRadius; // Use squared distance to avoid sqrt calculation
        float detectionRadius = DetectionRadius; // Cache the radius value

        PaintingPixel tempPixel = null;
        for (int i = 0; i < possibleTargetsCount; i++)
        {
            tempPixel = possibleTargets[i];

            if (tempPixel != null && tempPixel.InCount())
            {
                bool isCloseEnough = false;
                bool hasObstacle = false;
                bool hasBeenProcessed = false;

                if (hasValidMovementDirection)
                {
                    // Use coordinate-based detection depending on movement direction
                    // If moving more horizontally (X-axis), check only X positions
                    // If moving more vertically (Z-axis), check only Z positions

                    if (isHorizontalDirection)
                    {
                        // Moving horizontally - only compare X positions
                        float positionDiff = collectorPosition.x - tempPixel.worldPos.x;
                        // Use absolute value calculation directly to avoid Mathf.Abs call
                        float absPositionDiff = positionDiff >= 0 ? positionDiff : -positionDiff;
                        isCloseEnough = absPositionDiff <= detectionRadius;
                        if (!isCloseEnough) continue;

                        // Check if this column (X position) has already been processed in current direction
                        hasBeenProcessed = processedHorizontalPositions.Contains(tempPixel.column);
                        if (hasBeenProcessed) continue;

                        // Check for obstacles between collector and target pixel
                        hasObstacle = CheckForObstaclesInColumn(tempPixel);
                        if (hasObstacle) continue;
                    }
                    else // isVerticalDirection
                    {
                        // Moving vertically - only compare Z positions
                        float positionDiff = collectorPosition.z - tempPixel.worldPos.z;
                        // Use absolute value calculation directly to avoid Mathf.Abs call
                        float absPositionDiff = positionDiff >= 0 ? positionDiff : -positionDiff;
                        isCloseEnough = absPositionDiff <= detectionRadius;
                        if (!isCloseEnough) continue;

                        // Check if this row (Z position) has already been processed in current direction
                        hasBeenProcessed = processedVerticalPositions.Contains(tempPixel.row);
                        if (hasBeenProcessed) continue;

                        // Check for obstacles between collector and target pixel
                        hasObstacle = CheckForObstaclesInRow(tempPixel);
                        if (hasObstacle) continue;
                    }
                }
                else
                {
                    // Fallback to distance check if no valid movement direction
                    // Use squared distance to avoid sqrt calculation
                    float distanceSqr = (collectorPosition - tempPixel.worldPos).sqrMagnitude;
                    isCloseEnough = distanceSqr <= detectionRadiusSqr;
                    if (!isCloseEnough) continue;
                }

                if (isCloseEnough && !hasObstacle && !hasBeenProcessed)
                {
                    // Destroy the pixel
                    ShootPixel(tempPixel);
                    if (!hasRotated)
                    {
                        hasRotated = true;
                        RotateMesh();
                    }
                    // Mark position as processed in current direction
                    if (hasValidMovementDirection)
                    {
                        if (isHorizontalDirection)
                        {
                            // Moving horizontally - mark column as processed
                            processedHorizontalPositions.Add(tempPixel.column);
                        }
                        else // isVerticalDirection
                        {
                            // Moving vertically - mark row as processed
                            processedVerticalPositions.Add(tempPixel.row);
                        }
                    }
                }
            }
        }
    }

    private void RotateMesh()
    {
        if (RabbitRotateTransform) RabbitRotateTransform.localEulerAngles = cachedRotation;
    }

    // Method to destroy a specific pixel
    private void ShootPixel(PaintingPixel pixel)
    {
        if (pixel != null && pixel.InCount() && BulletLeft > 0)
        {
            // Mark bullet as used
            BulletLeft--;
            // Destroy the pixel using the grid's method
            CurrentGrid.ShootPixel(pixel);
            bulletDisplayHandler?.UpdateBulletDisplay(BulletLeft);

            // Get projectile from pool and configure it
            var proj = CollectorProjectilePool.Instance.GetProjectile(false);
            if (proj != null)
            {
                // Cache color and position to reduce property access
                Color currentColor = VisualHandler.CurrentColor;
                Vector3 spawnerPosition = VisualHandler.BulletSpawner.position;
                
                proj.SetColor(currentColor);
                proj.SetPosition(spawnerPosition);
                
                if (pixel.PixelComponent != null)
                {
                    proj.StartProjectile(pixel.PixelComponent);
                }
                else
                {
                    proj.StartProjectile(pixel.worldPos);
                }
            }

            VisualHandler.PlayMuzzleEffect();
            CollectorAnimator.PlayShootAnimation();

            if (BulletLeft <= 0)
            {
                OnRunOutOfBulletDestroyed();
            }
        }
    }

    public void SetBullet(int bulletCount)
    {
        BulletLeft = bulletCount;
        bulletDisplayHandler?.UpdateBulletDisplay(BulletLeft);
    }

    // Method to check for obstacles in the same row when moving horizontally
    private bool CheckForObstaclesInRow(PaintingPixel targetPixel)
    {
        if (CurrentGrid == null || CurrentGrid.paintingPixels == null)
            return false;

        // Check for pixels in the same row (z coordinate similar to target)
        // that are between the collector and target in the x-axis

        var pixelOnSameRow = CurrentGrid.GetPixelsInRow(targetPixel.row);

        switch (currentMovementDirection)
        {
            case MovementDirection.VerticalBottomToTop:
                foreach (PaintingPixel pixel in pixelOnSameRow)
                {
                    if (pixel.InCount() && pixel.column < targetPixel.column)
                    {
                        return true;
                    }
                }
                return false;
            case MovementDirection.VerticalTopToBottom:
                foreach (PaintingPixel pixel in pixelOnSameRow)
                {
                    if (pixel.InCount() && pixel.column > targetPixel.column)
                    {
                        return true;
                    }
                }
                return false;
        }

        return false; // No obstacles found
    }

    // Method to check for obstacles in the same column when moving vertically
    private bool CheckForObstaclesInColumn(PaintingPixel targetPixel)
    {
        // Check for pixels in the same column (x coordinate similar to target)
        // that are between the collector and target in the z-axis

        var pixelOnSameColumn = CurrentGrid.GetPixelsInColumn(targetPixel.column);

        switch (currentMovementDirection)
        {
            case MovementDirection.HorizontalLeftToRight:
                foreach (PaintingPixel pixel in pixelOnSameColumn)
                {
                    if (pixel.InCount() && pixel.row > targetPixel.row)
                    {
                        return true;
                    }
                }
                return false;
            case MovementDirection.HorizontalRightToLeft:
                foreach (PaintingPixel pixel in pixelOnSameColumn)
                {
                    if (pixel.InCount() && pixel.row < targetPixel.row)
                    {
                        return true;
                    }
                }
                return false;
        }

        return false; // No obstacles found
    }

    public void CheckTargetOnAbsoluteWin()
    {
        if (possibleTargetsCount <= 0) return;

    }
    #endregion

    #endregion

    #region SUPPORTIVE

    public void SelfDestroy()
    {
        SetActiveCollector(false);
        OnCompleteAllColorPixels?.Invoke();
        bulletDisplayHandler?.SetUpdateText(false);
    }

    #region _visual
    public void ApplyLockedState()
    {
        VisualHandler.SetLockedIcon(IsLocked);
    }

    public void ApplyHiddenState()
    {
        VisualHandler.SetHiddenState(IsHidden);
    }
    #endregion

    #endregion

#if UNITY_EDITOR
    // Visualize the detection radius in the editor
    private void OnDrawGizmosSelected()
    {
        if (IsCollectorActive)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(RabbitRootTransform.position, DetectionRadius);
        }
    }
#endif
}

public enum MovementDirection
{
    HorizontalLeftToRight,  // Bottom path: moving left to right
    HorizontalRightToLeft,  // Top path: moving right to left
    VerticalBottomToTop,    // Right path: moving bottom to top
    VerticalTopToBottom,     // Left path: moving top to bottom
    Unknown
}
