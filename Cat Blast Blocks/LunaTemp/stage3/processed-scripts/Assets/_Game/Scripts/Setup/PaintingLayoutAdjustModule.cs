using static PaintingSharedAttributes;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class PaintingLayoutAdjustModule : MonoBehaviour
{
    public PaintingConfigSetup PaintingSetup;

    public string ColorCode;
    public List<PaintingPixelComponent> SelectedPixels;

    [Header("CREATE BLOCK")]
    public LayerMask GridLayerMask;

    public bool IsPaintingToolActive = false;
    public bool IsDeleteToolActive = false;

    [ContextMenu("Set Color Selected Pixels")]
    public void SetColor()
    {
        if (PaintingSetup == null || PaintingSetup.CurrentPaintingConfig == null || PaintingSetup.PrefabSource.ColorPallete == null) return;
        if (SelectedPixels.Count > 0)
        {
            foreach (PaintingPixelComponent _pixel in SelectedPixels)
            {
                var respectedPixel = PaintingSetup.CurrentPaintingConfig.AdditionPixels
                    .Find(p => p.row == _pixel.PixelData.row && p.column == _pixel.PixelData.column);
                if (respectedPixel != null)
                {
                    respectedPixel.Hidden = false;
                    respectedPixel.color = PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(ColorCode);
                    respectedPixel.colorCode = ColorCode;
                }
                _pixel.PixelData.colorCode = ColorCode;
                _pixel.SetColor(PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(ColorCode));
            }

            foreach (PaintingPixelComponent _pixel in SelectedPixels)
            {
                var respectedPixel = PaintingSetup.CurrentPaintingConfig.Pixels
                    .Find(p => p.row == _pixel.PixelData.row && p.column == _pixel.PixelData.column);
                if (respectedPixel != null)
                {
                    respectedPixel.Hidden = false;
                    respectedPixel.color = PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(ColorCode);
                    respectedPixel.colorCode = ColorCode;
                }
                _pixel.PixelData.colorCode = ColorCode;
                _pixel.SetColor(PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(ColorCode));
            }
        }
    }

    [ContextMenu("Set Hide Selected Pixels")]
    public void SetHideSelected()
    {
        if (PaintingSetup == null || PaintingSetup.CurrentPaintingConfig == null || PaintingSetup.PrefabSource.ColorPallete == null) return;
        if (SelectedPixels.Count > 0)
        {
            Color _t = Color.white;
            _t.a = 0;
            foreach (PaintingPixelComponent _pixel in SelectedPixels)
            {
                var respectedPixel = PaintingSetup.CurrentPaintingConfig.AdditionPixels
                    .Find(p => p.row == _pixel.PixelData.row && p.column == _pixel.PixelData.column);
                if (respectedPixel != null)
                {
                    respectedPixel.color = _t;
                    respectedPixel.Hidden = true;
                    respectedPixel.colorCode = TransparentColorKey;
                }
                _pixel.PixelData.colorCode = TransparentColorKey;
                _pixel.SetColor(_t);
            }

            foreach (PaintingPixelComponent _pixel in SelectedPixels)
            {
                var respectedPixel = PaintingSetup.CurrentPaintingConfig.Pixels
                    .Find(p => p.row == _pixel.PixelData.row && p.column == _pixel.PixelData.column);
                if (respectedPixel != null)
                {
                    respectedPixel.color = _t;
                    respectedPixel.Hidden = true;
                    respectedPixel.colorCode = TransparentColorKey;
                }
                _pixel.PixelData.colorCode = TransparentColorKey;
                _pixel.SetColor(_t);
            }
        }
    }

    [ContextMenu("Add Line Above")]
    public void AddLineAbove()
    {
        if (PaintingSetup == null || PaintingSetup.CurrentPaintingConfig == null) return;
        (int lowestRow, int highestRow, int leftColumn, int rightColumn) = GetBounds();

        List<PaintingPixelConfig> newLineAbove = new List<PaintingPixelConfig>();
        int smallestColumn = 0;
        int biggestColumn = 0;
        int colCount = (int)PaintingSetup.CurrentPaintingConfig.PaintingSize.y;

        if (colCount % 2 == 0)
        {
            smallestColumn = -colCount / 2;
            biggestColumn = colCount / 2 - 1;
        }
        else
        {
            smallestColumn = -(colCount - 1) / 2;
            biggestColumn = (colCount - 1) / 2;
        }

        for (int i = smallestColumn; i <= biggestColumn; i++)
        {
            PaintingPixelConfig newPixel = new PaintingPixelConfig();
            newPixel.row = highestRow + 1;
            newPixel.column = i;
            newPixel.color = Color.white;
            newPixel.colorCode = DefaultColorKey;
            newLineAbove.Add(newPixel);
        }

        PaintingSetup.CurrentPaintingConfig.AdditionPixels.AddRange(newLineAbove);
        PaintingSetup.CurrentGridObject.ApplyPaintingConfig(PaintingSetup.CurrentPaintingConfig);
    }

    [ContextMenu("Add Line Below")]
    public void AddLineBelow()
    {
        if (PaintingSetup == null || PaintingSetup.CurrentPaintingConfig == null) return;
        (int lowestRow, int highestrow, int leftColumn, int rightColumn) = GetBounds();

        List<PaintingPixelConfig> newLineBelow = new List<PaintingPixelConfig>();
        int smalestColumn = 0;
        int biggestColumn = 0;
        int colCount = (int) PaintingSetup.CurrentPaintingConfig.PaintingSize.y;
        if (colCount % 2 == 0)
        {
            smalestColumn = -colCount / 2;
            biggestColumn = colCount / 2 - 1;
        }
        else
        {
            smalestColumn = -(colCount - 1) / 2;
            biggestColumn = (colCount - 1) / 2;
        }
        for (int i = smalestColumn; i <= biggestColumn; i++)
        {
            PaintingPixelConfig newPixel = new PaintingPixelConfig();
            newPixel.row = lowestRow - 1;
            newPixel.column = i;
            newPixel.color = Color.white;
            newPixel.colorCode = DefaultColorKey;
            newLineBelow.Add(newPixel);
        }

        PaintingSetup.CurrentPaintingConfig.AdditionPixels.AddRange(newLineBelow);
        PaintingSetup.CurrentGridObject.ApplyPaintingConfig(PaintingSetup.CurrentPaintingConfig);
    }

    [ContextMenu("Add Line Left")]
    public void AddLineLeft()
    {
        if (PaintingSetup == null || PaintingSetup.CurrentPaintingConfig == null) return;
        (int lowestRow, int highestRow, int leftColumn, int rightColumn) = GetBounds();

        List<PaintingPixelConfig> newLineLeft = new List<PaintingPixelConfig>();
        int smallestRow = 0;
        int biggestRow = 0;
        int rowCount = (int)PaintingSetup.CurrentPaintingConfig.PaintingSize.x;

        if (rowCount % 2 == 0)
        {
            smallestRow = -rowCount / 2;
            biggestRow = rowCount / 2 - 1;
        }
        else
        {
            smallestRow = -(rowCount - 1) / 2;
            biggestRow = (rowCount - 1) / 2;
        }

        for (int i = smallestRow; i <= biggestRow; i++)
        {
            PaintingPixelConfig newPixel = new PaintingPixelConfig();
            newPixel.row = i;
            newPixel.column = leftColumn - 1;
            newPixel.color = Color.white;
            newPixel.colorCode = DefaultColorKey;
            newLineLeft.Add(newPixel);
        }

        PaintingSetup.CurrentPaintingConfig.AdditionPixels.AddRange(newLineLeft);
        PaintingSetup.CurrentGridObject.ApplyPaintingConfig(PaintingSetup.CurrentPaintingConfig);
    }

    [ContextMenu("Add Line Right")]
    public void AddLineRight()
    {
        if (PaintingSetup == null || PaintingSetup.CurrentPaintingConfig == null) return;
        (int lowestRow, int highestRow, int leftColumn, int rightColumn) = GetBounds();

        List<PaintingPixelConfig> newLineRight = new List<PaintingPixelConfig>();
        int smallestRow = 0;
        int biggestRow = 0;
        int rowCount = (int)PaintingSetup.CurrentPaintingConfig.PaintingSize.x;

        if (rowCount % 2 == 0)
        {
            smallestRow = -rowCount / 2;
            biggestRow = rowCount / 2 - 1;
        }
        else
        {
            smallestRow = -(rowCount - 1) / 2;
            biggestRow = (rowCount - 1) / 2;
        }

        for (int i = smallestRow; i <= biggestRow; i++)
        {
            PaintingPixelConfig newPixel = new PaintingPixelConfig();
            newPixel.row = i;
            newPixel.column = rightColumn + 1;
            newPixel.color = Color.white;
            newPixel.colorCode = DefaultColorKey;
            newLineRight.Add(newPixel);
        }

        PaintingSetup.CurrentPaintingConfig.AdditionPixels.AddRange(newLineRight);
        PaintingSetup.CurrentGridObject.ApplyPaintingConfig(PaintingSetup.CurrentPaintingConfig);
    }

    public void AddPixel([Bridge.Ref] Vector3 _pos, bool save = true)
    {
        var respectedPixel = PaintingSetup.CurrentGridObject.GetPixelBasedOnPosition(_pos);
        if (respectedPixel != null)
        {
            PaintingPixelConfig newPixel = new PaintingPixelConfig(respectedPixel.PixelData);
            newPixel.Hidden = false;
            Color _c = Color.white;
            try
            {
                _c = PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(ColorCode);
            }
            catch { }

            newPixel.color = _c;
            newPixel.Hidden = false;
            newPixel.colorCode = ColorCode;

            respectedPixel.gameObject.SetActive(true);
            respectedPixel.SetColor(_c);
            respectedPixel.ShowVisualOnly();

            var pixelConfig = PaintingSetup.CurrentPaintingConfig.Pixels
                .Find(p => p.row == newPixel.row && p.column == newPixel.column);
            if (pixelConfig != null)
            {
                PaintingSetup.CurrentPaintingConfig.Pixels.Remove(pixelConfig);
            }
            PaintingSetup.CurrentPaintingConfig.Pixels.Add(newPixel);

            PaintingSetup.CurrentGridObject.ApplyPaintingConfig(PaintingSetup.CurrentPaintingConfig);
        }
    }

    public void AddPixel([Bridge.Ref] Vector3 _pos, string _colorCode, bool save = true)
    {
        var respectedPixel = PaintingSetup.CurrentGridObject.GetPixelBasedOnPositionNewNeeded(_pos, PaintingSetup.CurrentPaintingConfig, addToAdditional: true);
        if (respectedPixel != null)
        {
            PaintingPixelConfig newPixel = new PaintingPixelConfig(respectedPixel.PixelData);
            newPixel.Hidden = false;
            Color _c = Color.white;
            try
            {
                _c = PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(_colorCode);
            }
            catch { }

            newPixel.color = _c;
            newPixel.Hidden = false;
            newPixel.colorCode = _colorCode;

            respectedPixel.gameObject.SetActive(true);
            respectedPixel.SetColor(_c);
            respectedPixel.ShowVisualOnly();

            var pixelConfig = PaintingSetup.CurrentPaintingConfig.Pixels
                .Find(p => p.row == newPixel.row && p.column == newPixel.column);
            if (pixelConfig != null)
            {
                PaintingSetup.CurrentPaintingConfig.Pixels.Remove(pixelConfig);
                PaintingSetup.CurrentPaintingConfig.Pixels.Add(newPixel);
            }
            else
            {
                var additionPixelConfig = PaintingSetup.CurrentPaintingConfig.AdditionPixels
                    .Find(p => p.row == newPixel.row && p.column == newPixel.column);
                if (additionPixelConfig != null)
                {
                    PaintingSetup.CurrentPaintingConfig.AdditionPixels.Remove(additionPixelConfig);
                }
                PaintingSetup.CurrentPaintingConfig.AdditionPixels.Add(newPixel);
            }

            PaintingSetup.CurrentGridObject.ApplyPaintingConfig(PaintingSetup.CurrentPaintingConfig);

            if (save) Save();
        }
    }

    public void HidePixel([Bridge.Ref] Vector3 _pos)
    {
        var respectedPixel = PaintingSetup.CurrentGridObject.GetPixelBasedOnPosition(_pos);
        if (respectedPixel != null)
        {
            PaintingPixelConfig newPixel = new PaintingPixelConfig(respectedPixel.PixelData);
            Color _c = Color.white;

            var respectedPixelConfig = PaintingSetup.CurrentPaintingConfig.AdditionPixels
                   .Find(p => p.row == respectedPixel.PixelData.row && p.column == respectedPixel.PixelData.column);
            if (respectedPixelConfig == null)
            {
                respectedPixelConfig = PaintingSetup.CurrentPaintingConfig.Pixels
                   .Find(p => p.row == respectedPixel.PixelData.row && p.column == respectedPixel.PixelData.column);
            }
            if (respectedPixelConfig != null)
            {
                respectedPixelConfig.color = _c;
                respectedPixelConfig.Hidden = true;
                respectedPixelConfig.colorCode = TransparentColorKey;
            }
            respectedPixel.HideVisualOnly();
            respectedPixel.PixelData.colorCode = TransparentColorKey;
            respectedPixel.SetColor(_c);

            Save();
        }
    }

    public void SetActivePaintingTool()
    {
        IsPaintingToolActive = !IsPaintingToolActive;
        if (IsPaintingToolActive) IsDeleteToolActive = false;
    }

    public void SetActiveDeleteTool()
    {
        IsDeleteToolActive = !IsDeleteToolActive;
        if (IsDeleteToolActive) IsPaintingToolActive = false;
    }

    private (int lowestRow, int highestrow, int leftColumn, int rightColumn) GetBounds()
    {
        if (PaintingSetup.CurrentPaintingConfig == null) return (0, 0, 0, 0);
        int lowestRow = int.MaxValue;
        int highestRow = int.MinValue;
        int leftColumn = int.MaxValue;
        int rightColumn = int.MinValue;
        foreach (var pixel in PaintingSetup.CurrentPaintingConfig.Pixels)
        {
            if (pixel.row < lowestRow) lowestRow = pixel.row;
            if (pixel.row > highestRow) highestRow = pixel.row;
            if (pixel.column < leftColumn) leftColumn = pixel.column;
            if (pixel.column > rightColumn) rightColumn = pixel.column;
        }
        return (lowestRow, highestRow, leftColumn, rightColumn);
    }

    public Color GetCurrentColor()
    {
        Color _c = Color.white;
        try
        {
            _c = PaintingSetup.PrefabSource.ColorPallete.GetColorByCode(ColorCode);
        }
        catch { }
        return _c;
    }

    public void Save()
    {
        #if UNITY_EDITOR
        Undo.RecordObject(PaintingSetup.CurrentPaintingConfig, "Change pixel configs");
        #endif
        PaintingSetup.Save();
    }
}
