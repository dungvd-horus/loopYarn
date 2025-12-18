using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class PaintingSharedAttributes
{
    public static string LockKeyColorDefine = "KeyColor";
    public static string DefaultColorKey = "DefaultColor";
    public static string TransparentColorKey = "TransparentColor";

    public static string LevelConfigPath = "Assets/_Game/Data/LevelConfig/";
    public static string CollectorsConfigPath = "Assets/_Game/Data/GunnerConfig/";
    public static string PaintingConfigPath = "Assets/_Game/Data/PaintingConfig/";
    public static string IndestructibleColorKey = "IndestructibleColor";
    public static void MoveRelative<T>(List<T> list, T itemToMove, T targetItem, bool higher)
    {
        if (list == null || itemToMove == null || targetItem == null)
            return;

        // Nếu 2 phần tử giống nhau thì không làm gì
        if (EqualityComparer<T>.Default.Equals(itemToMove, targetItem))
            return;

        // Lấy index hiện tại của item và target
        int currentIndex = list.IndexOf(itemToMove);
        int targetIndex = list.IndexOf(targetItem);

        // Nếu không tìm thấy một trong hai thì bỏ qua
        if (currentIndex == -1 || targetIndex == -1)
            return;

        // Bỏ item ra khỏi list
        list.RemoveAt(currentIndex);

        // Nếu item nằm TRƯỚC target ban đầu, thì index của target đã thay đổi sau khi Remove
        if (currentIndex < targetIndex)
            targetIndex--;

        // Tính index mới để chèn vào
        int newIndex = higher ? targetIndex : targetIndex + 1;

        // Giới hạn index trong phạm vi hợp lệ
        newIndex = Mathf.Clamp(newIndex, 0, list.Count);

        // Chèn lại item
        list.Insert(newIndex, itemToMove);
    }

    public static void InsertRelative<T>(List<T> list, T newItem, T targetItem, bool higher)
    {
        if (list == null || newItem == null || targetItem == null)
            return;

        int targetIndex = list.IndexOf(targetItem);
        if (targetIndex == -1)
        {
            // Nếu targetItem không có trong list, thêm vào cuối danh sách
            list.Add(newItem);
            return;
        }

        int insertIndex = higher ? targetIndex : targetIndex + 1;
        insertIndex = Mathf.Clamp(insertIndex, 0, list.Count);

        list.Insert(insertIndex, newItem);
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

    #region _custom attributes
    [System.Serializable]
    public class IntPixelListPair
    {
        public int key;
        public List<PaintingPixel> pixels;

        public IntPixelListPair(int key, List<PaintingPixel> pixels)
        {
            this.key = key;
            this.pixels = pixels;
        }
    }

    [System.Serializable]
    public class ColorPixelListPair
    {
        public string ColorCode;
        public List<PaintingPixel> pixels;

        public ColorPixelListPair(string colorCode, List<PaintingPixel> pixels)
        {
            this.ColorCode = colorCode;
            this.pixels = pixels;
        }
    }

    #endregion

    #region _config
#if UNITY_EDITOR
    public static LevelConfig GetLevelConfig(Sprite painting)
    {
        string assetPath = LevelConfigPath + painting.name + "_LevelConfig" + ".asset";
        var existingAsset = AssetDatabase.LoadAssetAtPath<LevelConfig>(assetPath);
        return existingAsset;
    }

    public static LevelColorCollectorsConfig GetCollectorConfig(string configName)
    {
        string assetPath = CollectorsConfigPath + configName + "_CollectorConfig" + ".asset";
        var existingAsset = AssetDatabase.LoadAssetAtPath<LevelColorCollectorsConfig>(assetPath);
        return existingAsset;
    }

    public static PaintingConfig GetPaintingConfig(Sprite painting)
    {
        string assetPath = PaintingConfigPath + painting.name + "_PaintingConfig" + ".asset";
        var existingAsset = AssetDatabase.LoadAssetAtPath<PaintingConfig>(assetPath);
        return existingAsset;
    }
#endif
    #endregion

    #region _extension methods
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    #endregion

    #region BlockFountain Data Classes

    [System.Serializable]
    public class BlockFountainObjectSetup
    {
        public List<BlockFountainBulletSet> BlockSets = new List<BlockFountainBulletSet>();
        public List<PaintingPixelConfig> PixelCovered = new List<PaintingPixelConfig>();

        public BlockFountainObjectSetup() { }

        public BlockFountainObjectSetup(BlockFountainObjectSetup _stock)
        {
            BlockSets = new List<BlockFountainBulletSet>();
            PixelCovered = new List<PaintingPixelConfig>();
            if (_stock.BlockSets != null)
                foreach (var b in _stock.BlockSets)
                    BlockSets.Add(new BlockFountainBulletSet(b));
            if (_stock.PixelCovered != null)
                foreach (var p in _stock.PixelCovered)
                    PixelCovered.Add(new PaintingPixelConfig(p));
        }
    }

    [System.Serializable]
    public class BlockFountainBulletSet
    {
        public string ColorCode;
        public int BlockCount;

        public BlockFountainBulletSet() { }
        public BlockFountainBulletSet(BlockFountainBulletSet _stock)
        {
            ColorCode = _stock.ColorCode;
            BlockCount = _stock.BlockCount;
        }
    }

    #endregion
}
