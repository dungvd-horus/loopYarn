using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelCollectorsSystem : MonoBehaviour
{
    #region PROPERTIES
    [Header("Configuration")]
    public LevelConfigSetup LevelSetup;
    public LevelColorCollectorsConfig CurrentLevelCollectorsConfig; // Current level config

    [Header("Formation Settings")]
    public Transform FormationCenter; // Center of the formation, formation's columns will aligns to this center
    public float SpaceBetweenColumns = 1.0f; // Space between each column
    public float SpaceBetweenCollectors = 1.0f; // Space between each collector in a column
    public Transform CollectorContainer; // Collector's parent to contain collector gameobject
    public Vector3 CollectorRotation = Vector3.zero; // Rotation to apply to spawned collectors
    public LevelMechanicObjectPrefabs PrefabSource;

    [Header("Runtime Data")]
    public List<LockObject> CurrentLocks;
    public List<ColorPixelsCollectorObject> CurrentCollectors; // Spawned collectors in current config
    public List<CollectorColumn> ObjectsInColumns; // List of columns created
    public List<List<CollectorController>> CollectorControllersColumns;
    public int CurrentTotalCollectors = 0;

    [Header("Performance Optimization")]
    // Pre-allocated collections to avoid GC allocations
    // Reuse pre-allocated static collections to avoid GC allocations for large maps
    private static readonly HashSet<int> _staticProcessedIndices = new HashSet<int>();
    private static readonly Queue<ColorPixelsCollectorObject> _staticQueue = new Queue<ColorPixelsCollectorObject>();

    private List<int> _tempProgressedIndex;
    private List<ColorPixelsCollectorObject> _tempCurrentGroup;
    private Dictionary<int, int> _collectorIndexByID; // For O(1) index lookup

    // Main dictionary for collector lookup
    public Dictionary<int, CollectorController> collectorControllersByID = new Dictionary<int, CollectorController>();

    // Additional dictionary for direct collector lookup by ID to avoid expensive searches
    private Dictionary<int, ColorPixelsCollectorObject> _collectorsByID = new Dictionary<int, ColorPixelsCollectorObject>();

    // Static collections for BFS to avoid allocations
    private static readonly HashSet<CollectorController> _tempVisited = new HashSet<CollectorController>();
    private static readonly Queue<CollectorController> _tempQueue = new Queue<CollectorController>();
    #endregion

    #region UNITY CORE
    //private void Start()
    //{
    //    SetupCollectorsAndMechanic();
    //}
    private void Awake()
    {
        GameplayEventsManager.GetFirstCollector += GetFirstCollector;
        GameplayEventsManager.OnCollectAKey += OnPlayerCollectAKey;
    }

    private void OnDestroy()
    {
        GameplayEventsManager.GetFirstCollector -= GetFirstCollector;
        GameplayEventsManager.OnCollectAKey -= OnPlayerCollectAKey;
    }

    private Transform GetFirstCollector()
    {
        if (CurrentCollectors.Count > 0)
        {
            return CurrentCollectors[0].transform;
        }
        return null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.yellow;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleLeft;

        if (CurrentCollectors.Count > 0)
        {
            for (int i = 0; i < CurrentCollectors.Count; i++)
            {
                if (CurrentCollectors[i] == null) continue;
                style.alignment = TextAnchor.MiddleLeft;
                Vector3 labelPos = CurrentCollectors[i].transform.position + CurrentCollectors[i].transform.right * 0.5f;
                //Handles.Label(labelPos, i.ToString() + $"({CurrentCollectors[i].BulletCapacity})", style);
                Handles.Label(labelPos, $"({CurrentCollectors[i].BulletCapacity})", style);

                style.alignment = TextAnchor.MiddleRight;
                if (CurrentCollectors[i].IsHidden)
                {
                    labelPos = CurrentCollectors[i].transform.position - CurrentCollectors[i].transform.right * 0.5f;
                    Handles.Label(labelPos, $"({CurrentCollectors[i].CollectorColor})", style);
                }
            }
        }
    }
#endif
    #endregion

    #region MAIN

    #region _initialize
    public void SetupCollectorsAndMechanic()
    {
        // Clear any existing collectors
        ClearExistingLocks();
        ClearExistingCollectors();

        if (CurrentLevelCollectorsConfig == null)
        {
            Debug.LogWarning("No LevelColorCollectorsConfig assigned!");
            return;
        }

        if (PrefabSource.GunnerPrefab == null)
        {
            Debug.LogError("CollectorPrefab is not assigned!");
            return;
        }

        if (CurrentLevelCollectorsConfig.CollectorColumns == null || CurrentLevelCollectorsConfig.CollectorColumns.Count == 0)
        {
            Debug.LogWarning("No collector setups found in the config!");
            return;
        }

        int numberOfColumns = CurrentLevelCollectorsConfig.NumberOfColumns();

        if (numberOfColumns <= 0)
        {
            Debug.LogWarning("NumberOfColumns must be greater than 0!");
            numberOfColumns = 1; // Default to 1 column if not set properly
        }

        // Initialize lists with capacity to avoid resizing
        InitializeCollections();

        SetUpAndArrangePosition(numberOfColumns);
        SetupConnectedCollectors();
    }

    private void InitializeCollections()
    {
        // Reuse existing collections instead of creating new ones
        CurrentLocks ??= new List<LockObject>();
        CurrentCollectors ??= new List<ColorPixelsCollectorObject>();
        ObjectsInColumns ??= new List<CollectorColumn>();
        CollectorControllersColumns ??= new List<List<CollectorController>>();
        _tempProgressedIndex ??= new List<int>();
        _tempCurrentGroup ??= new List<ColorPixelsCollectorObject>();
        _collectorIndexByID ??= new Dictionary<int, int>();
        _collectorsByID ??= new Dictionary<int, ColorPixelsCollectorObject>();

        // Clear existing data
        CurrentLocks.Clear();
        CurrentCollectors.Clear();
        ObjectsInColumns.Clear();
        CollectorControllersColumns.Clear();
        collectorControllersByID.Clear();
        _collectorIndexByID.Clear();
        _collectorsByID.Clear();
    }
    public void CreateLockObject()
    {
        LockObjectConfig config = new LockObjectConfig();
        // Spawn the collector with specified rotation

        Vector3 spawnPosition = FormationCenter.position;

        // Apply horizontal offset (perpendicular to forward direction) based on column
        spawnPosition += FormationCenter.right * (0 - (CurrentLevelCollectorsConfig.NumberOfColumns() - 1) / 2.0f) * SpaceBetweenColumns;

        // Apply depth offset (opposite to forward direction) based on row
        spawnPosition -= FormationCenter.forward * (ObjectsInColumns[0].CollectorsInColumn.Count) * SpaceBetweenCollectors;

        GameObject lockGO = Instantiate(PrefabSource.LockPrefab, spawnPosition, Quaternion.identity, CollectorContainer);
        lockGO.transform.localEulerAngles = CollectorRotation;
        LockObject lockObj = lockGO.GetComponent<LockObject>();

        if (lockObj != null)
        {
            lockObj.ID = config.ID;
            lockObj.Row = config.Row;

            // Add to our lists
            ObjectsInColumns[0].CollectorsInColumn.Add(lockObj);
            CurrentLocks.Add(lockObj);
        }
    }
    public void ClearExistingCollectors()
    {
        if (CurrentCollectors != null)
        {
            foreach (var collector in CurrentCollectors)
            {
                if (collector != null)
                {
                    DestroyImmediate(collector.gameObject);
                }
            }
            CurrentCollectors.Clear();
        }

        if (ObjectsInColumns != null)
        {
            ObjectsInColumns.Clear();
        }

        if (CollectorControllersColumns != null)
        {
            CollectorControllersColumns.Clear();
        }
    }

    public void SetupConnectedCollectors()
    {
        // Reuse pre-allocated collections to avoid GC allocations
        _tempProgressedIndex.Clear();
        _tempCurrentGroup.Clear();

        for (int i = 0; i < CurrentCollectors.Count; i++)
        {
            var collector = CurrentCollectors[i];
            if (collector == null) continue;

            if (_tempProgressedIndex.Contains(i)) continue;

            if (collector.ConnectedCollectorsIDs.Count <= 0)
            {
                _tempProgressedIndex.Add(i);
                collector.VisualHandler.SetupRope(false, null);
                continue;
            }

            _tempCurrentGroup.Clear();
            _tempCurrentGroup.Add(collector);
            _tempProgressedIndex.Add(i);

            // Tìm tất cả các collector được kết nối (optimized BFS)
            _staticProcessedIndices.Clear();
            _staticProcessedIndices.UnionWith(_tempProgressedIndex);
            _staticQueue.Clear();
            _staticQueue.Enqueue(collector);

            while (_staticQueue.Count > 0)
            {
                ColorPixelsCollectorObject currentCollector = _staticQueue.Dequeue();

                foreach (int _id in currentCollector.ConnectedCollectorsIDs)
                {
                    ColorPixelsCollectorObject connectTarget = GetCollectorByID(_id, out int _index);

                    if (_index == -1) continue;
                    if (_staticProcessedIndices.Contains(_index)) continue;

                    _staticProcessedIndices.Add(_index);
                    _tempCurrentGroup.Add(connectTarget);
                    _staticQueue.Enqueue(connectTarget);
                }
            }

            // Setup ropes between connected collectors to form a chain (like connected rope)
            for (int j = 0; j < _tempCurrentGroup.Count - 1; j++)
            {
                _tempCurrentGroup[j].VisualHandler.SetupRope(true, _tempCurrentGroup[j + 1].VisualHandler);
                // #if UNITY_EDITOR
                //                 _tempCurrentGroup[j].VisualHandler.TankRopeMesh.OnValidate();
                // #endif
            }
        }

        SetupCollectorControllersConnect();
    }

    public void SetupCollectorControllersConnect()
    {
        // Sử dụng Dictionary đã tạo ở bước 1
        if (collectorControllersByID == null) return;

        foreach (var controller in collectorControllersByID.Values)
        {
            // Reset danh sách kết nối
            controller.collectorConnect?.Clear();
            if (controller.collectorConnect == null)
            {
                controller.collectorConnect = new List<CollectorController>();
            }

            // Tìm tất cả các controller trong nhóm kết nối của controller này
            var connectedGroup = FindConnectedGroup(controller);

            // Sắp xếp theo thứ tự cột
            var sortedGroup = connectedGroup.OrderBy(c => c.ColumnIndex).ToList();

            // Gán nhóm đã sắp xếp cho controller
            controller.collectorConnect.AddRange(sortedGroup);
        }
    }

    // Hàm mới để tìm toàn bộ nhóm kết nối - optimized to avoid allocations
    private HashSet<CollectorController> FindConnectedGroup(CollectorController startController)
    {
        // Clear static collections to avoid allocations
        _tempVisited.Clear();
        _tempQueue.Clear();

        _tempVisited.Add(startController);
        _tempQueue.Enqueue(startController);

        while (_tempQueue.Count > 0)
        {
            var current = _tempQueue.Dequeue();
            var cp = current.ColorCollector;
            if (cp == null || cp.ConnectedCollectorsIDs == null) continue;

            foreach (int nextId in cp.ConnectedCollectorsIDs)
            {
                var nextController = FindCollectorControllerByID(nextId);
                if (nextController != null && !_tempVisited.Contains(nextController))
                {
                    _tempVisited.Add(nextController);
                    _tempQueue.Enqueue(nextController);
                }
            }
        }

        // Return the visited set (this is the static one, be careful with reuse)
        return _tempVisited;
    }

    private CollectorController FindCollectorControllerByID(int id)
    {
        collectorControllersByID.TryGetValue(id, out CollectorController controller);
        return controller; // Sẽ trả về controller nếu tìm thấy, hoặc null nếu không.
    }



    public void SetUpAndArrangePosition(int collumnCount)
    {
        ObjectsInColumns = new List<CollectorColumn>();
        CollectorControllersColumns = new List<List<CollectorController>>();
        collectorControllersByID.Clear();
        int col = 0;
        foreach (ColumnOfCollectorConfig colConfig in CurrentLevelCollectorsConfig.CollectorColumns)
        {
            int row = 0;
            var locks = colConfig.Locks;
            var collectors = colConfig.Collectors;
            CollectorColumn colObjects = new CollectorColumn();
            List<CollectorController> collectorControllersInThisColumn = new List<CollectorController>();

            int totalObjectCount = collectors.Count + locks.Count;

            int lockIndex = 0;
            int collectorIndex = 0;

            for (int i = 0; i < totalObjectCount; i++)
            {

                // Calculate the position relative to the formation center (which is the highest point)
                // Use the formation center's transform to properly orient the formation
                Vector3 spawnPosition = FormationCenter.position;

                // Apply horizontal offset (perpendicular to forward direction) based on column
                spawnPosition += FormationCenter.right * (col - (collumnCount - 1) / 2.0f) * SpaceBetweenColumns;

                // Apply depth offset (opposite to forward direction) based on row
                spawnPosition -= FormationCenter.forward * row * SpaceBetweenCollectors;

                if (locks.Any(x => x.Row == i))
                {
                    LockObjectConfig config = locks[lockIndex];
                    // Spawn the collector with specified rotation
                    GameObject lockGO = Instantiate(PrefabSource.LockPrefab, spawnPosition, Quaternion.identity, CollectorContainer);
                    lockGO.transform.localEulerAngles = CollectorRotation;
                    LockObject lockObj = lockGO.GetComponent<LockObject>();

                    if (lockObj != null)
                    {
                        lockObj.ID = config.ID;
                        lockObj.Row = config.Row;
                        // Add to our lists
                        colObjects.CollectorsInColumn.Add(lockObj);
                        CurrentLocks.Add(lockObj);
                        var controller = lockObj.GetComponent<CollectorController>();

                        if (!collectorControllersByID.ContainsKey(lockObj.ID))
                        {
                            collectorControllersByID.Add(lockObj.ID, controller);
                        }
                        else
                        {
                            Debug.LogWarning($"Trùng lặp ID của Lock: {lockObj.ID}. Sẽ ghi đè.", this);
                            collectorControllersByID[lockObj.ID] = controller;
                        }
                        controller.IndexInColumn = row;
                        controller.LockController = lockObj;
                        controller.IsLockObject = true;
                        controller.ColumnIndex = col;
                        collectorControllersInThisColumn.Add(controller);
                    }
                    lockIndex++;
                }
                else
                {
                    SingleColorCollectorConfig config = collectors[collectorIndex];

                    // Spawn the collector with specified rotation
                    GameObject collectorObj = Instantiate(PrefabSource.GunnerPrefab, spawnPosition, Quaternion.identity, CollectorContainer);
                    collectorObj.transform.localEulerAngles = CollectorRotation;
                    CollectorController controller = collectorObj.GetComponent<CollectorController>();
                    if (controller == null)
                    {
                        Debug.LogError("CollectorPrefab does not have CollectorController component!");
                        continue;
                    }
                    ColorPixelsCollectorObject pixelsCollector = controller.ColorCollector;

                    if (pixelsCollector != null)
                    {
                        controller.IndexInColumn = i;
                        controller.ColumnIndex = col;
                        controller.LockController = null;
                        controller.IsLockObject = false;
                        pixelsCollector.ID = config.ID;
                        controller.SetFadeBulletText(controller.IndexInColumn > 0);
                        if (!collectorControllersByID.ContainsKey(pixelsCollector.ID))
                        {
                            collectorControllersByID.Add(pixelsCollector.ID, controller);
                        }
                        else
                        {
                            Debug.LogWarning($"Trùng lặp ID của Collector: {pixelsCollector.ID}. Sẽ ghi đè.", this);
                            collectorControllersByID[pixelsCollector.ID] = controller;
                        }
                        // Find color from palette based on ColorCode
                        if (PrefabSource.ColorPallete != null && PrefabSource.ColorPallete.colorPallete.ContainsKey(config.ColorCode))
                        {
                            // Set the collector's color and shooting color
                            pixelsCollector.CollectorColor = config.ColorCode;
                            pixelsCollector.VisualHandler.SetColor(config.ColorCode);
                        }
                        else
                        {
                            pixelsCollector.CollectorColor = "Undefined";
                        }

                        // Apply bullet settings
                        pixelsCollector.BulletCapacity = config.Bullets;
                        pixelsCollector.BulletLeft = config.Bullets;
                        pixelsCollector.ConnectedCollectorsIDs = new List<int>(config.ConnectedCollectorsIDs);
                        pixelsCollector.IsLocked = config.Locked;
                        pixelsCollector.IsHidden = config.Hidden;
                        pixelsCollector.CurrentGrid = LevelSetup.CurrentGridObject;
                        pixelsCollector.IsCollectorActive = false;
                        // Set locked state (this might be handled by deactivating the collector)
                        if (false) pixelsCollector.SetCollectorActive(!config.Locked);

                        //pixelsCollector.IsCollectorActive = false;
                        pixelsCollector.ApplyHiddenState();
                        pixelsCollector.ApplyLockedState();

                        // Add to our lists
                        colObjects.CollectorsInColumn.Add(pixelsCollector);
                        CurrentCollectors.Add(pixelsCollector);
                        // Update index dictionary for O(1) lookup
                        _collectorIndexByID[pixelsCollector.ID] = CurrentCollectors.Count - 1;
                        // Also add to the direct collectors dictionary for O(1) lookup
                        if (!_collectorsByID.ContainsKey(pixelsCollector.ID))
                        {
                            _collectorsByID.Add(pixelsCollector.ID, pixelsCollector);
                        }
                        else
                        {
                            // In case of ID collision, log a warning and overwrite
                            Debug.LogWarning($"Trùng lặp ID của Collector trong _collectorsByID: {pixelsCollector.ID}. Sẽ ghi đè.", this);
                            _collectorsByID[pixelsCollector.ID] = pixelsCollector;
                        }
                        collectorControllersInThisColumn.Add(controller);
                    }
                    collectorIndex++;
                }

                row++;
            }
            CollectorControllersColumns.Add(collectorControllersInThisColumn);
            col++;
            ObjectsInColumns.Add(colObjects);
        }

        CurrentTotalCollectors = CurrentCollectors.Count;
        OnInitializedCollectors();
    }

    public void ReArrangePosition()
    {
        int col = 0;
        foreach (CollectorColumn column in ObjectsInColumns)
        {
            int row = 0;
            int columnCount = ObjectsInColumns.Count;
            var collectors = column.CollectorsInColumn;
            for (int i = 0; i < collectors.Count; i++)
            {
                // Calculate the position relative to the formation center (which is the highest point)
                // Use the formation center's transform to properly orient the formation
                Vector3 spawnPosition = FormationCenter.position;

                // Apply horizontal offset (perpendicular to forward direction) based on column
                spawnPosition += (col - (columnCount - 1) / 2.0f) * SpaceBetweenColumns * FormationCenter.right;

                // Apply depth offset (opposite to forward direction) based on row
                spawnPosition -= row * SpaceBetweenCollectors * FormationCenter.forward;

                // Spawn the collector with specified rotation
                collectors[i].transform.position = spawnPosition;
                collectors[i].transform.localEulerAngles = CollectorRotation;

                // Update index dictionary if this is a ColorPixelsCollectorObject
                // Avoid expensive CurrentCollectors.IndexOf call by using the collector's ID directly
                if (collectors[i] is ColorPixelsCollectorObject colorCollector)
                {
                    // We don't need to update the index in _collectorIndexByID here because the position in the CurrentCollectors list hasn't changed
                    // The positions are just being updated in the world, not the order in the list
                    // Only update if the collector is in our main dictionary
                    if (_collectorsByID.ContainsKey(colorCollector.ID))
                    {
                        // The index in CurrentCollectors remains the same, only world position changes
                        // No need to update the dictionary since the order hasn't changed
                    }
                }

                row++;
            }
            col++;
        }
    }
    #endregion

    #region _events
    private void OnPlayerCollectAKey()
    {
        LockObject lockToUnlock = GetFirstLockedCollectorMet();
        if (lockToUnlock != null)
        {
            lockToUnlock.Unlock();
        }
    }

    private void OnInitializedCollectors()
    {
        foreach (var column in CollectorControllersColumns)
        {
            if (column != null && column.Count > 0 && !column[0].IsLockObject)
            {
                GameplayEventsManager.OnCollectorMoveToFirstLine?.Invoke(column[0], column[0].ColorCollector.IsHidden);
            }
        }
    }
    #endregion

    #endregion

    #region SUPPORTIVE
    private LockObject GetFirstLockedCollectorMet()
    {
        foreach (CollectorColumn column in ObjectsInColumns)
        {
            foreach (var _object in column.CollectorsInColumn)
            {
                if (_object is LockObject)
                {
                    return _object as LockObject;
                }
            }
        }

        return null;
    }

    public void RemoveCollector(ColorPixelsCollectorObject target)
    {
        // Remove from dictionaries first
        if (target != null)
        {
            _collectorIndexByID.Remove(target.ID);
            _collectorsByID.Remove(target.ID);
        }

        CurrentCollectors.Remove(target);
        foreach (var colObjects in ObjectsInColumns)
        {
            if (colObjects.CollectorsInColumn.Contains(target))
            {
                colObjects.CollectorsInColumn.Remove(target);
                break;
            }
        }
        DestroyImmediate(target.gameObject);
        ReArrangePosition();
        SetupConnectedCollectors();
    }

    public ColorPixelsCollectorObject CloneNewFromCollector(ColorPixelsCollectorObject original)
    {
        // Spawn the collector with specified rotation
        GameObject collectorObj = Instantiate(PrefabSource.GunnerPrefab, Vector3.zero, Quaternion.identity, CollectorContainer);
        collectorObj.transform.localEulerAngles = CollectorRotation;
        ColorPixelsCollectorObject collector = collectorObj.GetComponent<ColorPixelsCollectorObject>();

        if (collector != null)
        {
            collector.ID = -1;

            // Find color from palette based on ColorCode
            if (PrefabSource.ColorPallete != null && PrefabSource.ColorPallete.colorPallete.ContainsKey(original.CollectorColor))
            {
                // Set the collector's color and shooting color
                collector.CollectorColor = original.CollectorColor;
                collector.VisualHandler.SetColor(original.CollectorColor);
            }

            // Apply bullet settings
            collector.BulletCapacity = original.BulletCapacity;
            collector.BulletLeft = original.BulletCapacity;
            collector.ConnectedCollectorsIDs = new List<int>(original.ConnectedCollectorsIDs);
            collector.IsLocked = original.IsLocked;
            collector.IsHidden = original.IsHidden;

            // Set locked state (this might be handled by deactivating the collector)
            if (false) collector.SetCollectorActive(!original.IsLocked);

            collector.ApplyHiddenState();
            collector.ApplyLockedState();

            CurrentCollectors.Add(collector);
            // Update index dictionary for O(1) lookup
            _collectorIndexByID[collector.ID] = CurrentCollectors.Count - 1;
            // Also add to the direct collectors dictionary for O(1) lookup if it has a valid ID
            if (collector.ID != -1 && !_collectorsByID.ContainsKey(collector.ID))
            {
                _collectorsByID.Add(collector.ID, collector);
            }
            else if (collector.ID != -1)
            {
                // In case of ID collision, log a warning and overwrite
                Debug.LogWarning($"Trùng lặp ID của Collector trong _collectorsByID: {collector.ID}. Sẽ ghi đè.", this);
                _collectorsByID[collector.ID] = collector;
            }
        }

        return collector;
    }

    // VIẾT LẠI HÀM GetCollectorByID - optimized to avoid expensive O(n) searches
    private ColorPixelsCollectorObject GetCollectorByID(int ID, out int index)
    {
        // First try to get from direct collectors dictionary for O(1) lookup
        if (_collectorsByID.TryGetValue(ID, out ColorPixelsCollectorObject collector))
        {
            // Get index from the precomputed dictionary for O(1) lookup instead of O(n)
            if (_collectorIndexByID.TryGetValue(ID, out index))
            {
                return collector;
            }
            else
            {
                // Fallback: compute index if not in dictionary
                index = CurrentCollectors.IndexOf(collector);
                if (index >= 0)
                {
                    _collectorIndexByID[collector.ID] = index;
                }
                return collector;
            }
        }

        // If not found in direct dictionary, try the old method
        CollectorController controller = FindCollectorControllerByID(ID);
        if (controller != null && !controller.IsLockObject && controller.ColorCollector != null)
        {
            collector = controller.ColorCollector;
            // Get index from the precomputed dictionary for O(1) lookup instead of O(n)
            if (_collectorIndexByID.TryGetValue(ID, out index))
            {
                return collector;
            }
            else
            {
                // Fallback: compute index if not in dictionary
                index = CurrentCollectors.IndexOf(collector);
                if (index >= 0)
                {
                    _collectorIndexByID[collector.ID] = index;
                }
                return collector;
            }
        }

        index = -1;
        return null;
    }

    public void ClearExistingLocks()
    {
        if (CurrentLocks != null)
        {
            foreach (var _lock in CurrentLocks)
            {
                if (_lock != null)
                {
                    DestroyImmediate(_lock.gameObject);
                }
            }
            CurrentLocks.Clear();
        }
    }

    public int GetBulletsByColor(string colorCode)
    {
        int rs = 0;
        foreach (var collector in CurrentCollectors)
        {
            if (collector.CollectorColor.Equals(colorCode)) rs += collector.BulletCapacity;
        }
        return rs;
    }

    #endregion
}
