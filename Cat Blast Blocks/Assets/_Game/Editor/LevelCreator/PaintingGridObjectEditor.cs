using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PaintingGridObject))]
public class PaintingGridObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Get reference to the script
        PaintingGridObject paintingGridObject = (PaintingGridObject)target;
        if (paintingGridObject == null) return;

        // Add a separator
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Painting Config Operations", EditorStyles.boldLabel);

        // Clear Grid to White button
        if (GUILayout.Button("Clear Painting"))
        {
            paintingGridObject.ClearAllPipes();
            paintingGridObject.ClearAllWalls();
            paintingGridObject.ClearAllKeys();
            paintingGridObject.ClearToWhite();
            paintingGridObject.ClearAllAdditionPixels();
            EditorUtility.SetDirty(target);
            Debug.Log("Grid cleared to white successfully.");
        }

        if (GUILayout.Button("Update position"))
        {
            paintingGridObject.UpdatePixelWorldPos();
            EditorUtility.SetDirty(target);
            Debug.Log("Grid cleared to white successfully.");
        }

        // Regenerate Grid button (if needed for convenience)
        if (GUILayout.Button("Regenerate Grid"))
        {
            GridGenerator gridGenerator = paintingGridObject.GetComponent<GridGenerator>();
            if (gridGenerator != null)
            {
                gridGenerator.ClearGrid();
                gridGenerator.GenerateGrid();
                Debug.Log("Grid regenerated successfully.");
            }
            else
            {
                Debug.LogWarning("No GridGenerator component found on this object.");
            }
        }
    }
}
