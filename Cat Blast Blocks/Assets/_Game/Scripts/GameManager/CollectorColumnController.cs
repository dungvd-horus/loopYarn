using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorColumnController : MonoBehaviour
{
    [SerializeField] private LevelCollectorsSystem collectorsSystem;

    // Cache commonly accessed properties for performance
    private Transform _cachedFormationCenter;
    private float _cachedSpaceBetweenColumns;
    private float _cachedSpaceBetweenCollectors;
    private Vector3 _cachedCollectorRotation;

    // Pre-allocated lists to reduce GC allocations
    private readonly List<CollectorController> _tempCollectorsToRemove = new List<CollectorController>();
    private readonly List<CollectorController> _tempCollectorsToDestroy = new List<CollectorController>();
    private readonly List<CollectorController> _tempActiveCollectors = new List<CollectorController>();
    private readonly List<CollectorController> _tempDeadCollectors = new List<CollectorController>();

    // Cache for string comparisons to avoid boxing/unboxing
    private static readonly Dictionary<string, string> _colorCodeCache = new Dictionary<string, string>();

    // Cached WaitForSeconds for coroutine reuse
    private static WaitForSeconds _cachedWaitForSeconds;

    private void Awake()
    {
        CacheSystemReferences();
        RegisterEvents();
    }

    private void CacheSystemReferences()
    {
        if (collectorsSystem != null)
        {
            _cachedFormationCenter = collectorsSystem.FormationCenter;
            _cachedSpaceBetweenColumns = collectorsSystem.SpaceBetweenColumns;
            _cachedSpaceBetweenCollectors = collectorsSystem.SpaceBetweenCollectors;
            _cachedCollectorRotation = collectorsSystem.CollectorRotation;
        }
    }

    private void RegisterEvents()
    {
        GameplayEventsManager.OnUnlockLockObject += OnCollectorStartMove;
        GameplayEventsManager.OnCollectorStartMove += OnCollectorStartMove;
        GameplayEventsManager.OnAKeyReadyToBeCollected += OnAKeyReadyToBeCollected;
    }

    private void OnCollectorStartMove(CollectorController collector)
    {
        if (collector.ColumnIndex >= 0)
        {
            collectorsSystem.CollectorControllersColumns[collector.ColumnIndex].Remove(collector);
            UpdatePosition(collectorsSystem.CollectorControllersColumns[collector.ColumnIndex]);
            collector.ColumnIndex = -1;
            collector.IndexInColumn = -1;
            UpdateHiddenCollectorsState();
        }
        GameplayEventsManager.OnCollectorsSquadChanged?.Invoke(this);
    }

    private void OnDestroy()
    {
        GameplayEventsManager.OnUnlockLockObject -= OnCollectorStartMove;
        GameplayEventsManager.OnCollectorStartMove -= OnCollectorStartMove;
        GameplayEventsManager.OnAKeyReadyToBeCollected -= OnAKeyReadyToBeCollected;
    }

    private void UpdatePosition(List<CollectorController> collectorsInColumn)
    {
        if (collectorsInColumn == null || collectorsInColumn.Count == 0) return;

        // Use cached values for better performance
        int columnIndex = collectorsInColumn[0].ColumnIndex;
        int columnCount = collectorsSystem.ObjectsInColumns.Count;
        Transform formationCenter = _cachedFormationCenter != null ? _cachedFormationCenter : collectorsSystem.FormationCenter;
        float spaceBetweenColumns = _cachedSpaceBetweenColumns;
        float spaceBetweenCollectors = _cachedSpaceBetweenCollectors;
        Vector3 collectorRotation = _cachedCollectorRotation;

        // Pre-calculate common values to avoid repeated calculations
        Vector3 startPosition = formationCenter.position;
        Vector3 columnOffset = (columnIndex - (columnCount - 1) / 2.0f) * spaceBetweenColumns * formationCenter.right;
        Vector3 basePosition = startPosition + columnOffset;
        Vector3 forwardOffset = formationCenter.forward * spaceBetweenCollectors;

        for (int i = 0; i < collectorsInColumn.Count; i++)
        {
            // Calculate position using pre-calculated values
            Vector3 spawnPosition = basePosition - (i * forwardOffset);
            var collector = collectorsInColumn[i];
            collector.MoveToPos(spawnPosition, CollectorAnimState.PushForward, null);
            collector.transform.localEulerAngles = collectorRotation;
            collector.IndexInColumn = i;
            if (i == 0)
            {
                collector.SetFadeBulletText(false);
            }
            //collector.UpdateVisiblityBasedOnRow();
        }
    }

    #region _key/lock mechanics
    public CollectorController GetLockReadyToUnlock()
    {
        var columns = collectorsSystem.CollectorControllersColumns;
        for (int i = 0; i < columns.Count; i++)
        {
            var col = columns[i];
            if (col.Count > 0)
            {
                var firstCollector = col[0];
                if (firstCollector.IsLockObject && !firstCollector.LockController.IsUnlocked)
                {
                    return firstCollector;
                }
            }
        }
        return null;
    }

    public void OnAKeyReadyToBeCollected()
    {
        //invoke this to make grid finding key (temporary)
        GameplayEventsManager.OnCollectorsSquadChanged?.Invoke(this);
    }
    #endregion

    #region hidden mechanic
    public void UpdateHiddenCollectorsState()
    {
        var columns = collectorsSystem.CollectorControllersColumns;
        for (int i = 0; i < columns.Count; i++)
        {
            var col = columns[i];
            if (col != null && col.Count > 0)
            {
                var firstCollector = col[0];
                if (!firstCollector.IsLockObject && firstCollector.ColorCollector.IsHidden)
                {
                    firstCollector.ColorCollector.Reveal();
                }
            }
        }
    }
    #endregion

    #region _super rabbit
    public void RemoveAllCollectorWithColor(string colorCode)
    {
        // Use cached color code to avoid string allocation
        if (!_colorCodeCache.ContainsKey(colorCode))
        {
            _colorCodeCache[colorCode] = colorCode;
        }
        string cachedColorCode = _colorCodeCache[colorCode];

        // Use cached lists to reduce GC allocations
        _tempCollectorsToRemove.Clear();
        _tempCollectorsToDestroy.Clear();

        // Process collectors in columns
        foreach (var col in collectorsSystem.CollectorControllersColumns)
        {
            if (col.Count <= 0) continue;
            foreach (CollectorController _collector in col)
            {
                if (_collector.IsLockObject) continue;

                bool sameColor = _collector.ColorCollector.CollectorColor == cachedColorCode;

                if (!sameColor) continue;

                bool hasConnect = _collector.collectorConnect != null && _collector.collectorConnect.Count > 1;

                if (hasConnect)
                {
                    bool connectStillProgress = false;
                    foreach (var _connector in _collector.collectorConnect)
                    {
                        if (_connector.ColorCollector.ID == _collector.ColorCollector.ID) continue;
                        if (_connector.ColorCollector.BulletLeft > 0 && _connector.ColorCollector.CollectorColor != cachedColorCode)
                        {
                            connectStillProgress = true;
                            break;
                        }
                    }

                    if (!connectStillProgress)
                    {
                        _tempCollectorsToRemove.AddRange(_collector.collectorConnect);
                    }
                    else
                    {
                        _collector.ColorCollector.SetBullet(0);
                    }
                }
                else _tempCollectorsToRemove.Add(_collector);
            }
        }

        // Process active moving controllers
        _tempActiveCollectors.Clear();
        _tempActiveCollectors.AddRange(CollectorGameManager.Instance.ActiveMovingControllers);
        foreach (var collector in _tempActiveCollectors)
        {
            if (collector.IsLockObject) continue;
            bool sameColor = collector.ColorCollector.CollectorColor == cachedColorCode;
            if (sameColor) _tempCollectorsToDestroy.Add(collector);
        }

        // Process dead collectors
        _tempDeadCollectors.Clear();
        _tempDeadCollectors.AddRange(CollectorGameManager.Instance.collectorOnDead);
        foreach (var collector in _tempDeadCollectors)
        {
            if (collector.IsLockObject) continue;
            bool sameColor = collector.ColorCollector.CollectorColor == cachedColorCode;
            if (sameColor) _tempCollectorsToDestroy.Add(collector);
        }

        // Destroy collectors to be destroyed
        foreach (var collector in _tempCollectorsToDestroy)
        {
            CollectorGameManager.Instance.collectorOnDead.Remove(collector);
            CollectorGameManager.Instance.ActiveMovingControllers.Remove(collector);
            collector.ColorCollector.SetBullet(0);
            collector.ColorCollector.SelfDestroy();
        }

        StartCoroutine(RemoveAndRePositionCollectors(_tempCollectorsToRemove));
    }

    private IEnumerator RemoveAndRePositionCollectors(List<CollectorController> collectorsToRemove)
    {
        // Use cached WaitForSeconds to avoid creating new objects
        if (_cachedWaitForSeconds == null)
        {
            _cachedWaitForSeconds = new WaitForSeconds(5f / 60f);
        }

        for (int i = 0; i < collectorsToRemove.Count; i++)
        {
            CollectorController collector = collectorsToRemove[i];
            collector.ColorCollector.SetBullet(0);
            GameplayEventsManager.OnCollectorStartMove.Invoke(collector);
            collector.gameObject.SetActive(false);
            yield return _cachedWaitForSeconds;
        }
    }
    #endregion

    #region _free pick
    public void HighlightRows(int row = 5, float min = 0f, float max = 1.5f, float duration = 0.5f)
    {
        var columns = collectorsSystem.CollectorControllersColumns;
        for (int colIdx = 0; colIdx < columns.Count; colIdx++)
        {
            var col = columns[colIdx];
            int count = Mathf.Min(col.Count, row);
            for (int i = 0; i < count; i++)
            {
                var colorCollector = col[i].ColorCollector;
                if (colorCollector != null && colorCollector.VisualHandler != null)
                {
                    colorCollector.VisualHandler.StartHighlight(min, max, duration);
                }
            }
        }
    }
    public void DeHighlightRows(int row = 5)
    {
        var columns = collectorsSystem.CollectorControllersColumns;
        for (int colIdx = 0; colIdx < columns.Count; colIdx++)
        {
            var col = columns[colIdx];
            int count = Mathf.Min(col.Count, row);
            for (int i = 0; i < count; i++)
            {
                var colorCollector = col[i].ColorCollector;
                if (colorCollector != null && colorCollector.VisualHandler != null)
                {
                    colorCollector.VisualHandler.StopHighlight();
                }
            }
        }
    }
    #endregion
}
