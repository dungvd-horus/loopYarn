using static PaintingSharedAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Module to handle BlockFountain creation and management on the grid
/// Attach this to the same GameObject as PaintingGridObject
/// </summary>
[RequireComponent(typeof(PaintingGridObject))]
public class GridBlockFountainModule : MonoBehaviour
{
    public PaintingGridObject CurrentGrid;

    private void OnValidate()
    {
        if (CurrentGrid == null)
        {
            CurrentGrid = GetComponent<PaintingGridObject>();
        }
    }

    private void Awake()
    {
        if (CurrentGrid == null)
        {
            CurrentGrid = GetComponent<PaintingGridObject>();
        }
    }

    public BlockFountainObject CreateBlockFountainObject(BlockFountainObjectSetup fountainSetup)
    {
        Debug.Log($"[GridBlockFountainModule] CreateBlockFountainObject called");

        try
        {
            if (fountainSetup.PixelCovered == null || fountainSetup.PixelCovered.Count <= 1)
            {
                Debug.LogWarning($"[GridBlockFountainModule] FAILED: PixelCovered is null or count <= 1");
                return null;
            }

            List<PaintingPixel> fountainPixels = new List<PaintingPixel>();

            foreach (PaintingPixelConfig pixelConfig in fountainSetup.PixelCovered)
            {
                PaintingPixel respectedPixel = CurrentGrid.GetPixelAtGridPosition(pixelConfig.column, pixelConfig.row);
                if (respectedPixel != null)
                {
                    respectedPixel.colorCode = IndestructibleColorKey;
                    fountainPixels.Add(respectedPixel);
                }
                else
                {
                    PaintingPixel additionPixel = CurrentGrid.CreateNewPaintingPixelAbstract(pixelConfig, true);
                    additionPixel.color = Color.white;
                    additionPixel.colorCode = IndestructibleColorKey;
                    if (additionPixel != null) fountainPixels.Add(additionPixel);
                }
            }

            Debug.Log($"[GridBlockFountainModule] fountainPixels.Count = {fountainPixels.Count}");

            if (fountainPixels.Count != fountainSetup.PixelCovered.Count)
            {
                Debug.LogError($"[GridBlockFountainModule] FAILED: Pixel count mismatch!");
                return null;
            }

            Vector3 fountainPosition = CurrentGrid.GetCenterByBoundingBox(fountainPixels);
            Debug.Log($"[GridBlockFountainModule] Position: {fountainPosition}");

            if (CurrentGrid.PrefabSource == null || CurrentGrid.PrefabSource.BlockFountainPrefab == null)
            {
                Debug.LogError("[GridBlockFountainModule] FAILED: Prefab is NULL!");
                return null;
            }

            Debug.Log($"[GridBlockFountainModule] Instantiating prefab...");

            GameObject fountainGO = Instantiate(
                CurrentGrid.PrefabSource.BlockFountainPrefab,
                fountainPosition,
                Quaternion.identity,
                CurrentGrid.GridTransform
            );

            Debug.Log($"[GridBlockFountainModule] fountainGO created: {fountainGO != null}");

            fountainGO.name = "BLOCK_FOUNTAIN";

            BlockFountainObject fountainObject = fountainGO.GetComponent<BlockFountainObject>();
            Debug.Log($"[GridBlockFountainModule] GetComponent result: {(fountainObject != null ? "FOUND" : "NULL, will AddComponent")}");

            if (fountainObject == null)
            {
                fountainObject = fountainGO.AddComponent<BlockFountainObject>();
                Debug.Log($"[GridBlockFountainModule] AddComponent done: {fountainObject != null}");
            }

            // Calculate scale
            (int height, int width) = CurrentGrid.GetShapeSize(fountainPixels);
            Debug.Log($"[GridBlockFountainModule] Shape: height={height}, width={width}");

            Vector3 defaultPixelScale = CurrentGrid.blockScale;
            Vector3 fountainScale = new Vector3(
                defaultPixelScale.x * (height / 2f),
                defaultPixelScale.y,
                defaultPixelScale.z * (height / 2f)
            );

            Debug.Log($"[GridBlockFountainModule] FountainRootObject null? {fountainObject.FountainRootObject == null}");

            if (fountainObject.FountainRootObject != null)
                fountainObject.FountainRootObject.localScale = fountainScale;

            // Setup fountain pixels
            if (CurrentGrid.BlockFountainObjectsPixels == null)
                CurrentGrid.BlockFountainObjectsPixels = new List<PaintingPixel>();

            Debug.Log($"[GridBlockFountainModule] Setting up {fountainPixels.Count} fountain pixels...");

            foreach (PaintingPixel pixel in fountainPixels)
            {
                pixel.PixelComponent?.HideVisualOnly();
                if (!CurrentGrid.BlockFountainObjectsPixels.Contains(pixel))
                {
                    pixel.destroyed = false;
                    pixel.Indestructible = true;
                    pixel.IsFountainObjectPixel = true;
                    CurrentGrid.BlockFountainObjectsPixels.Add(pixel);
                }
            }

            Debug.Log($"[GridBlockFountainModule] Fountain pixels setup done");

            // Check BlockSets
            Debug.Log($"[GridBlockFountainModule] BlockSets null? {fountainSetup.BlockSets == null}");
            Debug.Log($"[GridBlockFountainModule] BlockSets count: {fountainSetup.BlockSets?.Count ?? 0}");

            if (fountainSetup.BlockSets != null)
            {
                for (int i = 0; i < fountainSetup.BlockSets.Count; i++)
                {
                    var bs = fountainSetup.BlockSets[i];
                    Debug.Log($"[GridBlockFountainModule] BlockSet[{i}]: ColorCode={bs?.ColorCode ?? "NULL"}, BlockCount={bs?.BlockCount ?? 0}");
                }
            }

            // Initialize the fountain
            Debug.Log($"[GridBlockFountainModule] Calling Initialize...");
            fountainObject.LevelMechanicPrefabs = CurrentGrid.PrefabSource;
            fountainObject.Initialize(fountainSetup.BlockSets, fountainPixels, CurrentGrid);
            Debug.Log($"[GridBlockFountainModule] Initialize done");

            Debug.Log($"[GridBlockFountainModule] Adding to list...");
            CurrentGrid.BlockFountainObjects.Add(fountainObject);
            Debug.Log($"[GridBlockFountainModule] SUCCESS! Count: {CurrentGrid.BlockFountainObjects.Count}");

            return fountainObject;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[GridBlockFountainModule] EXCEPTION: {ex.Message}");
            Debug.LogError($"[GridBlockFountainModule] StackTrace: {ex.StackTrace}");
            return null;
        }
    }

    public void RemoveBlockFountainObject(BlockFountainObject _fountain)
    {
        if (_fountain == null) return;
        if (CurrentGrid.BlockFountainObjects.Contains(_fountain))
        {
            _fountain.SelfDestroyGameobject();
            CurrentGrid.BlockFountainObjects.Remove(_fountain);
        }
    }
}
