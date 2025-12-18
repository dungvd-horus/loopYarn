using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelCollectorsConfigSetup))]
public class LevelCollectorsConfigSetupEditor : Editor
{
    private string newConfigName = "CollectorsConfig";

    LevelCollectorsConfigSetup manager;
    public CollectorMachanicObjectBase SelectedItem;
    public CollectorMachanicObjectBase CollidedItem;

    private static int selectedMode = 0;
    private static readonly string[] labels = { "Move", "Swap", "Combine", "Split", "Connect" };

    private static bool[] checkboxes = new bool[2];
    private static readonly string[] checkBoxlabels = { "Lock", "Hide"};

    private bool debug = false;

    private void OnEnable()
    {
        manager = (LevelCollectorsConfigSetup)target;
        manager.ToolActive = false;
        debug = false;
    }

    private void OnDisable()
    {
        manager.ToolActive = false;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        manager = (LevelCollectorsConfigSetup)target;

        #region _inspector
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Create New Config Asset:", EditorStyles.boldLabel);

        if (GUILayout.Button("Create Config Asset"))
        {
            if (manager != null)
            {
                if (manager.paintingConfig != null) newConfigName = manager.paintingConfig.Sprite.name;
                LevelColorCollectorsConfig newConfig = manager.CreateConfigAsset(newConfigName);
                if (newConfig != null)
                {
                    manager.CurentCollectorsConfig = newConfig;
                }
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Load & Preview Config:", EditorStyles.boldLabel);
        if (GUILayout.Button("Load & Preview Config"))
        {
            if (manager.CurentCollectorsConfig != null)
            {
                manager.LoadConfigAsset(manager.CurentCollectorsConfig);
            }
            else
            {
                Debug.LogWarning("Please assign a config asset to load!");
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Import Config:", EditorStyles.boldLabel);
        if (GUILayout.Button("Import from Scene"))
        {
            if (EditorUtility.DisplayDialog("Confirm Generation",
                    "This will replace all existing collector setups in the config asset with new ones based on the one in scene. Are you sure?",
                    "Yes", "Cancel"))
            {
                if (manager.previewSystem != null && manager.CurentCollectorsConfig != null)
                {
                    manager.ImportCollectorsFromScene();
                    manager.Save();
                }
                else
                {
                    Debug.LogWarning("Please assign both source system and target config asset!");
                }
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Import from PaintingConfig:", EditorStyles.boldLabel);
        if (GUILayout.Button("Generate from PaintingConfig"))
        {
            if (manager.paintingConfig != null && manager.CurentCollectorsConfig != null)
            {
                if (EditorUtility.DisplayDialog("Confirm Generation",
                    "This will replace all existing collector setups in the config asset with new ones based on the painting config. Are you sure?",
                    "Yes", "Cancel"))
                {
                    manager.GenerateCollectorsFromPaintingConfig();
                    manager.LoadConfigAsset(manager.CurentCollectorsConfig);
                }
            }
            else
            {
                Debug.LogWarning("Please assign both paintingConfig and configAsset!");
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("UPDATE from PaintingConfig:", EditorStyles.boldLabel);
        if (GUILayout.Button("UPDATE from PaintingConfig"))
        {
            if (manager.paintingConfig != null && manager.CurentCollectorsConfig != null)
            {
                if (EditorUtility.DisplayDialog("Confirm Generation",
                    "This will replace all existing collector setups in the config asset with new ones based on the painting config. Are you sure?",
                    "Yes", "Cancel"))
                {
                    manager.UpdateCollectorsFromPaintingConfig();
                    manager.LoadConfigAsset(manager.CurentCollectorsConfig);
                }
            }
            else
            {
                Debug.LogWarning("Please assign both paintingConfig and configAsset!");
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Lock setup:", EditorStyles.boldLabel);
        if (GUILayout.Button("Create new Lock"))
        {
            manager.previewSystem.CreateLockObject();
            manager.ImportCollectorsFromScene();
            manager.Save();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Clear Setup:", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear Setup in Scene"))
        {
            manager.previewSystem.ClearExistingLocks();
            manager.previewSystem.ClearExistingCollectors();
        }

        if (GUILayout.Button("SAVE", GUILayout.Width(70), GUILayout.Height(30)))
        {
            manager.Save();
        }
        #endregion

        #region _scene kits
        GUILayout.Space(10);
        GUILayout.Label("TOOL", EditorStyles.foldoutHeader);
        if (GUILayout.Button("ACTIVE TOOL"))
        {
            manager.StartUpTool();
        }
        GUILayout.Label("Scene Interaction", EditorStyles.boldLabel);
        GUILayout.Label(SelectedItem ? $"Selected: {SelectedItem.name}" : "Click an item in Scene");
        #endregion
    }

    bool firstClicked = false;
    private void OnSceneGUI()
    {
        if (!manager.ToolActive) return;
        ShowToolToggles();
        if (debug) ShowLevelInfomations();
        if (SelectedItem != null)
        {
            ShowCollectorOptionsCheckBox();
        }

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            if (firstClicked) firstClicked = false;

            Ray worldRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);

            if (Physics.Raycast(worldRay, out RaycastHit hit, 1000f, manager.CollectorObjectLayerMask))
            {
                var item = hit.collider.GetComponent<CollectorMachanicObjectBase>();
                if (item != null && item != SelectedItem)
                {
                    firstClicked = true;
                    CollidedItem = null;
                    SelectedItem = item;
                    Selection.activeGameObject = item.gameObject;
                    SceneView.RepaintAll();
                    e.Use(); // chặn click này không bị SceneView dùng
                }
            }
        }

        if (firstClicked) return;

        bool endAction = e.type == EventType.MouseUp && e.button == 0 && !e.alt;
        //Debug.Log("Reset: " +  reset);

        if (SelectedItem != null && !endAction)
        {
            Handles.color = Color.cyan;
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(SelectedItem.transform.position, SelectedItem.transform.rotation);

            Undo.RecordObject(SelectedItem.transform, "Move Item");
            SelectedItem.transform.position = newPos;

            CheckCollisions(SelectedItem);

            Handles.color = Color.yellow;
            Handles.DrawWireDisc(SelectedItem.transform.position, Vector3.up, manager.CollisionRadius);
        }
        else if (endAction)
        {
            bool hasSelectedItem = SelectedItem != null;
            bool hasCollidedItem = CollidedItem != null;

            ColorPixelsCollectorObject obj1 = null;
            ColorPixelsCollectorObject obj2 = null;
            if (SelectedItem != null && SelectedItem is ColorPixelsCollectorObject) obj1 = SelectedItem as ColorPixelsCollectorObject;
            if (CollidedItem != null && CollidedItem is ColorPixelsCollectorObject) obj2 = CollidedItem as ColorPixelsCollectorObject;

            if (!firstClicked)
            {
                switch (selectedMode)
                {
                    case 0: if(hasSelectedItem && hasCollidedItem) manager.MoveModule.Move(SelectedItem, CollidedItem); break;
                    case 1: if (hasSelectedItem && hasCollidedItem) manager.SwapModule.Swap(SelectedItem, CollidedItem); break;
                    case 2: if (hasSelectedItem && hasCollidedItem) manager.CombineModule.Combine(obj1, obj2); break;
                    case 3: if (hasSelectedItem) manager.SplitModule.Split(obj1); break;
                    case 4: if (hasSelectedItem && hasCollidedItem) manager.ConnectModule.Connect(obj1, obj2); break;
                }
                Selection.activeGameObject = null;
                SceneView.RepaintAll();
                SelectedItem = null;
                CollidedItem = null;
                manager.ReCountCollectors();
            }
            firstClicked = false;
            manager.ReApplyCollectorsPosition();
        }
    }

    private void CheckCollisions(CollectorMachanicObjectBase current)
    {
        foreach (var other in manager.previewSystem.CurrentCollectors)
        {
            if (other == null || other == current) continue;
            float distance = Vector3.Distance(current.transform.position, other.transform.position);
            float minDist = manager.CollisionRadius * 2;

            if (distance < minDist)
            {
                CollidedItem = other;
                Handles.color = Color.red;
                Handles.DrawLine(current.transform.position, other.transform.position);
                Debug.Log($"Collision between {current.name} and {other.name}");
            }
        }
    }

    private void ShowToolToggles()
    {
        Handles.BeginGUI();

        float width = 400f;
        float height = 50f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 20f;

        float xRight = SceneView.currentDrawingSceneView.position.width - width;
        float xLeft = xRight - width - 50f;  // thêm area bên trái và cách 50px

        GUILayout.BeginArea(new Rect(xLeft, y, width, height), GUI.skin.box);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("< UNDO", GUILayout.Height(30)))
        {
            manager.CurentCollectorsConfig.RestoreBackup();
            manager.LoadConfigAsset(manager.CurentCollectorsConfig);
        }
        if (GUILayout.Button("REDO >", GUILayout.Height(30)))
        {
            manager.CurentCollectorsConfig.RestoreBackupForward();
            manager.LoadConfigAsset(manager.CurentCollectorsConfig);
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(xRight, y, width, height), GUI.skin.box);
        GUILayout.BeginHorizontal();

        Color color = GUI.backgroundColor;
        GUI.backgroundColor = Color.yellow;
        if (GUILayout.Button("CHECK", GUILayout.Width(55), GUILayout.Height(30)))
        {
            debug = !debug;
        }
        GUI.backgroundColor = color;

        if (GUILayout.Button("Add Lock", GUILayout.Width(70), GUILayout.Height(30)))
        {
            manager.previewSystem.CreateLockObject();
            manager.ImportCollectorsFromScene();
            manager.Save();
        }

        for (int i = 0; i < labels.Length; i++)
        {
            bool newValue = GUILayout.Toggle(selectedMode == i, labels[i], "Button", GUILayout.Height(30));
            if (newValue && selectedMode != i)
            {
                selectedMode = i;
                Debug.Log($"Selected Mode: {labels[i]}");
                SceneView.RepaintAll();
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        Handles.EndGUI();
    }

    private void ShowCollectorOptionsCheckBox()
    {
        if (SelectedItem == null || SelectedItem is not ColorPixelsCollectorObject) return;

        ColorPixelsCollectorObject selectedCollector = SelectedItem as ColorPixelsCollectorObject;

        float width = 300f;
        float height = 50f;
        float x = (SceneView.currentDrawingSceneView.position.width - width);  // right align
        float y = SceneView.currentDrawingSceneView.position.height - height - 90f;

        Handles.BeginGUI();

        Color oldColor = GUI.backgroundColor;
        //GUI.backgroundColor = new Color(0.3f, 0.7f, 1f, 1f);
        GUI.backgroundColor = manager.previewSystem.PrefabSource.ColorPallete.GetColorByCode(selectedCollector.CollectorColor);
        GUILayout.BeginArea(new Rect(x, y, width, height), GUI.skin.box);
        GUILayout.Label($"{SelectedItem.name} - {selectedCollector.CollectorColor} - {selectedCollector.BulletCapacity} Bullets", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal(GUI.skin.box);
        GUILayout.Label("Options", EditorStyles.boldLabel);

        // --- LOCK TOGGLE ---
        //bool newLockState = GUILayout.Toggle(selectedCollector.IsLocked, checkBoxlabels[0]);
        //if (newLockState != selectedCollector.IsLocked)
        //{
        //    Undo.RecordObject(SelectedItem, "Toggle Lock");
        //    selectedCollector.IsLocked = newLockState;
        //    checkboxes[0] = newLockState;
        //    selectedCollector.ApplyLockedState();
        //    manager.ImportCollectorsFromScene();
        //    EditorUtility.SetDirty(SelectedItem);
        //}

        // --- HIDE TOGGLE ---
        bool newHideState = GUILayout.Toggle(selectedCollector.IsHidden, checkBoxlabels[1]);
        if (newHideState != selectedCollector.IsHidden)
        {
            Undo.RecordObject(SelectedItem, "Toggle Hide");
            selectedCollector.IsHidden = newHideState;
            checkboxes[0] = newHideState;
            selectedCollector.ApplyHiddenState();
            manager.ImportCollectorsFromScene();
            manager.Save();
            EditorUtility.SetDirty(SelectedItem);
        }


        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal(GUI.skin.box);
        GUILayout.Label("Options", EditorStyles.boldLabel);

        bool newState = GUILayout.Toggle(checkboxes[0], checkBoxlabels[0], "Button", GUILayout.Width(80));
        checkboxes[0] = selectedCollector.IsLocked;

        GUILayout.EndHorizontal();

        GUILayout.EndArea();

        GUI.backgroundColor = oldColor;
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
        string levelName = manager.paintingConfig != null ? manager.paintingConfig.name : "No Painting Config Assigned";
        GUILayout.Label(levelName, EditorStyles.boldLabel);
        GUILayout.Space(10);
        GUILayout.Label("=> TOTAL BLOCKS: " + manager.paintingConfig.GetAllWorkingPixels().Count, EditorStyles.boldLabel);
        GUILayout.Space(5);
        manager.CountGunnersAsSet();
        manager.ReCountCollectors();
        foreach (var colorSet in manager.colorSetCounters)
        {
            Color _c = Color.white;
            try
            {
                _c = manager.previewSystem.PrefabSource.ColorPallete.GetColorByCode(colorSet.Key);
            }
            catch { }
            GUI.color = _c;
            GUILayout.Label( $"         {colorSet.Key} - {colorSet.Value} blocks", EditorStyles.boldLabel);
        }

        GUI.color = Color.green;

        GUILayout.Space(10);
        GUILayout.Label("=> TOTAL GUNNERS: " + manager.previewSystem.CurrentCollectors.Count, EditorStyles.boldLabel);
        GUILayout.Space(5);
        foreach (var colorSet in manager.collectorSetCounters)
        {
            Color _c = Color.white;
            try
            {
                _c = manager.previewSystem.PrefabSource.ColorPallete.GetColorByCode(colorSet.Key);
            }
            catch { }
            GUI.color = _c;
            GUILayout.Label($"         {colorSet.Key} - {colorSet.Value} gunners: {manager.previewSystem.GetBulletsByColor(colorSet.Key)} ", EditorStyles.boldLabel);
        }
        GUI.backgroundColor = oldBgColor;

        GUI.color = Color.green;
        GUILayout.Space(10);
        GUILayout.Label($"=> BLOCKS / BULLETS:  {manager.NumberOfWorkingPixels} / {manager.TotalBulletsCount}", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        manager.ReCountCollectors();
        GUILayout.Label($"=> KEYS / LOCKS:  {manager.paintingConfig.KeySetups.Count} / {manager.NumberOfLockedCollector}", EditorStyles.boldLabel);
        GUI.color = oldColor;

        GUILayout.EndArea();
    }
}