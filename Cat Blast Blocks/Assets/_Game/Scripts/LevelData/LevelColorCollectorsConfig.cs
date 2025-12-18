using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelColorCollectorsConfig", menuName = "ColorPixelFlow/Level Color Collectors Config", order = 2)]
public class LevelColorCollectorsConfig : ScriptableObject
{
    [Header("Collector Squad Configuration")]
    public List<ColumnOfCollectorConfig> CollectorColumns; // list of SingleColorCollectorObject in a level

    public int NumberOfColumns()
    {
        return CollectorColumns.Count;
    }

    public int NumberOfCollectors()
    {
        int num = 0;
        foreach (var col in CollectorColumns)
        {
            num += col.Collectors.Count;
        }
        return num;
    }

    public List<SingleColorCollectorConfig> GetAllCollectorConfigs()
    {
        List<SingleColorCollectorConfig> rs = new List<SingleColorCollectorConfig>();
        int maxRow = 0;
        foreach (var col in CollectorColumns)
        {
            if (col.Collectors.Count > maxRow)
            {
                maxRow = col.Collectors.Count;
            }
        }

        for (int i = 0; i < maxRow; i++)
        {
            foreach (var col in CollectorColumns)
            {
                if (i < col.Collectors.Count)
                {
                    rs.Add(col.Collectors[i]);
                }
            }
        }

        return rs;
    }

    public void RemoveCollector(int id)
    {
        foreach (var col in CollectorColumns)
        {
            col.Collectors.RemoveAll(c => c.ID == id);
        }
        ReArrangeID();
    }

    public void ReArrangeID()
    {
        var rs = GetAllCollectorConfigs();
        // Create a map oldID -> newID (index in list)
        Dictionary<int, int> idMap = new Dictionary<int, int>();

        for (int i = 0; i < rs.Count; i++)
        {
            idMap[rs[i].ID] = i;
        }

        // Update IDs to new ids
        for (int i = 0; i < rs.Count; i++)
        {
            rs[i].ID = i;
        }

        // Update ConnectedIDs according to the map
        foreach (var obj in rs)
        {
            for (int j = 0; j < obj.ConnectedCollectorsIDs.Count; j++)
            {
                int oldID = obj.ConnectedCollectorsIDs[j];
                obj.ConnectedCollectorsIDs[j] = idMap[oldID];
            }
        }
        for (int i = 0; i < rs.Count; i++)
        {
            rs[i].ID = i;
        }
    }
    public void EnsureBidirectionalConnections()
    {
        List<SingleColorCollectorConfig> allCollectorConfig = new List<SingleColorCollectorConfig>();

        foreach (ColumnOfCollectorConfig column in CollectorColumns)
        {
            allCollectorConfig.AddRange(column.Collectors);
        }

        if (allCollectorConfig.Count <= 0) return;

        // Iterate through each collector in the config
        for (int i = 0; i < allCollectorConfig.Count; i++)
        {
            SingleColorCollectorConfig collector = allCollectorConfig[i];

            // For each connection in this collector, ensure the reverse connection exists
            foreach (int connectedID in collector.ConnectedCollectorsIDs)
            {
                if (connectedID == collector.ID) continue;
                if (connectedID >= 0)
                {
                    SingleColorCollectorConfig targetCollector = GetCollectorConfigByID(allCollectorConfig, connectedID);
                    if (targetCollector == null || targetCollector.ID == collector.ID) return;

                    // If the target collector doesn't have this collector in its connections, add it
                    if (!targetCollector.ConnectedCollectorsIDs.Contains(collector.ID))
                    {
                        targetCollector.ConnectedCollectorsIDs.Add(collector.ID);
                        Debug.Log($"Added reverse connection: Collector {connectedID} now connected to ID {collector.ID}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid connection index {connectedID} in collector {i}");
                }
            }
        }

        Debug.Log($"Ensured bidirectional connections for {allCollectorConfig.Count} collectors in {allCollectorConfig}");
    }
    public int NumberOfLocks()
    {
        int num = 0;
        foreach (var col in CollectorColumns)
        {
            num += col.Locks.Count;
        }
        return num;
    }
    public static SingleColorCollectorConfig GetCollectorConfigByID(List<SingleColorCollectorConfig> _list, int ID)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i].ID == ID)
            {
                return _list[i];
            }
        }
        return null;
    }
    public void ClearData()
    {
        CollectorColumns = new List<ColumnOfCollectorConfig>();
    }

    #region BACK UP
#if UNITY_EDITOR
    public List<LevelColorCollectorsConfigBackUp> BackUpVariants = new List<LevelColorCollectorsConfigBackUp>();
    public int CurrentBackUpIndex = -1;

    [ContextMenu("Save")]
    public void Save()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    [ContextMenu("Create Backup")]
    public void CreateBackUp()
    {
        if (BackUpVariants.Count >= 30)
        {
            BackUpVariants.RemoveAt(0);
        }
        LevelColorCollectorsConfigBackUp newBackUp = new LevelColorCollectorsConfigBackUp(this);
        BackUpVariants.Add(newBackUp);
    }

    [ContextMenu("Restore Backup")]
    public void RestoreBackup()
    {
        if (BackUpVariants.Count == 0) return;
        CurrentBackUpIndex--;
        CurrentBackUpIndex = Mathf.Clamp(CurrentBackUpIndex, 0, BackUpVariants.Count - 1);
        LevelColorCollectorsConfigBackUp _backup = BackUpVariants[CurrentBackUpIndex];
        this.ClearData();

        foreach( var col in _backup.CollectorColumns)
        {
            this.CollectorColumns.Add(new ColumnOfCollectorConfig(col));
        }
    }

    [ContextMenu("Restore Backup Forward")]
    public void RestoreBackupForward()
    {
        if (BackUpVariants.Count == 0) return;
        CurrentBackUpIndex++;
        CurrentBackUpIndex = Mathf.Clamp(CurrentBackUpIndex, 0, BackUpVariants.Count - 1);
        LevelColorCollectorsConfigBackUp _backup = BackUpVariants[CurrentBackUpIndex];
        this.ClearData();

        foreach( var col in _backup.CollectorColumns)
        {
            this.CollectorColumns.Add(new ColumnOfCollectorConfig(col));
        }
    }

    [ContextMenu("Clear Backups")]
    public void ClearBackUps()
    {
        CurrentBackUpIndex = -1;
        BackUpVariants.Clear();
    }
#endif
    #endregion
}

[Serializable]
public class ColumnOfCollectorConfig
{
    public List<SingleColorCollectorConfig> Collectors;
    public List<LockObjectConfig> Locks;

    public ColumnOfCollectorConfig()
    {
        Collectors = new List<SingleColorCollectorConfig>();
        Locks = new List<LockObjectConfig>();
    }

    public ColumnOfCollectorConfig(ColumnOfCollectorConfig _stock)
    {
        this.Collectors = new List<SingleColorCollectorConfig>();
        foreach (var c in _stock.Collectors)
        {
            this.Collectors.Add(new SingleColorCollectorConfig(c));
        }

        this.Locks = new List<LockObjectConfig>();
        foreach (var l in _stock.Locks)
        {
            LockObjectConfig newLock = new LockObjectConfig();
            newLock.ID = l.ID;
            newLock.Row = l.Row;
            this.Locks.Add(newLock);
        }
    }
}

[Serializable]
public class LevelColorCollectorsConfigBackUp
{
    public string DateTime;
    public List<ColumnOfCollectorConfig> CollectorColumns;

    public LevelColorCollectorsConfigBackUp(LevelColorCollectorsConfig _stock)
    {
        DateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        this.CollectorColumns = new List<ColumnOfCollectorConfig>();
        foreach (var c in _stock.CollectorColumns)
        {
            this.CollectorColumns.Add(new ColumnOfCollectorConfig(c));
        }
    }
}
