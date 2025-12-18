using System.Collections;
using System.Collections.Generic;

using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PaintingAdvancedSetup))]
public class PaintingAdvancedSetupEditor : Editor
{
    PaintingAdvancedSetup manager;
    private static readonly string[] toolLabels = { "PIPE", "KEY", "BIG BLOCK", "PAINT", "ERASE" };
    private static readonly string[] toolModeLabels = { "CREATE", "DELETE"};
    private static readonly string[] drawModeLabels = { "SINGLE", "MULTIPLE"};
    private static readonly string[] bigBlockToolModeLabels = { "PRE-SETUP", "CREATE", "DELETE"};

    private Plane groundPlane;

    private static int selectedTool = 0;

    private string[] LevelColors = new string[0];
    private string pipeSetupLabel = "Pipe color";
    int currentRow = 0;
    int currentColumn = 0;
    private bool debug = false;

    private void OnEnable()
    {
        manager = (PaintingAdvancedSetup)target;
        if (manager != null)
        {
            manager.ToolActive = false;
        }
        groundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
        debug = false;
    }

    private void OnDisable()
    {
        if (manager != null)
        {
            manager.ToolActive = false;
        }
    }

    public override void OnInspectorGUI()
    {
        if (manager == null) return;
        base.OnInspectorGUI();
        if (GUILayout.Button("ACTIVE TOOL", GUILayout.Height(30)))
        {
            manager.SetToolActive();
            if (manager.ToolActive)
            {
                SelectedPipeObj = null;
                LevelColors = manager.PaintingSetup?.ColorCodeInUse?.ToArray() ?? new string[0];
            }
        }
    }

    private void OnSceneGUI()
    {
        if (manager == null || !manager.ToolActive) return;
        if (manager.PaintingSetup?.ColorCodeInUse == null) return;
        LevelColors = manager.PaintingSetup.ColorCodeInUse.ToArray();
        ShowToolToggles();

        if (debug) ShowLevelInfomations();

        switch (selectedTool)
        {
            case 0:
                ShowDeleteLastPipe();
                ShowPipeToolToggles();
                if (selectedPipeToolMode == 0)
                {
                    ShowPipeConfigLabel();
                    CreatePipeMode();
                    ShowMouseGridPosition(true, Color.red);
                }
                else
                {
                    CheckDeletePipeObject();
                }
                break;
            case 1:
                ShowDeleteLastKey();
                ShowKeyToolToggles();
                if (selectedKeyToolMode == 0)
                {
                    CreateKeyMode();
                    ShowMouseGridPosition(true, Color.red);
                }
                else
                {
                    CheckDeleteKeyObject();
                }
                break;
            case 2:
                ShowDeleteLastWall();
                ShowBigBlockToolToggles();
                if (selectedBigBlockToolMode == 0)
                {
                    ShowBigBlockConfigLabel();
                }
                if (selectedBigBlockToolMode == 1)
                {
                    CreateBigBlockMode();
                    ShowMouseGridPosition(true, Color.red);
                }
                else
                {
                    CheckDeleteBigBlockObject();
                }
                break;
            case 3:
                ShowDrawToolToggles();
                ShowPaintToolConfigLabel();
                PaintPixel();
                break;
            case 4:
                ErasePixel();
                break;
        }
    }

    #region tool selection
    private void ShowToolToggles()
    {
        Handles.BeginGUI();

        float width = 355f;
        float height = 50f;

        float xRight = SceneView.currentDrawingSceneView.position.width - width;
        float xLeft = xRight - width - 50f;  // thêm area bên trái và cách 50px

        float y = SceneView.currentDrawingSceneView.position.height - height - 20f;

        // --- LEFT TOGGLES ---
        GUILayout.BeginArea(new Rect(xLeft, y, width, height), GUI.skin.box);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("< UNDO", GUILayout.Height(30)))
        {
            manager.PaintingSetup.CurrentPaintingConfig.RestoreBackup();
            manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);
        }
        if (GUILayout.Button("REDO >", GUILayout.Height(30)))
        {
            manager.PaintingSetup.CurrentPaintingConfig.RestoreBackupForward();
            manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        // --- ORIGINAL TOGGLES ---
        GUILayout.BeginArea(new Rect(xRight, y, width, height), GUI.skin.box);
        GUILayout.BeginHorizontal();

        Color color = GUI.backgroundColor;
        GUI.backgroundColor = Color.yellow;
        if (GUILayout.Button("CHECK", GUILayout.Width(55), GUILayout.Height(30)))
        {
            debug = !debug;
        }
        GUI.backgroundColor = color;

        for (int i = 0; i < toolLabels.Length; i++)
        {
            bool newValue = GUILayout.Toggle(selectedTool == i, toolLabels[i], "Button", GUILayout.Height(30));
            if (newValue && selectedTool != i)
            {
                selectedTool = i;
                SceneView.RepaintAll();
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        Handles.EndGUI();
    }
    #endregion

    #region pipe
    PaintingPixel startPixel;
    PaintingPixel endPixel;
    public PipeObject SelectedPipeObj;
    private static int selectedPipeToolMode = 0;
    private List<PaintingPixel> dragPipePixels = null;
    private PipeObjectSetup lastPipeSetup;
    private void ShowPipeToolToggles()
    {
        Handles.BeginGUI();

        float width = 355f;
        float height = 50f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 60f;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        GUILayout.BeginHorizontal();

        Color color = GUI.backgroundColor;
        GUI.backgroundColor = Color.yellow;
        //if (GUILayout.Button("CHECK", GUILayout.Width(55), GUILayout.Height(30)))
        //{
        //    debug = !debug;
        //}
        GUI.backgroundColor = color;

        for (int i = 0; i < toolModeLabels.Length; i++)
        {
            bool newValue = GUILayout.Toggle(selectedPipeToolMode == i, toolModeLabels[i], "Button", GUILayout.Height(30));
            if (newValue && selectedPipeToolMode != i)
            {
                selectedPipeToolMode = i;
                UnityEngine.Debug.Log($"Selected Mode: {toolModeLabels[i]}");
                SceneView.RepaintAll();
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        Handles.EndGUI();
    }

    public void CreatePipeMode()
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            startPixel = new PaintingPixel();
            startPixel.row = currentRow;
            startPixel.column = currentColumn;
            e.Use();
            dragPipePixels = null; // reset
        }

        if (e.type == EventType.MouseDrag && e.button == 0 && !e.alt && startPixel != null)
        {
            PaintingPixel tmpEnd = new PaintingPixel { row = currentRow, column = currentColumn };
            dragPipePixels = manager.WallSetupModule.GetPixelsBetweenRectangle(startPixel, tmpEnd);

            HandleUtility.Repaint();
            e.Use(); // optional, nếu bạn muốn ngăn event khác xử lý
        }

        if (e.type == EventType.MouseUp && e.button == 0 && !e.alt)
        {
            endPixel = new PaintingPixel();
            endPixel.row = currentRow;
            endPixel.column = currentColumn;

            if (endPixel == null || startPixel == null)
            {
                endPixel = null;
                startPixel = null;
                return;
            }

            int rowDistance = Mathf.Abs(endPixel.row - startPixel.row);
            int colDistance = Mathf.Abs(endPixel.column - startPixel.column);

            if (rowDistance == 0 && colDistance == 0)
            {
                return;
            }

            bool vertical = (rowDistance > colDistance);

            if (vertical) endPixel.column = startPixel.column;
            else endPixel.row = startPixel.row;

            e.Use();

            var _new = manager.PipeSetupModule.CreatePipe(startPixel, endPixel, LevelColors[manager.currentSelectedColorCode], manager.heartInput);
            manager.PipeSetupModule.Save();
            manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);

            if (_new != null) lastPipeSetup = _new;
            endPixel = null;
            startPixel = null;

            dragPipePixels = null;
        }

        if (e.type == EventType.Repaint && dragPipePixels != null && dragPipePixels.Count > 0)
        {
            Color oldColor = Handles.color;
            var oldZTest = Handles.zTest;

            //Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual; // hoặc Always để luôn thấy

            foreach (PaintingPixel pixel in dragPipePixels)
            {
                Color _c = Color.white;
                try
                {
                    _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(LevelColors[manager.currentSelectedColorCode]);
                }
                catch { }
                Handles.color = _c;
                Handles.DrawSolidDisc(pixel.worldPos, Vector3.up, 0.1f);
            }

            Handles.color = oldColor;
            Handles.zTest = oldZTest;
        }
    }

    public void CheckDeletePipeObject()
    {
        if (SelectedPipeObj != null)
        {
            Handles.BeginGUI();

            float width = 200f;
            float height = 80f;
            float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
            float y = SceneView.currentDrawingSceneView.position.height - height - 120;

            GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

            Color oldColor = GUI.color;
            Color _c = GUI.color;
            try
            {
                _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(LevelColors[manager.currentSelectedColorCode]);
            }
            catch { }
            GUI.color = _c;
            GUILayout.Label("Pipe Selected: " + SelectedPipeObj.ColorCode);
            GUI.color = Color.red;

            GUILayout.Space(10);

            if (GUILayout.Button("DELETE"))
            {
                manager.PipeSetupModule.RemovePipeObjectAndItConfig(SelectedPipeObj, false);
                manager.PipeSetupModule.Save();
                manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);
            }
            GUI.color = oldColor;

            GUILayout.EndArea();

            Handles.EndGUI();
            if (SelectedPipeObj) Handles.DrawWireDisc(SelectedPipeObj.transform.position, Vector3.up, 0.2f);
        }

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);

            try
            {
                if (Physics.Raycast(worldRay, out RaycastHit hit, 1000f, manager.BlockObjectLayermask))
                {
                    var item = hit.collider.transform.parent.GetComponentInChildren<PipeObject>();
                    if (item != null && item != SelectedPipeObj)
                    {
                        SelectedPipeObj = item;
                        Selection.activeGameObject = item.gameObject;
                        SceneView.RepaintAll();
                        //e.Use(); // chặn click này không bị SceneView dùng
                    }
                }
                else
                {
                    SelectedPipeObj = null;
                }
            }
            catch { }
        }
    }

    public void ShowPipeConfigLabel()
    {
        Handles.BeginGUI();
        float width = 200f;
        float height = 80f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 120;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        GUILayout.Label("Pipe setup:");
        if (manager.PaintingSetup == null) pipeSetupLabel = GUILayout.TextField(pipeSetupLabel);
        else if (LevelColors.Length > 0)
        {
            Color oldColor = GUI.color;
            Color _c = GUI.color;
            try
            {
                _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(LevelColors[manager.currentSelectedColorCode]);
            }
            catch { }
            GUI.color = _c;
            manager.currentSelectedColorCode = Mathf.Clamp(manager.currentSelectedColorCode, 0, LevelColors.Length - 1);
            manager.currentSelectedColorCode = EditorGUILayout.Popup(manager.currentSelectedColorCode, LevelColors);
            GUILayout.Label("Hearts:");
            //heartTextInput = GUILayout.TextField(heartTextInput);
            GUIStyle style = new GUIStyle(EditorStyles.numberField);
            style.normal.textColor = _c;
            style.fontStyle = FontStyle.Bold;
            manager.heartInput = EditorGUILayout.IntField(manager.heartInput, style);
            GUILayout.Label("Current: " + LevelColors[manager.currentSelectedColorCode]);
            GUI.color = oldColor;
        }

        GUILayout.EndArea();
        Handles.EndGUI();
    }

    public void ShowDeleteLastPipe()
    {
        if (lastPipeSetup == null) return;
        Handles.BeginGUI();

        float width = 100;
        float height = 30;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 200;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        Color oldColor = GUI.color;
        GUI.color = Color.red;

        GUILayout.Space(10);

        if (GUILayout.Button("DELETE LAST"))
        {
            manager.PipeSetupModule.RemovePipeObjectAndItConfig(lastPipeSetup, false);
            manager.PipeSetupModule.Save();
            manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);
        }
        GUI.color = oldColor;

        GUILayout.EndArea();

        Handles.EndGUI();
    }

    #endregion

    #region big block
    public WallObject SelectedBigBLockObj;
    private static int selectedBigBlockToolMode = 0;
    private List<PaintingPixel> dragWallPixels = null;
    private WallObjectSetup lastWallSetup;
    private void ShowBigBlockToolToggles()
    {
        Handles.BeginGUI();

        float width = 355f;
        float height = 50f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 60f;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        GUILayout.BeginHorizontal();

        Color color = GUI.backgroundColor;
        GUI.backgroundColor = Color.yellow;
        //if (GUILayout.Button("CHECK", GUILayout.Width(55), GUILayout.Height(30)))
        //{
        //    debug = !debug;
        //}
        GUI.backgroundColor = color;

        for (int i = 0; i < bigBlockToolModeLabels.Length; i++)
        {
            bool newValue = GUILayout.Toggle(selectedBigBlockToolMode == i, bigBlockToolModeLabels[i], "Button", GUILayout.Height(30));
            if (newValue && selectedBigBlockToolMode != i)
            {
                selectedBigBlockToolMode = i;
                UnityEngine.Debug.Log($"Selected Mode: {bigBlockToolModeLabels[i]}");
                SceneView.RepaintAll();
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        Handles.EndGUI();
    }

    public void CreateBigBlockMode()
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            startPixel = new PaintingPixel();
            startPixel.row = currentRow;
            startPixel.column = currentColumn;

            dragWallPixels = null; // reset
            e.Use();
        }

        if (e.type == EventType.MouseDrag && e.button == 0 && !e.alt && startPixel != null)
        {
            PaintingPixel tmpEnd = new PaintingPixel { row = currentRow, column = currentColumn };
            dragWallPixels = manager.WallSetupModule.GetPixelsBetweenRectangle(startPixel, tmpEnd);

            HandleUtility.Repaint();
            e.Use(); // optional, nếu bạn muốn ngăn event khác xử lý
        }

        if (e.type == EventType.MouseUp && e.button == 0 && !e.alt && startPixel != null)
        {
            PaintingPixel endPixel = new PaintingPixel();
            endPixel.row = currentRow;
            endPixel.column = currentColumn;

            var _new = manager.WallSetupModule.CreateBigBlock(startPixel, endPixel, LevelColors[manager.currentSelectedColorCode], manager.heartInput);
            manager.WallSetupModule.Save();
            manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);

            if (_new != null) lastWallSetup = _new;
            startPixel = null;
            dragWallPixels = null;

            e.Use();
        }

        if (e.type == EventType.Repaint && dragWallPixels != null && dragWallPixels.Count > 0)
        {
            Color oldColor = Handles.color;
            var oldZTest = Handles.zTest;

            //Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual; // hoặc Always để luôn thấy

            foreach (PaintingPixel pixel in dragWallPixels)
            {
                Color _c = Color.white;
                try
                {
                    _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(LevelColors[manager.currentSelectedColorCode]);
                }
                catch { }
                Handles.color = _c;
                Handles.DrawSolidDisc(pixel.worldPos, Vector3.up, 0.1f);
            }

            Handles.color = oldColor;
            Handles.zTest = oldZTest;
        }
    }

    public void CheckDeleteBigBlockObject()
    {
        if (SelectedBigBLockObj != null)
        {
            Handles.BeginGUI();

            float width = 200f;
            float height = 80f;
            float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
            float y = SceneView.currentDrawingSceneView.position.height - height - 120;

            GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

            Color oldColor = GUI.color;
            Color _c = GUI.color;
            try
            {
                _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(LevelColors[manager.currentSelectedColorCode]);
            }
            catch { }
            GUI.color = _c;
            GUILayout.Label("Block Selected: " + SelectedBigBLockObj.ColorCode);
            GUI.color = Color.red;

            GUILayout.Space(10);

            if (GUILayout.Button("DELETE"))
            {
                manager.WallSetupModule.RemoveWallObjectAndItConfig(SelectedBigBLockObj, false);
                manager.WallSetupModule.Save();
                manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);
            }
            GUI.color = oldColor;

            GUILayout.EndArea();

            Handles.EndGUI();
            if (SelectedBigBLockObj) Handles.DrawWireDisc(SelectedBigBLockObj.transform.position, Vector3.up, 0.2f);
        }

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);

            try
            {
                if (Physics.Raycast(worldRay, out RaycastHit hit, 1000f, manager.BlockObjectLayermask))
                {
                    var item = hit.collider.transform.parent.GetComponentInChildren<WallObject>();
                    if (item != null && item != SelectedBigBLockObj)
                    {
                        SelectedBigBLockObj = item;
                        Selection.activeGameObject = item.gameObject;
                        SceneView.RepaintAll();
                        //e.Use(); // chặn click này không bị SceneView dùng
                    }
                }
                else
                {
                    SelectedBigBLockObj = null;
                }
            }
            catch { }
        }
    }

    public void ShowBigBlockConfigLabel()
    {
        Handles.BeginGUI();
        float width = 200f;
        float height = 80f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 120;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        GUILayout.Label("Block Setup:");
        if (manager.PaintingSetup == null) pipeSetupLabel = GUILayout.TextField(pipeSetupLabel);
        else if (LevelColors.Length > 0)
        {
            Color oldColor = GUI.color;
            Color _c = GUI.color;
            try
            {
                _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(LevelColors[manager.currentSelectedColorCode]);
            }
            catch { }
            GUI.color = _c;
            manager.currentSelectedColorCode = Mathf.Clamp(manager.currentSelectedColorCode, 0, LevelColors.Length - 1);
            manager.currentSelectedColorCode = EditorGUILayout.Popup(manager.currentSelectedColorCode, LevelColors);

            GUILayout.Label("Hearts:");
            //heartTextInput = GUILayout.TextField(heartTextInput);
            GUIStyle style = new GUIStyle(EditorStyles.numberField);
            style.normal.textColor = _c;
            style.fontStyle = FontStyle.Bold;
            manager.heartInput = EditorGUILayout.IntField(manager.heartInput, style);
            GUI.color = oldColor;
        }

        GUILayout.EndArea();
        Handles.EndGUI();
    }

    public void ShowDeleteLastWall()
    {
        if (lastWallSetup == null) return;
        Handles.BeginGUI();

        float width = 100;
        float height = 30;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 200;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        Color oldColor = GUI.color;
        GUI.color = Color.red;

        GUILayout.Space(10);

        if (GUILayout.Button("DELETE LAST"))
        {
            manager.WallSetupModule.RemoveWallObjectAndItConfig(lastWallSetup, false);
            manager.WallSetupModule.Save();
            manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);
        }
        GUI.color = oldColor;

        GUILayout.EndArea();

        Handles.EndGUI();
    }
    #endregion

    #region key
    public KeyObject SelectedKeyObj;
    private static int selectedKeyToolMode = 0;
    private List<PaintingPixel> dragKeyPixels = null;
    private KeyObjectSetup lastKeySetup;
    private void ShowKeyToolToggles()
    {
        Handles.BeginGUI();

        float width = 355f;
        float height = 50f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 60f;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        GUILayout.BeginHorizontal();

        Color color = GUI.backgroundColor;
        GUI.backgroundColor = Color.yellow;
        GUI.backgroundColor = color;

        for (int i = 0; i < toolModeLabels.Length; i++)
        {
            bool newValue = GUILayout.Toggle(selectedKeyToolMode == i, toolModeLabels[i], "Button", GUILayout.Height(30));
            if (newValue && selectedKeyToolMode != i)
            {
                selectedKeyToolMode = i;
                UnityEngine.Debug.Log($"Selected Mode: {toolModeLabels[i]}");
                SceneView.RepaintAll();
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        Handles.EndGUI();
    }

    public void CreateKeyMode()
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            startPixel = new PaintingPixel();
            startPixel.row = currentRow;
            startPixel.column = currentColumn;

            dragWallPixels = null; // reset
            e.Use();
        }

        if (e.type == EventType.MouseDrag && e.button == 0 && !e.alt && startPixel != null)
        {
            PaintingPixel tmpEnd = new PaintingPixel { row = currentRow, column = currentColumn };
            dragWallPixels = manager.KeySetupModule.GetPixelsBetweenRectangle(startPixel, tmpEnd);

            HandleUtility.Repaint();
            e.Use(); // optional, nếu bạn muốn ngăn event khác xử lý
        }

        if (e.type == EventType.MouseUp && e.button == 0 && !e.alt && startPixel != null)
        {
            PaintingPixel endPixel = new PaintingPixel();
            endPixel.row = currentRow;
            endPixel.column = currentColumn;

            var _new = manager.KeySetupModule.CreateKey(startPixel, endPixel);
            manager.KeySetupModule.Save();
            manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);

            if (_new != null) lastKeySetup = _new;
            startPixel = null;
            dragWallPixels = null;

            e.Use();
        }

        if (e.type == EventType.Repaint && dragWallPixels != null && dragWallPixels.Count > 0)
        {
            Color oldColor = Handles.color;
            var oldZTest = Handles.zTest;

            //Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual; // hoặc Always để luôn thấy

            foreach (PaintingPixel pixel in dragWallPixels)
            {
                Color _c = Color.yellow;
                Handles.color = _c;
                Handles.DrawSolidDisc(pixel.worldPos, Vector3.up, 0.1f);
            }

            Handles.color = oldColor;
            Handles.zTest = oldZTest;
        }
    }

    public void CheckDeleteKeyObject()
    {
        if (SelectedKeyObj != null)
        {
            Handles.BeginGUI();

            float width = 200f;
            float height = 80f;
            float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
            float y = SceneView.currentDrawingSceneView.position.height - height - 120;

            GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

            Color oldColor = GUI.color;
            Color _c = GUI.color;
            try
            {
                _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(LevelColors[manager.currentSelectedColorCode]);
            }
            catch { }
            GUI.color = _c;
            GUILayout.Label("Key Selected: " + SelectedKeyObj);
            GUI.color = Color.red;

            GUILayout.Space(10);

            if (GUILayout.Button("DELETE"))
            {
                manager.KeySetupModule.RemoveKeyObjectAndItConfig(SelectedKeyObj, false);
                manager.KeySetupModule.Save();
                manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);
            }
            GUI.color = oldColor;

            GUILayout.EndArea();

            Handles.EndGUI();
            if (SelectedKeyObj) Handles.DrawWireDisc(SelectedKeyObj.transform.position, Vector3.up, 0.2f);
        }

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);

            try
            {
                if (Physics.Raycast(worldRay, out RaycastHit hit, 1000f, manager.BlockObjectLayermask))
                {
                    var item = hit.collider.transform.GetComponentInChildren<KeyObject>();
                    if (item != null && item != SelectedKeyObj)
                    {
                        SelectedKeyObj = item;
                        Selection.activeGameObject = item.gameObject;
                        SceneView.RepaintAll();
                        //e.Use(); // chặn click này không bị SceneView dùng
                    }
                }
                else
                {
                    SelectedKeyObj = null;
                }
            }
            catch { }
        }
    }

    public void ShowDeleteLastKey()
    {
        if (lastKeySetup == null) return;
        Handles.BeginGUI();

        float width = 100;
        float height = 30;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 200;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        Color oldColor = GUI.color;
        GUI.color = Color.red;

        GUILayout.Space(10);

        if (GUILayout.Button("DELETE LAST"))
        {
            manager.KeySetupModule.RemoveKeyObjectAndItConfig(lastKeySetup, false);
            manager.KeySetupModule.Save();
            manager.PaintingSetup.CurrentGridObject.ApplyPaintingConfig(manager.PaintingSetup.CurrentPaintingConfig);
        }
        GUI.color = oldColor;

        GUILayout.EndArea();

        Handles.EndGUI();
    }
    #endregion

    #region add / remove painting pixel
    private static int selectedDrawToolMode = 0;
    private List<PaintingPixel> dragDrawPixels = null;
    public void PaintPixel()
    {
        Event e = Event.current;

        float width = 100f;
        float height = 50f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 90f;

        if (selectedDrawToolMode == 0)
        {
            if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
            {
                Ray worldRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);

                if (Physics.Raycast(worldRay, out RaycastHit hit, 1000f, manager.PaintModule.GridLayerMask))
                {
                    manager.PaintModule.AddPixel(hit.point, LevelColors[manager.currentSelectedColorCode]);
                }
            }
        }
        else
        {
            if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
            {
                startPixel = new PaintingPixel();
                startPixel.row = currentRow;
                startPixel.column = currentColumn;

                dragDrawPixels = null; // reset
                e.Use();
            }

            if (e.type == EventType.MouseDrag && e.button == 0 && !e.alt && startPixel != null)
            {
                PaintingPixel tmpEnd = new PaintingPixel { row = currentRow, column = currentColumn };
                dragDrawPixels = manager.KeySetupModule.GetPixelsBetweenRectangle(startPixel, tmpEnd);

                HandleUtility.Repaint();
                e.Use(); // optional, nếu bạn muốn ngăn event khác xử lý
            }

            if (e.type == EventType.MouseUp && e.button == 0 && !e.alt && startPixel != null)
            {
                PaintingPixel endPixel = new PaintingPixel();
                endPixel.row = currentRow;
                endPixel.column = currentColumn;
                
                if (dragDrawPixels == null || dragDrawPixels.Count == 0 || endPixel == null) return;
                
                if (!dragDrawPixels.Contains(endPixel)) dragDrawPixels.Add(endPixel);
                
                foreach (PaintingPixel pixel in dragDrawPixels)
                {
                    manager.PaintModule.AddPixel(pixel.worldPos, LevelColors[manager.currentSelectedColorCode], save: false); ;
                }
                manager.PaintModule.Save();
                startPixel = null;
                dragDrawPixels = null;

                e.Use();
            }

            if (e.type == EventType.Repaint && dragDrawPixels != null && dragDrawPixels.Count > 0)
            {
                Color oldColor = Handles.color;
                var oldZTest = Handles.zTest;
                Color _c = GUI.color;
                try
                {
                    _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(LevelColors[manager.currentSelectedColorCode]);
                }
                catch { }
                foreach (PaintingPixel pixel in dragDrawPixels)
                {
                    Handles.color = _c;
                    Handles.DrawSolidDisc(pixel.worldPos, Vector3.up, 0.1f);
                }

                Handles.color = oldColor;
                Handles.zTest = oldZTest;
            }
        }
    }

    public void ErasePixel()
    {
        Event e = Event.current;

        float width = 100f;
        float height = 25f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 90f;

        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);
        Color oldColor = GUI.color;
        GUI.color = Color.red;
        GUILayout.Label("ERASE PIXEL", EditorStyles.boldLabel);
        GUI.color = oldColor;
        GUILayout.EndArea();
        Handles.EndGUI();
        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(worldRay, out RaycastHit hit, 1000f, manager.PaintModule.GridLayerMask))
            {
                manager.PaintModule.HidePixel(hit.point);
            }
        }
    }

    public void ShowPaintToolConfigLabel()
    {
        Handles.BeginGUI();
        float width = 200f;
        float height = 80f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 120;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);
        manager.currentSelectedColorCode = Mathf.Clamp(manager.currentSelectedColorCode, 0, LevelColors.Length - 1);
        GUILayout.Label("Painting Color:" + LevelColors[manager.currentSelectedColorCode]);
        if (manager.PaintingSetup == null) pipeSetupLabel = GUILayout.TextField(pipeSetupLabel);
        else if (LevelColors.Length > 0)
        {
            Color oldColor = GUI.color;
            Color _c = GUI.color;
            try
            {
                _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(LevelColors[manager.currentSelectedColorCode]);
            }
            catch { }
            GUI.color = _c;
            manager.currentSelectedColorCode = Mathf.Clamp(manager.currentSelectedColorCode, 0, LevelColors.Length - 1);
            manager.currentSelectedColorCode = EditorGUILayout.Popup(manager.currentSelectedColorCode, LevelColors);
            GUI.color = oldColor;
            ShowMouseGridPosition(false, _c, 0.04f);
        }

        GUILayout.EndArea();
        Handles.EndGUI();
    }

    private void ShowDrawToolToggles()
    {
        Handles.BeginGUI();

        float width = 355f;
        float height = 50f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 60f;

        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        GUILayout.BeginHorizontal();

        Color color = GUI.backgroundColor;
        GUI.backgroundColor = Color.yellow;
        //if (GUILayout.Button("CHECK", GUILayout.Width(55), GUILayout.Height(30)))
        //{
        //    debug = !debug;
        //}
        GUI.backgroundColor = color;

        for (int i = 0; i < drawModeLabels.Length; i++)
        {
            bool newValue = GUILayout.Toggle(selectedDrawToolMode == i, drawModeLabels[i], "Button", GUILayout.Height(30));
            if (newValue && selectedDrawToolMode != i)
            {
                selectedDrawToolMode = i;
                UnityEngine.Debug.Log($"Selected Mode: {drawModeLabels[i]}");
                SceneView.RepaintAll();
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        Handles.EndGUI();
    }
    #endregion

    public void ShowMouseGridPosition(bool showGrid, Color pointerColor, float pointRadius = 0.02f)
    {
        Event e = Event.current;
        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
        Handles.color = Color.green;

        // Tính vị trí chuột giao với mặt phẳng
        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            (int col, int row, Vector3 cellPos) = manager.PaintingSetup.CurrentGridObject.GetPredictedPixel(hitPoint);
            currentRow = row;
            currentColumn = col;

            Vector3 horizontalStart = new Vector3(-100, 0.35f, cellPos.z);
            Vector3 horizontalEnd = new Vector3(100, 0.35f, cellPos.z);

            Vector3 verticalStart = new Vector3(cellPos.x, 0.35f, -100);
            Vector3 verticalEnd = new Vector3(cellPos.x, 0.35f, 100);

            Handles.color = Color.green;
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.normal.textColor = Color.red;
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.alignment = TextAnchor.MiddleRight;

            if (showGrid)
            {
                Handles.DrawLine(horizontalStart, horizontalEnd);
                Handles.DrawLine(verticalStart, verticalEnd);
            }

            Handles.color = pointerColor;
            string labelText = $"({col}, {row})";

            Handles.Label(cellPos + Vector3.down * 0.7f, labelText, labelStyle);

            Handles.DrawSolidDisc(hitPoint, Vector3.up, pointRadius);
        }

        // Bắt Scene view cập nhật lại mỗi frame để vẽ liên tục
        HandleUtility.Repaint();
    }

    private void ShowLevelInfomations()
    {
        float width = 250f;
        float height = 800f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 150f;

        Handles.BeginGUI();
        Color oldBgColor = GUI.backgroundColor;
        //GUI.DrawTexture(new Rect(x, y, width, height), Texture2D.whiteTexture);
        GUI.backgroundColor = manager.BGColor;
        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);

        Color oldColor = GUI.color;
        GUI.color = Color.green;
        string levelName = manager.PaintingSetup.CurrentPaintingConfig != null ? manager.PaintingSetup.CurrentPaintingConfig.name : "No Painting Config Assigned";
        GUILayout.Label(levelName, EditorStyles.boldLabel);
        GUILayout.Space(10);
        GUILayout.Label("=> TOTAL BLOCKS: " + manager.PaintingSetup.CurrentPaintingConfig.GetAllWorkingPixels().Count, EditorStyles.boldLabel);
        GUILayout.Space(5);
        manager.CollectorSetupModule.ReCountCollectors();
        manager.CollectorSetupModule.CountGunnersAsSet();
        foreach (var colorSet in manager.CollectorSetupModule.colorSetCounters)
        {
            Color _c = Color.white;
            try
            {
                _c = manager.PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(colorSet.Key);
            }
            catch { }
            GUI.color = _c;
            GUILayout.Label($"         {colorSet.Key} - {colorSet.Value} blocks", EditorStyles.boldLabel);
        }

        GUI.color = Color.green;

        GUILayout.Space(10);
        GUILayout.Label("=> TOTAL GUNNERS: " + manager.CollectorSetupModule.previewSystem.CurrentCollectors.Count, EditorStyles.boldLabel);
        GUILayout.Space(5);
        foreach (var colorSet in manager.CollectorSetupModule.collectorSetCounters)
        {
            Color _c = Color.white;
            try
            {
                _c = manager.CollectorSetupModule.previewSystem.PrefabSource.ColorPallete.GetColorByCode(colorSet.Key);
            }
            catch { }
            GUI.color = _c;
            GUILayout.Label($"         {colorSet.Key} - {colorSet.Value} gunners", EditorStyles.boldLabel);
        }
        GUI.backgroundColor = oldBgColor;

        GUI.color = Color.green;
        GUILayout.Space(10);
        GUILayout.Label($"=> BLOCKS / BULLETS:  {manager.CollectorSetupModule.NumberOfWorkingPixels} / {manager.CollectorSetupModule.TotalBulletsCount}", EditorStyles.boldLabel);

        GUILayout.Space(10);
        manager.CollectorSetupModule.ReCountCollectors();
        GUILayout.Label($"=> KEYS / LOCKS:  {manager.CollectorSetupModule.paintingConfig.KeySetups.Count} / {manager.CollectorSetupModule.NumberOfLockedCollector}", EditorStyles.boldLabel);
        GUI.color = oldColor;

        GUILayout.EndArea();
    }
}
