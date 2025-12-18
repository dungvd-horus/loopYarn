using System;
using UnityEngine;

#pragma warning disable
public static class GameplayEventsManager
{
    #region _grid
    public static Action<PaintingPixel> OnAPixelDestroyed;
    public static Action<PaintingPixel, ColorPixelsCollectorObject> OnAPixelDestroyedByCollector;
    public static Action<PaintingGridObject> OnGridObjectChanged;
    public static Action<PaintingGridObject> OnPaintingInitializeDone;
    #endregion

    #region _mechanic
    public static Action OnCollectAKey;
    public static Action OnAPipePixelDestroyed;
    public static Action OnAKeyReadyToBeCollected;
    public static Action<string> OnSuperRabbitBlockSelected;
    public static Action<CollectorController> OnFreePickACollector;
    #endregion

    #region _collectors
    public static Action<int> OnGridPixelsDestroyedPassive;
    public static Action<CollectorController> CompleteColor;
    public static Action<CollectorController, bool> OnClickACollector;
    public static Action<CollectorController> OnUnlockLockObject;
    public static Action<CollectorController> OnCollectorStartMove;
    public static Action<CollectorController> OnACollectorFinishMove;
    public static Action<CollectorController> OnACollectorMoveToQueue;
    public static Action<CollectorController> OnCollectorMoveToConveyor;
    public static Action<ColorPixelsCollectorObject> OnCollectorRevealed;
    public static Action<CollectorColumnController> OnCollectorsSquadChanged;
    public static Action<CollectorController, bool> OnCollectorMoveToFirstLine;
    public static Action<CollectorController> ForceRemoveCollectorActive;
    public static Action<bool> PauseGame;
    #endregion

    #region _booster
    public static Action<int> OnExecuteBooster;
    public static Action<int> OnExecuteBoosterNeedToHideUI;
    public static Action<int> OnEndExecuteBoosterNeedToHideUI;
    public static Action OnCancelExecuteBooster;
    #endregion

    #region _effect
    public static Action OnBlockDissapear;
    #endregion

    public static Action OnAbsoluteWin;

    public static Action<bool> OnEndGame;
    public static Action<bool> OnBlockGamePlayInput;


    public static Action ShowPopupRevive;
    //public static Action OnCollectorDead;
    public static Func<Transform> GetFirstCollector;

    public static void InstallFullGame()
    {

        Luna.Unity.Playable.InstallFullGame();
        Luna.Unity.LifeCycle.GameEnded();
    }
}
