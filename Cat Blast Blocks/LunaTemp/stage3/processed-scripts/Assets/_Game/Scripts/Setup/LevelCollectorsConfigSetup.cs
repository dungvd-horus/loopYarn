using static PaintingSharedAttributes;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class LevelCollectorsConfigSetup : MonoBehaviour
{
    #region PROPERTIES
    [Header("Configuration")]
    public LevelColorCollectorsConfig CurentCollectorsConfig; // The config asset to set up
    public PaintingConfig paintingConfig;

    [Header("Setup Parameters")]
    public int NumberOfColumns = 3; // Number of columns to arrange collectors in
    public int MaxBulletPerCollector = 20; // Maximum bullets per collector as per design document

    [Header("Default States")]
    public bool defaultLocked = false;
    public bool defaultHidden = false;

    [Header("Preview")]
    public LevelCollectorsSystem previewSystem; // Use LevelCollectorsSystem gameobject to preview the config file

    [Header("Tool")]
    public bool ToolActive = false;
    public LayerMask CollectorObjectLayerMask;
    public float CollisionRadius;
    public List<Vector3> OriginalCollectorPosition = new List<Vector3>();
    public List<Vector3> OriginalLocksPosition = new List<Vector3>();
    public int NumberOfWorkingPixels = 0;
    public int TotalBulletsCount = 0;
    public int NumberOfLockedCollector = 0;
    public Dictionary<string, int> colorSetCounters = new Dictionary<string, int>();
    public Dictionary<string, int> collectorSetCounters = new Dictionary<string, int>();
    public Color BGColor;

    [Header("TOOL MODULE(s)")]
    public MoveCollector MoveModule;
    public SwapCollectors SwapModule;
    public SplitCollector SplitModule;
    public CombinesCollector CombineModule;
    public ConnectCollectors ConnectModule;
    #endregion

    #region MAIN

    public void LoadConfigAsset(LevelColorCollectorsConfig sourceConfig)
    {
        if (sourceConfig == null)
        {
            Debug.LogError("Source config is null!");
            return;
        }

        CurentCollectorsConfig = sourceConfig;
        Debug.Log($"Loaded config asset: {CurentCollectorsConfig.name}");

        // Preview the loaded config if preview system is available
        if (previewSystem != null)
        {
            previewSystem.CurrentLevelCollectorsConfig = CurentCollectorsConfig;
            previewSystem.SetupCollectorsAndMechanic();
        }

#if UNITY_EDITOR
        BakeCollectorsPositionInTool();
#endif
    }

    // Import from a LevelCollectorsSystem's CurrentCollectors (runtime data)
    public void ImportCollectorsFromScene()
    {
        if (previewSystem == null || previewSystem.CurrentCollectors == null || previewSystem.CurrentCollectors.Count == 0)
        {
            Debug.LogError("Source system or its current collectors is null/empty!");
            return;
        }

        if (CurentCollectorsConfig == null)
        {
            Debug.LogError("Target config is null! Please assign a config asset to import to.");
            return;
        }

        // Clear the target config's existing setups
        CurentCollectorsConfig.CollectorColumns.Clear();

        // Import collector data from the system's current collectors
        foreach (CollectorColumn col in previewSystem.ObjectsInColumns)
        {
            int locksRow = 0;
            ColumnOfCollectorConfig newColumn = new ColumnOfCollectorConfig();
            foreach (CollectorMachanicObjectBase _object in col.CollectorsInColumn)
            {
                if (_object == null) continue;
                if (_object is ColorPixelsCollectorObject)
                {
                    ColorPixelsCollectorObject _collector = _object as ColorPixelsCollectorObject;
                    SingleColorCollectorConfig newCollector = new SingleColorCollectorConfig
                    {
                        ID = _collector.ID,
                        ColorCode = _collector.CollectorColor, // Use the color code the collector can destroy
                        Bullets = _collector.BulletCapacity, // Use remaining bullets
                        Locked = _collector.IsLocked,
                        Hidden = _collector.IsHidden,
                        ConnectedCollectorsIDs = new List<int>(_collector.ConnectedCollectorsIDs) // Default to no connections
                    };
                    newColumn.Collectors.Add(newCollector);
                }
                else if (_object is LockObject)
                {
                    LockObject _lock = _object as LockObject;
                    LockObjectConfig newLock = new LockObjectConfig()
                    {
                        ID = _lock.ID,
                        Row = locksRow
                    };
                    newColumn.Locks.Add(newLock);
                }
                locksRow++;
            }
            if (newColumn.Collectors.Count > 0) CurentCollectorsConfig.CollectorColumns.Add(newColumn);
        }

        // Update the number of columns (might need to set this manually or estimate)
        // For now, we'll keep the existing number of columns or default to 1 if not set
        EnsureBidirectionalConnections(false);
        Debug.Log($"Imported {previewSystem.CurrentCollectors.Count} collector data from LevelCollectorsSystem to {CurentCollectorsConfig.name}");
    }

    // Ensure bidirectional connections in the config
    public void EnsureBidirectionalConnections(bool save = true)
    {
        if (CurentCollectorsConfig == null || CurentCollectorsConfig.CollectorColumns == null || CurentCollectorsConfig.CollectorColumns.Count <= 0)
        {
            Debug.LogError("Config asset or collector setups is null!");
            return;
        }

        List<SingleColorCollectorConfig> allCollectorConfig = new List<SingleColorCollectorConfig>();

        foreach (ColumnOfCollectorConfig column in CurentCollectorsConfig.CollectorColumns)
        {
            allCollectorConfig.AddRange(column.Collectors);
        }

        if (allCollectorConfig.Count <= 0) return;

        // Iterate through each collector in the config
        for (int i = 0; i < allCollectorConfig.Count; i++)
        {
            SingleColorCollectorConfig collector = allCollectorConfig[i];
            
            // For each connection in this collector, ensure the reverse connection exists
            foreach (int connectedID in collector.ConnectedCollectorsIDs)
            {
                if (connectedID == collector.ID) continue;
                if (connectedID >= 0)
                {
                    SingleColorCollectorConfig targetCollector = GetCollectorConfigByID(allCollectorConfig, connectedID);
                    if (targetCollector == null || targetCollector.ID == collector.ID) return;
                    
                    // If the target collector doesn't have this collector in its connections, add it
                    if (!targetCollector.ConnectedCollectorsIDs.Contains(collector.ID))
                    {
                        targetCollector.ConnectedCollectorsIDs.Add(collector.ID);
                        Debug.Log($"Added reverse connection: Collector {connectedID} now connected to ID {collector.ID}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid connection index {connectedID} in collector {i}");
                }
            }
        }
        
        Debug.Log($"Ensured bidirectional connections for {allCollectorConfig.Count} collectors in {allCollectorConfig}");

        if (save)
        {
            Save();
        }
    }
    
    // Generate collector configurations from painting config
    public void GenerateCollectorsFromPaintingConfig()
    {
        if (paintingConfig == null)
        {
            Debug.LogError("PaintingConfig is null! Please assign a painting config to generate collectors from.");
            return;
        }

        if (CurentCollectorsConfig == null)
        {
            Debug.LogError("Config asset is null! Please assign a LevelColorCollectorsConfig asset to populate.");
            return;
        }

        // Clear existing collector setups
        if (CurentCollectorsConfig.CollectorColumns == null)
        {
            CurentCollectorsConfig.CollectorColumns = new List<ColumnOfCollectorConfig>();
        }
        CurentCollectorsConfig.CollectorColumns.Clear();

        // Collect all pixels from painting config (PaintingConfig.Pixels) and from pipe setups (PixelCovered)
        List<PaintingPixelConfig> allWorkingPixels = paintingConfig.GetAllWorkingPixels();
        List<PaintingPixel> allPixels = new List<PaintingPixel>();

        for (int i = 0; i < allWorkingPixels.Count; i++)
        {
            PaintingPixelConfig pConfig = allWorkingPixels[i];
            PaintingPixel pixel = new PaintingPixel
            {
                column = pConfig.column,
                row = pConfig.row,
                color = pConfig.color,
                colorCode = pConfig.colorCode,
                Hearts = 1, // Default to 1 heart if not specified in config
                Hidden = pConfig.Hidden
            };
            allPixels.Add(pixel);
        }

        // Group pixels by outline based on painting size
        List<List<PaintingPixel>> outlines = ExtractOutlinesByDepth_Custom(allPixels);

        // Process each outline to create collectors
        int outlineCount = outlines.Count;
        List<SingleColorCollectorConfig> allCollectorConfigs = new List<SingleColorCollectorConfig>();

        int id = 0;
        int keyColorNeedToArrange = 0;
        for (int i = 0; i < outlineCount; i++)
        {
            var outline = outlines[i];

            // Categorize pixels by color in this outline
            Dictionary<string, int> colorCounts = new Dictionary<string, int>();
            foreach (var pixel in outline)
            {
                if (pixel.Hidden || pixel.colorCode.Equals(TransparentColorKey) || pixel.colorCode.Equals(LockKeyColorDefine) || pixel.colorCode.Equals("WhiteDefault")) continue; // Skip hidden pixels
                if (colorCounts.ContainsKey(pixel.colorCode))
                {
                    colorCounts[pixel.colorCode] += pixel.Hearts; // Count hearts as multiples
                }
                else
                {
                    colorCounts[pixel.colorCode] = pixel.Hearts;
                }
            }

            // Create collectors based on color counts
            int colorSetCount = colorCounts.Count;
            bool hasKey = false;
            for (int j = 0; j < colorSetCount; j++)
            {
                var colorCount = colorCounts.ElementAt(j);
                string colorCode = colorCount.Key;
                int totalPixels = colorCount.Value;

                if (!hasKey && colorCode.Equals(LockKeyColorDefine))
                {
                    keyColorNeedToArrange += colorCount.Value;
                    hasKey = true;
                    continue;
                }

                // Create as many collectors as needed to handle the total pixel count
                int remainingPixels = totalPixels;
                
                while (remainingPixels > 0)
                {
                    int bulletsForThisCollector = Mathf.Min(remainingPixels, MaxBulletPerCollector);
                    
                    SingleColorCollectorConfig collector = new SingleColorCollectorConfig
                    {
                        ID = id,
                        ColorCode = colorCode,
                        Bullets = bulletsForThisCollector,
                        Locked = defaultLocked,
                        Hidden = defaultHidden,
                        ConnectedCollectorsIDs = new List<int>() // Empty by default
                    };

                    allCollectorConfigs.Add(collector);
                    
                    remainingPixels -= bulletsForThisCollector;
                    id++;
                }
            }

            if (i == outlineCount - 1)
            {
                allCollectorConfigs[allCollectorConfigs.Count - 1].Bullets += keyColorNeedToArrange;
            }
        }

        int collectorCount = allCollectorConfigs.Count;

        allCollectorConfigs = ReArrangeCollectorBullets(allCollectorConfigs, MaxBulletPerCollector);

        for (int i = 0; i < allCollectorConfigs.Count; i++)
        {
            allCollectorConfigs[i].ID = i; // Re-assign IDs after rearrangement
        }

        List<ColumnOfCollectorConfig> columnsConfig = new List<ColumnOfCollectorConfig>();
        for (int colIdx = 0; colIdx < NumberOfColumns; colIdx++)
        {
            ColumnOfCollectorConfig column = new ColumnOfCollectorConfig();
            // Add collectors to this column (every nth collector where n is the number of columns)
            // In row-major order, collectors in the same column are at indices: colIdx, colIdx+numberOfColumns, colIdx+2*numberOfColumns, etc.
            for (int idx = colIdx; idx < collectorCount; idx += NumberOfColumns)
            {
                if (idx < allCollectorConfigs.Count)
                {
                    column.Collectors.Add(allCollectorConfigs[idx]);
                }
            }
            columnsConfig.Add(column);
        }
        CurentCollectorsConfig.CollectorColumns = columnsConfig;
        //var a = SumColorBullets(configAsset.GetAllCollectorConfigs());

        Debug.Log($"Generated {CurentCollectorsConfig.CollectorColumns.Count} collector setups from painting config '{paintingConfig.name}'");

        Save();
    }

    #region _update collector
    public void UpdateCollectorsFromPaintingConfig()
    {
        if (paintingConfig == null)
        {
            Debug.LogError("PaintingConfig is null! Please assign a painting config to generate collectors from.");
            return;
        }

        if (CurentCollectorsConfig == null)
        {
            Debug.LogError("Config asset is null! Please assign a LevelColorCollectorsConfig asset to populate.");
            return;
        }

        // Collect all pixels from painting config (PaintingConfig.Pixels) and from pipe setups (PixelCovered)
        List<PaintingPixelConfig> allWorkingPixels = paintingConfig.GetAllWorkingPixels();
        List<PaintingPixel> allPixels = new List<PaintingPixel>();

        for (int i = 0; i < allWorkingPixels.Count; i++)
        {
            PaintingPixelConfig pConfig = allWorkingPixels[i];
            PaintingPixel pixel = new PaintingPixel
            {
                column = pConfig.column,
                row = pConfig.row,
                color = pConfig.color,
                colorCode = pConfig.colorCode,
                Hearts = 1, // Default to 1 heart if not specified in config
                Hidden = pConfig.Hidden
            };
            allPixels.Add(pixel);
        }

        // Group pixels by outline based on painting size
        List<List<PaintingPixel>> outlines = ExtractOutlinesByDepth_Custom(allPixels);

        // Process each outline to create collectors
        int outlineCount = outlines.Count;
        List<SingleColorCollectorConfig> allCollectorConfigs = new List<SingleColorCollectorConfig>();

        int id = 0;
        for (int i = 0; i < outlineCount; i++)
        {
            var outline = outlines[i];

            // Categorize pixels by color in this outline
            Dictionary<string, int> colorCounts = new Dictionary<string, int>();
            foreach (var pixel in outline)
            {
                if (pixel.Hidden || pixel.colorCode.Equals(TransparentColorKey) || pixel.colorCode.Equals(LockKeyColorDefine) || pixel.colorCode.Equals("WhiteDefault")) continue; // Skip hidden pixels
                if (colorCounts.ContainsKey(pixel.colorCode))
                {
                    colorCounts[pixel.colorCode] += pixel.Hearts; // Count hearts as multiples
                }
                else
                {
                    colorCounts[pixel.colorCode] = pixel.Hearts;
                }
            }

            // Create collectors based on color counts
            int colorSetCount = colorCounts.Count;
            for (int j = 0; j < colorSetCount; j++)
            {
                var colorCount = colorCounts.ElementAt(j);
                string colorCode = colorCount.Key;
                int totalPixels = colorCount.Value;

                // Create as many collectors as needed to handle the total pixel count
                int remainingPixels = totalPixels;

                while (remainingPixels > 0)
                {
                    int bulletsForThisCollector = Mathf.Min(remainingPixels, MaxBulletPerCollector);

                    SingleColorCollectorConfig collector = new SingleColorCollectorConfig
                    {
                        ID = id,
                        ColorCode = colorCode,
                        Bullets = bulletsForThisCollector,
                        Locked = defaultLocked,
                        Hidden = defaultHidden,
                        ConnectedCollectorsIDs = new List<int>() // Empty by default
                    };

                    allCollectorConfigs.Add(collector);

                    remainingPixels -= bulletsForThisCollector;
                    id++;
                }
            }
        }

        int collectorCount = allCollectorConfigs.Count;

        allCollectorConfigs = ReArrangeCollectorBullets(allCollectorConfigs, MaxBulletPerCollector);

        for (int i = 0; i < allCollectorConfigs.Count; i++)
        {
            allCollectorConfigs[i].ID = i; // Re-assign IDs after rearrangement
        }

        int columnCount = CurentCollectorsConfig.NumberOfColumns();
        
        List<ColumnOfCollectorConfig> columnsConfig = new List<ColumnOfCollectorConfig>();
        for (int colIdx = 0; colIdx < columnCount; colIdx++)
        {
            ColumnOfCollectorConfig column = new ColumnOfCollectorConfig();
            // Add collectors to this column (every nth collector where n is the number of columns)
            // In row-major order, collectors in the same column are at indices: colIdx, colIdx+numberOfColumns, colIdx+2*numberOfColumns, etc.
            for (int idx = colIdx; idx < collectorCount; idx += columnCount)
            {
                if (idx < allCollectorConfigs.Count)
                {
                    column.Collectors.Add(allCollectorConfigs[idx]);
                }
            }
            columnsConfig.Add(column);
        }
        //configAsset.CollectorColumns = columnsConfig;
        Dictionary<string, int> oldCollectors = SumColorBullets(CurentCollectorsConfig.GetAllCollectorConfigs());
        Dictionary<string, int> newCollector = SumColorBullets(allCollectorConfigs);
        Dictionary<string, int> colorDiff = Diff(oldCollectors, newCollector);
        var rsCollectors = CurentCollectorsConfig.GetAllCollectorConfigs();
        foreach (var colorSet in colorDiff)
        {
            if (colorSet.Value == 0) continue;
            if (colorSet.Key.Equals(LockKeyColorDefine))
            {
                continue;
            }
            else
            {
                if (colorSet.Value > 0) // need more bullets of this color
                {
                    int bulletsToAdd = colorSet.Value;
                    int allOldCollectorCount = CurentCollectorsConfig.GetAllCollectorConfigs().Count;
                    for (int i = allOldCollectorCount - 1; i >= 0; i--)
                    {
                        var collector = rsCollectors[i];
                        if (collector.ColorCode == colorSet.Key)
                        {
                            int canAdd = MaxBulletPerCollector - collector.Bullets;
                            if (canAdd > 0)
                            {
                                int adding = Mathf.Min(canAdd, bulletsToAdd);
                                collector.Bullets += adding;
                                bulletsToAdd -= adding;
                            }
                        }
                    }
                    if (bulletsToAdd > 0)
                    {
                        while (bulletsToAdd > 0)
                        {
                            SingleColorCollectorConfig newCollectorConfig = new SingleColorCollectorConfig
                            {
                                ID = rsCollectors.Count,
                                ColorCode = colorSet.Key,
                                Bullets = Mathf.Min(bulletsToAdd, MaxBulletPerCollector),
                                Locked = defaultLocked,
                                Hidden = defaultHidden,
                                ConnectedCollectorsIDs = new List<int>()
                            };
                            rsCollectors.Add(newCollectorConfig);
                            bulletsToAdd -= newCollectorConfig.Bullets;
                        }
                    }
                }
                else // need to reduce bullets of this color
                {
                    int bulletsToRemove = -colorSet.Value;
                    int allOldCollectorCount = CurentCollectorsConfig.GetAllCollectorConfigs().Count;
                    for (int i = allOldCollectorCount - 1; i >= 0; i--)
                    {
                        var collector = rsCollectors[i];
                        if (collector.ColorCode == colorSet.Key && collector.Bullets > 0)
                        {
                            if (collector.Bullets > 0)
                            {
                                int removing = Mathf.Min(collector.Bullets, bulletsToRemove);
                                collector.Bullets -= removing;
                                bulletsToRemove -= removing;
                            }
                        }

                        if (collector.Bullets <= 0)
                        {
                            CurentCollectorsConfig.RemoveCollector(collector.ID);
                        }
                    }
                }
            }
        }

        Save();
    }
    private Dictionary<string, int> SumColorBullets(List<SingleColorCollectorConfig> collectors)
    {
        Dictionary<string, int> map = new Dictionary<string, int>();

        foreach (var c in collectors)
        {
            if (!map.ContainsKey(c.ColorCode))
                map[c.ColorCode] = 0;

            map[c.ColorCode] += c.Bullets;
        }

        return map;
    }
    Dictionary<string, int> Diff(Dictionary<string, int> oldDict, Dictionary<string, int> newDict)
    {
        Dictionary<string, int> diff = new Dictionary<string, int>();

        // lấy tất cả key từ old + new
        HashSet<string> allKeys = new HashSet<string>(oldDict.Keys);
        allKeys.UnionWith(newDict.Keys);

        foreach (var key in allKeys)
        {
            int oldValue = oldDict.TryGetValue(key, out var o) ? o : 0;
            int newValue = newDict.TryGetValue(key, out var n) ? n : 0;

            diff[key] = newValue - oldValue; // + tăng, - giảm, 0 = không đổi
        }

        return diff;
    }
    void CleanupZeroCollectors(List<SingleColorCollectorConfig> list)
    {
        list.RemoveAll(c => c.Bullets <= 0);
    }
    #endregion
    // Extract outline pixels from outermost to innermost
    private List<List<PaintingPixel>> ExtractOutlinesByDepth(List<PaintingPixel> allPixels, [Bridge.Ref] Vector2 paintingSize)
    {
        List<List<PaintingPixel>> outlines = new List<List<PaintingPixel>>();
        if (allPixels == null || allPixels.Count == 0)
            return outlines;

        // Clone the list to avoid modifying the original
        List<PaintingPixel> workingPixels = new List<PaintingPixel>(allPixels);

        // Keep looping while there are still pixels
        while (workingPixels.Count > 0)
        {
            // Determine current bounds (outermost rectangle of remaining pixels)
            int minCol = int.MaxValue, maxCol = int.MinValue;
            int minRow = int.MaxValue, maxRow = int.MinValue;

            foreach (var pixel in workingPixels)
            {
                if (pixel.Hidden) continue;
                if (pixel.column < minCol) minCol = pixel.column;
                if (pixel.column > maxCol) maxCol = pixel.column;
                if (pixel.row < minRow) minRow = pixel.row;
                if (pixel.row > maxRow) maxRow = pixel.row;
            }

            List<PaintingPixel> currentOutline = new List<PaintingPixel>();

            // Top edge
            for (int col = minCol; col <= maxCol; col++)
            {
                var p = FindNonHiddenPixelsAt(workingPixels, col, minRow);
                if (p != null)
                {
                    currentOutline.AddRange(p);
                }
            }

            // Right edge
            for (int row = minRow + 1; row <= maxRow - 1; row++)
            {
                var p = FindNonHiddenPixelsAt(workingPixels, maxCol, row);
                if (p != null)
                {
                    currentOutline.AddRange(p);
                }
            }

            // Bottom edge
            for (int col = maxCol; col >= minCol; col--)
            {
                var p = FindNonHiddenPixelsAt(workingPixels, col, maxRow);
                if (p != null)
                {
                    currentOutline.AddRange(p);
                }
            }

            // Left edge
            for (int row = maxRow - 1; row >= minRow + 1; row--)
            {
                var p = FindNonHiddenPixelsAt(workingPixels, minCol, row);
                if (p != null)
                {
                    currentOutline.AddRange(p);
                }
            }

            // Remove found pixels from working list
            foreach (var p in currentOutline)
            {
                workingPixels.Remove(p);
            }

            // Add to results if we found any
            if (currentOutline.Count > 0)
            {
                outlines.Add(currentOutline);
            }
            else
            {
                // No outline found — break to avoid infinite loop
                break;
            }
        }

        return outlines;
    }

    public List<List<PaintingPixel>> ExtractOutlinesByDepth_Custom(List<PaintingPixel> allPixels)
    {
        List<List<PaintingPixel>> outlines = new List<List<PaintingPixel>>();

        List<PaintingPixel> workingPixels = allPixels
            .Where(p => p != null && !p.destroyed && !p.Hidden)
            .ToList();

        while (workingPixels.Count > 0)
        {
            List<PaintingPixel> currentOutline = SelectOutlinePixelsFromList(workingPixels);

            if (currentOutline.Count == 0)
                break;

            outlines.Add(currentOutline);

            foreach (var p in currentOutline)
                workingPixels.Remove(p);
        }

        return outlines;
    }

    public List<PaintingPixel> SelectOutlinePixelsFromList(List<PaintingPixel> pixels)
    {
        List<PaintingPixel> outlinePixels = new List<PaintingPixel>();
        HashSet<PaintingPixel> addedPixels = new HashSet<PaintingPixel>();

        var pixelsByRow = pixels.GroupBy(p => p.row).ToList();
        var pixelsByColumn = pixels.GroupBy(p => p.column).ToList();

        foreach (var rowGroup in pixelsByRow)
        {
            var rowPixels = rowGroup.ToList();
            int minCol = int.MaxValue, maxCol = int.MinValue;

            foreach (var pixel in rowPixels)
            {
                minCol = Mathf.Min(minCol, pixel.column);
                maxCol = Mathf.Max(maxCol, pixel.column);
            }

            foreach (var pixel in rowPixels)
            {
                if (pixel.column == minCol || pixel.column == maxCol)
                    if (addedPixels.Add(pixel))
                        outlinePixels.Add(pixel);
            }
        }

        foreach (var colGroup in pixelsByColumn)
        {
            var colPixels = colGroup.ToList();
            int minRow = int.MaxValue, maxRow = int.MinValue;

            foreach (var pixel in colPixels)
            {
                minRow = Mathf.Min(minRow, pixel.row);
                maxRow = Mathf.Max(maxRow, pixel.row);
            }

            foreach (var pixel in colPixels)
            {
                if (pixel.row == minRow || pixel.row == maxRow)
                    if (addedPixels.Add(pixel))
                        outlinePixels.Add(pixel);
            }
        }

        return outlinePixels;
    }

    // Helper method to find a pixel at specific coordinates
    private List<PaintingPixel> FindNonHiddenPixelsAt(List<PaintingPixel> pixels, int column, int row)
    {
        List<PaintingPixel> rs = new List<PaintingPixel>();
        for (int i = 0; i < pixels.Count; i++)
        {
            if (pixels[i].column == column && pixels[i].row == row && !pixels[i].Hidden)
            {
                rs.Add(pixels[i]);
            }
        }
        return rs;
    }
    #endregion

    #region SUPPORTIVE
    public void StartUpTool()
    {
        ToolActive = !ToolActive;
        if (ToolActive)
        {
            CountGunnersAsSet();

            ReCountCollectors();

            LoadConfigAsset(CurentCollectorsConfig);
        }
    }
    public void CountGunnersAsSet()
    {
        List<PaintingPixelConfig> allWorkingPixels = paintingConfig.GetAllWorkingPixels();
        NumberOfWorkingPixels = allWorkingPixels.Count;
        colorSetCounters.Clear();
        foreach (var pixel in allWorkingPixels)
        {
            if (pixel.Hidden || pixel == null || pixel.colorCode == null) continue; // Skip hidden pixels
            if (colorSetCounters.ContainsKey(pixel.colorCode))
            {
                colorSetCounters[pixel.colorCode]++;
            }
            else
            {
                colorSetCounters[pixel.colorCode] = 1;
            }
        }
    }
    public void BakeCollectorsPositionInTool()
    {
        OriginalCollectorPosition.Clear();
        for (int i = 0; i < previewSystem.CurrentCollectors.Count; i++)
        {
            OriginalCollectorPosition.Add(previewSystem.CurrentCollectors[i].transform.position);
        }

        OriginalLocksPosition.Clear();
        for (int i = 0; i < previewSystem.CurrentLocks.Count; i++)
        {
            OriginalLocksPosition.Add(previewSystem.CurrentLocks[i].transform.position);
        }
    }
    public void ReApplyCollectorsPosition()
    {
        for (int i = 0; i < previewSystem.CurrentCollectors.Count; i++)
        {
            previewSystem.CurrentCollectors[i].transform.position = OriginalCollectorPosition[i];
        }

        for (int i = 0; i < previewSystem.CurrentLocks.Count; i++)
        {
            previewSystem.CurrentLocks[i].transform.position = OriginalLocksPosition[i];
        }
    }
    public void ReCountCollectors()
    {
        TotalBulletsCount = 0;
        collectorSetCounters.Clear();
        foreach (var collector in previewSystem.CurrentCollectors)
        {
            if (collectorSetCounters.ContainsKey(collector.CollectorColor))
            {
                collectorSetCounters[collector.CollectorColor]++;
            }
            else
            {
                collectorSetCounters[collector.CollectorColor] = 1;
            }
            TotalBulletsCount += collector.BulletCapacity;
        }
        NumberOfLockedCollector = previewSystem.CurrentLocks.Count;
    }

    public List<SingleColorCollectorConfig> ReArrangeCollectorBullets(List<SingleColorCollectorConfig> _collectors, int maxBullets = 20)
    {
        List< SingleColorCollectorConfig> rs = new List<SingleColorCollectorConfig>(_collectors);
        if (rs == null || rs.Count == 0)
            return null;

        int i = 0;
        while (i < rs.Count)
        {
            var current = rs[i];

            if (current.Bullets >= maxBullets)
            {
                i++;
                continue;
            }

            int j = i + 1;
            while (j < rs.Count && current.Bullets < maxBullets)
            {
                var donor = rs[j];
                if (donor.ColorCode == current.ColorCode && donor.Bullets > 0)
                {
                    int needed = maxBullets - current.Bullets;
                    int take = Mathf.Min(needed, donor.Bullets);
                    current.Bullets += take;
                    donor.Bullets -= take;

                    if (donor.Bullets == 0)
                    {
                        rs.RemoveAt(j);
                        j--;
                    }
                }

                j++;
            }

            i++;
        }

        return rs;
    }

#if UNITY_EDITOR
    public LevelColorCollectorsConfig CreateConfigAsset(string configName)
    {
        if (string.IsNullOrEmpty(configName))
        {
            Debug.LogError("Config name cannot be empty!");
            return null;
        }

        var existedConfig = GetCollectorConfig(configName);
        if (existedConfig != null)
        {
            CurentCollectorsConfig = existedConfig;
        }

        string assetPath = CollectorsConfigPath + configName + "_CollectorConfig" + ".asset";

        // Ensure the path ends with a slash
        if (!CollectorsConfigPath.EndsWith("/"))
        {
            CollectorsConfigPath += "/";
        }

        // Create the directory if it doesn't exist
        if (!Directory.Exists(CollectorsConfigPath))
        {
            Directory.CreateDirectory(CollectorsConfigPath);
        }

        LevelColorCollectorsConfig newConfig = ScriptableObject.CreateInstance<LevelColorCollectorsConfig>();
        AssetDatabase.CreateAsset(newConfig, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Created new LevelColorCollectorsConfig asset at {assetPath}");
        return newConfig;
    }
#endif

    #endregion

    #region TOOL MODULES

    #region _move
    public void SwapCollectors(CollectorMachanicObjectBase a, CollectorMachanicObjectBase b)
    {
        if (a == null || b == null || a == b) return;

        CollectorColumn columnA = null;
        CollectorColumn columnB = null;

        foreach (var column in previewSystem.ObjectsInColumns)
        {
            if (column.CollectorsInColumn.Contains(a)) columnA = column;
            if (column.CollectorsInColumn.Contains(b)) columnB = column;
        }

        // Không tìm thấy column thì không swap được
        if (columnA == null || columnB == null) return;

        var listA = columnA.CollectorsInColumn;
        var listB = columnB.CollectorsInColumn;

        // Lấy vị trí
        int indexA = listA.IndexOf(a);
        int indexB = listB.IndexOf(b);

        // --- Nếu cùng column, swap trực tiếp ---
        if (columnA == columnB)
        {
            (listA[indexA], listA[indexB]) = (listA[indexB], listA[indexA]);
        }
        else
        {
            listA.RemoveAt(indexA);
            listB.RemoveAt(indexB);

            listA.Insert(indexA, b);
            listB.Insert(indexB, a);
        }

        previewSystem.ReArrangePosition();
        previewSystem.SetupConnectedCollectors();
    }
    public void InsertAmongOtherCollector(CollectorMachanicObjectBase itemToInsert, CollectorMachanicObjectBase originItem, bool higher)
    {
        bool sameColumn = false;
        CollectorColumn targetColumnToMoveTo = null;
        CollectorColumn originColumn = null;
        foreach (CollectorColumn column in previewSystem.ObjectsInColumns)
        {
            if (column.CollectorsInColumn.Contains(originItem))
            {
                targetColumnToMoveTo = column;
                if (column.CollectorsInColumn.Contains(itemToInsert))
                {
                    sameColumn = true;
                    break;
                }
            }

            if (column.CollectorsInColumn.Contains(itemToInsert)) originColumn = column;
        }

        if (sameColumn)
        {
            MoveRelative(targetColumnToMoveTo.CollectorsInColumn, itemToInsert, originItem, higher);
        }
        else
        {
            originColumn.CollectorsInColumn.Remove(itemToInsert);
            InsertRelative(targetColumnToMoveTo.CollectorsInColumn, itemToInsert, originItem, higher);
        }
        previewSystem.ReArrangePosition();
        previewSystem.SetupConnectedCollectors();
    }

    public void InsertNewToOtherCollector(CollectorMachanicObjectBase itemToInsert, CollectorMachanicObjectBase originItem, bool higher)
    {
        bool sameColumn = false;
        CollectorColumn targetColumnToMoveTo = null;
        CollectorColumn originColumn = null;
        foreach (CollectorColumn column in previewSystem.ObjectsInColumns)
        {
            if (column.CollectorsInColumn.Contains(originItem))
            {
                targetColumnToMoveTo = column;
                if (column.CollectorsInColumn.Contains(itemToInsert))
                {
                    sameColumn = true;
                    break;
                }
            }

            if (column.CollectorsInColumn.Contains(originItem)) originColumn = column;
        }

        if (sameColumn)
        {
            MoveRelative(targetColumnToMoveTo.CollectorsInColumn, itemToInsert, originItem, higher);
        }
        else
        {
            originColumn.CollectorsInColumn.Remove(itemToInsert);
            InsertRelative(targetColumnToMoveTo.CollectorsInColumn, itemToInsert, originItem, higher);
        }
        previewSystem.ReArrangePosition();
        previewSystem.SetupConnectedCollectors();
    }
    #endregion

    #region _split
    public void SplitACollector(ColorPixelsCollectorObject originItem)
    {
        int maxBullets = originItem.BulletCapacity;
        if (maxBullets <= 1) return;
        int originObjBullets = maxBullets / 2;
        int cloneObjBullets = maxBullets - originObjBullets;

        ColorPixelsCollectorObject newCollector = previewSystem.CloneNewFromCollector(originItem);

        originItem.BulletCapacity = originObjBullets;
        newCollector.BulletCapacity = cloneObjBullets;

        InsertNewToOtherCollector(newCollector, originItem, higher: false);

        previewSystem.ReArrangePosition();
        previewSystem.SetupConnectedCollectors();
    }
    #endregion

    #endregion

    public void Save()
    {
#if UNITY_EDITOR
        
        CurentCollectorsConfig.ReArrangeID();
        CurentCollectorsConfig.EnsureBidirectionalConnections();
        CurentCollectorsConfig.CreateBackUp();
        UnityEditor.EditorUtility.SetDirty(CurentCollectorsConfig);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }
}
