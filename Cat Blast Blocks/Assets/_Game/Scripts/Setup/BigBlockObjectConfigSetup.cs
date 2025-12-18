
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class BigBlockObjectConfigSetup : MonoBehaviour
{
    public PaintingConfigSetup PaintingSetupModule;

    [Header("Wall Configuration")]
    public List<WallObjectSetup> CurrentLevelWallObjectSetups; // To store all the Wall that being setup
    public List<WallObjectSetup> wallObjectSetups; // To store all the Wall that being setup

    [Header("Wall Properties")]
    public string ColorCode = "Default"; // Color of the Wall
    public int WallHearts = 1;      // Hearts of the Wall

    [Header("Wall Positioning")]
    public int WallSpaceFromGrid = 1;     // Space from grid for placing Wall outside the grid

    [Header("Wall Setup")]
    public List<PaintingPixelComponent> WallPixelComponents;

    private void Awake()
    {
        if (wallObjectSetups == null) wallObjectSetups = new List<WallObjectSetup>();
    }

    /// <summary>
    /// Add a wall setup to the list
    /// </summary>
    /// <param name="wallSetup">The wall setup to add</param>
    public void AddWallSetup(WallObjectSetup wallSetup)
    {
        if (wallObjectSetups == null) wallObjectSetups = new List<WallObjectSetup>();
        if (wallSetup != null && !wallObjectSetups.Contains(wallSetup))
        {
            wallObjectSetups.Add(wallSetup);
        }
    }

    /// <summary>
    /// Remove a wall setup from the list
    /// </summary>
    /// <param name="wallSetup">The wall setup to remove</param>
    public void RemoveWallSetup(WallObjectSetup wallSetup)
    {
        if (wallSetup != null)
        {
            wallObjectSetups.Remove(wallSetup);
        }
    }
    public void RemoveWallObjectAndItConfig(WallObject _object, bool save)
    {
        if (PaintingSetupModule.CurrentGridObject.WallObjects != null && PaintingSetupModule.CurrentGridObject.WallObjects.Count > 0)
        {
            PaintingSetupModule.CurrentGridObject.RemoveWallObject(_object);

            var _wallConfig = PaintingSetupModule.CurrentPaintingConfig.GetWallSetup(_object.PaintingPixelsCovered[0], _object.PaintingPixelsCovered[_object.PaintingPixelsCovered.Count - 1]);
            if (_wallConfig != null)
            {
#if UNITY_EDITOR
                Undo.RecordObject(PaintingSetupModule.CurrentPaintingConfig, "Pipe adjust");
#endif
                PaintingSetupModule.CurrentPaintingConfig.WallSetups.Remove(_wallConfig);
                RemoveWallSetup(_wallConfig);
                if (save) PaintingSetupModule.Save();

            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        if (gameObject != null)
            EditorUtility.SetDirty(gameObject);
#endif
    }

    public void RemoveWallObjectAndItConfig(WallObjectSetup _wallSetup, bool save)
    {
        if (PaintingSetupModule.CurrentGridObject.WallObjects != null && PaintingSetupModule.CurrentGridObject.WallObjects.Count > 0)
        {
            PaintingSetupModule.CurrentGridObject.RemoveWallObject(_wallSetup);

            if (_wallSetup != null)
            {
#if UNITY_EDITOR
                Undo.RecordObject(PaintingSetupModule.CurrentPaintingConfig, "Pipe adjust");
#endif
                PaintingSetupModule.CurrentPaintingConfig.WallSetups.Remove(_wallSetup);
                RemoveWallSetup(_wallSetup);
                if (save) PaintingSetupModule.Save();

            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        if (gameObject != null)
            EditorUtility.SetDirty(gameObject);
#endif
    }

    /// <summary>
    /// Clear all wall setups
    /// </summary>
    public void ClearWallSetups()
    {
        wallObjectSetups.Clear();
    }

    /// <summary>
    /// Create a wall between StartPixel and EndPixel based on current settings
    /// </summary>
    public void CreateBigBlock()
    {
        wallObjectSetups = new List<WallObjectSetup>(CurrentLevelWallObjectSetups);
        if (WallPixelComponents == null || WallPixelComponents.Count <= 1)
        {
            Debug.LogWarning("WallPixelComponents is not valid. Cannot create wall.");
            return;
        }

        // Validate that the wall should be straight (horizontal or vertical)
        if (!IsValidWallOrientation(WallPixelComponents))
        {
            Debug.LogWarning("Wall must be either horizontal (same row) or vertical (same column). Cannot create wall.");
            return;
        }

        // Create and setup the wall in the scene - this will also create the wall pixels
        List<PaintingPixelConfig> wallPixelConfigs = new List<PaintingPixelConfig>();
        foreach (var pixelComponent in WallPixelComponents)
        {
            wallPixelConfigs.Add( new PaintingPixelConfig( pixelComponent.PixelData));
        }
        WallObjectSetup wallSetup = new WallObjectSetup(wallPixelConfigs, ColorCode, WallHearts);

        var newWallObject = SetupNewWallInScene(wallSetup);

        if (newWallObject != null)
        {
            AddWallSetup(wallSetup);
        }
    }

    public WallObjectSetup CreateBigBlock(PaintingPixel _startPixel, PaintingPixel _endPixel, string colorCode, int hearts)
    {
        wallObjectSetups = new List<WallObjectSetup>(CurrentLevelWallObjectSetups);
        List<PaintingPixel> _listBlocks = new List<PaintingPixel>();
        _listBlocks = GetPixelsBetweenRectangle(_startPixel, _endPixel);
        if (_listBlocks == null || _listBlocks.Count <= 1)
        {
            Debug.LogWarning("WallPixelComponents is not valid. Cannot create wall.");
            return null;
        }

        // Validate that the wall should be straight (horizontal or vertical)
        if (!IsValidWallOrientation(_listBlocks))
        {
            Debug.LogWarning("Wall must be either horizontal (same row) or vertical (same column). Cannot create wall.");
            return null;
        }

        // Create and setup the wall in the scene - this will also create the wall pixels
        List<PaintingPixelConfig> wallPixelConfigs = new List<PaintingPixelConfig>();
        foreach (var pixel in _listBlocks)
        {
            wallPixelConfigs.Add(new PaintingPixelConfig(pixel));
        }
        WallObjectSetup wallSetup = new WallObjectSetup(wallPixelConfigs, colorCode, hearts);

        var newWallObject = SetupNewWallInScene(wallSetup);

        if (newWallObject != null)
        {
            AddWallSetup(wallSetup);
        }
        return wallSetup;
    }

    public List<PaintingPixel> GetPixelsBetweenRectangle(PaintingPixel a, PaintingPixel b)
    {
        List<PaintingPixel> result = new List<PaintingPixel>();

        int minRow = Mathf.Min(a.row, b.row);
        int maxRow = Mathf.Max(a.row, b.row);
        int minCol = Mathf.Min(a.column, b.column);
        int maxCol = Mathf.Max(a.column, b.column);

        for (int r = minRow; r <= maxRow; r++)
        {
            for (int c = minCol; c <= maxCol; c++)
            {
                result.Add(new PaintingPixel
                {
                    row = r,
                    column = c,
                    worldPos = PaintingSetupModule.CurrentGridObject.CalculatePixelPosition(c, r, PaintingSetupModule.CurrentGridObject.YOffset),
                });
            }
        }

        return result;
    }

    /// <summary>
    /// Set up the actual wall object in the scene
    /// </summary>
    /// <param name="startPixel">Start pixel (head)</param>
    /// <param name="endPixel">End pixel (tail)</param>
    /// <param name="colorCode">Color code for the wall</param>
    /// <returns>Tuple with the created WallObject component and list of new wall pixels</returns>
    private WallObject SetupNewWallInScene(WallObjectSetup setup)
    {
        WallObject wallObject = PaintingSetupModule.CurrentGridObject.CreateWallObject(setup);

        return wallObject;
    }

    /// <summary>
    /// Validates if the wall orientation is valid (horizontal or vertical only)
    /// </summary>
    /// <param name="startPixel">Start pixel (head)</param>
    /// <param name="endPixel">End pixel (tail)</param>
    /// <returns>True if wall orientation is valid, false otherwise</returns>
    public bool IsValidWallOrientation(List<PaintingPixelComponent> _wallPixels)
    {
        if (_wallPixels == null || _wallPixels.Count == 0)
            return false;

        int minRow = _wallPixels.Min(p => p.PixelData.row);
        int maxRow = _wallPixels.Max(p => p.PixelData.row);
        int minCol = _wallPixels.Min(p => p.PixelData.column);
        int maxCol = _wallPixels.Max(p => p.PixelData.column);

        int width = maxCol - minCol + 1;
        int height = maxRow - minRow + 1;
        int expectedCount = width * height;

        if (_wallPixels.Count != expectedCount)
            return false;

        // HashSet<(int, int)> pointSet = _wallPixels
        //     .Select(p => (p.PixelData.row, p.PixelData.column))
        //     .ToHashSet();
        HashSet<(int, int)> pointSet = new HashSet<(int, int)>(_wallPixels
           .Select(p => (p.PixelData.row, p.PixelData.column)));


        for (int r = minRow; r <= maxRow; r++)
        {
            for (int c = minCol; c <= maxCol; c++)
            {
                if (!pointSet.Contains((r, c)))
                    return false;
            }
        }

        return true;
    }
    public bool IsValidWallOrientation(List<PaintingPixel> _wallPixels)
    {
        if (_wallPixels == null || _wallPixels.Count == 0)
            return false;

        int minRow = _wallPixels.Min(p => p.row);
        int maxRow = _wallPixels.Max(p => p.row);
        int minCol = _wallPixels.Min(p => p.column);
        int maxCol = _wallPixels.Max(p => p.column);

        int width = maxCol - minCol + 1;
        int height = maxRow - minRow + 1;
        int expectedCount = width * height;

        if (_wallPixels.Count != expectedCount)
            return false;

        // HashSet<(int, int)> pointSet = _wallPixels
        //     .Select(p => (p.row, p.column))
        //     .ToHashSet();

        HashSet<(int, int)> pointSet = new HashSet<(int, int)>(_wallPixels
           .Select(p => (p.row, p.column)));

        for (int r = minRow; r <= maxRow; r++)
        {
            for (int c = minCol; c <= maxCol; c++)
            {
                if (!pointSet.Contains((r, c)))
                    return false;
            }
        }

        return true;
    }

    private Vector3 GetCenterByBoundingBox(List<PaintingPixelComponent> points)
    {
        if (points == null || points.Count == 0)
            return Vector3.zero;

        float minX = points.Min(p => p.PixelData.worldPos.x);
        float maxX = points.Max(p => p.PixelData.worldPos.x);
        float minY = points.Min(p => p.PixelData.worldPos.y);
        float maxY = points.Max(p => p.PixelData.worldPos.y);
        float minZ = points.Min(p => p.PixelData.worldPos.z);
        float maxZ = points.Max(p => p.PixelData.worldPos.z);

        // trung tâm bounding box
        return new Vector3(
            (minX + maxX) * 0.5f,
            (minY + maxY) * 0.5f,
            (minZ + maxZ) * 0.5f
        );
    }

    /// <summary>
    /// Import all wall configurations from this setup to a PaintingConfig asset
    /// </summary>
    /// <param name="paintingConfig">The PaintingConfig to import to</param>
    public void ImportWallsToPaintingConfig(PaintingConfig paintingConfig)
    {
        if (paintingConfig == null)
        {
            Debug.LogError("PaintingConfig is null. Cannot import walls.");
            return;
        }

        // Clear existing wall setups in the config
        if (paintingConfig.WallSetups == null)
            paintingConfig.WallSetups = new List<WallObjectSetup>();
        else
            paintingConfig.WallSetups.Clear();

        // Copy all wall setups from this component to the config
        foreach (WallObjectSetup wallSetup in wallObjectSetups)
        {
            if (wallSetup != null)
            {
                // Add the wall setup to the painting config
                paintingConfig.WallSetups.Add(wallSetup);
            }
        }

        paintingConfig.HidePixelsUnderPipes();

        Debug.Log($"Imported {wallObjectSetups.Count} wall setups to PaintingConfig.");
    }

    public void Reload()
    {
        PaintingSetupModule.CurrentGridObject.ClearAllWalls();
        CurrentLevelWallObjectSetups = PaintingSetupModule.CurrentPaintingConfig.WallSetups;
        wallObjectSetups = new List<WallObjectSetup>(CurrentLevelWallObjectSetups);
        //return;
        foreach (var wall in CurrentLevelWallObjectSetups)
        {
            PaintingSetupModule.CurrentGridObject.CreateWallObject(wall);
        }
    }

    public void Save()
    {
#if UNITY_EDITOR
        Undo.RecordObject(PaintingSetupModule.CurrentPaintingConfig, "Save pipe configs");
#endif
        ImportWallsToPaintingConfig(PaintingSetupModule.CurrentPaintingConfig);
        PaintingSetupModule.Save();
    }
}
