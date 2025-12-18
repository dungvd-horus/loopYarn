using GogoGaga.OptimizedRopesAndCables;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CollectorVisualHandler : MonoBehaviour
{
    public ColorPixelsCollectorObject CollectorHandle;
    public CollectorAnimation Animator;

    public ColorPalleteData colorPalette;
    public List<Renderer> GunnerRenderers = new List<Renderer>();
    private Tween rimTween;
    private MaterialPropertyBlock block;
    private Dictionary<string, Color> colorCache = new Dictionary<string, Color>(); // Cache for color lookups

    [Header("Object: BULLET")]
    public TMP_Text BulletsText;

    [Header("Object: ROPE")]
    public GameObject TankRopeObj;
    public Rope TankRope;
    public RopeMesh TankRopeMesh;

    [Header("Object: LOCK")]
    public SpriteRenderer LockSpriteRenderer;

    [Header("Object: HIDDEN")]
    public GameObject CattonBox;
    public SpriteRenderer QuestionMarkSpriteRenderer;
    public Material OriginalMat;
    public Color HiddenOutlineColor = Color.white;
    public Color HiddenRopeColor = Color.white;

    [Header("VFX")]
    public Transform BulletSpawner;
    public ParticleSystem CollectorMuzzleVFX;
    public ParticleSystem ReavealVFX;
    public ParticleSystem HighSpeedTrail;

    [Header("Real time data")]
    public Color CurrentColor;
    private bool hidden = false;
    private CollectorVisualHandler connectedTarget;

    #region UNITY CORE
    private void Awake()
    {
        block = new MaterialPropertyBlock();
        RegisterEvents();
    }
    private void OnEnable()
    {
        StopHighlight();
    }

    private void OnDisable()
    {
        connectedTarget = null;
        StopHighlight();
    }

    private void OnDestroy()
    {
        UnRegisterEvents();
        // Clear the color cache to prevent memory leaks
        colorCache.Clear();
    }
    #endregion

    #region MAIN

    #region _events
    private void RegisterEvents()
    {
        GameplayEventsManager.OnCollectorRevealed += OnACollectorRevealed;
    }
    private void UnRegisterEvents()
    {
        GameplayEventsManager.OnCollectorRevealed -= OnACollectorRevealed;
    }
    private void OnACollectorRevealed(ColorPixelsCollectorObject _collector)
    {
        if (connectedTarget == null) return;
        if (connectedTarget.CollectorHandle.ID != _collector.ID || CollectorHandle.ID == _collector.ID) return;
        //var target = _collector.VisualHandler;
        //SetupRope(target, null);
        UpdateRopeColor();
    }
    #endregion

    public void SetColor(string colorCode)
    {
        if (GunnerRenderers != null)
        {
            // Use cached color if available, otherwise get from palette and cache it
            Color color;
            if (!colorCache.TryGetValue(colorCode, out color))
            {
                color = colorPalette.GetColorByCode(colorCode);
                colorCache[colorCode] = color;
            }
            CurrentColor = color; // Set CurrentColor first
#if UNITY_EDITOR
            SetMeshColor(color);
#else
            // Try to get material from colorPalette first, fallback to OriginalMat
            Material mat = OriginalMat;
            if (colorPalette != null && colorPalette.MatPallete.ContainsKey(colorCode))
            {
                Material paletteMat = colorPalette.MatPallete[colorCode];
                if (paletteMat != null)
                {
                    mat = paletteMat;
                }
            }
            SetMeshMaterial(mat);
#endif
            SetMuzzleColor();
            UpdateRopeColor(); // Update rope color when main color changes
        }
    }

    public void SetMaterial(string colorCode, Material mat)
    {
        if (GunnerRenderers != null)
        {
            // Use cached color if available, otherwise get from palette and cache it
            Color color;
            if (!colorCache.TryGetValue(colorCode, out color))
            {
                color = colorPalette.GetColorByCode(colorCode);
                colorCache[colorCode] = color;
            }
            CurrentColor = color; // Set CurrentColor first
#if UNITY_EDITOR
            SetMeshColor(color);
#else
            SetMeshMaterial(mat);
#endif
            SetMuzzleColor();
            UpdateRopeColor(); // Update rope color when main color changes
        }
    }
    private void SetMeshMaterial(Material mat)
    {
        // Apply the material to all renderers in the pipe part
        for (int i = 0; i < GunnerRenderers.Count; i++)
        {
            Renderer renderer = GunnerRenderers[i];
            if (renderer != null)
            {
                renderer.material = mat;
            }
        }
    }
    private void SetMeshColor(Color _color)
    {
        // Create a MaterialPropertyBlock to set the color
        block.SetColor("_Color", _color);

        // Apply the color to all renderers in the pipe part
        for (int i = 0; i < GunnerRenderers.Count; i++)
        {
            Renderer renderer = GunnerRenderers[i];
            if (renderer != null)
            {
                renderer.material = OriginalMat;
                renderer.SetPropertyBlock(block);
            }
        }
    }
    public void SetRopeColor(Color _firstColor, Color _secondColor)
    {
        TankRopeMesh.SetColor(_firstColor, _secondColor);
    }

    public void SetupRope(bool active, CollectorVisualHandler target)
    {
        connectedTarget = target;
        if (connectedTarget == null) return;
        if (active)
        {
            TankRopeObj.SetActive(true);
            TankRope.SetEndPoint(target.TankRopeObj.transform);
            Color stColor = CollectorHandle.IsHidden ? HiddenRopeColor : CurrentColor;
            Color ndColor = target.CollectorHandle.IsHidden ? HiddenRopeColor : target.CurrentColor;
            SetRopeColor(stColor, ndColor);
        }
        else TankRopeObj.SetActive(false);
    }

    public void HideRope()
    {
        TankRopeObj.SetActive(false);
    }

    public void UpdateRopeColor()
    {
        if (connectedTarget == null) return;

        TankRope.SetEndPoint(connectedTarget.TankRopeObj.transform);
        Color stColor = CollectorHandle.IsHidden ? HiddenRopeColor : CurrentColor;
        Color ndColor = connectedTarget.CollectorHandle.IsHidden ? HiddenRopeColor : connectedTarget.CurrentColor;
        SetRopeColor(stColor, ndColor);
    }

    public void RefreshColor()
    {
#if UNITY_EDITOR
        if (hidden) return;
        if (CurrentColor != null)
        {
            SetMeshColor(CurrentColor);
            UpdateRopeColor(); // Update rope color when refreshing
        }
#endif
    }

    #region _bullets
    public void SetBulletText(int bullet)
    {
        BulletsText.enabled = !hidden && bullet > 0;
        BulletsText.text = bullet.ToString();
    }
    #endregion

    #region _lock
    public void SetLockedIcon(bool locked)
    {
        LockSpriteRenderer.enabled = locked;
    }
    #endregion

    #region _hidden
    public void SetHiddenState(bool _hidden, bool instantly = true)
    {
        hidden = _hidden;
        Color stColor = CollectorHandle.IsHidden ? HiddenRopeColor : CurrentColor;
        Color ndColor = CurrentColor;
        if (connectedTarget) ndColor = connectedTarget.CollectorHandle.IsHidden ? HiddenRopeColor : connectedTarget.CurrentColor;
        BulletsText.enabled = !_hidden && CollectorHandle.BulletLeft > 0;
        QuestionMarkSpriteRenderer.enabled = _hidden;
        if (!Application.isPlaying)
        {
            QuestionMarkSpriteRenderer.color = Color.cyan;
        }
        SetRopeColor(stColor, ndColor);

        if (!instantly)
        {
            if (!_hidden) StartCoroutine(DelayReveal(0.3f));
        }
        else
        {
            SetVisisble(!_hidden);
            CattonBox.SetActive(_hidden);
        }

        // The code after this return was unreachable, so I've removed it
    }

    private IEnumerator DelayReveal(float delay)
    {
        SetVisisble(true);
        yield return new WaitForSeconds(delay);
        if (!Animator.JumpingToBelt) Animator.PlayJump();
        Animator.PlayBoxReveal();
        ReavealVFX?.Play();
        yield return new WaitForSeconds(0.7f);
        CattonBox.SetActive(false);
    }
    #endregion

    #region _muzzle
    public void SetMuzzleColor()
    {
        var main = CollectorMuzzleVFX.main;
        main.startColor = new ParticleSystem.MinMaxGradient(CurrentColor);
    }
    public void PlayMuzzleEffect()
    {
        CollectorMuzzleVFX.Stop();
        CollectorMuzzleVFX.Play();
    }
    #endregion

    #region _highlight
    public void StartHighlight(float min = 0f, float max = 1.5f, float duration = 0.5f)
    {
        StartRimBlink(min, max, duration);
    }

    public void StopHighlight()
    {
        // Stop Tween
        rimTween?.Kill();
        rimTween = null;

        // Turn off rim
        SetFloat("_RimAmount", 0);
        SetFloat("_Rim", 0);
        for (int i = 0; i < GunnerRenderers.Count; i++)
        {
            Renderer renderer = GunnerRenderers[i];
            if (renderer != null && renderer.sharedMaterial != null)
            {
                renderer.sharedMaterial.DisableKeyword("RIM");
            }
        }
    }

    public void StartRimBlink(float min = 0f, float max = 1.5f, float duration = 0.5f)
    {
        for (int i = 0; i < GunnerRenderers.Count; i++)
        {
            Renderer renderer = GunnerRenderers[i];
            if (renderer != null && renderer.sharedMaterial != null)
            {
                renderer.sharedMaterial.EnableKeyword("RIM");
                SetFloat("_Rim", 1);
            }
        }

        // Kill old tween if exists
        rimTween?.Kill();

        float current = min;
        rimTween = DOTween.To(() => current, x =>
        {
            current = x;
            SetFloat("_RimAmount", current);
        }, max, duration)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutQuad);
    }

    private void SetFloat(string prop, float value)
    {
        block.SetFloat(prop, value);
        for (int i = 0; i < GunnerRenderers.Count; i++)
        {
            Renderer _render = GunnerRenderers[i];
            if (_render != null)
            {
                _render.SetPropertyBlock(block);
            }
        }
    }

    public void StartTrailHighSpeed() => HighSpeedTrail.Play();
    public void StopTrailHighSpeed() => HighSpeedTrail.Stop();
    #endregion

    #region _visible
    public void SetVisisble(bool visible)
    {
        if (Animator?.RabbitAnimator != null)
            Animator.RabbitAnimator.enabled = visible;
            
        for (int i = 0; i < GunnerRenderers.Count; i++)
        {
            Renderer renderer = GunnerRenderers[i];
            if (renderer != null)
                renderer.enabled = visible;
        }
    }
    #endregion

    #endregion
}
