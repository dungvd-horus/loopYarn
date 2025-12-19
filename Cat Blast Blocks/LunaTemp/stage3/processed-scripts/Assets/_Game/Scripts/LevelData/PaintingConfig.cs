using static PaintingSharedAttributes;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "PaintingConfig", menuName = "ScriptableObjects/PaintingConfig", order = 1)]
public class PaintingConfig : ScriptableObject
{
    public Vector2 PaintingSize;

    public Sprite Sprite;

    public List<PaintingPixelConfig> Pixels = new List<PaintingPixelConfig>();

    [Header("SPECIAL OBJECT: PIPE")]
    public List<PipeObjectSetup> PipeSetups = new List<PipeObjectSetup>();

    [Header("SPECIAL OBJECT: WALL")]
    public List<WallObjectSetup> WallSetups = new List<WallObjectSetup>();

    [Header("SPECIAL OBJECT: KEY")]
    public List<KeyObjectSetup> KeySetups = new List<KeyObjectSetup>();

    [Header("SPECIAL OBJECT: ADDITION PIXELS")]
    public List<PaintingPixelConfig> AdditionPixels = new List<PaintingPixelConfig>();

    [Header("SPECIAL OBJECT: BLOCK FOUNTAIN")]
    public List<BlockFountainObjectSetup> BlockFountainSetup = new List<BlockFountainObjectSetup>();

    /// <summary>
    /// Sets the Hidden property to true for any PaintingPixelConfig in _pixels 
    /// that appears in any PipeObjectSetup's PixelCovered list based on matching row and column
    /// </summary>
    public void HidePixelsUnderPipes()
    {
        foreach (var pixelConfig in Pixels)
        {
            bool isPixelUnderPipe = false;
            
            // Check if this pixel config appears in any pipe setup
            foreach (var pipeSetup in PipeSetups)
            {
                foreach (var coveredPixel in pipeSetup.PixelCovered)
                {
                    if (pixelConfig.row == coveredPixel.row && pixelConfig.column == coveredPixel.column)
                    {
                        isPixelUnderPipe = true;
                        break;
                    }
                }
                
                if (isPixelUnderPipe)
                    break;
            }
            
            // If the pixel is covered by a pipe, set it as hidden
            if (isPixelUnderPipe)
            {
                pixelConfig.Hidden = true;
            }
        }
    }

    public void ReShowPixelsUnderPipe(PipeObject _pipe)
    {
        foreach (var pixelConfig in Pixels)
        {
            bool isPixelUnderPipe = false;

            // Check if this pixel config appears in any pipe setup
            foreach (var coveredPixel in _pipe.PaintingPixelsCovered)
            {
                if (pixelConfig.row == coveredPixel.row && pixelConfig.column == coveredPixel.column)
                {
                    coveredPixel.PixelComponent?.gameObject.SetActive(true);
                    coveredPixel.PixelComponent?.ShowVisualOnly();
                    isPixelUnderPipe = true;
                    break;
                }
            }

            // If the pixel is covered by a pipe, set it as hidden
            if (isPixelUnderPipe && !pixelConfig.colorCode.Equals(TransparentColorKey))
            {
                pixelConfig.Hidden = false;
            }
        }
    }

    public List<PaintingPixelConfig> GetAllWorkingPixels()
    {
        List<PaintingPixelConfig> allPixels = new List<PaintingPixelConfig>();

        // Add pixels from PaintingConfig.Pixels (convert from PaintingPixelConfig to PaintingPixel)
        foreach (var pixelConfig in Pixels)
        {
            if (pixelConfig.Hidden || pixelConfig.color.Equals(TransparentColorKey)) continue;
            allPixels.Add(pixelConfig);
        }

        // Add pixels from PipeSetups.PixelCovered
        foreach (var pipeSetup in PipeSetups)
        {
            foreach (var pixel in pipeSetup.PixelCovered)
            {
                if (allPixels.Any(x => (x.column == pixel.column && x.row == pixel.row)))
                {
                    allPixels.Remove(allPixels.First(x => (x.column == pixel.column && x.row == pixel.row)));
                }
            }
            for (int i = 0; i < pipeSetup.Hearts; i++)
            {
                int pixelCoveredCount = pipeSetup.PixelCovered.Count;
                PaintingPixelConfig _new = new PaintingPixelConfig(pipeSetup.PixelCovered[i % (pixelCoveredCount - 1)]);
                _new.colorCode = pipeSetup.ColorCode;
                allPixels.Add(_new);
            }
        }

        // Add pixels from PipeSetups.WallSetups
        foreach (var wallSetup in WallSetups)
        {
            int pixelCoveredCount = wallSetup.PixelCovered.Count;

            foreach (PaintingPixelConfig _p in wallSetup.PixelCovered)
            {
                if (allPixels.Any(x => (x.column == _p.column && x.row == _p.row)))
                {
                    allPixels.Remove(allPixels.First(x => (x.column == _p.column && x.row == _p.row)));
                }
            }

            for (int i = 0; i < wallSetup.Hearts; i++)
            {
                PaintingPixelConfig _new = new PaintingPixelConfig(wallSetup.PixelCovered[i % (pixelCoveredCount - 1)]);
                _new.colorCode = wallSetup.ColorCode;
                allPixels.Add(_new);
            }
        }

        // Add pixels from PipeSetups.KeySetups
        foreach (var keySetup in KeySetups)
        {
            int pixelCoveredCount = keySetup.PixelCovered.Count;

            foreach (PaintingPixelConfig _p in keySetup.PixelCovered)
            {
                if (allPixels.Any(x => (x.column == _p.column && x.row == _p.row)))
                {
                    allPixels.Remove(allPixels.First(x => (x.column == _p.column && x.row == _p.row)));
                }
            }
            //PaintingPixelConfig _key = new PaintingPixelConfig()
            //{
            //    column = keySetup.PixelCovered[0].column,
            //    row = keySetup.PixelCovered[0].row,
            //    color = keySetup.PixelCovered[0].color,
            //    colorCode = LockKeyColorDefine,
            //    Hidden = false
            //};
            //allPixels.Add(_key);
        }

        // Add pixels from PipeSetups.AdditionPixels
        foreach (var _p in AdditionPixels)
        {
            if (_p.Hidden || _p.color.Equals(TransparentColorKey)) continue;
            if (allPixels.Any(x => (x.column == _p.column && x.row == _p.row)))
            {
                allPixels.Remove(allPixels.First(x => (x.column == _p.column && x.row == _p.row)));
            }
            allPixels.Add(_p);
        }
        int tmpIndex = 9999;
        if (BlockFountainSetup != null)
        {
            foreach (var _fountain in BlockFountainSetup)
            {
                if (_fountain.BlockSets != null)
                {
                    foreach (var _blockset in _fountain.BlockSets)
                    {
                        if (string.IsNullOrEmpty(_blockset.ColorCode) ||
                            _blockset.ColorCode.Equals(TransparentColorKey)) continue;

                        for (int i = 0; i < _blockset.BlockCount; i++)
                        {
                            tmpIndex++;
                            PaintingPixelConfig _new = new PaintingPixelConfig();
                            _new.column = tmpIndex;
                            _new.row = tmpIndex;
                            _new.colorCode = _blockset.ColorCode;
                            allPixels.Add(_new);
                        }
                    }
                }

                // Remove pixels covered by fountain from count
                if (_fountain.PixelCovered != null)
                {
                    foreach (var p in _fountain.PixelCovered)
                    {
                        allPixels.RemoveAll(px => p.column == px.column && p.row == px.row);
                    }
                }
            }
        }
        return allPixels;
    }

    public List<PaintingPixelConfig> GetAllWorkingPixelsExceptHearts()
    {
        List<PaintingPixelConfig> allPixels = new List<PaintingPixelConfig>();

        // Add pixels from PaintingConfig.Pixels (convert from PaintingPixelConfig to PaintingPixel)
        foreach (var pixelConfig in Pixels)
        {
            if (pixelConfig.Hidden || pixelConfig.color.Equals(TransparentColorKey)) continue;
            allPixels.Add(pixelConfig);
        }

        // Add pixels from PipeSetups.PixelCovered
        foreach (var pipeSetup in PipeSetups)
        {
            foreach (var pixel in pipeSetup.PixelCovered)
            {
                if (allPixels.Any(x => (x.column == pixel.column && x.row == pixel.row)))
                {
                    allPixels.Remove(allPixels.First(x => (x.column == pixel.column && x.row == pixel.row)));
                }
                pixel.colorCode = pipeSetup.ColorCode;
                allPixels.Add(pixel);
            }
            //for (int i = 0; i < pipeSetup.Hearts; i++)
            //{
            //    int pixelCoveredCount = pipeSetup.PixelCovered.Count;
            //    PaintingPixelConfig _new = new PaintingPixelConfig(pipeSetup.PixelCovered[i % (pixelCoveredCount - 1)]);
            //    _new.colorCode = pipeSetup.ColorCode;
            //    allPixels.Add(_new);
            //}
        }

        // Add pixels from PipeSetups.WallSetups
        foreach (var wallSetup in WallSetups)
        {
            int pixelCoveredCount = wallSetup.PixelCovered.Count;

            foreach (PaintingPixelConfig _p in wallSetup.PixelCovered)
            {
                if (allPixels.Any(x => (x.column == _p.column && x.row == _p.row)))
                {
                    allPixels.Remove(allPixels.First(x => (x.column == _p.column && x.row == _p.row)));
                }
                _p.colorCode = wallSetup.ColorCode;
                allPixels.Add(_p);
            }

            //for (int i = 0; i < wallSetup.Hearts; i++)
            //{
            //    PaintingPixelConfig _new = new PaintingPixelConfig(wallSetup.PixelCovered[i % (pixelCoveredCount - 1)]);
            //    _new.colorCode = wallSetup.ColorCode;
            //    allPixels.Add(_new);
            //}
        }

        // Add pixels from PipeSetups.KeySetups
        foreach (var keySetup in KeySetups)
        {
            int pixelCoveredCount = keySetup.PixelCovered.Count;

            foreach (PaintingPixelConfig _p in keySetup.PixelCovered)
            {
                if (allPixels.Any(x => (x.column == _p.column && x.row == _p.row)))
                {
                    allPixels.Remove(allPixels.First(x => (x.column == _p.column && x.row == _p.row)));
                }
            }
            //PaintingPixelConfig _key = new PaintingPixelConfig()
            //{
            //    column = keySetup.PixelCovered[0].column,
            //    row = keySetup.PixelCovered[0].row,
            //    color = keySetup.PixelCovered[0].color,
            //    colorCode = LockKeyColorDefine,
            //    Hidden = false
            //};
            //allPixels.Add(_key);
        }

        // Add pixels from PipeSetups.AdditionPixels
        foreach (var _p in AdditionPixels)
        {
            if (_p.Hidden || _p.color.Equals(TransparentColorKey)) continue;
            if (allPixels.Any(x => (x.column == _p.column && x.row == _p.row)))
            {
                allPixels.Remove(allPixels.First(x => (x.column == _p.column && x.row == _p.row)));
            }
            allPixels.Add(_p);
        }

        return allPixels;
    }

    public PipeObjectSetup GetPipeSetup(PaintingPixel startPixel, PaintingPixel endPixel)
    {
        PipeObjectSetup pipeSetup = null;
        foreach (var setup in PipeSetups)
        {
            if (setup.PixelCovered.Count < 2) continue;

            var firstPixel = setup.PixelCovered[0];
            var lastPixel  = setup.PixelCovered[setup.PixelCovered.Count - 1];

            if ((firstPixel.column == startPixel.column && firstPixel.row == startPixel.row &&
                 lastPixel.column == endPixel.column && lastPixel.row == endPixel.row) ||
                (firstPixel.column == endPixel.column && firstPixel.row == endPixel.row &&
                 lastPixel.column == startPixel.column && lastPixel.row == startPixel.row))
            {
                pipeSetup = setup;
                break;
            }
        }
        return pipeSetup;
    }

    public WallObjectSetup GetWallSetup(PaintingPixel startPixel, PaintingPixel endPixel)
    {
        WallObjectSetup wallSetup = null;
        foreach (var setup in WallSetups)
        {
            if (setup.PixelCovered.Count < 2) continue;

            var firstPixel = setup.PixelCovered[0];
            var lastPixel = setup.PixelCovered[setup.PixelCovered.Count - 1];
            if ((firstPixel.column == startPixel.column && firstPixel.row == startPixel.row &&
                 lastPixel.column == endPixel.column && lastPixel.row == endPixel.row) ||
                (firstPixel.column == endPixel.column && firstPixel.row == endPixel.row &&
                 lastPixel.column == startPixel.column && lastPixel.row == startPixel.row))
            {
                wallSetup = setup;
                break;
            }
        }
        return wallSetup;
    }

    public KeyObjectSetup GetKeySetup(PaintingPixel startPixel, PaintingPixel endPixel)
    {
        KeyObjectSetup keySetup = null;
        foreach (var setup in KeySetups)
        {
            var firstPixel = setup.PixelCovered[0];
            var lastPixel  = setup.PixelCovered[setup.PixelCovered.Count - 1];

            if ((firstPixel.column == startPixel.column && firstPixel.row == startPixel.row &&
                 lastPixel.column == endPixel.column && lastPixel.row == endPixel.row) ||
                (firstPixel.column == endPixel.column && firstPixel.row == endPixel.row &&
                 lastPixel.column == startPixel.column && lastPixel.row == startPixel.row))
            {
                keySetup = setup;
                break;
            }
        }
        return keySetup;
    }

    public void ClearData()
    {
        Pixels.Clear();
        PipeSetups.Clear();
        WallSetups.Clear();
        KeySetups.Clear();
        AdditionPixels.Clear();
        BlockFountainSetup?.Clear();
    }

#if UNITY_EDITOR
    [Space]
    public List<PaintingConfigBackUp> BackUpVariants = new List<PaintingConfigBackUp>();
    public int CurrentBackUpIndex = -1;
    [ContextMenu("Create Backup")]
    public void CreateBackUp()
    {
        if (BackUpVariants.Count >= 30)
        {
            BackUpVariants.RemoveAt(0);
        }
        PaintingConfigBackUp _backup = new PaintingConfigBackUp(this);
        BackUpVariants.Add(_backup);
        CurrentBackUpIndex = BackUpVariants.Count - 1;
    }

    [ContextMenu("Restore Backup")]
    public void RestoreBackup()
    {
        if (BackUpVariants.Count == 0) return;
        CurrentBackUpIndex--;
        CurrentBackUpIndex = Mathf.Clamp(CurrentBackUpIndex, 0, BackUpVariants.Count - 1);
        PaintingConfigBackUp _backup = BackUpVariants[CurrentBackUpIndex];
        this.ClearData();

        foreach (var p in _backup.Pixels)
        {
            this.Pixels.Add(new PaintingPixelConfig(p));
        }

        foreach (var p in _backup.PipeSetup)
        {
            this.PipeSetups.Add(new PipeObjectSetup(p));
        }

        foreach (var w in _backup.WallSetup)
        {
            this.WallSetups.Add(new WallObjectSetup(w));
        }

        foreach (var k in _backup.KeySetup)
        {
            this.KeySetups.Add(new KeyObjectSetup(k));
        }

        foreach (var p in _backup.AdditionPixels)
        {
            this.AdditionPixels.Add(new PaintingPixelConfig(p));
        }
    }

    [ContextMenu("Restore Backup Forward")]
    public void RestoreBackupForward()
    {
        if (BackUpVariants.Count == 0) return;
        CurrentBackUpIndex++;
        CurrentBackUpIndex = Mathf.Clamp(CurrentBackUpIndex, 0, BackUpVariants.Count - 1);
        PaintingConfigBackUp _backup = BackUpVariants[CurrentBackUpIndex];
        this.ClearData();

        foreach (var p in _backup.Pixels)
        {
            this.Pixels.Add(new PaintingPixelConfig(p));
        }

        foreach (var p in _backup.PipeSetup)
        {
            this.PipeSetups.Add(new PipeObjectSetup(p));
        }

        foreach (var w in _backup.WallSetup)
        {
            this.WallSetups.Add(new WallObjectSetup(w));
        }

        foreach (var k in _backup.KeySetup)
        {
            this.KeySetups.Add(new KeyObjectSetup(k));
        }

        foreach (var p in _backup.AdditionPixels)
        {
            this.AdditionPixels.Add(new PaintingPixelConfig(p));
        }
    }

    [ContextMenu("Clear Backups")]
    public void ClearBackUps()
    {
        CurrentBackUpIndex = -1;
        BackUpVariants.Clear();
    }
#endif
}

[Serializable]
public class PaintingConfigBackUp
{
    public string DateTime;

    public Vector2 _paintingSize;

    public List<PaintingPixelConfig> Pixels = new List<PaintingPixelConfig>();

    public List<PipeObjectSetup> PipeSetup = new List<PipeObjectSetup>();

    public List<WallObjectSetup> WallSetup = new List<WallObjectSetup>();

    public List<KeyObjectSetup> KeySetup = new List<KeyObjectSetup>();

    public List<PaintingPixelConfig> AdditionPixels = new List<PaintingPixelConfig>();

    public PaintingConfigBackUp(PaintingConfig _stock)
    {
        DateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Pixels = new List<PaintingPixelConfig>();
        WallSetup = new List<WallObjectSetup>();
        KeySetup = new List<KeyObjectSetup>();
        AdditionPixels = new List<PaintingPixelConfig>();

        foreach (var p in _stock.Pixels)
        {
            this.Pixels.Add(new PaintingPixelConfig(p));
        }

        foreach (var p in _stock.PipeSetups)
        {
            this.PipeSetup.Add(new PipeObjectSetup(p));
        }

        foreach (var w in _stock.WallSetups)
        {
            this.WallSetup.Add(new WallObjectSetup(w));
        }

        foreach (var k in _stock.KeySetups)
        {
            this.KeySetup.Add(new KeyObjectSetup(k));
        }

        foreach (var p in _stock.AdditionPixels)
        {
            this.AdditionPixels.Add(new PaintingPixelConfig(p));
        }
    }
}
