using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BigBlockObjectConfigSetup))]
public class WallObjectConfigSetupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BigBlockObjectConfigSetup wallConfigSetup = (BigBlockObjectConfigSetup)target;

        // Draw the default inspector fields
        DrawDefaultInspector();

        // Add a separator
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Wall Creation", EditorStyles.boldLabel);

        if (GUILayout.Button("Reload"))
        {
            wallConfigSetup.Reload();
        }

        PaintingGridObject _grid = wallConfigSetup.PaintingSetupModule.CurrentGridObject;

        // Button to create a wall between the selected start and end pixels
        if (GUILayout.Button("Create Wall"))
        {

            if (_grid == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign the GridObject reference.", "OK");
                return;
            }

            // Create the wall
            wallConfigSetup.CreateBigBlock();

            // Mark scene as dirty to save changes
            EditorUtility.SetDirty(wallConfigSetup);
            if (wallConfigSetup.gameObject != null)
                EditorUtility.SetDirty(wallConfigSetup.gameObject);
        }

        // Button to clear all wall setups
        if (GUILayout.Button("Clear All Wall Setups"))
        {
            if (EditorUtility.DisplayDialog("Confirm", "Are you sure you want to clear all wall setups?", "Yes", "No"))
            {
                wallConfigSetup.ClearWallSetups();

                // Also clear wall objects from the grid if they exist
                if (_grid != null && _grid.WallObjects != null)
                {
                    // Destroy the wall gameobjects
                    List<WallObject> currentWalls = new List<WallObject>(_grid.WallObjects);
                    foreach (var wallObj in currentWalls)
                    {
                        _grid.RemoveWallObject(wallObj);
                    }
                    _grid.WallObjects.Clear();
                }

                EditorUtility.SetDirty(wallConfigSetup);
                if (wallConfigSetup.gameObject != null)
                    EditorUtility.SetDirty(wallConfigSetup.gameObject);
            }
        }

        // Button to import wall setups to PaintingConfig
        if (GUILayout.Button("SAVE"))
        {
            if (EditorUtility.DisplayDialog("Confirm", "Save to config?", "Yes", "No"))
            {
                if (_grid == null || wallConfigSetup.PaintingSetupModule.CurrentPaintingConfig == null)
                {
                    EditorUtility.DisplayDialog("Error", "GridObject or PaintingConfig reference is missing.", "OK");
                    return;
                }

                wallConfigSetup.ImportWallsToPaintingConfig(wallConfigSetup.PaintingSetupModule.CurrentPaintingConfig);
                wallConfigSetup.PaintingSetupModule.Save();
            }
        }

        // Display some useful information
        EditorGUILayout.Space();
        if (wallConfigSetup.wallObjectSetups != null)
        {
            EditorGUILayout.LabelField($"Wall Setups Created: {wallConfigSetup.wallObjectSetups.Count}", EditorStyles.helpBox);
        }

        if (_grid != null && _grid.WallObjects != null)
        {
            EditorGUILayout.LabelField($"Wall Objects in Scene: {_grid.WallObjects.Count}", EditorStyles.helpBox);
        }

        // Add information about wall rotation
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Note: Horizontal Wall (in same row) will be rotated 90 degrees on Y-axis. Vertical pipes (in same column) maintain default orientation.", MessageType.Info);
    }
}
