using DG.Tweening;
using UnityEngine;

public class PaintingPixelComponent : MonoBehaviour
{
    public Transform CubeTransform;
    public PaintingPixel PixelData;
    public MeshRenderer CubeRenderer;
    public int CurrentHearts; // default 0 implicit

    [Header("ANIM: SHAKE")] public InGameEffectOptions EffectOptions;
    public int shakeLoops = 5;
    public float shakeDuration = 0.1f;

    private Sequence pumpTween;
    private Sequence shakeTween;
    [SerializeField] private Vector3 initScale;
    void Awake()
    {
        initScale = transform.localScale;
    }
    void OnEnable()
    {
        transform.localScale = initScale;
    }
    [Header("Perf/Debug")] public bool UseLazyTweens = true;
    private static bool _logMissingOnce;

    // Assign renderer material strictly from ScriptableObject pixel data or global fallback; never create new material.
    private void SetRendererMaterial()
    {
        if (CubeRenderer == null) return;
        Material target = PixelData?.Material ?? PixelMaterialFallbackProvider.GlobalFallback;
        if (target == null)
        {
            CubeRenderer.enabled = false;
            if (!_logMissingOnce)
            {
                Debug.LogError($"[PaintingPixelComponent] Missing material for pixel ({PixelData?.column},{PixelData?.row}). Renderer disabled.", this);
                _logMissingOnce = true;
            }
            return;
        }
        CubeRenderer.sharedMaterial = target; // single assignment (shared asset from SO)
    }

    // Apply color override using MaterialPropertyBlock (no material cloning)
    private void ApplyColorOverride()
    {
        if (CubeRenderer == null || PixelData == null) return;

        // Kiểm tra xem có cần override color không
        if (PixelData.UsePaletteMaterialColor && PixelData.Material != null)
        {
            // So sánh color hiện tại với material color
            bool needsOverride = !ColorEqual(PixelData.Material.color, PixelData.color, 0.01f);

            if (needsOverride)
            {
                // Sử dụng MaterialPropertyBlock thay vì clone material
                var propertyBlock = new MaterialPropertyBlock();
                propertyBlock.SetColor("_Color", PixelData.color);
                CubeRenderer.SetPropertyBlock(propertyBlock);
            }
            else
            {
                // Không cần override, clear property block bằng cách set thành null
                var propertyBlock = new MaterialPropertyBlock();
                CubeRenderer.SetPropertyBlock(propertyBlock);
            }
        }
    }

    // Helper method để so sánh color với tolerance
    private bool ColorEqual(Color a, Color b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.r - b.r) <= tolerance &&
               Mathf.Abs(a.g - b.g) <= tolerance &&
               Mathf.Abs(a.b - b.b) <= tolerance &&
               Mathf.Abs(a.a - b.a) <= tolerance;
    }

    public void SetUp(PaintingPixel newPixel)
    {
        CubeTransform = transform;
        newPixel.PixelComponent = this;
        PixelData = newPixel;
        CurrentHearts = PixelData.Hearts;
        SetRendererMaterial();
        if (!UseLazyTweens)
        {
            CreateShakeTween();
            CreatePumpTween();
        }
        ApplyVisual();
    }

    public void ApplyVisual()
    {
        if (PixelData == null || CubeRenderer == null) return;
        CubeRenderer.enabled = !PixelData.Hidden;
        if (!PixelData.Hidden)
        {
            if (PixelData.UsePaletteMaterialColor)
            {
                ApplyColorOverride(); // Sử dụng MaterialPropertyBlock
            }
            else
            {
                SetColor(PixelData.color); // Legacy method for non-palette colors
            }
        }
    }

    public void SetColor(Color color)
    {
        if (CubeRenderer == null) return;
        if (PixelData == null) return;

        PixelData.color = color; // Always store the logical color

        if (PixelData.UsePaletteMaterialColor)
        {
            // Use MaterialPropertyBlock for palette materials (no cloning)
            var propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_Color", color);
            CubeRenderer.SetPropertyBlock(propertyBlock);
        }
        else
        {
            // Legacy method: direct material color assignment
            if (CubeRenderer.sharedMaterial != null)
            {
                CubeRenderer.sharedMaterial.color = color;
            }
        }
    }

    public void ClearColorOverride()
    {
        if (CubeRenderer == null) return;
        // Clear MaterialPropertyBlock bằng cách set property block rỗng
        var propertyBlock = new MaterialPropertyBlock();
        CubeRenderer.SetPropertyBlock(propertyBlock);
    }

    public bool IsDestroyed() => PixelData != null && PixelData.destroyed;
    public void Destroyed() { if (PixelData != null) PixelData.destroyed = true; }
    public void ApplyPosition() { if (PixelData != null) transform.localPosition = PixelData.worldPos; }
    public void ShowVisualOnly() { if (CubeRenderer) CubeRenderer.enabled = true; }
    public void HideVisualOnly() { if (CubeRenderer) CubeRenderer.enabled = false; }
    public Vector3 GetWorldPosition() => PixelData != null ? PixelData.worldPos : transform.position;

    public void DestroyPixelVisually() => PlayPumpAndDisable();

    public void SelfDestroy()
    {
        if (Application.isPlaying) Destroy(gameObject); else DestroyImmediate(gameObject);
    }

    private void CreatePumpTween()
    {
        if (pumpTween != null) return;
        pumpTween = DOTween.Sequence().Pause().SetAutoKill(false);
        Vector3 upScale = CubeTransform.localScale * 1.5f;
        pumpTween
            .Append(CubeTransform.DOScale(upScale, 0.05f).SetEase(Ease.InBack))
            .Append(CubeTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack))
            .OnComplete(() => { gameObject.SetActive(false); CubeTransform.localScale = Vector3.one; });
    }

    public void PlayPumpAndDisable()
    {
        if (UseLazyTweens && pumpTween == null) CreatePumpTween();
        GameplayEventsManager.OnBlockDissapear?.Invoke();
        gameObject.SetActive(true);
        pumpTween.Restart();
    }
    /// <summary>
    /// Revive the pixel with animation (called when fountain refills a destroyed pixel)
    /// </summary>
    public void ReviveAnimate()
    {
        if (PixelData == null) return;

        PixelData.destroyed = false;
        gameObject.SetActive(true);

        // Optional: Add scale animation
        transform.localScale = Vector3.zero;
        transform.DOScale(initScale, 0.3f).SetEase(Ease.OutBack);

        // Show visual
        ShowVisualOnly();
    }
    [ContextMenu("Create Shake Tween")]
    private void CreateShakeTween()
    {
        if (EffectOptions == null || shakeTween != null) return;
        Vector3 startRot = CubeTransform.localEulerAngles;
        shakeTween = DOTween.Sequence().SetAutoKill(false).Pause();
        for (int i = 0; i < shakeLoops; i++)
        {
            Vector3 randomRot = startRot + Random.insideUnitSphere * EffectOptions.ShakeValue;
            shakeTween.Append(CubeTransform.DOLocalRotate(randomRot, shakeDuration * 0.5f).SetEase(Ease.OutQuad));
            shakeTween.Append(CubeTransform.DOLocalRotate(startRot, shakeDuration * 0.5f).SetEase(Ease.InOutQuad));
        }
        shakeTween.OnComplete(() => { CubeTransform.localRotation = Quaternion.identity; });
    }

    [ContextMenu("Play Shake")]
    public void PlayShake()
    { if (EffectOptions == null) return; if (UseLazyTweens && shakeTween == null) CreateShakeTween(); if (EffectOptions.ShakeNeighborBlocks) shakeTween.Restart(); }
    public void StopShake() { shakeTween?.Pause(); CubeTransform.localRotation = Quaternion.identity; }

    private void OnDisable()
    {
        pumpTween?.Kill(false); pumpTween = null;
        shakeTween?.Kill(false); shakeTween = null;
    }
}