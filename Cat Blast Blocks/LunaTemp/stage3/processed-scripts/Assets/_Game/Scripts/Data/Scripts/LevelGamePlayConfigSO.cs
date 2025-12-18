using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects/LevelConfigGamePlay", menuName = "ScriptableObjects/LevelGamePlayConfig")]
public class LevelGamePlayConfigSO : ScriptableObject
{
    [SerializeField] public List<LevelConfig> levelConfigDataList;
    public LevelConfig GetLevelConfigData(int levelId)
    {
        if (levelId < 0 || levelId >= levelConfigDataList.Count)
        {
            Debug.LogError("Level ID is out of range: " + levelId + ". Returning first level config.");
            return levelConfigDataList[0];
        }

        return levelConfigDataList[levelId]; // Không còn -1
    }


    public int GetLevelToLoad(int playerCurrentLevel)
    {
        int levelCount = levelConfigDataList.Count;

        // Player level tính từ 1 nên convert về index 0-based
        int levelToLoad = playerCurrentLevel - 1;

        if (levelToLoad < 0) levelToLoad = 0;

        // Loop từ level cuối
        int loopStart = levelCount - 1;

        if (levelToLoad >= levelCount)
        {
            levelToLoad = loopStart + (levelToLoad - loopStart) % 1;
        }

        return levelToLoad;
    }

}
