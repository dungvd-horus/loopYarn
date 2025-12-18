using UnityEngine;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2 gridSize = new Vector2(20, 40); // 20 columns, 40 rows
    public Vector3 BlockScale = Vector3.one; // Pixel gameobject scale factor
    public float pixelArrangeSpace = 1.0f; // Space distance between each pixels
    public float YOffset = 0.0f; // Y Offset for pixel placement
    public Transform pixelsParent; // Transform to use as parent for pixel objects
    public Material pixelMaterial;
    public LevelMechanicObjectPrefabs PrefabSource;

    [Space]
    public List<PaintingGridObject> AlreadyHave = new List<PaintingGridObject>();

    [Header("Generated Grid")]
    public PaintingGridObject paintingGridObject; // The grid object that will be the parent of all pixels

    private void Reset()
    {
        // Default values when component is added
        gridSize = new Vector2(20, 40); // 20 columns, 40 rows
        BlockScale = Vector3.one;
        pixelArrangeSpace = 1.0f;
    }

    public void ContextGenerateGrid()
    {
        GenerateGrid();
    }
    
    public void ContextClearGrid()
    {
        ClearGrid();
    }

    public void GenerateGrid()
    {
        paintingGridObject = null;
        // Create or get the grid object to use as parent for pixels
        GameObject gridObj = null;
        
        if (pixelsParent != null)
        {
            // Use the provided parent transform
            gridObj = pixelsParent.gameObject;
        }
        else
        {
            // Create a new GameObject to hold the pixels
            gridObj = new GameObject("PaintingGridObject");
            gridObj.transform.SetParent(this.transform);
            paintingGridObject = gridObj.AddComponent<PaintingGridObject>();
        }

        // If the parent doesn't have the PaintingGridObject component, add it
        if (paintingGridObject == null)
        {
            paintingGridObject = gridObj.GetComponent<PaintingGridObject>();
            if (paintingGridObject == null)
            {
                paintingGridObject = gridObj.AddComponent<PaintingGridObject>();
            }
        }

        // Initialize the grid
        // csharp
        // Inside GenerateGrid(), replace the InitializeGrid call with:
        paintingGridObject.InitializeGrid(
            gridSize,
            pixelArrangeSpace,
            PrefabSource.DefaultBlockPrefab,
            BlockScale,
            PrefabSource,          // <- missing argument (LevelMechanicObjectPrefabs)
            pixelMaterial          // pixelSharedMaterial
        );


        // Generate the grid using the PaintingGridObject component
        paintingGridObject.GenerateGrid(YOffset);

        AlreadyHave.Add(paintingGridObject);
    }

    public void ClearGrid()
    {
        if (paintingGridObject != null)
        {
            paintingGridObject.DestroyAllPixelsObjects();
        }
    }
    
    // Get the total number of pixels in the grid
    public int GetTotalPixels()
    {
        if (paintingGridObject != null)
        {
            return paintingGridObject.GetTotalPixels();
        }
        return 0;
    }
}
