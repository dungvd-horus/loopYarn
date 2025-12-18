using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif
public class GameplayManager : MonoBehaviour
{
    [SerializeField] private LevelGamePlayConfigSO levelGamePlayConfig;
    [SerializeField] private LevelConfigSetup levelSetup;
    [SerializeField] private LevelCollectorsSystem levelCollectorsSystem;
    [SerializeField] private LevelConfig currentLevelConfig;
    [SerializeField] private int levelId = 0;
    public int LevelId => levelId;
    [Header("Playable Store Config")]
    [SerializeField] private bool enableStoreLimitForFirstLevel = false;
    public bool IsStoreLimitForFirstLevelEnabled => enableStoreLimitForFirstLevel;

    // giống IsFirstLevelAndNotLast() bên GamePlayManagerExample
    public bool IsFirstLevelAndNotLast()
    {
        if (levelGamePlayConfig == null ||
            levelGamePlayConfig.levelConfigDataList == null ||
            levelGamePlayConfig.levelConfigDataList.Count == 0)
            return false;

        return levelId == 0 && levelGamePlayConfig.levelConfigDataList.Count > 1;
    }

    [ContextMenu("Start Game")]
    private void StartGame()
    {
        currentLevelConfig = levelGamePlayConfig.GetLevelConfigData(levelId);
        levelSetup.LoadLevel(currentLevelConfig);
        GameplayEventsManager.OnEndGame += OnEndGameAction;
        Luna.Unity.LifeCycle.GameStarted();
    }

    private void OnEnable()
    {
        StartGame();
    }

    private void Start()
    {
        GameplayEventsManager.CompleteColor += OnCollectorDead;
    }

    private void OnDestroy()
    {
        GameplayEventsManager.OnEndGame -= OnEndGameAction;
        GameplayEventsManager.CompleteColor -= OnCollectorDead;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnEndGameAction(true);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            OnEndGameAction(false);
        }
    }
#endif

    private void OnEndGameAction(bool isWin)
    {
        Debug.Log("End Game: " + isWin);
        // var data = new EndGameLayerData()
        // {
        //     IsWinGame = isWin
        // };
        // LayerManager.Instance.ShowEndgameLayer(data);
    }

    private void OnCollectorDead(CollectorController collectors)
    {
        levelCollectorsSystem.CurrentTotalCollectors--;

        // Điều kiện thắng tuyệt đối (giữ nguyên)
        if (levelCollectorsSystem.CurrentTotalCollectors <= CollectorGameManager.Instance.moveLimiter.GetMaxActiveMoving())
        {
            GameplayEventsManager.OnAbsoluteWin?.Invoke();
        }

        // --- LOGIC CHUYỂN ROUND ---

        // Nếu hết collector trong round hiện tại
        if (levelCollectorsSystem.CurrentTotalCollectors <= 0)
        {
            bool isLastRound = levelId >= levelGamePlayConfig.levelConfigDataList.Count - 1;

            if (isLastRound)
            {
                // Đây là round cuối -> End Game
                Debug.Log("All Collectors Completed FINAL Round, End Game");

                this.DelaySeconds(1f, () =>
                {
                    UiEndGame.Instance.ShowEndGameWin();
                    Luna.Unity.LifeCycle.GameEnded();
                    Luna.Unity.Playable.InstallFullGame();
                });
            }

            else
            {
                // Chưa phải round cuối -> sang round tiếp theo
                levelId++;
                Debug.Log("Completed Round " + (levelId - 1) + " -> Start Round " + levelId);

                // Use the level transition tween to smoothly cover and reveal during level loading
                UiEndGame.Instance.StartLevelTransition(() =>
                {
                    StartGame(); // Load round mới
                });
            }
        }
    }


}
