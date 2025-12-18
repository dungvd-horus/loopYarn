 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAdvancedSetup : MonoBehaviour
{
    [Header("MODULE(s)")]
    public LevelConfigSetup LevelManager;
    public PaintingConfigSetup PaintingSetup;
    public KeyObjectConfigSetup KeySetupModule;
    public PipeObjectConfigSetup PipeSetupModule;
    public PaintingLayoutAdjustModule PaintModule;
    public BigBlockObjectConfigSetup WallSetupModule;
    public LevelCollectorsConfigSetup CollectorSetupModule;

    [Header("TOOL(s)")]
    public bool ToolActive = false;
    public LayerMask BlockObjectLayermask;
    public List<PaintingPixelComponent> SelectedItems = new List<PaintingPixelComponent>();
    public Color BGColor;
    [Header("TOOL RUNTIME DATA: PIPE")]
    public int currentSelectedColorCode = 0;

    public int heartInput;
#if UNITY_EDITOR
    [ContextMenu("ACTIVE TOOL")]
    public void SetToolActive()
    {
        ToolActive = !ToolActive;
        if (ToolActive)
        {
            SelectedItems.Clear();
            LevelManager.LoadLevel();
        }
    }
#endif
}
