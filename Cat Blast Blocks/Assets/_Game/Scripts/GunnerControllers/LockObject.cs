using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LockObject : CollectorMachanicObjectBase
{
    public int Row;
    public bool IsUnlocked = false;
    [SerializeField] private CollectorController collectorController;

    private void Awake()
    {
        IsUnlocked = false;
    }

    public void Unlock()
    {
        IsUnlocked = true;
    }

    [ContextMenu("UnlockWithVisual")]
    public void UnlockWithVisual()
    {
        IsUnlocked = true;
        GameplayEventsManager.OnUnlockLockObject?.Invoke(collectorController);
        collectorController.UnlockCollector();
    }
}