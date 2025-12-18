using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PipeObject : MonoBehaviour
{
    public PipePartVisualHandle VisualHandler;

    [Header("Pipe Structure")]
    public List<PaintingPixel> PaintingPixelsCovered;
    public int PixelDestroyed = 0;
    public TMP_Text HeartText;

    [Header("Pipe Properties")]
    public int Hearts;
    public string ColorCode;                      // Color code defining the pipe's color
    public bool IsHorizontal;                     // True if pipe is horizontal (in same row), false if vertical (in same column)

    [Header("OBJECT(s)")]
    public Transform PipeTransform;
    public Transform PipeBodyTransform;

    [Header("Default scale values")]
    public Vector3 PipeHeadDefaultScale = new Vector3(1, 1, 1);
    public Vector3 PipeBodyDefaultScale = new Vector3(1, 1, 1);

    public bool Destroyed = false;

    public int RemainingHearts = 0;
    public int HeartLoss = 0;
    private float scaleOriginalZ = 0f;
    private float heartLossFromLastPixelDestroyed = 0f;
    private float heartPerPixel = 0f;
    private float ScaleDownValuePerHeart = 0f;

    public Vector3 WorldPos;

    private void Awake()
    {
        PipeTransform = PipeTransform ?? transform;
    }

    private void OnDisable()
    {
        VisualHandler.TextureScaler.DeActiveTextureScaler();
    }

    /// <summary>
    /// Initialize the pipe structure with head and body parts
    /// </summary>
    /// <param name="head">The head transform</param>
    /// <param name="bodyParts">List of body parts transforms (including tail), ordered from head to tail</param>
    /// <param name="isHorizontal">True if the pipe is horizontal (in same row), false if vertical (in same column)</param>
    public void Initialize(List<PaintingPixel> pipePixels, string colorCode, int hearts, bool isHorizontal = false)
    {
        Destroyed = false;
        Hearts = hearts;
        PaintingPixelsCovered = pipePixels != null ? pipePixels : new List<PaintingPixel>();
        IsHorizontal = isHorizontal;
        ColorCode = colorCode;

        Vector3 targetScale = PipeBodyDefaultScale;
        targetScale.z *= pipePixels.Count;
        PipeBodyTransform.localScale = targetScale;
        scaleOriginalZ = targetScale.z;

        //Hearts = Mathf.Max(PaintingPixelsCovered.Count, Hearts);
        Hearts = Mathf.Max(1, Hearts);
        heartPerPixel = (float)Hearts / (float)PaintingPixelsCovered.Count;

        HeartLoss = 0;
        RemainingHearts = Hearts;
        heartLossFromLastPixelDestroyed = 0f;

        ScaleDownValuePerHeart = (float)(targetScale.z / (float)Hearts);

        VisualHandler.TextureScaler.ActiveScaler();
#if UNITY_EDITOR
        HeartText.enabled = true;
        HeartText.text = Hearts.ToString();
#else
        HeartText.enabled = false;
#endif
        VisualHandler.TextureScaler.DeActiveTextureScaler();
    }

    /// <summary>
    /// Rotates all pipe parts based on orientation
    /// </summary>
    public void ApplyOrientationRotation()
    {
        var pipeHead = PaintingPixelsCovered[0];
        var pipeTail = PaintingPixelsCovered[PaintingPixelsCovered.Count - 1];
        if (IsHorizontal)
        {
            // Rotate 90 degrees on Y axis for horizontal pipes
            bool leftToRight = pipeHead.column < pipeTail.column;
            float rotateHead = leftToRight ? 90f : -90f;
            PipeTransform.localEulerAngles = new Vector3(0, rotateHead, 0);
        }
        else
        {
            bool bottomToTop = pipeHead.row < pipeTail.row;
            float rotateHead = bottomToTop ? 0 : 180;
            PipeTransform.Rotate(Vector3.up, rotateHead, Space.Self);
        }
    }

    public void OnAPixelDestroyed()
    {
        if (Destroyed) return;
        HeartLoss++;
        RemainingHearts--;
        GameplayEventsManager.OnAPipePixelDestroyed?.Invoke();
        HeartText.text = RemainingHearts.ToString();
        heartLossFromLastPixelDestroyed++;
        if (heartLossFromLastPixelDestroyed >= heartPerPixel)
        {
            PixelDestroyed++;

            PaintingPixelsCovered[PaintingPixelsCovered.Count - PixelDestroyed].DestroyPixel(false);
            heartLossFromLastPixelDestroyed = heartLossFromLastPixelDestroyed - heartPerPixel;
        }

        if (PixelDestroyed >= PaintingPixelsCovered.Count || RemainingHearts <= 0) Destroyed = true;

        for (int i = 0; i < (PaintingPixelsCovered.Count - 1) - PixelDestroyed; i++)
        {
            PaintingPixelsCovered[i].destroyed = Destroyed;
        }

        if (Destroyed)
        {
            SelfDestroy();
        }
        else
        {
            DOTween.Kill(PipeBodyTransform);
            VisualHandler.PlayFlash();
            VisualHandler.TextureScaler.ActiveScaler();
            PipeBodyTransform.DOScaleZ(scaleOriginalZ - (ScaleDownValuePerHeart * HeartLoss), 0.25f).OnComplete(() =>
            {
                VisualHandler.StopFlash();
                VisualHandler.TextureScaler.DeActiveTextureScaler();
            });
        }
    }

    public void SelfDestroy()
    {
        Destroyed = true;
        foreach (var pixel in PaintingPixelsCovered)
        {
            pixel.DestroyPixel(invokeEvent: false);
        }
        PipeTransform.DOScale(0, 0.5f);
        VisualHandler.TextureScaler.DeActiveTextureScaler();
    }

    public void SelfDestroyGameobject()
    {
        foreach (var pixel in PaintingPixelsCovered)
        {
            if (Application.isPlaying) GameObject.Destroy(pixel.PixelComponent?.gameObject);
            else GameObject.DestroyImmediate(pixel.PixelComponent?.gameObject);
        }
        if (Application.isPlaying) GameObject.Destroy(gameObject);
        else GameObject.DestroyImmediate(gameObject);
    }

    public void ApplyPosition()
    {
        PipeTransform.position = WorldPos;
    }
}