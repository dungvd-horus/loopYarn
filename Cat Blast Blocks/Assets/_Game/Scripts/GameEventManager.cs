// using System;
// using UnityEngine;
// using UnityEngine.Events;
//
// #pragma warning disable
// public static class GameEventManager
// {
//     #region IAP
//
//     public static Action<IAPProduct> OnPurchaseSuccess;
//
//     #endregion
//
//     #region SoundManager
//
//     public static UnityEvent<float> OnChangeMainSound = new UnityEvent<float>();
//     public static UnityEvent<float> OnChangeSensitivity = new UnityEvent<float>();
//     public static UnityEvent<float> OnChangeSound = new UnityEvent<float>();
//     public static UnityEvent<float> OnChangeSoundFx = new UnityEvent<float>();
//     public static UnityEvent<bool> OnEnableSoundFxIngame = new UnityEvent<bool>();
//     public static UnityEvent<bool> OnSetBoolEndGameUI = new UnityEvent<bool>();
//
//     #endregion
//
//     #region LayerManager
//
//     //public static Action<ShowLayerData> OnShowLayerComplete;
//     //public static Action<ShowLayerData> OnPrepareHideLayer;
//     public static Action OnQuitToMainMenu;
//
//     public static Action OnMainmenuInit;
//     #endregion
//
//     #region Response Loading
//
//     public static Action<bool> ShowResponseLoading;
//
//     #endregion
//
//     #region BottomBarLayer
//
//     public static Action<BottomBarTabType, bool> SetBottomBarTabActive;
//
//     #endregion
//
//     #region GamePlay Event
//     public static Action<GameState> OnGameStateChange;
//     public static Action<bool> OnEndGameAction;
//     public static Action<bool> SetupObjectSpawnActive;
//     public static Action<bool> SetupObjectTargetActive;
//     public static Action<bool> OnGoldChange;
//     public static Action OnGainGold;
//     public static Action<int> OnBoosterChange;
//     public static Action<RewardItem, Vector3, bool> OnReceiveBoosterFromBundle;
//     public static Action<RewardType, Vector3> OnReceiveBoosterFromPopUpBuyMore;
//     public static Action OnWinThisLevel;
//     public static Action OnLostThisLevel;
//
//     public static Action OnInstanceNewLevel;
//     public static Action OnNewLevelLoaded;
//     public static Action<int> OnNewLevelLoadStart;
//     public static Action ShowPopupRevive;
//     public static Action OnLoadLevelDone;
//
//     public static Action<float> ChangeCameraFOVThroughButton;
//     public static Action ReCenterModelThroughButton;
//
//     public static Action OnLevelCleared;
//     public static Action OnLevelFailed;
//
//     public static Action<int> OnWinStreakChange;
//     public static Action<int> OnMaxWinStreakChange;
//
//     #endregion
//
//     #region UI
//     public static Action OnEndGameUIActive;
//     public static Action OnEndGameUIDeactive;
//     public static Action OnModelStopRotatingInEndgameUI;
//     public static Action OnBossWarningUIHide;
//     #endregion
//
//     #region Profile
//
//     public static Action<int> OnAvatarSelected;
//     public static Action<string> OnChangeNameSuccess;
//     public static Action<int> OnChangeAvatarSuccess;
//     public static Action<string> OnLanguageSelected;
//     public static Action<string> OnLanguageChanged;
//     public static Action OnHeartDataChange;
//
//     #endregion
//
//     #region DynamicDifficulty
//
//     public static Action OnUseSawAdsBooster;
//     public static Action OnUseSaveBooster;
//     public static Action OnSaveBooster;
//     public static Action OnReceiveBooster;
//
//     #endregion
//
//     #region Intro
//     public static Action OnIntroStart;
//     public static Action<bool> OnIntroComplete;
//     #endregion
//
//     #region Daily Gift
//
//     public static Action<bool> OnActiveNotifyDailyGift;
//     public static Action<bool> OnDailyGiftActive;
//     public static Action<int> TestNextDayDailyGift;
//     public static Action OnCountDownFinished;
//
//     #endregion
//
//     #region CURRENCY LAYER
//
//     #region gold fly effect
//     public static Action<int> OnFirstCoinArriveAtTopBar;
//     #endregion
//
//     #endregion
//
//     public static Action OnOfferPackClosed;
//
//     public static Action<string> OnOfferPackOpened;
//     public static Action OnMeshCountClickChange;
//     public static Action IgnoreLastPack;
//     public static Action CancelIgnoreLastPackShop;
//
//     public static Action<string> OnReceivePackInfoFromStore;
//
//     public static Action<LayerType> OnHideLayer;
//     public static Action<LayerType> OnShowLayer;
//
//     #region PreLose
//     public static Action<bool> PlayAnimPreLose;
//     public static Action<RewardType> RePlayAnimPreLoseBooster;
//     #endregion
//
//     #region Direct booster
//     public static Action OnExecuteBroomBoosterDirectly;
//     public static Action OnExecuteRedoBoosterDirectly;
//     public static Action OnExecuteVacuumBoosterDirectly;
//     public static Action OnExecuteAddHoleBoosterDirectly;
//     #endregion
// }