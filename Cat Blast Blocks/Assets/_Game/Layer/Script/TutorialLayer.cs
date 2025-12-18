using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Tối giản TutorialLayer: dùng Coroutine cho hand sprite + flare; DOTween cho scale text;
public class TutorialLayer : MonoBehaviour
{
    [SerializeField] private RectTransform handTrans;
    [SerializeField] private Image handImage;
    [SerializeField] private Sprite[] handSprites;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private Image flareImage;
    [SerializeField] private Canvas canvas;

    private bool playAnim;
    private Tween tweenScaleText;
    private Coroutine swapHandCoroutine;
    private Coroutine flareCoroutine;

    private int spriteIndex;


    private void OnEnable()
    {
        GameplayEventsManager.OnCollectorStartMove += OnCollectorStartMove;
    }

    private void OnDisable()
    {
        GameplayEventsManager.OnCollectorStartMove -= OnCollectorStartMove;
        StopAllCoroutinesSafe();
        tweenScaleText?.Kill();
    }

    void Start()
    {
        InitLayer();
        ShowLayer();
    }

    // Initialize visuals (safe null checks)
    public void InitLayer()
    {
        if (handImage != null && handSprites != null && handSprites.Length > 0)
        {
            spriteIndex = 0;
            handImage.sprite = handSprites[0];
        }

        if (tutorialText != null)
            tutorialText.text = "Tap the cat to place into the conveyor";

        StartScaleText();

        // try position once (best-effort)
        var first = GameplayEventsManager.GetFirstCollector?.Invoke();
        if (first != null && handTrans != null && canvas != null)
        {
            handTrans.anchoredPosition = WorldToCanvasLocalPosition(canvas, Camera.main, first.position);
            handTrans.gameObject.SetActive(true);
        }
    }

    public void ShowLayer()
    {
        playAnim = true;
        if (swapHandCoroutine != null) StopCoroutine(swapHandCoroutine);
        swapHandCoroutine = StartCoroutine(SwapHandSpriteCoroutine());
    }

    public void HideLayer()
    {
        playAnim = false;
        StopAllCoroutinesSafe();
        tweenScaleText?.Kill();
    }

    private IEnumerator SwapHandSpriteCoroutine()
    {
        if (handImage == null || handSprites == null || handSprites.Length == 0) yield break;
        while (playAnim)
        {
            handImage.sprite = handSprites[spriteIndex];
            spriteIndex = (spriteIndex + 1) % handSprites.Length;

            if (spriteIndex == 1)
            {
                if (flareCoroutine != null) StopCoroutine(flareCoroutine);
                flareCoroutine = StartCoroutine(FlareEffectCoroutine());
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator FlareEffectCoroutine()
    {
        if (flareImage == null) yield break;
        flareImage.gameObject.SetActive(true);
        var rt = flareImage.rectTransform;
        rt.localScale = Vector3.zero;
        Color c = flareImage.color;
        c.a = 1f;
        flareImage.color = c;

        float d = 0.3f;
        float t = 0f;
        while (t < d)
        {
            t += Time.deltaTime;
            float p = Mathf.Clamp01(t / d);
            rt.localScale = Vector3.one * Mathf.Lerp(0f, 1f, p);
            c.a = Mathf.Lerp(1f, 0f, p);
            flareImage.color = c;
            yield return null;
        }

        rt.localScale = Vector3.one;
        c.a = 0f;
        flareImage.color = c;
        flareImage.gameObject.SetActive(false);
        flareCoroutine = null;
    }

    private void StartScaleText()
    {
        if (tutorialText == null) return;
        tweenScaleText = tutorialText.rectTransform.DOScale(Vector3.one * 1.1f, 0.7f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCollectorStartMove(CollectorController obj)
    {
        GameplayEventsManager.OnCollectorStartMove -= OnCollectorStartMove;
        gameObject.SetActive(false);
        Luna.Unity.LifeCycle.GameUpdated();
    }

    private void StopAllCoroutinesSafe()
    {
        if (swapHandCoroutine != null) { StopCoroutine(swapHandCoroutine); swapHandCoroutine = null; }
        if (flareCoroutine != null) { StopCoroutine(flareCoroutine); flareCoroutine = null; }
    }

    // Converts a world position to canvas local (anchoredPosition) safely
    private Vector2 WorldToCanvasLocalPosition(Canvas targetCanvas, Camera cam, Vector3 worldPos)
    {
        if (targetCanvas == null) return Vector2.zero;
        var canvasRect = targetCanvas.GetComponent<RectTransform>();
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam ?? Camera.main, worldPos);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint,
            targetCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : (cam ?? Camera.main), out localPoint);
        return localPoint;
    }
}