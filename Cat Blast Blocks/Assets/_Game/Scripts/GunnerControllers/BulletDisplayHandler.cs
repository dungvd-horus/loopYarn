using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BulletDisplayHandler : MonoBehaviour
{
    public ColorPixelsCollectorObject Collector;
    [SerializeField] private TMP_Text text;

    // Cache a reusable rotation that represents world +Z facing with world up
    private Quaternion cachedWorldForward;
    private Transform _textTransform;

    private Transform camTrans;

    private bool needUpdateText = false;

    private void Awake()
    {
        cachedWorldForward = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        if (text != null)
            _textTransform = text.transform;
        camTrans = Camera.main.transform;
    }

    private void Update()
    {
        if (!needUpdateText)
            return;

        UpdateRotateText();
    }

    public void UpdateBulletDisplay(int bulletLeft)
    {
        text.enabled = !Collector.IsHidden && bulletLeft > 0;
        text.text = bulletLeft.ToString();
    }

    private void UpdateRotateText()
    {
        if (text == null)
        {
            Debug.LogError("BulletDisplayHandler: text is not assigned.");
            return;
        }
        if (_textTransform == null)
        {
            _textTransform = text.transform;
            //_textTransform.LookAt(camTrans);
        }

        if (camTrans != null)
        {
            _textTransform.forward = camTrans.forward;
            //_textTransform.localScale = Vector3.one * 1.25f;
        }
        
    }

    public void SetUpdateText(bool enable)
    {
        needUpdateText = enable;
    }

    public void SetFadeText(bool isFaded)
    {
        text.color = isFaded ? new Color(1f, 1f, 1f, 0.6f) : Color.white;
    }

    private Tween scaleTwween;
    private void ScaleText(bool isScale)
    {
        if (scaleTwween != null && scaleTwween.IsActive())
        {
            scaleTwween.Kill();
        }
        scaleTwween = _textTransform.DOScale(isScale ? 1.25f : 1f, 0.2f);
    }
}
