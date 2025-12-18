using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IdleMoveUpDown : MonoBehaviour
{
    public Transform target;
    public float moveAmount = 0.2f;
    public float duration = 0.5f;
    public Ease ease = Ease.InOutSine;

    private Tween moveTween;

    private void Awake()
    {
        CreateMoveTween();
    }

    private void OnDisable()
    {
        StopTween();
    }

    private void CreateMoveTween()
    {
        if (target == null) target = transform;

        Vector3 startPos = target.localPosition;
        Vector3 upPos = startPos + new Vector3(0f, moveAmount, 0f);

        moveTween = target.DOLocalMoveY(upPos.y, duration)
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo)
            .Pause()
            .SetAutoKill(false);
    }

    public void PlayTween()
    {
        moveTween.Play();
    }

    public void StopTween()
    {
        moveTween.Pause();
    }
}
