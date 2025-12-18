using static PaintingSharedAttributes;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


public class LevelConfigSetup : MonoBehaviour
{
    #region PROPERTIES
    public bool EDITOR = false;

    [Space]
    [Header("INPUT(s)")]
    public Sprite NewTargetPainting;                    // Sprite mới để tạo level (dùng trong editor)
    public List<string> ColorCodesUsed = new List<string>(); // Danh sách mã màu được sử dụng
    public PaintingGridObject CurrentGridObject;        // Grid object hiện tại của level
    public List<PaintingGridObject> CurrentGridObjects; // Danh sách các grid objects có thể dùng

    [Header("LEVEL DATA(s)")]
    public LevelConfig CurrentLevel;                    // Cấu hình level hiện tại
    public PaintingConfig CurrentLevelPaintingConfig;   // Cấu hình painting của level
    public LevelColorCollectorsConfig CurrentLevelCollectorConfig; // Cấu hình collectors của level
    public Sprite CurrentLevelPainting;                 // Sprite của level hiện tại
    public List<string> CurrentLevelColorCodes = new List<string>(); // Danh sách mã màu của level

    [Header("CONTROLLER(s)")]
    public PaintingConfigSetup PaintingSetup;           // Controller để thiết lập painting
    public PipeObjectConfigSetup PipeObjectSetup;       // Controller để thiết lập pipe objects
    public BigBlockObjectConfigSetup WallObjectSetup;   // Controller để thiết lập wall objects
    public KeyObjectConfigSetup KeyObjectSetup;         // Controller để thiết lập key objects

    [Space]
    public LevelCollectorsSystem LevelCollectorsManager; // Quản lý hệ thống collectors
    public LevelCollectorsConfigSetup LevelCollectorsSetup; // Thiết lập cấu hình collectors
    #endregion

    #region UNITY CORE
    [ContextMenu("Refresh")]
    private void OnValidate()
    {
        // Hàm này chạy trong editor khi có thay đổi
        // Nếu đang chạy game thì không làm gì
        if (Application.isPlaying) return;
        // Thiết lập lại các components
        SetUpComponents();
    }

    private void Start()
    {
        // Nếu chế độ EDITOR được bật thì load level
        if (EDITOR) LoadLevel();
    }
    #endregion

    public void LoadLevel(LevelConfig levelConfig)
    {
        // Gán cấu hình level mới
        CurrentLevel = levelConfig;
        // Thiết lập các components cần thiết cho level
        SetUpComponents();
        // Gọi phương thức load level không tham số
        LoadLevel();
    }

    private static readonly List<string> tempColorList = new List<string>(); // Tái sử dụng danh sách tạm thời


    #region MAIN
    [ContextMenu("LOAD LEVEL")]
    public void LoadLevel()
    {
        ClearLevel();
        // Kiểm tra nếu không có level nào được gán thì không làm gì
        if (CurrentLevel == null) return;
        //Chọn grid object phù hợp với kích thước level
        SelectGridObject();

        // Thiết lập collectors và cơ chế hoạt động
        LevelCollectorsManager.SetupCollectorsAndMechanic();
        // Khởi tạo level với cấu hình painting
        CurrentGridObject.InitializeLevel(CurrentLevel.BlocksPaintingConfig);
    }

    [ContextMenu("CLEAR LEVEL")]
    public void ClearLevel()
    {
        // Xóa các khóa hiện có
        LevelCollectorsManager.ClearExistingLocks();
        // Xóa các collector hiện có
        LevelCollectorsManager.ClearExistingCollectors();
        // Xóa tất cả các key trên grid
        CurrentGridObject.ClearAllKeys();
        // Xóa tất cả các pipe trên grid
        CurrentGridObject.ClearAllPipes();
        // Xóa tất cả các wall trên grid
        CurrentGridObject.ClearAllWalls();
        // Đặt toàn bộ grid về màu trắng
        CurrentGridObject.ClearToWhite();
    }

    /// <summary>
    /// Thiết lập các components cần thiết cho level
    /// </summary>
    public void SetUpComponents()
    {
        if (CurrentLevel == null)
        {
            // Nếu không có level nào được gán thì xóa các cấu hình hiện tại
            CurrentLevelPaintingConfig = null;
            CurrentLevelCollectorConfig = null;
            CurrentLevelPainting = null;
            return;
        }

        // Tối ưu: Sử dụng danh sách tạm để tránh tạo object mới nhiều lần
        tempColorList.Clear();
        tempColorList.AddRange(CurrentLevel.ColorsUsed);
        CurrentLevelColorCodes.Clear();
        CurrentLevelColorCodes.AddRange(tempColorList);
        // Gán danh sách mã màu cho grid object hiện tại
        CurrentGridObject.CurrentLevelColor = CurrentLevelColorCodes;
        // Gán cấu hình collector cho level hiện tại
        CurrentLevelCollectorConfig = CurrentLevel.CollectorsConfig;
        // Gán cấu hình painting cho level hiện tại
        CurrentLevelPaintingConfig = CurrentLevel.BlocksPaintingConfig;

        // Nếu có cấu hình painting thì gán sprite cho level hiện tại
        if (CurrentLevelPaintingConfig != null) CurrentLevelPainting = CurrentLevelPaintingConfig.Sprite;

        // Thiết lập painting nếu tồn tại
        if (PaintingSetup)
        {
            PaintingSetup.CurrentGridObject = CurrentGridObject;
            PaintingSetup.CurrentPaintingConfig = CurrentLevelPaintingConfig;
        }

        // Thiết lập pipe object nếu tồn tại
        if (PipeObjectSetup)
        {
            PipeObjectSetup.CurrentLevelObjectSetups = CurrentLevelPaintingConfig.PipeSetups;
        }

        // Thiết lập wall object nếu tồn tại
        if (WallObjectSetup)
        {
            WallObjectSetup.CurrentLevelWallObjectSetups = CurrentLevelPaintingConfig.WallSetups;
        }

        // Thiết lập level collectors manager nếu tồn tại
        if (LevelCollectorsManager)
        {
            LevelCollectorsManager.CurrentLevelCollectorsConfig = CurrentLevelCollectorConfig;
        }

        // Thiết lập level collectors config nếu tồn tại
        if (LevelCollectorsSetup)
        {
            LevelCollectorsSetup.CurentCollectorsConfig = CurrentLevelCollectorConfig;
            LevelCollectorsSetup.paintingConfig = CurrentLevelPaintingConfig;
        }

        // Thiết lập key object nếu tồn tại
        if (KeyObjectSetup)
        {
            KeyObjectSetup.CurrentLevelKeyObjectSetups = CurrentLevelPaintingConfig.KeySetups;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("CREATE NEW LEVEL")]
    public void CreateNewLevel()
    {
        // Kiểm tra nếu không có sprite mới thì không làm gì
        if (NewTargetPainting == null) return;

        // Kiểm tra xem đã tồn tại cấu hình cho sprite này chưa
        var existingConfig = GetLevelConfig(NewTargetPainting);

        if (existingConfig != null)
        {
            // Nếu đã tồn tại thì sử dụng cấu hình đó
            CurrentLevel = existingConfig;
            // Tối ưu: Sử dụng danh sách tạm để tránh tạo object mới nhiều lần
            PaintingSetup.ColorCodeInUse.Clear();
            PaintingSetup.ColorCodeInUse.AddRange(existingConfig.ColorsUsed);
            PaintingSetup.TargetPainting = NewTargetPainting;
            PaintingSetup.ExtractColorCodesFromPainting(NewTargetPainting);
            SetUpComponents();
            return;
        }

        // Nếu chưa tồn tại thì tạo mới cấu hình
        PaintingSetup.ColorCodeInUse.Clear();
        PaintingSetup.ColorCodeInUse.AddRange(ColorCodesUsed);
        PaintingSetup.TargetPainting = NewTargetPainting;
        PaintingSetup.ExtractColorCodesFromPainting(NewTargetPainting);
        PaintingSetup.SamplePaintingToGrid(NewTargetPainting);

        // Tạo cấu hình collector mới
        string newCollectorConfigName = NewTargetPainting.name;
        var collectorConfig = LevelCollectorsSetup.CreateConfigAsset(newCollectorConfigName);

        // Tạo cấu hình level mới
        var newLvl = CreateConfigAsset(NewTargetPainting.name + "_LevelConfig", PaintingSetup.CurrentPaintingConfig, collectorConfig);
        // Tối ưu: Sử dụng danh sách tạm để tránh tạo object mới nhiều lần
        newLvl.ColorsUsed.Clear();
        newLvl.ColorsUsed.AddRange(PaintingSetup.ColorCodeInUse);
        EditorUtility.SetDirty(newLvl);
        CurrentLevel = newLvl;
        SetUpComponents();
    }

    public LevelConfig CreateConfigAsset(string configName, PaintingConfig paintingConfig, LevelColorCollectorsConfig collectorConfig)
    {
        if (string.IsNullOrEmpty(configName))
        {
            Debug.LogError("Config name cannot be empty!");
            return null;
        }

        // Đảm bảo đường dẫn kết thúc bằng dấu gạch chéo
        if (!LevelConfigPath.EndsWith("/"))
        {
            LevelConfigPath += "/";
        }

        string assetPath = LevelConfigPath + configName + ".asset";

        // Tạo asset mới với ScriptableObject
        LevelConfig newConfig = ScriptableObject.CreateInstance<LevelConfig>();
        newConfig.BlocksPaintingConfig = paintingConfig;
        newConfig.CollectorsConfig = collectorConfig;
        // Tối ưu: Sử dụng danh sách tạm để tránh tạo object mới nhiều lần
        newConfig.ColorsUsed.Clear();
        newConfig.ColorsUsed.AddRange(ColorCodesUsed);
        AssetDatabase.CreateAsset(newConfig, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Created new LevelColorCollectorsConfig asset at {assetPath}");
        return newConfig;
    }
#endif

    public void SelectGridObject()
    {
        // Tối ưu: Ẩn grid object hiện tại trước
        CurrentGridObject?.gameObject.SetActive(false);
        PaintingGridObject gridObject = null;

        // Tối ưu: Cache giá trị để tránh truy cập nhiều lần
        var targetSize = CurrentLevel.BlocksPaintingConfig.PaintingSize;

        // Duyệt qua danh sách grid objects để tìm kích thước phù hợp
        for (int i = 0; i < CurrentGridObjects.Count; i++)
        {
            var _grid = CurrentGridObjects[i];
            if (_grid.gridSize == targetSize)
            {
                gridObject = _grid;
                break;
            }
        }
        CurrentGridObject = gridObject;
        CurrentGridObject.gameObject.SetActive(true);
    }

    #endregion
}
