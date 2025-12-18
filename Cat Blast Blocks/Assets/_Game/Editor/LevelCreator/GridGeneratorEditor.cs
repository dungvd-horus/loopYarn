using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GridGenerator gridGenerator = (GridGenerator)target;

        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Grid Controls", EditorStyles.boldLabel);
        
        // Buttons for grid operations
        if (GUILayout.Button("Generate Grid"))
        {
            gridGenerator.ContextGenerateGrid();
        }
        
        if (GUILayout.Button("Clear Grid"))
        {
            gridGenerator.ContextClearGrid();
        }
        
        if (GUILayout.Button("Regenerate Grid"))
        {
            gridGenerator.ContextClearGrid();
            gridGenerator.ContextGenerateGrid();
        }
        
        // Display grid info if grid exists
        if (gridGenerator.paintingGridObject != null)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Grid Info", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Grid Size: {gridGenerator.gridSize.x} cols x {gridGenerator.gridSize.y} rows");
            EditorGUILayout.LabelField($"Total Pixels: {gridGenerator.GetTotalPixels()}");
        }
    }
}