using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WallObject : MonoBehaviour
{
    [Header("Wall Structure")]
    public Transform WallTransform;                    // Head transform
    public List<PaintingPixel> PaintingPixelsCovered;
    public string ColorCode;

    [Header("STAT(s)")]
    public int Hearts;
    public int HeartsLoss; // implicit 0
    public int RemainingHearts; // implicit 0

    [Header("VISUAL")]
    public Renderer WallRenderer;
    public TMP_Text HeartsText;

    public bool Destroyed; // implicit false
    public Vector3 WorldPos;


    // Initialize this wall: compute hearts & assign material from first pixel only.
    public void Initialize(List<PaintingPixel> pipePixels, int heart, [Bridge.Ref] Color color, string colorCode)
    {
        Destroyed = false;
        PaintingPixelsCovered = pipePixels != null ? pipePixels : new List<PaintingPixel>();
        Hearts = 0;
        if (heart > 0) Hearts = heart; else foreach (var pixel in PaintingPixelsCovered) Hearts += pixel.Hearts;
        HeartsLoss = 0; RemainingHearts = Hearts;
        HeartsText.text = RemainingHearts.ToString();
        ColorCode = colorCode;
        AssignPaletteSharedMaterial();
    }

    public void OnAPixelDestroyed()
    {
        if (Destroyed) return;
        HeartsLoss++;
        RemainingHearts--;
        RemainingHearts = Mathf.Clamp(RemainingHearts, 0, int.MaxValue);
        HeartsText.enabled = RemainingHearts > 0;
        HeartsText.text = RemainingHearts.ToString();
        if (RemainingHearts <= 0)
        {
            foreach (var pixel in PaintingPixelsCovered) pixel.DestroyPixel(invokeEvent: false);
            OnDestroyed();
            Destroyed = true;
            return;
        }
        foreach (var pixel in PaintingPixelsCovered) pixel.destroyed = false;
    }

    public void OnDestroyed() { WallTransform.DOScale(0, 0.25f); }

    public void SelfDestroy()
    {
        HeartsText.enabled = false;
        foreach (var pixel in PaintingPixelsCovered) pixel.DestroyPixel(invokeEvent: false);
        Destroyed = true; OnDestroyed();
    }

    public void SelfDestroyGameObject()
    { if (Application.isPlaying) GameObject.Destroy(gameObject); else GameObject.DestroyImmediate(gameObject); }

    public void ApplyPosition() { WallTransform.position = WorldPos; }

    // Only assign shared palette material from first pixel (if any)
    private void AssignPaletteSharedMaterial()
    {
        if (WallRenderer == null) return;
        if (PaintingPixelsCovered == null || PaintingPixelsCovered.Count == 0) return;
        var mat = PaintingPixelsCovered[0]?.Material;
        if (mat != null) WallRenderer.sharedMaterial = mat;
    }

    public void RefreshPaletteMaterial() { AssignPaletteSharedMaterial(); }
}