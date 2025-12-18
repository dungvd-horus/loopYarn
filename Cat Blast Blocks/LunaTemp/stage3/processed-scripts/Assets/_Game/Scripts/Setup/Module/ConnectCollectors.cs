
using UnityEngine;

public class ConnectCollectors : MonoBehaviour
{
    public LevelCollectorsConfigSetup LevelCollectorsSetup;
    public int ConnectIndexOne;
    public int ConnectIndexTwo;
    public ColorPixelsCollectorObject Gunner0ne;
    public ColorPixelsCollectorObject GunnerTwo;

    private void OnValidate()
    {
        if (LevelCollectorsSetup == null) return;
        if (LevelCollectorsSetup.previewSystem.CurrentCollectors.Count <= 0) return;
        if (ConnectIndexOne < 0 || ConnectIndexOne >= LevelCollectorsSetup.previewSystem.CurrentCollectors.Count) return;
        if (ConnectIndexTwo < 0 || ConnectIndexTwo >= LevelCollectorsSetup.previewSystem.CurrentCollectors.Count) return;
        Gunner0ne = LevelCollectorsSetup.previewSystem.CurrentCollectors[ConnectIndexOne];
        GunnerTwo = LevelCollectorsSetup.previewSystem.CurrentCollectors[ConnectIndexTwo];
    }

    [ContextMenu("CONNECT")]
    public void Connect()
    {
        if (Gunner0ne == null || GunnerTwo == null) return;

        if (Gunner0ne.ID == GunnerTwo.ID) return;

        if (!Gunner0ne.ConnectedCollectorsIDs.Contains(GunnerTwo.ID)) Gunner0ne.ConnectedCollectorsIDs.Add(GunnerTwo.ID);
        if (!GunnerTwo.ConnectedCollectorsIDs.Contains(Gunner0ne.ID)) GunnerTwo.ConnectedCollectorsIDs.Add(Gunner0ne.ID);

        Gunner0ne.VisualHandler.RefreshColor();
        GunnerTwo.VisualHandler.RefreshColor();

        LevelCollectorsSetup.ImportCollectorsFromScene();
        LevelCollectorsSetup.EnsureBidirectionalConnections();
        LevelCollectorsSetup.previewSystem.SetupConnectedCollectors();
    }

    public void Connect(ColorPixelsCollectorObject first, ColorPixelsCollectorObject second)
    {
        if (first == null || second == null) return;

        if (first.ID == second.ID) return;

        bool alreadyConnected = first.ConnectedCollectorsIDs.Contains(second.ID) || second.ConnectedCollectorsIDs.Contains(first.ID);

        if (alreadyConnected)
        {
            if (first.ConnectedCollectorsIDs.Contains(second.ID)) first.ConnectedCollectorsIDs.Remove(second.ID);
            if (second.ConnectedCollectorsIDs.Contains(first.ID)) second.ConnectedCollectorsIDs.Remove(first.ID);
        }
        else
        {
            if (!first.ConnectedCollectorsIDs.Contains(second.ID)) first.ConnectedCollectorsIDs.Add(second.ID);
            if (!second.ConnectedCollectorsIDs.Contains(first.ID)) second.ConnectedCollectorsIDs.Add(first.ID);
        }

        first.VisualHandler.RefreshColor();
        second.VisualHandler.RefreshColor();

        LevelCollectorsSetup.ImportCollectorsFromScene();
        LevelCollectorsSetup.EnsureBidirectionalConnections();
        LevelCollectorsSetup.previewSystem.SetupConnectedCollectors();
    }
}
