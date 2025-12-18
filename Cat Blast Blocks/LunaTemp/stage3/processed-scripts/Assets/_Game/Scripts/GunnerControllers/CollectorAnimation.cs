using System.Threading;
using DG.Tweening;
using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;


public class CollectorAnimation : MonoBehaviour
{
    #region PROPERTIES
    [Header("CONTROLLER(s)")]
    public ColorPixelsCollectorObject CollectorController;
    public CollectorController CollectorInfoController;
    public InGameEffectOptions EffectOptions;
    public Animator RabbitAnimator;
    public Animator BoxAnimator;

    [Header("SCALE")]
    public Transform RootTransform;
    public Transform RabbitTransform;
    public float DefaultScale = 1f;
    public float OnBeltScale = 0.7f;
    public float OnDeadScale = 0.6f;

    [Header("JUMPING")]
    public Transform CollectorBody;
    public float jumpHeight = 0.5f;
    public float jumpScaleY = 1.4f;
    public float squashScaleY = 0.6f;
    public float squashScaleX = 0.6f;
    public float durationUp = 0.15f;
    public float durationDown = 0.1f;
    public float durationRecover = 0.1f;
    public Ease upEase = Ease.OutQuad;
    public Ease downEase = Ease.InQuad;
    public Ease recoverEase = Ease.OutBack;
    Vector3 defaultRabbitRootScale;
    Vector3 defaultCollectorScale;
    Vector3 defaultPos;
    Sequence cachedJumpSeq;

    [Header("SQUASH")]
    public float squashScaleY2 = 0.6f;
    public float squashScaleX2 = 0.6f;
    Sequence cachedSquashSeq;


    [Header("SHOOT")]
    public string ShootAnimation;
    public float ShootRate = 0.2f;
    private float shootTimer = 0;

    [Header("IDLE")]
    public string[] IdleAnimations;
    public string RareIdleAnimation;
    public string EarIdleAnimation;
    public Vector2 EarIdleRate = new Vector2(2f, 6f);
    private float earIdleTimer = 0f;
    Coroutine earAnimCoroutine;

    [Header("BREATH")]
    public Transform RabbitRoot;
    public float breathScaleX = 0.9f;   // scale X when breathing in
    public float breathScaleY = 1.1f;   // scale Y when breathing in
    public float duration = 0.6f;
    private Sequence breathTween;
    Coroutine breathAnimCoroutine;

    [Header("STRETCH ON CLICK")]
    public float stretchScaleY = 0.6f;
    public float stretchDuration = 0.5f;
    private Sequence stretchTween;

    [Header("BOX ANIMATION")]
    public string BoxJumpAnimation;
    public string BoxRevealAnimation;
    public EnableRandomRotate BoxRandomRotate;

    [Header("RUNTIME DATA")]
    public bool JumpingToBelt;

    private Tween tweenPushForward;
    private Tween tweenPushForwardOnQueue;
    private Sequence tweenMoveToQueuePosition;
    #endregion

    #region UNITY CORE

    private void Awake()
    {
        RootTransform = transform;
        RegisterEvents();
    }

    private void Start()
    {
        if (CollectorBody)
        {
            defaultRabbitRootScale = RabbitRoot.localScale;
            defaultCollectorScale = CollectorBody.localScale;
            defaultPos = CollectorBody.localPosition;
            CreateJumpTween();
            CacheBreathTween();
            CreateSquashTween();
            CacheStretchTween();
        }
    }

    private void OnEnable()
    {
        shootTimer = ShootRate;
        JumpingToBelt = false;
        earAnimCoroutine = StartCoroutine(StartEarIdleAnimation());
    }

    private void Update()
    {
        if (shootTimer >= 0) shootTimer -= Time.deltaTime;
    }

    private void OnDisable()
    {
        StopAnimation();
    }

    private void OnDestroy()
    {
        StopAnimation();
        UnRegisterEvents();
    }
    #endregion

    #region MAIN

    #region _events
    private void RegisterEvents()
    {
        GameplayEventsManager.OnClickACollector += OnClickACollector;
        GameplayEventsManager.OnCollectorStartMove += OnCollectorStartMove;
        GameplayEventsManager.OnACollectorMoveToQueue += OnCollectorStartMove;
        GameplayEventsManager.OnCollectorMoveToConveyor += OnCollectorMoveToBelt;
        GameplayEventsManager.OnCollectorMoveToFirstLine += OnCollectorMoveToFirstLine;
    }

    private void UnRegisterEvents()
    {
        GameplayEventsManager.OnClickACollector -= OnClickACollector;
        GameplayEventsManager.OnCollectorStartMove -= OnCollectorStartMove;
        GameplayEventsManager.OnACollectorMoveToQueue -= OnCollectorStartMove;
        GameplayEventsManager.OnCollectorMoveToConveyor -= OnCollectorMoveToBelt;
        GameplayEventsManager.OnCollectorMoveToFirstLine -= OnCollectorMoveToFirstLine;
    }

    private void OnCollectorMoveToBelt(CollectorController _collector)
    {
        if (CollectorController == null || _collector == null) return;
        if (CollectorController.ID != _collector.ColorCollector.ID) return;
        StopJump();
        StopBreathing();
    }
    private void OnCollectorStartMove(CollectorController _collector)
    {
        if (CollectorController == null || _collector == null) return;
        if (CollectorController.ID != _collector.ColorCollector.ID) return;
        StopBreathing();
    }
    private void OnCollectorMoveToFirstLine(CollectorController _collector, bool hidden)
    {
        if (CollectorController == null || _collector == null) return;
        if (CollectorController.ID != _collector.ColorCollector.ID) return;
        StartCoroutine(DelayStartBreathing(hidden ? 1f : 0f));
    }
    private void OnClickACollector(CollectorController _collector, bool canBeMoved)
    {
        if (CollectorController == null || _collector == null) return;
        if (CollectorController.ID != _collector.ColorCollector.ID) return;
        RabbitAnimator?.Rebind();
        if (canBeMoved)
        {

        }
        else
        {
            PlaySquash();
        }
    }
    #endregion

    public void PlayAnim([Bridge.Ref] Vector3 vector3, CollectorAnimState animState, Action actionOnComplete = null)
    {

        switch (animState)
        {
            case CollectorAnimState.Idle:
                break;
            case CollectorAnimState.MoveToQueue:
                MoveToQueuePosition(vector3, actionOnComplete);
                break;
            case CollectorAnimState.MoveToConveyorBelt:
                MoveToBeltPosition(vector3, actionOnComplete);
                break;
            case CollectorAnimState.PushForward:
                PushForwardAnimation(vector3, actionOnComplete);
                if (CollectorController != null && CollectorController.VisualHandler.CattonBox.activeSelf)
                {
                    BoxRandomRotate?.RandomRotate();
                    BoxAnimator.Play(BoxJumpAnimation);
                }
                break;
            case CollectorAnimState.CompleteColorPixels:
                CollectorController.VisualHandler.HideRope();
                CompleteColorPixelsAnimation(actionOnComplete);
                break;
            case CollectorAnimState.PushForwardOnConveyorBelt:
                PushForwardOnQueue(vector3, actionOnComplete);
                break;
            case CollectorAnimState.UnlockLockObject:
                UnlockLockObjectAnimation(vector3, actionOnComplete);
                break;
            case CollectorAnimState.MoveToDeadPosition:
                MoveToDeadPosition(vector3, actionOnComplete);
                break;
            case CollectorAnimState.MoveToQueueRotateBack:
                MoveToQueueRotateBack(vector3, actionOnComplete);
                break;
            default:
                RootTransform.DOMove(vector3, 0.5f);
                break;
        }
    }

    private void MoveToQueuePosition([Bridge.Ref] Vector3 newPos, Action actionOnComplete = null)
    {
        PlayStretch();
        if (tweenMoveToQueuePosition != null && tweenMoveToQueuePosition.IsActive())
        {
            tweenMoveToQueuePosition.Kill();
        }
        tweenMoveToQueuePosition = RootTransform.DOJump(newPos, 2, 1, 0.4f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            actionOnComplete?.Invoke();
            tweenMoveToQueuePosition = null;
            PlaySquash();
            StopStretch();
            StartCoroutine(DelayStartBreathing());
        });
        RabbitTransform.DOScale(DefaultScale, 0.5f);
        RootTransform.DOLocalRotate(Vector3.zero, 0.3f);
    }

    private void MoveToQueueRotateBack([Bridge.Ref] Vector3 newPos, Action actionOnComplete = null)
    {
        PlayStretch();
        if (tweenMoveToQueuePosition != null && tweenMoveToQueuePosition.IsActive())
        {
            tweenMoveToQueuePosition.Kill();
        }
        tweenMoveToQueuePosition = RootTransform.DOJump(newPos, 2, 1, 0.4f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            actionOnComplete?.Invoke();
            tweenMoveToQueuePosition = null;
            PlaySquash();
            StopStretch();
            StartCoroutine(DelayStartBreathing());
        });
        RabbitTransform.DOScale(DefaultScale, 0.5f);
        //RootTransform.DOLocalRotate(new Vector3(0, 180, 0), 0.3f);
    }

    private void MoveToDeadPosition([Bridge.Ref] Vector3 newPos, Action actionOnComplete = null)
    {
        StopBreathing();
        if (tweenMoveToQueuePosition != null && tweenMoveToQueuePosition.IsActive())
        {
            tweenMoveToQueuePosition.Kill();
        }
        tweenMoveToQueuePosition = RootTransform.DOJump(newPos, 2, 1, 0.3f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            actionOnComplete?.Invoke();
            tweenMoveToQueuePosition = null;
            PlaySquash();
            StartCoroutine(DelayStartBreathing());
        });
        RabbitTransform.DOScale(OnDeadScale, 0.5f);
        RootTransform.DOLocalRotate(Vector3.zero, 0.3f);
        //RabbitTransform.DOLocalRotate(new Vector3(0, 180, 0), 0.3f);
    }

    private void MoveToBeltPosition([Bridge.Ref] Vector3 newPos, Action actionOnComplete = null)
    {
        JumpingToBelt = true;
        PlayStretch();
        if (tweenMoveToQueuePosition != null && tweenMoveToQueuePosition.IsActive())
        {
            tweenMoveToQueuePosition.Kill();
        }
        tweenMoveToQueuePosition = RootTransform.DOJump(newPos, 2, 1, 0.4f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            actionOnComplete?.Invoke();
            tweenMoveToQueuePosition = null;
            PlaySquash();
            StopStretch();
        });
        RabbitTransform.DOScale(OnBeltScale, 0.5f);
        RootTransform.DOLocalRotate(Vector3.zero, 0.3f).OnComplete(() => JumpingToBelt = false);
    }

    private void PushForwardAnimation([Bridge.Ref] Vector3 newPos, Action actionOnComplete = null)
    {
        if (tweenPushForward != null && tweenPushForward.IsActive())
        {
            tweenPushForward.Kill();
        }
        tweenPushForward = RootTransform.DOMove(newPos, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            actionOnComplete?.Invoke();
        });
    }

    private void PushForwardOnQueue([Bridge.Ref] Vector3 newPos, Action actionOnComplete = null)
    {
        if (tweenPushForwardOnQueue != null && tweenPushForwardOnQueue.IsActive())
        {
            tweenPushForwardOnQueue.Kill();
        }

        tweenPushForwardOnQueue = RootTransform.DOMove(newPos, 0.1f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            actionOnComplete?.Invoke();
        });
    }

    private void CompleteColorPixelsAnimation(Action actionOnComplete = null)
    {
        RabbitTransform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            actionOnComplete?.Invoke();
            gameObject.SetActive(false);
            RabbitTransform.localScale = Vector3.one;

        });

        RootTransform.DOLocalRotate(new Vector3(0, 360, 0), 0.3f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);

    }

    private void UnlockLockObjectAnimation([Bridge.Ref] Vector3 newPos, Action actionOnComplete = null)
    {
        gameObject.SetActive(false);
        actionOnComplete?.Invoke();
    }

    #region _squash
    void CreateSquashTween()
    {
        // ensure any previous one is killed
        if (cachedSquashSeq != null)
        {
            cachedSquashSeq.Kill();
            cachedSquashSeq = null;
        }
        cachedSquashSeq = DOTween.Sequence()
            .SetAutoKill(false)   // keep it after complete so we can restart
            .Pause();

        cachedSquashSeq.Append(
            DOTween.Sequence()
                .Append(CollectorBody.DOScaleY(defaultCollectorScale.y * squashScaleY, durationDown * 0.6f))
                .Join(CollectorBody.DOScaleX(defaultCollectorScale.x * squashScaleX, durationDown * 0.6f))
        );

        // 3) Recover scale back to default
        cachedSquashSeq.Append(
            DOTween.Sequence()
                .Append(CollectorBody.DOScaleX(defaultCollectorScale.x, durationRecover).SetEase(Ease.OutBack))
                .Join(CollectorBody.DOScaleY(defaultCollectorScale.y, durationRecover).SetEase(Ease.OutBack))
        );

        // Safety: ensure position and scale reset when rewind/complete
        cachedSquashSeq.OnRewind(() =>
        {
            CollectorBody.localScale = defaultCollectorScale;
        });

        cachedSquashSeq.OnComplete(() =>
        {
            CollectorBody.localScale = defaultCollectorScale;
            // note: we keep sequence alive (SetAutoKill(false)) so it can be restarted
        });
    }
    [ContextMenu("Play Squash")]
    public void PlaySquash()
    {
        if (cachedSquashSeq == null) return;
        cachedSquashSeq.Restart();
    }
    #endregion

    #region _jump

    void CreateJumpTween()
    {
        // ensure any previous one is killed
        if (cachedJumpSeq != null)
        {
            cachedJumpSeq.Kill();
            cachedJumpSeq = null;
        }

        float upY = defaultPos.y + jumpHeight;

        cachedJumpSeq = DOTween.Sequence()
            .SetAutoKill(false)   // keep it after complete so we can restart
            .Pause();

        // 1) Jump up: move up + stretch on Y
        cachedJumpSeq.Append(CollectorBody.DOLocalMoveY(upY, durationUp).SetEase(upEase));
        cachedJumpSeq.Join(CollectorBody.DOScaleY(defaultCollectorScale.y * jumpScaleY, durationUp).SetEase(upEase));

        // 2) Fall down: move down + squash on landing (we use slightly shorter time)
        cachedJumpSeq.Append(CollectorBody.DOLocalMoveY(defaultPos.y, durationDown).SetEase(downEase));
        cachedJumpSeq.Append(
            DOTween.Sequence()
                .Append(CollectorBody.DOScaleY(defaultCollectorScale.y * squashScaleY, durationDown * 0.6f))
                .Join(CollectorBody.DOScaleX(defaultCollectorScale.x * squashScaleX, durationDown * 0.6f))
        );

        // 3) Recover scale back to default
        cachedJumpSeq.Append(
            DOTween.Sequence()
                .Append(CollectorBody.DOScaleX(defaultCollectorScale.x, durationRecover).SetEase(recoverEase))
                .Join(CollectorBody.DOScaleY(defaultCollectorScale.y, durationRecover).SetEase(recoverEase))
        );

        // Safety: ensure position and scale reset when rewind/complete
        cachedJumpSeq.OnRewind(() =>
        {
            CollectorBody.localPosition = defaultPos;
            CollectorBody.localScale = defaultCollectorScale;
        });

        cachedJumpSeq.OnComplete(() =>
        {
            // ensure final exact values
            CollectorBody.localPosition = defaultPos;
            CollectorBody.localScale = defaultCollectorScale;
            // note: we keep sequence alive (SetAutoKill(false)) so it can be restarted
        });
    }

    [ContextMenu("Play Jump")]
    public void PlayJump()
    {
        cachedJumpSeq.Restart();
    }

    public void PlayBoxReveal()
    {
        if (CollectorController.VisualHandler.CattonBox.activeSelf) BoxAnimator.Play(BoxRevealAnimation);
    }

    public void StopJump()
    {
        if (CollectorBody)
        {
            cachedJumpSeq.Pause();
            CollectorBody.localScale = defaultCollectorScale;
            CollectorBody.localPosition = defaultPos;
        }
    }
    #endregion

    #region _idle animator
    private IEnumerator StartEarIdleAnimation()
    {
        if (!EffectOptions.RabbitEarAnimation || CollectorController == null) yield break;
        while (EffectOptions.RabbitEarAnimation || EffectOptions.RabbitRandomIdleAnimation)
        {
            earIdleTimer = UnityEngine.Random.Range(EarIdleRate.x, EarIdleRate.y);
            yield return new WaitForSeconds(earIdleTimer);

            float _r = UnityEngine.Random.Range(0, 100);
            bool execute = _r < EffectOptions.IdleRate && CollectorInfoController.IndexInColumn < 4;
            if (CollectorController.BulletLeft > 0)
            {
                if (execute && !CollectorController.IsCollectorActive)
                {
                    if (EffectOptions.RabbitEarAnimation)
                    {
                        _r = UnityEngine.Random.Range(0, 100);
                        bool earOrIdle = _r > 50;
                        RabbitAnimator.Rebind();
                        if (earOrIdle)
                        {
                            RabbitAnimator.Play(EarIdleAnimation);
                        }
                        else
                        {
                            if (_r <= 10 && !CollectorController.IsHidden) RabbitAnimator.Play(RareIdleAnimation);
                            else RabbitAnimator.Play(IdleAnimations[Random.Range(0, IdleAnimations.Length)]);
                        }
                    }
                    else
                    {
                        _r = UnityEngine.Random.Range(0, 100);
                        if (_r <= 10 && !CollectorController.IsHidden) RabbitAnimator.Play(RareIdleAnimation);
                        else RabbitAnimator.Play(IdleAnimations[Random.Range(0, IdleAnimations.Length)]);
                    }
                }
            }
            else yield break;
        }
    }
    #endregion

    #region _breath

    void CacheBreathTween()
    {
        Vector3 inhaleScale = new Vector3(defaultRabbitRootScale.x * breathScaleX, defaultRabbitRootScale.y * breathScaleY, defaultRabbitRootScale.z);
        Vector3 exhaleScale = defaultRabbitRootScale;

        breathTween = DOTween.Sequence()
            .Append(RabbitRoot.DOScale(inhaleScale, duration).SetEase(Ease.InOutSine))
            .Append(RabbitRoot.DOScale(exhaleScale, duration).SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Restart);

        breathTween.Pause(); // cached but not playing yet
    }

    [ContextMenu("Start Breathing")]
    public void StartBreathing() => breathTween.Restart();
    public IEnumerator DelayStartBreathing(float extraDelay = 0)
    {
        yield return new WaitForSeconds(durationDown * 2.7f + UnityEngine.Random.Range(0.1f, 0.5f) + extraDelay);
        breathTween.Restart();
    }
    public void StopBreathing()
    {
        if (RabbitRoot)
        {
            if (breathAnimCoroutine != null)
            {
                StopCoroutine(breathAnimCoroutine);
                breathAnimCoroutine = null;
            }
            breathTween.Pause();
            RabbitRoot.localScale = defaultRabbitRootScale;
        }
    }
    #endregion

    #region _shoot
    [ContextMenu("Play Shoot Animation")]
    public void PlayShootAnimation()
    {
        if (shootTimer <= 0)
        {
            shootTimer = ShootRate;
            RabbitAnimator.Rebind();
            RabbitAnimator.Play(ShootAnimation);
            //DeviceVibrationManager.Instance.ExecuteShootVibration();
        }
    }
    #endregion

    #region _stretch
    private void CacheStretchTween()
    {
        if (stretchTween != null)
        {
            stretchTween.Kill();
            stretchTween = null;
        }
        stretchTween = DOTween.Sequence()
            .Append(CollectorBody.DOScaleY(stretchScaleY, stretchDuration).SetEase(Ease.InOutSine))
            .SetAutoKill(false)
            .Pause();

        stretchTween.Pause();
    }
    [ContextMenu("Play Stretch")]
    public void PlayStretch() => stretchTween.Restart();
    public void StopStretch()
    {
        stretchTween.Pause();
        CollectorBody.localScale = defaultCollectorScale;
    }
    #endregion

    #endregion

    #region SUPPORTIVE
    private void StopAnimation()
    {
        if (earAnimCoroutine != null)
        {
            StopCoroutine(earAnimCoroutine);
            earAnimCoroutine = null;
        }
        if (breathAnimCoroutine != null)
        {
            StopCoroutine(breathAnimCoroutine);
            breathAnimCoroutine = null;
        }
        StopBreathing();
        StopJump();
    }
    #endregion
}

public enum CollectorAnimState
{
    Idle,
    MoveToQueue,
    MoveToConveyorBelt,
    PushForward,
    CompleteColorPixels,
    PushForwardOnConveyorBelt,
    UnlockLockObject,
    MoveToDeadPosition,
    MoveToQueueRotateBack,
}
