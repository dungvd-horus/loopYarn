using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PipePartVisualHandle : MonoBehaviour
{
    public AutoTextureScale TextureScaler;

    public List<Renderer > pipeRenderers = new List<Renderer>();
    public Renderer PipeScaleBody;

    [Header("BLINKS")]
    public Vector2 BrightnessRange = new Vector2(0.45f, 1f);
    public float FlashDuration = 0.2f;
    private float currentBrightness = 0.45f;
    Tween cachedFlashTween;

    private void Awake()
    {
        CacheFlashTween();
    }

    private void OnDisable()
    {
        StopFlash();
    }

    /// <summary>
    /// Change the color of the pipe part using sharedMaterial with unique materials
    /// </summary>
    /// <param name="color">The color to apply</param>
    public void SetColor(Color color)
    {
        if (pipeRenderers != null)
        {
            // Create unique materials for each renderer to avoid color conflicts
            foreach (Renderer renderer in pipeRenderers)
            {
                if (renderer != null)
                {
                    // Create a new material instance for this renderer
                    Material uniqueMaterial = new Material(renderer.sharedMaterial);
                    uniqueMaterial.SetColor("_Color", color);
                    renderer.sharedMaterial = uniqueMaterial;
                }
            }
        }
    }

    #region _flash animation
    private void CacheFlashTween()
    {
        if (cachedFlashTween != null)
        {
            cachedFlashTween.Kill();
        }

        currentBrightness = BrightnessRange.x;

        cachedFlashTween = DOTween.To(() => currentBrightness, x =>
        {
            currentBrightness = x;
            // Ensure PipeScaleBody has its own material instance
            if (PipeScaleBody.sharedMaterial.name.Contains("Instance") == false)
            {
                Material uniqueMaterial = new Material(PipeScaleBody.sharedMaterial);
                PipeScaleBody.sharedMaterial = uniqueMaterial;
            }
            PipeScaleBody.sharedMaterial.SetFloat("_Brightness", currentBrightness);
        },
        BrightnessRange.y, FlashDuration)
        .SetEase(Ease.OutQuad)
        .Pause()
        .SetAutoKill(false);

        cachedFlashTween.OnComplete(() =>
        {
            SetBrightness(BrightnessRange.x);
        });
    }
    public void PlayFlash()
    {
        cachedFlashTween.Restart();
    }
    public void StopFlash()
    {
        cachedFlashTween.Pause();
        SetBrightness(BrightnessRange.x);
    }
    private void SetBrightness(float _brightness)
    {
        // Ensure PipeScaleBody has its own material instance
        if (PipeScaleBody.sharedMaterial.name.Contains("Instance") == false)
        {
            Material uniqueMaterial = new Material(PipeScaleBody.sharedMaterial);
            PipeScaleBody.sharedMaterial = uniqueMaterial;
        }
        PipeScaleBody.sharedMaterial.SetFloat("_Brightness", _brightness);
    }
    #endregion
}
