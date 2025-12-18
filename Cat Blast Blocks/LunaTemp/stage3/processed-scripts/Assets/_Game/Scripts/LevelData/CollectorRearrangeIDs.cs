using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CollectorRearrangeIDs : MonoBehaviour
{
    public List<LevelColorCollectorsConfig> CurrentCollectorsConfigs;
#if UNITY_EDITOR
    [ContextMenu("Rearrange IDs")]
    public void RearrangeIDs()
    {
        foreach (var CurrentCollectorsConfig in CurrentCollectorsConfigs)
        {
            CurrentCollectorsConfig.ReArrangeID();
            CurrentCollectorsConfig.EnsureBidirectionalConnections();
            CurrentCollectorsConfig.CreateBackUp();
            UnityEditor.EditorUtility.SetDirty(CurrentCollectorsConfig);
        }
        UnityEditor.AssetDatabase.SaveAssets();
    }
#endif
}
