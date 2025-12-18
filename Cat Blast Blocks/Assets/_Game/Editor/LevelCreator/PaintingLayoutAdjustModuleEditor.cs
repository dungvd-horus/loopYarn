using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PaintingLayoutAdjustModule))]
public class PaintingLayoutAdjustModuleEditor : Editor
{
    PaintingLayoutAdjustModule manager;

    PaintingPixelComponent SelectedItem;

    private void OnEnable()
    {
        manager = (PaintingLayoutAdjustModule)target;
        manager.IsPaintingToolActive = false;
        manager.IsDeleteToolActive = false;
    }

    private void OnDisable()
    {
        manager.IsPaintingToolActive = false;
        manager.IsDeleteToolActive = false;
    }

    public override void OnInspectorGUI()
    {
        if (manager == null) return;
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("ADJUST LAYOUT", EditorStyles.boldLabel);
        if (GUILayout.Button("ACTIVE ADD TOOL"))
        {
            manager.SetActivePaintingTool();
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("ACTIVE DELETE TOOL"))
        {
            manager.SetActiveDeleteTool();
            SceneView.RepaintAll();
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("HIDE"))
        {
            manager.SetHideSelected();
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("ADD LINE ABOVE"))
        {
            manager.AddLineAbove();
        }
        if (GUILayout.Button("ADD LINE BELOW"))
        {
            manager.AddLineBelow();
        }
        if (GUILayout.Button("ADD COLUMN LEFT"))
        {
            manager.AddLineLeft();
        }
        if (GUILayout.Button("ADD COLUMN RIGHT"))
        {
            manager.AddLineRight();
        }
    }

    //private void OnSceneGUI()
    //{
    //    Event e = Event.current;

    //    float width = 100f;
    //    float height = 50f;
    //    float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
    //    float y = SceneView.currentDrawingSceneView.position.height - height - 90f;

    //    if (manager.IsPaintingToolActive || manager.IsDeleteToolActive)
    //    {
    //        Handles.BeginGUI();
    //        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);
    //        Color oldColor = GUI.color;
    //        GUI.color = manager.GetCurrentColor();
    //        GUILayout.Label(manager.IsPaintingToolActive ? "ADD PIXEL" : "DELETE PIXEL", EditorStyles.boldLabel);
    //        GUI.color = oldColor;
    //        GUILayout.EndArea();
    //        Handles.EndGUI();
    //    }
    //    else return;

    //    if (manager.IsPaintingToolActive)
    //    {
    //        GUILayout.Label("ADD PIXEL", EditorStyles.boldLabel);
    //        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
    //        {
    //            Ray worldRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);

    //            if (Physics.Raycast(worldRay, out RaycastHit hit, 1000f, manager.GridLayerMask))
    //            {
    //                manager.AddPixel(hit.point);
    //            }
    //        }
    //    }
    //    else if (manager.IsDeleteToolActive)
    //    {
    //        GUILayout.Label("DELETE PIXEL", EditorStyles.boldLabel);
    //        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
    //        {
    //            Ray worldRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);
    //            if (Physics.Raycast(worldRay, out RaycastHit hit, 1000f, manager.GridLayerMask))
    //            {
    //                manager.HidePixel(hit.point);
    //            }
    //        }
    //    }
    //}
}