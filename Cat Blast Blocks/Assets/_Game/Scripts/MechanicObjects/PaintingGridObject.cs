using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HarmonyLib.Code;
using static PaintingSharedAttributes;
using Random = UnityEngine.Random;

public class PaintingGridObject : MonoBehaviour
{
    #region PROPERTIES
    public PaintingGridEffects EffectHandler;
    public InGameEffectOptions EffectOptions;
    public ColorPalleteData ColorPalette;

    [Header("Grid Properties")]
    public Vector2 gridSize;
    public List<PaintingPixel> paintingPixels;
    public Transform GridTransform;
    public List<string> CurrentLevelColor = new List<string>();

    // Dictionary để lưu trữ các Material đã được clone/tạo cho mỗi colorCode
    [SerializeField] private Dictionary<string, Material> colorCodeMaterials = new Dictionary<string, Material>();

    [Header("Pipes")]
    public List<PipeObject> PipeObjects = new List<PipeObject>();

    [Header("Pipe Object Pixels")]
    public List<PaintingPixel> pipeObjectsPixels = new List<PaintingPixel>();  // New list for pipe pixels that are outside the grid


    [Header("Walls")]
    public List<WallObject> WallObjects = new List<WallObject>();

    [Header("Wall Object Pixels")]
    public List<PaintingPixel> wallObjectsPixels = new List<PaintingPixel>();

    [Header("Keys")]
    public List<KeyObject> KeyObjects = new List<KeyObject>();

    [Header("Key Object Pixels")]
    public List<PaintingPixel> keyObjectsPixels = new List<PaintingPixel>();

    [Header("Addition Pixels")]
    public List<PaintingPixel> AdditionPaintingPixels = new List<PaintingPixel>();

    [Header("Grid Settings")]

    public float pixelArrangeSpace = 1.0f;
    public Vector3 blockScale = Vector3.one;
    public GameObject pixelPrefab;
    public float YOffset = 0;

    [Header("Color Variation")]
    [Range(0.0f, 1.0f)]
    public float colorVariationAmount = 0.0f;

    [Header("Default Prefabs")]
    public LevelMechanicObjectPrefabs PrefabSource;

    // Thêm Shared Material gốc để clone
    [Header("Shared Material for Pixels")]
    public Material basePixelSharedMaterial;

    private List<PaintingPixel> currentOutlinePixels = new List<PaintingPixel>();

    [Header("Block Fountain")]
    public GridBlockFountainModule BlockFountainModule;
    public List<BlockFountainObject> BlockFountainObjects = new List<BlockFountainObject>();
    public List<PaintingPixel> BlockFountainObjectsPixels = new List<PaintingPixel>();


    [Space]
    public int PixelCount = 0;
    public int PixelDestroyed = 0;

    [Space]
    public int MinRow = 0;
    public int MaxRow = 0;
    public int MinColumn = 0;
    public int MaxColumn = 0;

    // Lists for faster lookup by row and column (serializable)
    private Dictionary<int, List<PaintingPixel>> pixelsByRow = new Dictionary<int, List<PaintingPixel>>();
    private Dictionary<int, List<PaintingPixel>> pixelsByColumn = new Dictionary<int, List<PaintingPixel>>();
    [SerializeField] private Dictionary<string, List<PaintingPixel>> pixelByColors = new Dictionary<string, List<PaintingPixel>>();

    public Dictionary<int, List<PaintingPixel>> PixelsByRow => pixelsByRow;
    public Dictionary<int, List<PaintingPixel>> PixelsByColumn => pixelsByColumn;
    public Dictionary<string, List<PaintingPixel>> PixelByColor => pixelByColors;

    // Helper: chọn palette đang dùng (ưu tiên ColorPalette nếu được gán trực tiếp)
    private ColorPalleteData ActivePalette => ColorPalette != null ? ColorPalette : PrefabSource != null ? PrefabSource.ColorPallete : null;

    #endregion

    #region UNITY CORE
    private void Awake()
    {
        ColorPalette.SetupMaterials();
        GridTransform = transform;
        RegisterEvent();
        if (BlockFountainModule == null)
            BlockFountainModule = GetComponent<GridBlockFountainModule>();
    }

    private void OnDestroy()
    {
        UnregisterEvent();
        ClearColorCodeMaterials(); // Giải phóng các material đã tạo
    }
    #endregion


    #region _initialize

    public void InitializeLevel(PaintingConfig paintingConfig)
    {
        ApplyPaintingConfig(paintingConfig);

        // Initialize mappings from existing pixels if they exist
        InitializePixelMappings();

        // Setup mechanic dependencies
        SetUpMechanicDependencies();

        PixelCount = paintingConfig.GetAllWorkingPixels().Count;
        PixelDestroyed = 0;

        currentOutlinePixels = SelectOutlinePixels();

        GameplayEventsManager.OnPaintingInitializeDone?.Invoke(this);
    }

    // Method to apply painting configuration to the grid
    public void ApplyPaintingConfig(PaintingConfig paintingConfig)
    {
        ClearAllAdditionPixels();

        ClearAllKeys();
        ClearAllPipes();
        ClearAllWalls();
        ClearColorCodeMaterials(); // Clear materials for new level

        if (paintingConfig == null)
        {
            Debug.LogWarning("No PaintingConfig assigned to apply.");
            return;
        }

        // Iterate through all pixel configs in the painting config
        foreach (PaintingPixelConfig pixelConfig in paintingConfig.Pixels)
        {
            // ★ FIX: Luôn lookup màu từ ColorPalette bằng colorCode
            Color pixelColor = Color.white;

            if (!string.IsNullOrEmpty(pixelConfig.colorCode) &&
                pixelConfig.colorCode.Equals(TransparentColorKey))
            {
                pixelColor = Color.clear;
            }
            else if (ActivePalette != null && !string.IsNullOrEmpty(pixelConfig.colorCode))
            {
                pixelColor = ActivePalette.GetColorByCode(pixelConfig.colorCode);
            }
            else if (pixelConfig.color != Color.black && pixelConfig.color.a > 0)
            {
                // Fallback: dùng color từ config nếu có và không phải black
                pixelColor = pixelConfig.color;
            }

            Color variedColor = ApplyColorVariation(pixelColor, colorVariationAmount);
            SetupPixelObject(pixelConfig.column, pixelConfig.row, variedColor, pixelConfig.colorCode, pixelConfig.Hidden);
        }

        foreach (var additionPixelConfig in paintingConfig.AdditionPixels)
        {
            PaintingPixel additionPixel = CreateNewPaintingPixelReal(additionPixelConfig, false);
            if (additionPixel != null)
            {
                AdditionPaintingPixels.Add(additionPixel);
            }
        }

        // Apply wall configurations as well
        ApplyWallConfigurations(paintingConfig);

        // Apply pipe configurations as well
        ApplyPipeConfigurations(paintingConfig);

        // Apply key configurations as well
        ApplyKeyConfigurations(paintingConfig);

        // NEW: Apply block fountain configurations
        ApplyBlockFountainConfigurations(paintingConfig);
    }

    public void SetUpMechanicDependencies()
    {
        foreach (var key in KeyObjects)
        {
            key.BorderPixels = new List<Vector2>();
            key.BorderPixels = GetLocalBorderPositions(key.PaintingPixelsCovered);
        }
    }
    #endregion

    #region _events
    private void RegisterEvent()
    {
        GameplayEventsManager.OnBlockDissapear += OnBlockDissapear;
        GameplayEventsManager.OnAPixelDestroyed += OnAPixelDestroyed;
        GameplayEventsManager.OnAPipePixelDestroyed += OnAPipePixelDestroyed;
        GameplayEventsManager.OnCollectorsSquadChanged += OnCollectorsFormationChanged;
    }

    private void UnregisterEvent()
    {
        GameplayEventsManager.OnBlockDissapear -= OnBlockDissapear;
        GameplayEventsManager.OnAPixelDestroyed -= OnAPixelDestroyed;
        GameplayEventsManager.OnAPipePixelDestroyed -= OnAPipePixelDestroyed;
        GameplayEventsManager.OnCollectorsSquadChanged -= OnCollectorsFormationChanged;
    }
    public void UpdateOutlinePixels()
    {
        currentOutlinePixels = SelectOutlinePixels();
    }
    public void OnAPixelDestroyed(PaintingPixel _pixel)
    {
        if (_pixel == null) return;

        bool destroyed = false;
        bool isPipePixel = false;
        if (!destroyed)
        {
            foreach (PipeObject pipe in PipeObjects)
            {
                if (pipe.Destroyed || _pixel.colorCode != pipe.ColorCode) continue;
                if (pipe.PaintingPixelsCovered.Contains(_pixel))
                {
                    destroyed = true;
                    isPipePixel = true;
                    _pixel.destroyed = false;
                    pipe.OnAPixelDestroyed();
                    break;
                }
            }
        }

        if (!destroyed)
        {
            foreach (WallObject wall in WallObjects)
            {
                if (wall.Destroyed || _pixel.colorCode != wall.ColorCode) continue;
                if (wall.PaintingPixelsCovered.Contains(_pixel))
                {
                    destroyed = true;
                    _pixel.destroyed = false;
                    wall.OnAPixelDestroyed();
                    break;
                }
            }
        }

        foreach (KeyObject key in KeyObjects)
        {
            if (key.Collected || key.ReadyToCollected) continue;
            if (key.BorderContains(_pixel))
            {
                destroyed = true;
                key.OnAPixelBorderDestroyed();
                break;
            }
        }

        if (!isPipePixel) UpdatePixelDestroyedCount(1);
        // NEW: Check if fountain can refill this pixel
        if (!_pixel.IsMechanicPixel() && BlockFountainObjects != null && BlockFountainObjects.Count > 0)
        {
            ReFillBlockUsingFountain(_pixel);
        }
        UpdateOutlinePixels();

        // Trigger event to notify that grid pixels have changed
        GameplayEventsManager.OnGridObjectChanged?.Invoke(this);
        ShakeNeighborPixelsOnDestroyed(_pixel);
    }
    private void ShakeNeighborPixelsOnDestroyed(PaintingPixel _pixel)
    {
        var neighbors = GetPixelComponentNeighbor(_pixel);
        if (neighbors.Count <= 0) return;
        foreach (var neighbor in neighbors)
        {
            neighbor.PlayShake();
        }
    }
    public void OnCollectorsFormationChanged(CollectorColumnController _collectors)
    {
        if (KeyObjects.Count > 0)
        {
            CollectorController lockAvailable = _collectors.GetLockReadyToUnlock();
            if (lockAvailable != null)
            {
                foreach (var key in KeyObjects)
                {
                    if (key.Collected || key.Locked) continue;
                    key.OnCollectedByLock(lockAvailable);
                }
            }
        }
    }
    private void OnAPipePixelDestroyed()
    {
        UpdatePixelDestroyedCount(1);
    }
    private void OnBlockDissapear()
    {
        EffectHandler?.PlayBlockDestroyedAudio();
    }
    public void UpdatePixelDestroyedCount(int _amount)
    {
        PixelDestroyed += _amount;

        if (PixelDestroyed >= PixelCount)
        {
            // schedule endgame after 0.5s without coroutine GC
            this.DelaySeconds(0.5f, () => GameplayEventsManager.OnEndGame?.Invoke(true));
        }

    }
    #endregion

    #region _actions
    public void ShootPixel(PaintingPixel pixel)
    {
        pixel.DestroyPixel();
    }

    // Batch: set color for all pixels with a given color code using the color-index mapping
    public int BatchSetColorByCode(string colorCode, Color newColor, bool includeHidden = false)
    {
        if (string.IsNullOrEmpty(colorCode) || pixelByColors == null) return 0;
        if (!pixelByColors.TryGetValue(colorCode, out var colorPixels)) return 0;
        // Use colorPixels directly since it's already the list we need
        if (colorPixels == null || colorPixels.Count == 0) return 0;

        int changed = 0;
        foreach (var px in colorPixels)
        {
            if (px == null || px.destroyed) continue;
            if (!includeHidden && px.Hidden) continue;
            if (ApproximatelyEqual(px.color, newColor)) continue;

            px.color = newColor;
            // Apply to visual if component exists and pixel is visible
            if (px.PixelComponent != null && (!px.Hidden))
            {
                px.PixelComponent.SetColor(newColor);
            }
            changed++;
        }
        return changed;
    }

    // Batch: set colors for multiple color codes in one pass
    public int BatchSetColorsByCodes(Dictionary<string, Color> colorMap, bool includeHidden = false)
    {
        if (colorMap == null || colorMap.Count == 0) return 0;
        int totalChanged = 0;
        foreach (var kvp in colorMap)
        {
            totalChanged += BatchSetColorByCode(kvp.Key, kvp.Value, includeHidden);
        }
        return totalChanged;
    }

    private static bool ApproximatelyEqual(Color a, Color b, float eps = 0.001f)
    {
        return Mathf.Abs(a.r - b.r) <= eps && Mathf.Abs(a.g - b.g) <= eps && Mathf.Abs(a.b - b.b) <= eps && Mathf.Abs(a.a - b.a) <= eps;
    }

    public void DestroyAllPixelsObjects()
    {
        for (int i = 0; i < paintingPixels.Count; i++)
        {
            if (paintingPixels[i] != null)
            {
                paintingPixels[i].DestroyObject();
            }
        }
        paintingPixels.Clear();

        // Destroy all pipe objects and clear the list
        for (int i = 0; i < PipeObjects.Count; i++)
        {
            if (PipeObjects[i] != null)
            {
                Destroy(PipeObjects[i].gameObject);

            }
        }
        PipeObjects.Clear();

        // Clear pipe object pixels and destroy their GameObjects as well
        for (int i = 0; i < pipeObjectsPixels.Count; i++)
        {
            if (pipeObjectsPixels[i] != null && pipeObjectsPixels[i].pixelObject != null)
            {
                Destroy(pipeObjectsPixels[i].pixelObject);

            }
        }
        pipeObjectsPixels.Clear();

        // Destroy all wall objects and clear the list
        for (int i = 0; i < WallObjects.Count; i++)
        {
            if (WallObjects[i] != null)
            {
                Destroy(WallObjects[i].gameObject);
            }
        }
        WallObjects.Clear();

        // Destroy all key objects and clear the list
        foreach (var keyObject in KeyObjects)
        {
            if (keyObject != null)
            {
                Destroy(keyObject.gameObject);
            }
        }
        KeyObjects.Clear();

        for (int i = 0; i < AdditionPaintingPixels.Count; i++)
        {
            if (AdditionPaintingPixels[i].PixelComponent == null) continue;
            Destroy(AdditionPaintingPixels[i].PixelComponent.gameObject);

        }
        AdditionPaintingPixels.Clear();

        // Clear the row and column mappings as well
        pixelsByRow.Clear();
        pixelsByColumn.Clear();
        pixelByColors.Clear();

        // Release pooled materials (optional memory reclaim)
        // PaintingPixelComponent.ClearSharedPool(); // Nếu bạn có pool material
        ClearColorCodeMaterials(); // Clear materials khi hủy tất cả pixel
                                   // NEW: Clear fountain objects
        for (int i = 0; i < BlockFountainObjects.Count; i++)
        {
            if (BlockFountainObjects[i] != null)
            {
                if (Application.isPlaying)
                    GameObject.Destroy(BlockFountainObjects[i].gameObject);
                else
                    GameObject.DestroyImmediate(BlockFountainObjects[i].gameObject);
            }
        }
        BlockFountainObjects.Clear();
        BlockFountainObjectsPixels.Clear();
    }

    public List<PaintingPixel> SelectOutlinePixels()
    {
        List<PaintingPixel> outlinePixels = new List<PaintingPixel>();
        HashSet<PaintingPixel> addedPixels = new HashSet<PaintingPixel>();

        // First, check the pixels in each row to find min/max columns
        if (pixelsByRow != null)
        {
            foreach (var rowPair in pixelsByRow)
            {
                var rowPixels = rowPair.Value;
                if (rowPixels != null && rowPixels.Count > 0)
                {
                    // Find min and max column for this row among non-destroyed pixels
                    int minCol = int.MaxValue;
                    int maxCol = int.MinValue;

                    foreach (var pixel in rowPixels)
                    {
                        if (pixel != null && !pixel.destroyed && !pixel.Hidden)
                        {
                            minCol = Mathf.Min(minCol, pixel.column);
                            maxCol = Mathf.Max(maxCol, pixel.column);
                        }
                    }

                    // Add the leftmost and rightmost pixels of this row to outline
                    if (minCol != int.MaxValue) // Check if we found any non-destroyed pixels
                    {
                        foreach (var pixel in rowPixels)
                        {
                            if (pixel != null && !pixel.destroyed && !pixel.Hidden && (pixel.column == minCol || pixel.column == maxCol))
                            {
                                if (addedPixels.Add(pixel)) // Add returns true if pixel was not already in the set
                                {
                                    outlinePixels.Add(pixel);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Next, check the pixels in each column to find min/max rows
        if (pixelsByColumn != null)
        {
            foreach (var colPair in pixelsByColumn)
            {
                var colPixels = colPair.Value;
                if (colPixels != null && colPixels.Count > 0)
                {
                    // Find min and max row for this column among non-destroyed pixels
                    int minRow = int.MaxValue;
                    int maxRow = int.MinValue;

                    foreach (var pixel in colPixels)
                    {
                        if (pixel != null && !pixel.destroyed && !pixel.Hidden)
                        {
                            minRow = Mathf.Min(minRow, pixel.row);
                            maxRow = Mathf.Max(maxRow, pixel.row);
                        }
                    }

                    // Add the topmost and bottommost pixels of this column to outline
                    if (minRow != int.MaxValue) // Check if we found any non-destroyed pixels
                    {
                        foreach (var pixel in colPixels)
                        {
                            if (pixel != null && !pixel.destroyed && !pixel.Hidden && (pixel.row == minRow || pixel.row == maxRow))
                            {
                                if (addedPixels.Add(pixel)) // Add returns true if pixel was not already in the set
                                {
                                    outlinePixels.Add(pixel);
                                }
                            }
                        }
                    }
                }
            }
        }

        return outlinePixels;
    }

    public List<PaintingPixel> SelectOutlinePixelsWithColor(string colorCode)
    {
        if (currentOutlinePixels.Count <= 0)
        {
            return new List<PaintingPixel>();
        }
        var rs = currentOutlinePixels.FindAll(x => (x.colorCode == colorCode || x.colorCode.Equals(LockKeyColorDefine))).ToList();
        return rs;
    }
    #endregion

    #region _grid
    // Generate the grid of pixels
    public void GenerateGrid(float yoffset = 0)
    {
        GridTransform = transform;
        Vector3 centerPos = GridTransform.position;
        YOffset = yoffset;
        DestroyAllPixelsObjects();
        ClearColorCodeMaterials(); // Clear materials trước khi generate grid mới

        // Generate pixels
        for (int col = 0; col < (int)gridSize.x; col++)
        {
            for (int row = 0; row < (int)gridSize.y; row++)
            {
                int halfCols = (int)(gridSize.x / 2);
                int halfRows = (int)(gridSize.y / 2);

                int gridCol = halfCols - col;
                if ((int)gridSize.x % 2 == 0)
                {
                    gridCol -= 1;
                }

                int gridRow = row - halfRows;
                if ((int)gridSize.y % 2 == 0)
                {
                    // No adjustment needed for even number of rows in this implementation
                }

                Vector3 worldPos = CalculatePixelPosition(gridCol, gridRow, yoffset);

                GameObject pixelGO = null;
                if (pixelPrefab != null)
                {
                    pixelGO = Instantiate(pixelPrefab, worldPos, Quaternion.identity, GridTransform);
                    pixelGO.transform.localScale = this.blockScale;
                    pixelGO.name = string.Format("Pixel ({0}, {1})", gridCol, gridRow);
                }

                // Create PaintingPixel with reference to its GameObject
                // TRUYỀN basePixelSharedMaterial VÀO ĐÂY KHI TẠO PAINTINGPIXEL
                PaintingPixel pixel = new PaintingPixel(gridCol, gridRow, Color.white, worldPos, 1, false, pixelGO, basePixelSharedMaterial);

                // Add to grid
                paintingPixels.Add(pixel);
                AddPixelToMappings(pixel);

                // Store reference to the pixel in the pixel gameobject if needed
                if (pixelGO != null)
                {
                    var pixelComponent = pixelGO.GetComponent<PaintingPixelComponent>();
                    if (pixelComponent != null)
                    {
                        pixelComponent.SetUp(pixel); // pixelComponent sẽ nhận sharedMaterial từ pixel
                    }
                }
            }
        }
    }

    public void InitializeGrid(Vector2 size, float arrangeSpace, GameObject prefab, Vector3 blockScale, LevelMechanicObjectPrefabs prefabSrc, Material pixelSharedMaterial)
    {
        this.gridSize = size;
        this.pixelPrefab = prefab;
        this.blockScale = blockScale;
        this.PrefabSource = prefabSrc;
        this.GridTransform = transform;
        this.pixelArrangeSpace = arrangeSpace;
        this.basePixelSharedMaterial = pixelSharedMaterial; // Gán shared material gốc

        KeyObjects = new List<KeyObject>();
        WallObjects = new List<WallObject>();
        PipeObjects = new List<PipeObject>();
        paintingPixels = new List<PaintingPixel>();
        AdditionPaintingPixels = new List<PaintingPixel>();
        if (paintingPixels == null) paintingPixels = new List<PaintingPixel>();
        else paintingPixels.Clear();

        if (PipeObjects == null) PipeObjects = new List<PipeObject>();
        else PipeObjects.Clear();

        ClearColorCodeMaterials(); // Clear materials khi khởi tạo grid
    }


    // Initialize the row and column mapping lists from the existing paintingPixels
    private void InitializePixelMappings()
    {
        pixelsByRow.Clear();
        pixelsByColumn.Clear();
        pixelByColors.Clear();

        // Populate the mappings with current painting pixels
        if (paintingPixels != null)
        {
            foreach (PaintingPixel pixel in paintingPixels)
            {
                if (pixel != null) AddPixelToMappings(pixel);
            }
        }

        // Populate the mappings with pipe object pixels
        if (pipeObjectsPixels != null)
        {
            foreach (PaintingPixel pixel in pipeObjectsPixels)
            {
                if (pixel != null) AddPixelToMappings(pixel);
            }
        }

        if (wallObjectsPixels != null)
        {
            foreach (PaintingPixel pixel in wallObjectsPixels)
            {
                if (pixel != null) AddPixelToMappings(pixel);
            }
        }

        if (AdditionPaintingPixels != null)
        {
            foreach (PaintingPixel pixel in AdditionPaintingPixels)
            {
                if (pixel != null) AddPixelToMappings(pixel);
            }
        }
    }

    public void SetupPixelObject(int column, int row, Color newColor, string colorCode, bool hidden)
    {
        PaintingPixel pixel = GetOriginalPixelAt(column, row);
        if (pixel != null)
        {
            // Lấy material trực tiếp từ ColorPalette nếu có, fallback sang basePixelSharedMaterial
            Material materialToAssign = GetMaterialByColorCode(colorCode);
            pixel.Material = materialToAssign;
            pixel.UsePaletteMaterialColor = true; // dùng đúng màu của material palette
            pixel.SetUp(newColor, colorCode, hidden);
        }
    }

    // Update the row and column mappings when a pixel is added
    private void AddPixelToMappings(PaintingPixel pixel)
    {
        if (pixel == null) return;

        // Add to row mapping
        AddPixelToRowMapping(pixel.row, pixel);

        // Add to column mapping
        AddPixelToColumnMapping(pixel.column, pixel);

        // Add to color mapping
        AddPixelToColorsMapping(pixel.colorCode, pixel);
    }

    // Helper method to add a pixel to row mapping
    private void AddPixelToRowMapping(int row, PaintingPixel pixel)
    {
        if (!pixelsByRow.TryGetValue(row, out var rowPixels))
        {
            rowPixels = new List<PaintingPixel>();
            pixelsByRow[row] = rowPixels;
        }

        if (!rowPixels.Contains(pixel))
        {
            rowPixels.Add(pixel);
        }

        if (row < MinRow) MinRow = row;
        if (row > MaxRow) MaxRow = row;
    }

    // Helper method to add a pixel to column mapping
    private void AddPixelToColumnMapping(int column, PaintingPixel pixel)
    {
        if (!pixelsByColumn.TryGetValue(column, out var columnPixels))
        {
            columnPixels = new List<PaintingPixel>();
            pixelsByColumn[column] = columnPixels;
        }

        if (!columnPixels.Contains(pixel))
        {
            columnPixels.Add(pixel);
        }

        if (column < MinColumn) MinColumn = column;
        if (column > MaxColumn) MaxColumn = column;
    }

    private void AddPixelToColorsMapping(string colorCode, PaintingPixel pixel)
    {
        if (!pixelByColors.TryGetValue(colorCode, out var colorPixels))
        {
            colorPixels = new List<PaintingPixel>();
            pixelByColors[colorCode] = colorPixels;
        }

        if (!colorPixels.Contains(pixel))
        {
            colorPixels.Add(pixel);
        }
    }
    #endregion

    #region Material Management
    // Xóa EnsureMaterialForColorCode: không dùng nữa vì luôn lấy trực tiếp từ ColorPalette

    // Lấy material dựa trên colorCode (ưu tiên từ ColorPalette, fallback sang basePixelSharedMaterial)
    public Material GetMaterialByColorCode(string colorCode)
    {
        if (string.IsNullOrEmpty(colorCode)) return basePixelSharedMaterial;

        var code = colorCode.Trim();
        if (colorCodeMaterials.TryGetValue(code, out var cached))
        {
            return cached;
        }

        Material matFromPalette = ActivePalette != null
            ? ActivePalette.GetMaterialByCode(code)
            : null;

        if (matFromPalette != null)
        {
            colorCodeMaterials[code] = matFromPalette;
            return matFromPalette;
        }

        // Fallback
        colorCodeMaterials[code] = basePixelSharedMaterial;
        return basePixelSharedMaterial;
    }

    // Xóa cache materials (KHÔNG Destroy vì dùng shared trong ColorPalette)
    private void ClearColorCodeMaterials()
    {
        colorCodeMaterials.Clear();
    }

    #endregion

    #region _pipe objects
    // Method to apply pipe configurations to the grid
    public void ApplyPipeConfigurations(PaintingConfig paintingConfig)
    {
        if (paintingConfig == null || paintingConfig.PipeSetups == null || paintingConfig.PipeSetups.Count <= 0)
        {
            Debug.LogWarning("No PaintingConfig or PipeSetups assigned to apply.");
            return;
        }

        // Clear existing pipe objects
        ClearAllPipes();

        // Create pipe objects based on the configurations in the painting config
        foreach (var pipeSetup in paintingConfig.PipeSetups)
        {
            if (pipeSetup != null && pipeSetup.PixelCovered != null && pipeSetup.PixelCovered.Count > 0)
            {
                // Create a new pipe object based on the setup
                CreatePipeObject(pipeSetup);
            }
        }

        foreach (PipeObject _pipe in PipeObjects) _pipe.ApplyPosition();
    }

    // Helper method to create a pipe object from a pipe setup configuration
    public PipeObject CreatePipeObject(PipeObjectSetup pipeSetup)
    {
        if (pipeSetup.PixelCovered == null || pipeSetup.PixelCovered.Count < 2)
        {
            Debug.LogWarning("Pipe setup has less than 2 pixels. Cannot create pipe.");
            return null;
        }

        List<PaintingPixel> respectedPixels = new List<PaintingPixel>();
        Color pipeColor = ActivePalette != null ? ActivePalette.GetColorByCode(pipeSetup.ColorCode) : Color.white;

        // Lấy shared material trực tiếp từ ColorPalette
        Material pipeSharedMaterial = GetMaterialByColorCode(pipeSetup.ColorCode);

        for (int i = 0; i < pipeSetup.PixelCovered.Count; i++)
        {
            PaintingPixelConfig pixelConfig = pipeSetup.PixelCovered[i];
            PaintingPixel respectedPixel = GetOriginalPixelAt(pixelConfig.column, pixelConfig.row);
            if (respectedPixel == null)
            {
                // Truyền pipeSharedMaterial vào khi tạo AdditionPixel
                PaintingPixel additionPixel = CreateNewPaintingPixelAbstract(pixelConfig, true, pipeSharedMaterial);
                additionPixel.color = pipeColor;
                additionPixel.colorCode = pipeSetup.ColorCode;
                if (additionPixel != null) respectedPixels.Add(additionPixel);
            }
            else
            {
                respectedPixel.colorCode = pipeSetup.ColorCode;
                respectedPixel.Material = pipeSharedMaterial;
                respectedPixel.PixelComponent?.SetUp(respectedPixel);
                respectedPixels.Add(respectedPixel);
            }
        }

        // Determine if the pipe is horizontal or vertical based on the first and last pixels
        // Get the head and tail positions from the first and last pixels
        PaintingPixel headPixel = respectedPixels[0];
        PaintingPixel tailPixel = respectedPixels[respectedPixels.Count - 1];
        bool isHorizontal = headPixel.row == tailPixel.row;
        Vector3 worldPos = headPixel.worldPos;
        if (headPixel.PixelComponent != null) worldPos = headPixel.PixelComponent.transform.position;
        GameObject pipeGO = Instantiate(PrefabSource.PipeObjectPrefab, worldPos, Quaternion.identity, GridTransform);

        pipeGO.name = string.Format("PIPE_OBJECT_{0}", pipeSetup.ColorCode);

        // Apply direct scale to the head
        Transform pipeTransform = pipeGO.transform;
        pipeTransform.localScale = blockScale;

        // Get the PipeObject component from the head
        PipeObject pipeObject = pipeTransform.GetComponent<PipeObject>();
        if (pipeObject == null)
        {
            pipeObject = pipeGO.AddComponent<PipeObject>();
        }

        pipeObject.WorldPos = worldPos;

        // Change the color of the head part using PipePartVisualHandle
        PipePartVisualHandle pipeVisual = pipeGO.GetComponent<PipePartVisualHandle>();
        if (pipeVisual == null)
            pipeVisual = pipeGO.GetComponentInChildren<PipePartVisualHandle>();
        if (pipeVisual != null)
            pipeVisual.SetColor(pipeColor); // Đặt màu cho pipe object

        // Add the pipe setup pixels to the pipeObjectsPixels list if they're not already there
        if (pipeObjectsPixels == null) pipeObjectsPixels = new List<PaintingPixel>();
        List<PaintingPixel> newPipePixels = new List<PaintingPixel>();

        foreach (PaintingPixel pixel in respectedPixels)
        {
            PaintingPixel tmp = CreatePipePixel(pixel, pipeSharedMaterial); // Truyền pipeSharedMaterial vào đây
            if (tmp != null)
            {
                newPipePixels.Add(tmp);
                tmp.PixelComponent?.ApplyPosition();
            }
        }

        pipeObject.Initialize(newPipePixels, pipeSetup.ColorCode, pipeSetup.Hearts, isHorizontal);
        pipeObject.ApplyOrientationRotation();
        PipeObjects.Add(pipeObject);
        return pipeObject;
    }

    /// <summary>
    /// Create a new PaintingPixel and PaintingPixelComponent for a pipe part
    /// <returns>New PaintingPixel object for the pipe part</returns>
    private PaintingPixel CreatePipePixel(PaintingPixel stock, Material sharedMaterial) // Thêm sharedMaterial parameter
    {
        GameObject pipePixelGO = new GameObject(string.Format("PipePixel ({0}, {1})", stock.column, stock.row));

        pipePixelGO.transform.SetParent(GridTransform);
        pipePixelGO.transform.position = stock.worldPos;

        // Add a PaintingPixelComponent to the GameObject
        PaintingPixelComponent pipePixelComponent = pipePixelGO.AddComponent<PaintingPixelComponent>();

        // Create the PaintingPixel object
        PaintingPixel pipePixel = new PaintingPixel(stock.column, stock.row, stock.color, stock.worldPos, 1, false, pipePixelGO, sharedMaterial); // Truyền sharedMaterial
        pipePixel.SetUp(stock.color, stock.colorCode, false); // Set both color and color code

        // Set the pixel data for the component
        pipePixelComponent.SetUp(pipePixel); // Component sẽ nhận material từ pixel

        // Add the pipe pixel to the grid object's list of pipe pixels
        if (!pipeObjectsPixels.Contains(pipePixel))
        {
            pipeObjectsPixels.Add(pipePixel);
            return pipePixel;
        }
        return null;
    }

    public void RemovePipeObject(PipeObject _pipe)
    {
        if (_pipe == null) return;
        if (PipeObjects.Contains(_pipe))
        {
            foreach (var coverPixel in _pipe.PaintingPixelsCovered)
            {
                if (pipeObjectsPixels.Contains(coverPixel)) pipeObjectsPixels.Remove(coverPixel);
            }
            _pipe.SelfDestroyGameobject();
            PipeObjects.Remove(_pipe);
        }
    }

    public void ClearAllPipes()
    {
        if (PipeObjects == null) PipeObjects = new List<PipeObject>();
        else
        {
            List<PipeObject> tmp = new List<PipeObject>(PipeObjects);
            foreach (var pipeObj in tmp)
            {
                RemovePipeObject(pipeObj);
            }
        }
        PipeObjects.Clear();
        pipeObjectsPixels.Clear();
    }
    #endregion

    #region _walls
    public void ApplyWallConfigurations(PaintingConfig paintingConfig)
    {
        if (paintingConfig == null || paintingConfig.WallSetups == null || paintingConfig.WallSetups.Count <= 0)
        {
            Debug.LogWarning("No PaintingConfig or WallSetups assigned to apply.", gameObject);
            return;
        }

        // Clear existing wall objects
        ClearAllWalls();

        // Create wall objects based on the configurations in the painting config
        foreach (var wallSetup in paintingConfig.WallSetups)
        {
            if (wallSetup != null)
            {
                // Create a new wall object based on the setup
                CreateWallObject(wallSetup);
            }
        }
    }

    public WallObject CreateWallObject(WallObjectSetup wallSetup)
    {
        if (wallSetup.PixelCovered == null || wallSetup.PixelCovered.Count <= 1)
        {
            Debug.LogWarning("Cannot create wall.");
            return null;
        }

        List<PaintingPixel> wallPixels = new List<PaintingPixel>();
        Color wallColor = ActivePalette != null ? ActivePalette.GetColorByCode(wallSetup.ColorCode) : Color.white;
        Material wallSharedMaterial = GetMaterialByColorCode(wallSetup.ColorCode);

        foreach (PaintingPixelConfig pixelConfig in wallSetup.PixelCovered)
        {
            PaintingPixel respectedPixel = GetOriginalPixelAt(pixelConfig.column, pixelConfig.row);
            if (respectedPixel != null)
            {
                respectedPixel.colorCode = wallSetup.ColorCode;
                respectedPixel.Material = wallSharedMaterial; // Cập nhật sharedMaterial cho pixel bị tường cover
                respectedPixel.PixelComponent?.SetUp(respectedPixel);
                wallPixels.Add(respectedPixel);
            }
            else
            {
                // Truyền wallSharedMaterial khi tạo AdditionPixel
                PaintingPixel additionPixel = CreateNewPaintingPixelAbstract(pixelConfig, true, wallSharedMaterial);
                try { additionPixel.color = wallColor; }
                catch { additionPixel.color = Color.white; }
                additionPixel.colorCode = wallSetup.ColorCode;
                if (additionPixel != null) wallPixels.Add(additionPixel);
            }
        }

        if (wallPixels.Count != wallSetup.PixelCovered.Count) return null;

        Vector3 wallPosition = Vector3.zero;
        if (wallPixels.Any(p => p.PixelComponent == null))
        {
            wallPosition = GetCenterByBoundingBox(wallPixels);
        }
        else wallPosition = GetCenterByBoundingBox(wallPixels.Select(p => p.PixelComponent).ToList());

        // Create the pipe game object with head transform as parent
        GameObject wallGO = Instantiate(PrefabSource.BigBlockPrefab, wallPosition, Quaternion.identity, GridTransform);

        wallGO.name = string.Format("WALL_OBJECT_{0}", wallSetup.ColorCode);

        // Get the PipeObject component from the head
        WallObject wallObject = wallGO.GetComponent<WallObject>();
        if (wallObject == null)
        {
            wallObject = wallGO.AddComponent<WallObject>();
        }
        (int height, int width) = GetShapeSize(wallPixels);
        Vector3 defaultPixelScale = blockScale;
        Vector3 wallScale = new Vector3(defaultPixelScale.x * width, defaultPixelScale.y, defaultPixelScale.z * height);
        wallObject.WallTransform.localScale = wallScale;
        wallObject.WorldPos = wallPosition;
        // Hide all pixel that covered by wall
        for (int i = 0; i < wallPixels.Count; i++)
        {
            wallPixels[i].PixelComponent?.HideVisualOnly();
        }

        // Add the pipe setup pixels to the pipeObjectsPixels list if they're not already there
        if (wallObjectsPixels == null) wallObjectsPixels = new List<PaintingPixel>();
        foreach (PaintingPixel pixel in wallPixels)
        {
            if (!wallObjectsPixels.Contains(pixel))
            {
                wallObjectsPixels.Add(pixel);
            }
        }

        wallObject.Initialize(wallPixels, wallSetup.Hearts, wallColor, wallSetup.ColorCode);
        WallObjects.Add(wallObject);
        return wallObject;
    }

    public void ClearAllWalls()
    {
        if (WallObjects == null) WallObjects = new List<WallObject>();
        else
        {
            List<WallObject> tmp = new List<WallObject>(WallObjects);
            foreach (var wallObj in tmp)
            {
                RemoveWallObject(wallObj);
            }
        }
        WallObjects.Clear();
        wallObjectsPixels.Clear();
    }

    public void RemoveWallObject(WallObject _wall)
    {
        if (_wall == null) return;
        if (WallObjects.Contains(_wall))
        {
            foreach (var coverPixel in _wall.PaintingPixelsCovered)
            {
                if (wallObjectsPixels.Contains(coverPixel))
                {
                    coverPixel.PixelComponent?.ShowVisualOnly();
                    wallObjectsPixels.Remove(coverPixel);
                }
            }
            _wall.SelfDestroyGameObject();
            WallObjects.Remove(_wall);
        }
    }
    public void RemoveWallObject(WallObjectSetup _wall)
    {
        if (_wall == null) return;
        WallObject _wallObject = WallObjects.Find(w => (
        w.ColorCode == _wall.ColorCode &&
        w.PaintingPixelsCovered[0].column == _wall.PixelCovered[0].column)
        && w.PaintingPixelsCovered[0].row == _wall.PixelCovered[0].row
        && w.PaintingPixelsCovered[w.PaintingPixelsCovered.Count - 1].column == _wall.PixelCovered[_wall.PixelCovered.Count - 1].column
        && w.PaintingPixelsCovered[w.PaintingPixelsCovered.Count - 1].row == _wall.PixelCovered[_wall.PixelCovered.Count - 1].row);
        if (_wallObject == null) return;
        if (WallObjects.Contains(_wallObject))
        {
            foreach (var coverPixel in _wallObject.PaintingPixelsCovered)
            {
                if (wallObjectsPixels.Contains(coverPixel))
                {
                    coverPixel.PixelComponent?.ShowVisualOnly();
                    wallObjectsPixels.Remove(coverPixel);
                }
            }
            _wallObject.SelfDestroyGameObject();
            WallObjects.Remove(_wallObject);
        }
    }
    #endregion

    #region _keys
    public void ApplyKeyConfigurations(PaintingConfig paintingConfig)
    {
        if (paintingConfig == null || paintingConfig.KeySetups == null || paintingConfig.KeySetups.Count <= 0)
        {
            Debug.LogWarning("No PaintingConfig or KeySetups assigned to apply.");
            return;
        }

        // Clear existing key objects
        ClearAllKeys();

        // Create key objects based on the configurations in the painting config
        foreach (var KeySetup in paintingConfig.KeySetups)
        {
            if (KeySetup != null)
            {
                // Create a new key object based on the setup
                CreateKeyObject(KeySetup);
            }
        }
    }

    public KeyObject CreateKeyObject(KeyObjectSetup keySetup)
    {
        if (keySetup.PixelCovered == null || keySetup.PixelCovered.Count <= 0)
        {
            Debug.LogWarning("Cannot create key.");
            return null;
        }

        List<PaintingPixel> keyPixels = new List<PaintingPixel>();
        Color keyColor = ActivePalette != null ? ActivePalette.GetColorByCode(keySetup.ColorCode) : Color.white;
        Material keySharedMaterial = GetMaterialByColorCode(keySetup.ColorCode);

        foreach (PaintingPixelConfig pixelConfig in keySetup.PixelCovered)
        {
            PaintingPixel respectedPixel = GetOriginalPixelAt(pixelConfig.column, pixelConfig.row);
            if (respectedPixel != null)
            {
                respectedPixel.Hidden = true;
                respectedPixel.colorCode = keySetup.ColorCode;
                respectedPixel.Material = keySharedMaterial; // Cập nhật sharedMaterial cho pixel bị key cover
                respectedPixel.PixelComponent?.SetUp(respectedPixel);
                keyPixels.Add(respectedPixel);
            }
        }

        if (keyPixels.Count != keySetup.PixelCovered.Count) return null;

        Vector3 keyPosition = GetCenterByBoundingBox(keyPixels.Select(p => p.PixelComponent).ToList());

        // Create the pipe game object with head transform as parent
        GameObject keyGO = Instantiate(PrefabSource.KeyObjectPrefab, keyPosition, Quaternion.identity, GridTransform);

        keyGO.name = "KEY_OBJECT";

        // Get the PipeObject component from the head
        KeyObject keyObject = keyGO.GetComponent<KeyObject>();
        if (keyObject == null)
        {
            keyObject = keyGO.AddComponent<KeyObject>();
        }

        // Hide all pixel that covered by key
        for (int i = 0; i < keyPixels.Count; i++)
        {
            keyPixels[i].PixelComponent?.HideVisualOnly();
        }

        // Add the pipe setup pixels to the pipeObjectsPixels list if they're not already there
        if (keyObjectsPixels == null) keyObjectsPixels = new List<PaintingPixel>();
        foreach (PaintingPixel pixel in keyPixels)
        {
            if (!keyObjectsPixels.Contains(pixel))
            {
                keyObjectsPixels.Add(pixel);
            }
        }

        keyObject.KeyTransform.localScale = blockScale;
        keyObject.Initialize(keyPixels);
        KeyObjects.Add(keyObject);
        return keyObject;
    }

    public void ClearAllKeys()
    {
        if (KeyObjects == null) KeyObjects = new List<KeyObject>();
        else
        {
            List<KeyObject> tmp = new List<KeyObject>(KeyObjects);
            foreach (var keyObj in tmp)
            {
                RemoveKeyObject(keyObj);
            }
        }
        KeyObjects.Clear();
        keyObjectsPixels.Clear();
    }

    public void RemoveKeyObject(KeyObject _key)
    {
        if (_key == null) return;
        if (KeyObjects.Contains(_key))
        {
            foreach (var coverPixel in _key.PaintingPixelsCovered)
            {
                if (keyObjectsPixels.Contains(coverPixel))
                {
                    coverPixel.PixelComponent?.ShowVisualOnly();
                    keyObjectsPixels.Remove(coverPixel);
                }
            }
            _key.SelfDestroy();
            KeyObjects.Remove(_key);
        }
    }

    public void RemoveKeyObject(KeyObjectSetup _key)
    {
        if (_key == null) return;
        KeyObject _keyObject = KeyObjects.Find(w => (
        w.PaintingPixelsCovered[0].column == _key.PixelCovered[0].column)
        && w.PaintingPixelsCovered[0].row == _key.PixelCovered[0].row
        && w.PaintingPixelsCovered[w.PaintingPixelsCovered.Count - 1].column == _key.PixelCovered[_key.PixelCovered.Count - 1].column
        && w.PaintingPixelsCovered[w.PaintingPixelsCovered.Count - 1].row == _key.PixelCovered[_key.PixelCovered.Count - 1].row);
        if (_keyObject == null) return;
        if (KeyObjects.Contains(_keyObject))
        {
            foreach (var coverPixel in _keyObject.PaintingPixelsCovered)
            {
                if (keyObjectsPixels.Contains(coverPixel))
                {
                    coverPixel.PixelComponent?.ShowVisualOnly();
                    keyObjectsPixels.Remove(coverPixel);
                }
            }
            _keyObject.SelfDestroy();
            KeyObjects.Remove(_keyObject);
        }
    }
    #endregion

    #region SUPPORTIVE
    public Vector3 GetCenterByBoundingBox(List<PaintingPixelComponent> points)
    {
        if (points == null || points.Count == 0)
            return Vector3.zero;

        if (points.Count == 1)
            return points[0].transform.position;

        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;
        float minZ = float.MaxValue, maxZ = float.MinValue;

        foreach (var p in points)
        {
            if (p == null) continue;
            var pos = p.transform.position;
            if (pos.x < minX) minX = pos.x;
            if (pos.x > maxX) maxX = pos.x;
            if (pos.y < minY) minY = pos.y;
            if (pos.y > maxY) maxY = pos.y;
            if (pos.z < minZ) minZ = pos.z;
            if (pos.z > maxZ) maxZ = pos.z;
        }

        return new Vector3(
            (minX + maxX) * 0.5f,
            (minY + maxY) * 0.5f,
            (minZ + maxZ) * 0.5f
        );
    }
    public List<PaintingPixelComponent> GetPixelComponentNeighbor(PaintingPixel _origin)
    {
        List<PaintingPixelComponent> _rs = new List<PaintingPixelComponent>();
        int count = 0;
        if (pixelsByColumn.TryGetValue(_origin.column, out var columnPixels))
        {
            foreach (var pixel in columnPixels)
            {
                if ((pixel.row == _origin.row + 1 || pixel.row == _origin.row - 1)
                    && pixel.PixelComponent != null)
                {
                    _rs.Add(pixel.PixelComponent);
                    count++;
                    if (count >= 2) break;
                }
            }
        }

        if (pixelsByRow.TryGetValue(_origin.row, out var rowPixels))
        {
            foreach (var pixel in rowPixels)
            {
                if ((pixel.column == _origin.column + 1 || pixel.column == _origin.column - 1)
                    && pixel.PixelComponent != null)
                {
                    _rs.Add(pixel.PixelComponent);
                    count++;
                    if (count >= 4) break;
                }
            }
        }

        return _rs;
    }

    public Vector3 GetCenterByBoundingBox(List<PaintingPixel> points)
    {
        if (points == null || points.Count == 0) return Vector3.zero;

        if (points.Count == 1)
            return CalculatePixelPosition(points[0].column, points[0].row, YOffset);

        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;
        float minZ = float.MaxValue, maxZ = float.MinValue;

        foreach (var p in points)
        {
            var pos = CalculatePixelPosition(p.column, p.row, YOffset);
            if (pos.x < minX) minX = pos.x;
            if (pos.x > maxX) maxX = pos.x;
            if (pos.y < minY) minY = pos.y;
            if (pos.y > maxY) maxY = pos.y;
            if (pos.z < minZ) minZ = pos.z;
            if (pos.z > maxZ) maxZ = pos.z;
        }

        return new Vector3(
            (minX + maxX) * 0.5f,
            (minY + maxY) * 0.5f,
            (minZ + maxZ) * 0.5f
        );
    }

    public (int rowCount, int columnCount) GetShapeSize(List<PaintingPixel> pixels)
    {
        if (pixels == null || pixels.Count == 0)
            return (0, 0);

        if (pixels.Count == 1) return (1, 1);

        int minRow = int.MaxValue, maxRow = int.MinValue;
        int minCol = int.MaxValue, maxCol = int.MinValue;

        foreach (var p in pixels)
        {
            if (p.row < minRow) minRow = p.row;
            if (p.row > maxRow) maxRow = p.row;
            if (p.column < minCol) minCol = p.column;
            if (p.column > maxCol) maxCol = p.column;
        }

        int rowCount = maxRow - minRow + 1;
        int columnCount = maxCol - minCol + 1;

        return (rowCount, columnCount);
    }
    public PaintingPixel GetPixelAtGridPosition(int column, int row)
    {
        if (pixelsByColumn.TryGetValue(column, out var colPixels))
        {
            foreach (PaintingPixel pixel in colPixels)
            {
                if (pixel.row == row) return pixel;
            }
        }
        return GetOriginalPixelAt(column, row);
    }

    public PaintingPixel GetOriginalPixelAt(int column, int row)
    {
        foreach (PaintingPixel pixel in paintingPixels)
        {
            if (pixel.column == column && pixel.row == row)
            {
                return pixel;
            }
        }
        return null;
    }

    /// <summary>
    /// Gets all pixels in a specific row
    /// </summary>
    /// <param name="row">The row index to get pixels from</param>
    /// <returns>List of pixels in the specified row, or empty list if row doesn't exist</returns>
    public List<PaintingPixel> GetPixelsInRow(int row)
    {
        if (pixelsByRow == null)
        {
            return new List<PaintingPixel>(); // Return empty list if mappings not initialized
        }

        if (!pixelsByRow.TryGetValue(row, out var rowPixels)) return new List<PaintingPixel>();
        return new List<PaintingPixel>(rowPixels);
    }

    /// <summary>
    /// Gets all pixels in a specific column
    /// </summary>
    /// <param name="column">The column index to get pixels from</param>
    /// <returns>List of pixels in the specified column, or empty list if column doesn't exist</returns>
    public List<PaintingPixel> GetPixelsInColumn(int column)
    {
        if (pixelsByColumn == null)
        {
            return new List<PaintingPixel>(); // Return empty list if mappings not initialized
        }

        if (!pixelsByColumn.TryGetValue(column, out var colPixels)) return new List<PaintingPixel>();
        return new List<PaintingPixel>(colPixels);
    }

    // Helper method to apply random variation to a color
    private Color ApplyColorVariation(Color originalColor, float variationAmount)
    {
        if (variationAmount <= 0f)
        {
            return originalColor;
        }

        // Generate random variations within the specified range for each color component
        float rVariation = Random.Range(-variationAmount, variationAmount);
        float gVariation = Random.Range(-variationAmount, variationAmount);
        float bVariation = Random.Range(-variationAmount, variationAmount);

        // Apply the variations to each color component, ensuring values stay within [0, 1] range
        float newR = Mathf.Clamp01(originalColor.r + rVariation);
        float newG = Mathf.Clamp01(originalColor.g + gVariation);
        float newB = Mathf.Clamp01(originalColor.b + bVariation);

        // Return the new color with applied variation (keeping the original alpha)
        return new Color(newR, newG, newB, originalColor.a);
    }

    public int GetTotalPixels()
    {
        return paintingPixels.Count;
    }

    // Trong file PaintingGridObject.cs

    public void ClearToWhite()
    {
        // Lấy material trắng từ palette nếu có, fallback base
        Material whiteMaterial = GetMaterialByColorCode("white");

        foreach (PaintingPixel pixel in paintingPixels)
        {
            if (pixel != null && pixel.PixelComponent != null)
            {
                pixel.color = Color.white;
                pixel.colorCode = "white";
                pixel.Material = whiteMaterial;
                pixel.UsePaletteMaterialColor = true; // bảo đảm không override

                pixel.PixelComponent.CubeRenderer.sharedMaterial = whiteMaterial;
                pixel.PixelComponent.ClearColorOverride(); // xoá MPB nếu còn
                pixel.ShowPixelObject();
            }
        }
        ClearAllAdditionPixels();
    }

    // Calculate world position based on grid coordinates
    public Vector3 CalculatePixelPosition(int col, int row, float yOffset = 0)
    {
        float xPos = GridTransform.position.x + col * pixelArrangeSpace;
        float zPos = GridTransform.position.z + row * pixelArrangeSpace; // Positive rows go "up" in z-axis
        return new Vector3(xPos, GridTransform.position.y + yOffset, zPos);
    }
    public Vector3 CalculatePixelPositionAbstract(int col, int row, float yOffset = 0)
    {
        float xPos = GridTransform.position.x + col * pixelArrangeSpace;
        float zPos = GridTransform.position.z + row * pixelArrangeSpace; // Positive rows go "up" in z-axis
        Vector3 rs = new Vector3(xPos, GridTransform.position.y + yOffset, zPos);
        return rs;
    }
    // Thêm sharedMaterial parameter vào hàm tạo pixel trừu tượng
    public PaintingPixel CreateNewPaintingPixelAbstract(PaintingPixelConfig pixelConfig, bool calculatePositon = false, Material sharedMaterial = null)
    {
        if (sharedMaterial == null) sharedMaterial = basePixelSharedMaterial; // Fallback
        PaintingPixel pixel = new PaintingPixel
        {
            column = pixelConfig.column,
            row = pixelConfig.row,
            color = pixelConfig.color,
            colorCode = pixelConfig.colorCode,
            Hidden = pixelConfig.Hidden,
            Material = sharedMaterial // Gán sharedMaterial
        };
        if (calculatePositon)
        {
            pixel.worldPos = CalculatePixelPositionAbstract(pixel.column, pixel.row, YOffset);
        }

        return pixel;
    }
    public PaintingPixel CreateNewPaintingPixelAbstract(PaintingPixelConfig pixelConfig, bool calculatePosition = false)
    {
        PaintingPixel pixel = new PaintingPixel
        {
            column = pixelConfig.column,
            row = pixelConfig.row,
            colorCode = pixelConfig.colorCode,
            Hidden = pixelConfig.Hidden
        };

        if (calculatePosition)
        {
            pixel.worldPos = CalculatePixelPosition(pixel.column, pixel.row, YOffset);
        }

        return pixel;
    }
    public PaintingPixel CreateNewPaintingPixelReal(PaintingPixelConfig pixelConfig, bool calculatePositon = false)
    {
        Vector3 worldPos = CalculatePixelPosition(pixelConfig.column, pixelConfig.row, YOffset);

        GameObject pixelGO = null;
        pixelGO = Instantiate(pixelPrefab, worldPos, Quaternion.identity, GridTransform);
        pixelGO.transform.localScale = this.blockScale;
        pixelGO.name = string.Format("Addition_Pixel ({0}, {1})", pixelConfig.column, pixelConfig.row);

        Color _c = Color.white;
        string effectiveColorCode = pixelConfig.colorCode;

        if (effectiveColorCode != null)
        {
            _c = effectiveColorCode.Equals(TransparentColorKey) ? Color.white : (ActivePalette != null ? ActivePalette.GetColorByCode(effectiveColorCode) : Color.white);
        }

        // Lấy material trực tiếp từ ColorPalette (hoặc fallback)
        Material pixelSharedMaterial = GetMaterialByColorCode(effectiveColorCode);

        // Create PaintingPixel with reference to its GameObject
        PaintingPixel newPixel = new PaintingPixel(pixelConfig.column, pixelConfig.row, _c, worldPos, 1, pixelConfig.Hidden, pixelGO, pixelSharedMaterial);
        newPixel.colorCode = effectiveColorCode;
        newPixel.UsePaletteMaterialColor = true; // tôn trọng màu từ material palette

        // Store reference to the pixel in the pixel gameobject if needed
        if (pixelGO != null)
        {
            var pixelComponent = pixelGO.GetComponent<PaintingPixelComponent>();
            if (pixelComponent != null)
            {
                pixelComponent.SetUp(newPixel);
            }
        }
        newPixel.SetUp(_c, effectiveColorCode, pixelConfig.Hidden);

        return newPixel;
    }

    public void ClearAllAdditionPixels()
    {
        if (AdditionPaintingPixels.Count <= 0) return;
        foreach (var pixel in AdditionPaintingPixels)
        {
            pixel.PixelComponent?.SelfDestroy();
        }
        AdditionPaintingPixels.Clear();
    }

    public List<Vector2> GetLocalBorderPositions(List<PaintingPixel> regionPixels)
    {
        List<Vector2> result = new List<Vector2>();
        if (regionPixels == null || regionPixels.Count == 0)
            return result;

        int minCol = regionPixels.Min(p => p.column);
        int maxCol = regionPixels.Max(p => p.column);
        int minRow = regionPixels.Min(p => p.row);
        int maxRow = regionPixels.Max(p => p.row);

        // --- TOP & BOTTOM ---
        for (int col = minCol; col <= maxCol; col++)
        {
            int topRow = maxRow + 1;
            int bottomRow = minRow - 1;

            if (PixelExists(col, topRow))
                result.Add(new Vector2(col, topRow));

            if (PixelExists(col, bottomRow))
                result.Add(new Vector2(col, bottomRow));
        }

        // --- LEFT & RIGHT ---
        for (int row = minRow; row <= maxRow; row++)
        {
            int leftCol = minCol - 1;
            int rightCol = maxCol + 1;

            if (PixelExists(leftCol, row))
                result.Add(new Vector2(leftCol, row));

            if (PixelExists(rightCol, row))
                result.Add(new Vector2(rightCol, row));
        }

        return result;
    }

    private bool PixelExists(int col, int row)
    {
        if (pixelsByRow.TryGetValue(row, out var rowPixels))
        {
            foreach (var pixel in rowPixels)
            {
                if (pixel.column == col && !pixel.Hidden)
                    return true;
            }
        }

        if (pixelsByColumn.TryGetValue(col, out var colPixels))
        {
            foreach (var pixel in colPixels)
            {
                if (pixel.row == row && !pixel.Hidden)
                    return true;
            }
        }

        return false;
    }

    public bool IsPixelDestroyed(int col, int row)
    {
        var rs = GetPixelAtGridPosition(col, row);
        return rs != null && rs.destroyed;
    }
   
    #endregion
    #region TOOL RELATED
    public PaintingPixelComponent GetPixelBasedOnPosition(Vector3 _pos, float presicion = 0.5f)
    {
        PaintingPixelComponent rs = null;
        float minDist = float.MaxValue;
        foreach (var pixel in paintingPixels)
        {
            if (pixel != null && pixel.PixelComponent != null)
            {
                float dis = Vector3.Distance(pixel.PixelComponent.transform.position, _pos);
                if (dis < minDist)
                {
                    minDist = dis;
                    rs = pixel.PixelComponent;
                }
            }
        }

        foreach (var pixel in AdditionPaintingPixels)
        {
            if (pixel != null && pixel.PixelComponent != null)
            {
                float dis = Vector3.Distance(pixel.PixelComponent.transform.position, _pos);
                if (dis < minDist)
                {
                    minDist = dis;
                    rs = pixel.PixelComponent;
                }
            }
        }

        return rs;
    }

    public PaintingPixelComponent GetPixelBasedOnPositionNewNeeded(Vector3 _pos, PaintingConfig paintingConfig, float presicion = 0.25f, bool addToAdditional = false)
    {
        PaintingPixelComponent _temprs = null;
        PaintingPixelComponent _rs = null;
        float minDist = float.MaxValue;
        foreach (var pixel in paintingPixels)
        {
            if (pixel != null && pixel.PixelComponent != null)
            {
                float dis = Vector3.Distance(pixel.PixelComponent.transform.position, _pos);
                if (dis < minDist)
                {
                    minDist = dis;
                    _temprs = pixel.PixelComponent;
                }
            }
        }

        foreach (var pixel in AdditionPaintingPixels)
        {
            if (pixel != null && pixel.PixelComponent != null)
            {
                float dis = Vector3.Distance(pixel.PixelComponent.transform.position, _pos);
                if (dis < minDist)
                {
                    minDist = dis;
                    _temprs = pixel.PixelComponent;
                }
            }
        }

        if (minDist > presicion)
        {
            _rs = null;
            _pos.y = YOffset;
            (int col, int row, Vector3 cellPos) = GetPredictedPixel(_pos);

            PaintingPixelConfig _new = new PaintingPixelConfig();
            _new.row = row;
            _new.column = col;

            if (row == _temprs.PixelData.row && col == _temprs.PixelData.column)
            {
                return _temprs;
            }
            else if (addToAdditional)
            {
                bool alreadyInOfficalPixels = paintingPixels.Any(x => (x.column == _new.column && x.row == _new.row));
                bool alreadyInAdditionPixels = AdditionPaintingPixels.Any(x => (x.column == _new.column && x.row == _new.row));

                if (alreadyInOfficalPixels || alreadyInAdditionPixels) return _temprs;

                var _newPixel = CreateNewPaintingPixelReal(_new, false); // CreateNewPaintingPixelReal sẽ tự động quản lý sharedMaterial
                _rs = _newPixel.PixelComponent;
                if (!AdditionPaintingPixels.Contains(_newPixel)) AdditionPaintingPixels.Add(_newPixel);
                if (!paintingConfig.AdditionPixels.Contains(_new)) paintingConfig.AdditionPixels.Add(_new);
                return _rs;
            }
        }

        return _temprs;
    }

    public (int column, int row, Vector3 pixelPos) GetPredictedPixel(Vector3 pos)
    {
        float localX = pos.x + GridTransform.position.x - GridTransform.position.x;
        float localZ = pos.z - GridTransform.position.z;

        int col = Mathf.RoundToInt(localX / pixelArrangeSpace);
        int row = Mathf.RoundToInt(localZ / pixelArrangeSpace);
        Vector3 pixelPos = CalculatePixelPosition(col, row, YOffset);

        return (col, row, pixelPos);
    }

    public void UpdatePixelWorldPos()
    {
        foreach (var pixel in paintingPixels)
        {
            pixel.worldPos = pixel.PixelComponent.transform.localPosition;
        }
    }
    #endregion
    #region BLOCK FOUNTAIN

    public void ApplyBlockFountainConfigurations(PaintingConfig paintingConfig)
    {

        if (paintingConfig == null || paintingConfig.BlockFountainSetup == null || paintingConfig.BlockFountainSetup.Count <= 0)
        {
            return;
        }

        ClearAllBlockFountains();

        if (BlockFountainModule == null)
        {
            BlockFountainModule = GetComponent<GridBlockFountainModule>();
            if (BlockFountainModule == null)
            {
                
                return;
            }
        }

        // ★ FIX: Đảm bảo CurrentGrid trỏ đến đúng object
        BlockFountainModule.CurrentGrid = this;

        foreach (var fountainSetup in paintingConfig.BlockFountainSetup)
        {
            if (fountainSetup != null)
            {
               
                BlockFountainModule.CreateBlockFountainObject(fountainSetup);
            }
        }
    }

    public void ClearAllBlockFountains()
    {
        if (BlockFountainObjects == null)
            BlockFountainObjects = new List<BlockFountainObject>();
        else
        {
            List<BlockFountainObject> tmp = new List<BlockFountainObject>(BlockFountainObjects);
            foreach (var fountainObj in tmp)
            {
                if (BlockFountainModule != null)
                    BlockFountainModule.RemoveBlockFountainObject(fountainObj);
                else if (fountainObj != null)
                    fountainObj.SelfDestroyGameobject();
            }
        }
        BlockFountainObjects.Clear();

        if (BlockFountainObjectsPixels == null)
            BlockFountainObjectsPixels = new List<PaintingPixel>();
        else
            BlockFountainObjectsPixels.Clear();
    }

    public void ReFillBlockUsingFountain(PaintingPixel _block)
    {
        if (BlockFountainObjects == null || BlockFountainObjects.Count <= 0) return;

        foreach (var fountain in BlockFountainObjects)
        {
            if (fountain.SprayBlock(_block.colorCode, _block.PixelComponent))
            {
                _block.destroyed = false;
                UpdateOutlinePixels();
                break;
            }
        }
    }

    public bool AnyPixelLeftWithColor(string colorCode, out List<PaintingPixel> pixels)
    {
        pixels = new List<PaintingPixel>();

        if (pixelByColors.TryGetValue(colorCode, out var colorPixels))
        {
            pixels = colorPixels;
            foreach (var pixel in colorPixels)
            {
                if (pixel != null && !pixel.destroyed && !pixel.Hidden)
                {
                    return true;
                }
            }
        }
        return false;
    }

    #endregion
}
