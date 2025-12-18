using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PipeObjectSetup
{
    public int Hearts = 0;
    public List<PaintingPixelConfig> PixelCovered; // Grid pixels that pipe lie on (sort from head to tail)
    public string ColorCode; // PipeColorCode
    public Vector3 Scale = Vector3.one;      // Scale applied to pipe parts (direct scale values)

    public PipeObjectSetup()
    {
        Hearts = 1;
        PixelCovered = new List<PaintingPixelConfig>();
        ColorCode = "";
        Scale = Vector3.one;
    }

    public PipeObjectSetup(List<PaintingPixelConfig> pixelCovered, string colorCode, Vector3 scale, int heart)
    {
        this.Hearts = heart;
        this.PixelCovered = pixelCovered != null ? new List<PaintingPixelConfig>(pixelCovered) : new List<PaintingPixelConfig>();
        this.ColorCode = colorCode;
        this.Scale = scale;
    }

    public PipeObjectSetup(PipeObjectSetup _stock)
    {
        this.Hearts = _stock.Hearts;
        this.PixelCovered = new List<PaintingPixelConfig>();
        foreach (var pixel in _stock.PixelCovered)
        {
            this.PixelCovered.Add(new PaintingPixelConfig(pixel));
        }
        this.ColorCode = _stock.ColorCode;
        this.Scale = _stock.Scale;
    }
}