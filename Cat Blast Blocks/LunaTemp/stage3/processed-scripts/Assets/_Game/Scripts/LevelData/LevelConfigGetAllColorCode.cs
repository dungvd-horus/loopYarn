using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelConfigGetAllColorCode : MonoBehaviour
{
    public List<LevelConfig> Levels;
    [ContextMenu("Extract Color Codes")]
    public void ExtractColorCodes()
    {
        foreach(var level in Levels)
        {
            if (level.BlocksPaintingConfig != null)
            {
                level.ColorsUsed = level.BlocksPaintingConfig.Pixels.Select(p => p.colorCode).Distinct().ToList();
                #if UNITY_EDITOR
                EditorUtility.SetDirty(level);
                #endif
            }
        }
    }
}
