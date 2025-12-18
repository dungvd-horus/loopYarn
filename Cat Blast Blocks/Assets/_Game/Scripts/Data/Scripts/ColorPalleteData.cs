using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects/ColorPallete", menuName = "ScriptableObjects/ColorPallete")]
public class ColorPalleteData : ScriptableObject
{
    // Dữ liệu màu sắc
    public Dictionary<string, Color>    colorPallete = new Dictionary<string, Color>(System.StringComparer.OrdinalIgnoreCase);
    public List<string>                 ColorKeys    = new List<string>();
    public List<Color>                  ColorsValues = new List<Color>();

    // Dữ liệu material tương ứng
    public List<Material>               MatValues    = new List<Material>();
    public Dictionary<string, Material> MatPallete   = new Dictionary<string, Material>(System.StringComparer.OrdinalIgnoreCase);

    public Color GetColorByCode(string code)
    {
        code = string.IsNullOrEmpty(code) ? code : code.Trim();
        if (!string.IsNullOrEmpty(code) && colorPallete.TryGetValue(code, out var colorByCode))
            return colorByCode;

        Debug.LogWarning($"Color not found: {code}");
        return Color.white;
    }

    // Hàm mới để lấy Material bằng mã màu
    public Material GetMaterialByCode(string code)
    {
        code = string.IsNullOrEmpty(code) ? code : code.Trim();
        if (!string.IsNullOrEmpty(code) && MatPallete.TryGetValue(code, out var materialByCode))
            return materialByCode;
        
        Debug.LogWarning($"Material not found for code: {code}");
        return null;
    }

    public string FindKeyByColor(Color colorToFind)
    {
        for (int i = 0; i < ColorsValues.Count; i++)
            if (ColorsValues[i] == colorToFind)
                return ColorKeys[i];
        return null;
    }

    [ContextMenu("SetupColor")]
    public void SetupColor()
    {
        colorPallete.Clear();
        for (int i = 0; i < Mathf.Min(ColorKeys.Count, ColorsValues.Count); i++)
        {
            if (!string.IsNullOrEmpty(ColorKeys[i]))
                colorPallete[ColorKeys[i].Trim()] = ColorsValues[i];
        }
        Debug.Log($"{colorPallete.Count} colors added to the palette.");
    }

    // Hàm mới để đồng bộ List và Dictionary cho Material
    [ContextMenu("SetupMaterials")]
    public void SetupMaterials()
    {
        MatPallete.Clear();
        for (int i = 0; i < Mathf.Min(ColorKeys.Count, MatValues.Count); i++)
        {
            if (!string.IsNullOrEmpty(ColorKeys[i]) && MatValues[i] != null)
            {
                MatPallete[ColorKeys[i].Trim()] = MatValues[i];
            }
        }
        Debug.Log($"{MatPallete.Count} materials added to the palette dictionary.");
    }


    private void OnValidate()
    {
        // Đảm bảo cả hai dictionary luôn được đồng bộ khi có thay đổi trong Inspector
        SetupColor();
        SetupMaterials();
    }

    private void OnEnable()
    {
        // Khi asset được load, đảm bảo các dictionary được khởi tạo
        if (colorPallete == null || colorPallete.Count == 0)
            SetupColor();
        if (MatPallete == null || MatPallete.Count == 0)
            SetupMaterials();
    }
}