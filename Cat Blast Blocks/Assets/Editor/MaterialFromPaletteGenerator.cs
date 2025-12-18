#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class MaterialFromPaletteGenerator : EditorWindow
{
    // Enum để định nghĩa các chế độ hoạt động của công cụ
    private enum GenerationMode
    {
        GenerateAndUpdate, // Tạo mới và cập nhật cả những cái đã có
        GenerateOnlyNew    // Chỉ tạo mới, bỏ qua những cái đã có
    }

    private ColorPalleteData colorPalette;
    private Shader targetShader;
    private string savePath = "Assets/GeneratedMaterials";

    private static readonly int ColorID = Shader.PropertyToID("_Color");

    [MenuItem("Tools/Material Palette Generator")]
    public static void ShowWindow()
    {
        GetWindow<MaterialFromPaletteGenerator>("Material Generator");
    }

    // Giao diện của cửa sổ editor
    private void OnGUI()
    {
        GUILayout.Label("Material From Palette Generator", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Các trường nhập liệu
        colorPalette = (ColorPalleteData)EditorGUILayout.ObjectField("Color Palette", colorPalette, typeof(ColorPalleteData), false);
        targetShader = (Shader)EditorGUILayout.ObjectField("Target Shader", targetShader, typeof(Shader), false);

        EditorGUILayout.Space();

        // Đường dẫn lưu file
        GUILayout.Label("Save Path", EditorStyles.label);
        EditorGUILayout.BeginHorizontal();
        savePath = EditorGUILayout.TextField(savePath);
        if (GUILayout.Button("Browse", GUILayout.Width(70)))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Choose a location to save materials", "Assets", "");
            if (!string.IsNullOrEmpty(selectedPath) && selectedPath.StartsWith(Application.dataPath))
            {
                savePath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        // --- NÚT 1: TẠO MỚI VÀ CẬP NHẬT ---
        EditorGUILayout.HelpBox("Tạo material mới cho các key chưa có, và CẬP NHẬT (ghi đè) những material đã tồn tại để khớp với màu trong palette.", MessageType.Info);
        if (GUILayout.Button("Generate and Update All Materials"))
        {
            ProcessMaterials(GenerationMode.GenerateAndUpdate);
        }

        EditorGUILayout.Space(15);

        // --- NÚT 2: CHỈ THÊM MỚI ---
        EditorGUILayout.HelpBox("Chỉ tạo material mới cho các key chưa có. Sẽ BỎ QUA và không thay đổi bất kỳ material nào đã tồn tại.", MessageType.Info);
        if (GUILayout.Button("Generate Only New Materials (Add Only)"))
        {
            ProcessMaterials(GenerationMode.GenerateOnlyNew);
        }
    }

    // Hàm xử lý chính, nhận vào chế độ hoạt động
    private void ProcessMaterials(GenerationMode mode)
    {
        if (colorPalette == null || targetShader == null || string.IsNullOrEmpty(savePath))
        {
            EditorUtility.DisplayDialog("Error", "Please assign Color Palette, Target Shader, and a valid Save Path.", "OK");
            return;
        }

        // Tạo thư mục nếu chưa tồn tại
        if (!AssetDatabase.IsValidFolder(savePath))
        {
            string[] folders = savePath.Split('/');
            string currentPath = folders[0];
            for (int i = 1; i < folders.Length; i++)
            {
                string parentPath = currentPath;
                currentPath = Path.Combine(currentPath, folders[i]);
                if (!AssetDatabase.IsValidFolder(currentPath))
                {
                    AssetDatabase.CreateFolder(parentPath, folders[i]);
                }
            }
            Debug.Log($"Created directory: {savePath}");
        }

        int createdCount = 0;
        int updatedCount = 0;
        int skippedCount = 0;

        colorPalette.SetupColor();

        // Chuẩn bị list MatValues
        colorPalette.MatValues.Clear();
        colorPalette.MatValues.AddRange(new Material[colorPalette.ColorKeys.Count]);

        // Duyệt qua list các key màu sắc để đảm bảo đúng thứ tự
        for (int i = 0; i < colorPalette.ColorKeys.Count; i++)
        {
            string colorKey = colorPalette.ColorKeys[i];
            if (string.IsNullOrEmpty(colorKey))
            {
                Debug.LogWarning($"Skipping empty color key at index {i}");
                continue;
            }

            Color colorValue = colorPalette.ColorsValues[i];
            string materialName = $"{colorKey}.mat";
            string fullPath = Path.Combine(savePath, materialName);

            Material materialAsset = AssetDatabase.LoadAssetAtPath<Material>(fullPath);

            if (materialAsset == null) // Nếu material chưa tồn tại -> Luôn tạo mới
            {
                materialAsset = new Material(targetShader);
                materialAsset.SetColor(ColorID, colorValue);
                AssetDatabase.CreateAsset(materialAsset, fullPath);
                createdCount++;
            }
            else // Nếu material đã tồn tại
            {
                // Dựa vào chế độ (mode) để quyết định hành động
                if (mode == GenerationMode.GenerateAndUpdate)
                {
                    // Chế độ CẬP NHẬT: Ghi đè thông tin
                    materialAsset.shader = targetShader;
                    materialAsset.SetColor(ColorID, colorValue);
                    EditorUtility.SetDirty(materialAsset);
                    updatedCount++;
                }
                else // Chế độ CHỈ THÊM MỚI (GenerateOnlyNew)
                {
                    // Bỏ qua, không làm gì cả
                    skippedCount++;
                }
            }

            // Dù là tạo mới, cập nhật hay bỏ qua, đều liên kết material (nếu có) vào list
            colorPalette.MatValues[i] = materialAsset;
        }

        EditorUtility.SetDirty(colorPalette);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        colorPalette.SetupMaterials();

        // Hiển thị thông báo kết quả chi tiết hơn
        string message = $"Process Complete!\n\n" +
                         $"Created: {createdCount} new materials\n" +
                         $"Updated: {updatedCount} existing materials\n" +
                         $"Skipped: {skippedCount} existing materials\n\n" +
                         $"All materials have been linked to the Color Palette asset.";

        EditorUtility.DisplayDialog("Success", message, "OK");

        EditorUtility.FocusProjectWindow();
        Object folder = AssetDatabase.LoadAssetAtPath(savePath, typeof(Object));
        if (folder != null)
        {
            EditorGUIUtility.PingObject(folder);
        }
    }
}
#endif