using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectorController : MonoBehaviour
{
    // Cache commonly used Vector3 values to avoid allocation
    private static readonly Vector3 Vector3One = Vector3.one;
    private static readonly Vector3 Vector3Zero = Vector3.zero;
    private static readonly Vector3 Vector3Up = Vector3.up;
    private static readonly Vector3 Vector3Forward = Vector3.forward;
    private static readonly Vector3 Vector3Right = Vector3.right;
    public CachedTransformPathMover movementHandle;
    public ColorPixelsCollectorObject ColorCollector;
    public List<CollectorController> collectorConnect;
    public CollectorState State = CollectorState.InColumn;
    public int IndexInColumn = 0;
    public int ColumnIndex = 0;
    public int SlotOnQueue = 1;

    public LockObject LockController;
    public bool IsLockObject = false;

    [SerializeField] private CollectorAnimation anim;
    [SerializeField] private BulletDisplayHandler bulletDisplayHandler;

    public bool IsCompleteColor = false;
    private void Awake()
    {
        if (ColorCollector != null)
        {
            ColorCollector.OnCompleteAllColorPixels += HandleCompleteColorPixels;
        }

        GameplayEventsManager.OnAbsoluteWin += OnAbsoluteWin;
    }

    private void OnDestroy()
    {
        if (ColorCollector != null)
        {
            ColorCollector.OnCompleteAllColorPixels -= HandleCompleteColorPixels;
        }
        GameplayEventsManager.OnAbsoluteWin -= OnAbsoluteWin;
    }

    public void PauseMovement()
    {
        movementHandle.PauseMovement();
    }

    public void ResumeMovement()
    {
        ColorCollector.SetStatusAttributes();
    }

    public void StartMovement(float targetTF)
    {
        // Nếu collector đã di chuyển thì không gọi lại nữa để tránh lặp vô hạn
        if (State == CollectorState.Moving)
            return;

        // if (State == CollectorState.InDeadQueue)
        // {
        //     transform.DOScale(Vector3One, 0.1f);
        // }
        movementHandle.HandleSelectCollector(targetTF, OnCollectorMoveFinished);
        ColorCollector.SetActiveCollector(true);
        ColorCollector.SetStatusAttributes();
        State = CollectorState.Moving;
        bulletDisplayHandler?.SetUpdateText(true);
        ColorCollector.OnStartMove();
        ColorCollector.RabbitRotateTransform.eulerAngles = Vector3Zero;
    }

    private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    public IEnumerator StopMovementAtCurrentPosition()
    {
        movementHandle.StopMovementAtCurrentPosition();
        State = CollectorState.InDeadQueue;
        yield return waitForEndOfFrame;
        ColorCollector.RabbitRotateTransform.eulerAngles = Vector3Zero;
    }

    public void OnCollectorMoveFinished()
    {
        GameplayEventsManager.OnACollectorFinishMove?.Invoke(this);
        GameplayEventsManager.OnACollectorMoveToQueue?.Invoke(this);
        State = CollectorState.InQueue;
        ColumnIndex = -1;

        //bulletDisplayHandler?.SetUpdateText(false);
        // Action action = () => { bulletDisplayHandler?.SetUpdateText(false); };
        // anim.PlayAnim(transform.position, CollectorAnimState.MoveToQueue, action);
        ColorCollector.RabbitRotateTransform.DORotate(new Vector3(0, 180, 0), 0.3f);
        ColorCollector.SetActiveCollector(false);
        ColorCollector.SetStatusAttributes();
    }

    public void DeUpdateText()
    {
        bulletDisplayHandler?.SetUpdateText(false);
    }


    public bool CanMove()
    {
        // Cho phép di chuyển nếu đang ở queue hoặc là index 0 của cột
        return State == CollectorState.InDeadQueue || State == CollectorState.InQueue || (State == CollectorState.InColumn && IndexInColumn == 0);
    }

    public Vector3 GetStartPosition()
    {
        return movementHandle.GetPositionAtTF(0f);
    }

    public Vector3 GetPositionByTF(float tf)
    {
        return movementHandle.GetPositionAtTF(tf);
    }

    public float GetCurrentTF()
    {
        return movementHandle.currentTF;
    }

    public void MoveToPos(Vector3 newPos, CollectorAnimState animState, Action actionOnComplete = null)
    {
        anim.PlayAnim(newPos, animState, actionOnComplete);
    }

    private void HandleCompleteColorPixelsDirect()
    {
        // Only trigger completion when all connected collectors have BulletLeft <= 0
        if (collectorConnect == null || collectorConnect.Count == 0)
            return;

        // Check that every connected collector (skip nulls and collectors with null colorPixels)
        for (int i = 0; i < collectorConnect.Count; i++)
        {
            var connectCollector = collectorConnect[i];
            if (connectCollector == null) continue;
            if (connectCollector.ColorCollector == null) continue;
            if (connectCollector.ColorCollector.BulletLeft > 0)
            {
                // At least one still has bullets left -> do nothing
                return;
            }
        }

        // All have BulletLeft <= 0 -> invoke completion on each (skip nulls)
        for (int i = 0; i < collectorConnect.Count; i++)
        {
            var connectCollector = collectorConnect[i];
            if (connectCollector == null) continue;
            connectCollector.OnCompletePixel();
        }
    }

    public void HandleCompleteColorPixels()
    {
        HandleCompleteColorPixelsDirect();
    }

    private bool hasCompleted = false;
    public void OnCompletePixel()
    {
        if (hasCompleted) return;
        hasCompleted = true;

        if (State == CollectorState.Moving)
        {
            GameplayEventsManager.CompleteColor?.Invoke(this);
        }
        // else
        // {
        //     GameplayEventsManager.OnCompleteDead?.Invoke(this);
        // }
        State = CollectorState.Completed;
        ColumnIndex = -1;
        anim.PlayAnim(Vector3One, CollectorAnimState.CompleteColorPixels, null);
        ColorCollector.IsHided = true;
    }

    public void UnlockCollector()
    {
        State = CollectorState.Completed;
        anim.PlayAnim(Vector3One, CollectorAnimState.UnlockLockObject, null);
    }

    public void SetFadeBulletText(bool isFaded)
    {
        bulletDisplayHandler?.SetFadeText(isFaded);
    }

    public void UpdateVisiblityBasedOnRow()
    {
        ColorCollector?.VisualHandler?.SetVisisble(IndexInColumn <= 4);
    }

    #region _absolute win
    public void OnAbsoluteWin()
    {
        //if (collectorConnect.Count > 1)
        //{
        //    bool hasNonMovingCollector = false;
        //    for (int i = 0; i < collectorConnect.Count; i++)
        //    {
        //        if (collectorConnect[i] != null && collectorConnect[i].State != CollectorState.Moving)
        //        {
        //            hasNonMovingCollector = true;
        //            break;
        //        }
        //    }
        //    if (hasNonMovingCollector)
        //    {
        //        return;
        //    }
        //}
        ColorCollector?.OnAbsoluteWin(State == CollectorState.Moving);
    }
    #endregion

    private void OnDisable()
    {
        GameplayEventsManager.ForceRemoveCollectorActive?.Invoke(this);
    }
}

public enum CollectorState
{
    InColumn,
    InQueue,
    Moving,
    Completed,
    InDeadQueue
}
