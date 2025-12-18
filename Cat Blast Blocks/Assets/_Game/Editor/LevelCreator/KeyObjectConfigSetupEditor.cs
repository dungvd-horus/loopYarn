using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyObjectConfigSetup))]
public class KeyObjectConfigSetupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        KeyObjectConfigSetup keyConfigSetup = (KeyObjectConfigSetup)target;

        // Draw the default inspector fields
        DrawDefaultInspector();
        PaintingGridObject _grid = keyConfigSetup.PaintingSetupModule.CurrentGridObject;
        // Add a separator
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Key Creation", EditorStyles.boldLabel);

        if (GUILayout.Button("Reload"))
        {
            keyConfigSetup.Reload();
        }

        // Button to create a key between the selected start and end pixels
        if (GUILayout.Button("Create Key"))
        {

            if (_grid == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign the GridObject reference.", "OK");
                return;
            }

            // Create the key
            keyConfigSetup.CreateKey();

            // Mark scene as dirty to save changes
            EditorUtility.SetDirty(keyConfigSetup);
            if (keyConfigSetup.gameObject != null)
                EditorUtility.SetDirty(keyConfigSetup.gameObject);
        }

        // Button to clear all key setups
        if (GUILayout.Button("Clear All Key Setups"))
        {
            if (EditorUtility.DisplayDialog("Confirm", "Are you sure you want to clear all key setups?", "Yes", "No"))
            {
                keyConfigSetup.ClearAllKeySetups();
            }
        }

        // Button to import key setups to PaintingConfig
        if (GUILayout.Button("SAVE"))
        {
            if (EditorUtility.DisplayDialog("Confirm", "Save to config?", "Yes", "No"))
            {
                if (_grid == null || keyConfigSetup.PaintingSetupModule.CurrentPaintingConfig == null)
                {
                    EditorUtility.DisplayDialog("Error", "GridObject or PaintingConfig reference is missing.", "OK");
                    return;
                }

                keyConfigSetup.ImportKeysToPaintingConfig(keyConfigSetup.PaintingSetupModule.CurrentPaintingConfig);
                keyConfigSetup.PaintingSetupModule.Save();
            }
        }

        // Display some useful information
        EditorGUILayout.Space();
        if (keyConfigSetup.keyObjectSetups != null)
        {
            EditorGUILayout.LabelField($"Key Setups Created: {keyConfigSetup.keyObjectSetups.Count}", EditorStyles.helpBox);
        }

        if (_grid != null && _grid.KeyObjects != null)
        {
            EditorGUILayout.LabelField($"Key Objects in Scene: {_grid.KeyObjects.Count}", EditorStyles.helpBox);
        }

        // Add information about key rotation
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Note: Horizontal Key (in same row) will be rotated 90 degrees on Y-axis. Vertical Key (in same column) maintain default orientation.", MessageType.Info);
    }
}
