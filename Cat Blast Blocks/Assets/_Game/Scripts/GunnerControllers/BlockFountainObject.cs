using static PaintingSharedAttributes;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// BlockFountain - Adapted for Playable Ads
/// A fountain that sprays colored blocks to refill destroyed pixels
/// </summary>
public class BlockFountainObject : MonoBehaviour
{
    #region PROPERTIES
    public Dictionary<string, int> BlockSets = new Dictionary<string, int>();

    [Header("COMPONENT(s)")]
    public PaintingGridObject CurrentGrid;
    public BlockFountainVisualHandler VisualHandler;
    public LevelMechanicObjectPrefabs LevelMechanicPrefabs;

    [Header("TRANSFORMS")]
    public Transform FountainRootObject;
    public Transform ProjectileSpawnPos;
    public bool RotateToTarget = false;
    public Transform RotatePart;
    public float RotateSpeed = 180f;
    private float timer = 0;
    private Vector3 currentTargetPos;

    [Header("RUNTIME DATA")]
    public List<PaintingPixel> PaintingPixelsCovered;
    public string CurrentColorCode = "None";
    public Color CurrentColor;
    public int BulletLeft = 0;
    public bool Destroyed = false;
    public int TotalBlocksHolding = 0;
    public int BlocksReleased = 0;

    private Transform fountainTransform;
    #endregion

    #region UNITY CORE
    private void Awake()
    {
        fountainTransform = transform;
    }

    private void Update()
    {
        if (!RotateToTarget) return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            RotateToTargetYOnly(currentTargetPos);
        }
    }
    #endregion

    #region MAIN
    public void Initialize(List<BlockFountainBulletSet> blockSets, List<PaintingPixel> fountainPixels, PaintingGridObject currentGrid)
    {
        Destroyed = false;
        CurrentGrid = currentGrid;
        BlockSets = new Dictionary<string, int>();
        
        foreach (var blockSet in blockSets)
        {
            if (BlockSets.ContainsKey(blockSet.ColorCode)) 
                BlockSets[blockSet.ColorCode] += blockSet.BlockCount;
            else 
                BlockSets.Add(blockSet.ColorCode, blockSet.BlockCount);
        }
        
        // Remove invalid color codes
        BlockSets.Remove(DefaultColorKey);
        BlockSets.Remove(LockKeyColorDefine);
        BlockSets.Remove(TransparentColorKey);
        
        if (BlockSets.Count > 0)
            CurrentColorCode = BlockSets.ElementAt(0).Key;

        BulletLeft = 0;
        BlocksReleased = 0;
        TotalBlocksHolding = 0;
        
        foreach (var colorSet in BlockSets)
        {
            BulletLeft += colorSet.Value;
            TotalBlocksHolding += colorSet.Value;
        }
        
        VisualHandler?.SetBulletText(BulletLeft);

        PaintingPixelsCovered = fountainPixels ?? new List<PaintingPixel>();

        CheckChangeMeshesColor();
    }

    public bool SprayBlock(string colorCode, PaintingPixelComponent targetBlock)
    {
        if (!colorCode.Equals(CurrentColorCode) || BulletLeft <= 0 || !BlockSets.ContainsKey(colorCode)) 
            return false;

        if (BlockSets[colorCode] > 0)
        {
            // Spray block
            BulletLeft--;
            BlocksReleased++;
            BlockSets[colorCode]--;
            
            if (BlockSets[colorCode] <= 0)
            {
                BlockSets.Remove(colorCode);
                if (BlockSets.Count > 0)
                {
                    CurrentColorCode = BlockSets.ElementAt(0).Key;
                    CheckSprayRandomPixel(CurrentColorCode);
                }
            }
            
            VisualHandler?.SetBulletText(BulletLeft);
            VisualHandler?.PlayShootAnimation();

            timer = 2f;
            currentTargetPos = targetBlock.CubeTransform.position;
            CheckChangeMeshesColor();
            VisualHandler?.PlayMuzzleParticle();

            // Spawn projectile if pool exists
            if (BlockFountainProjectilePool.Instance != null)
            {
                var projectile = BlockFountainProjectilePool.Instance.GetProjectile();
                projectile.SetColor(targetBlock.PixelData.color);
                projectile.StartProjectile(ProjectileSpawnPos, targetBlock);
            }

            if (BlockSets.Count <= 0)
            {
                SelfDestroy();
            }

            return true;
        }
        return false;
    }

    private void CheckSprayRandomPixel(string _colorCode)
    {
        if (!BlockSets.ContainsKey(_colorCode)) return;
        if (CurrentGrid == null) return;
        
        if (!CurrentGrid.AnyPixelLeftWithColor(_colorCode, out var targetPixels))
        {
            for (int i = 0; i < targetPixels.Count; i++)
            {
                if (!BlockSets.ContainsKey(_colorCode)) return;
                if (BlockSets[_colorCode] > 0)
                {
                    var _p = targetPixels[i];
                    if (_p.PixelComponent != null && !_p.IsMechanicPixel()) 
                        CurrentGrid.ReFillBlockUsingFountain(_p);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void RemoveSet(string colorCode)
    {
        if (BlockSets.ContainsKey(colorCode))
        {
            BulletLeft -= BlockSets[colorCode];
            GameplayEventsManager.OnGridPixelsDestroyedPassive?.Invoke(BlockSets[colorCode]);
            BlockSets.Remove(colorCode);
            
            if (BlockSets.Count > 0)
            {
                CurrentColorCode = BlockSets.ElementAt(0).Key;
                CheckSprayRandomPixel(CurrentColorCode);
                CheckChangeMeshesColor();
            }
            else
            {
                SelfDestroy();
            }
        }
    }

    private void CheckChangeMeshesColor()
    {
        if (LevelMechanicPrefabs == null || LevelMechanicPrefabs.ColorPallete == null) return;
        
        Color _c = LevelMechanicPrefabs.ColorPallete.GetColorByCode(CurrentColorCode);
        if (_c != CurrentColor)
        {
            CurrentColor = _c;
            VisualHandler?.ChangeMeshColor(CurrentColor);
        }
    }

    public void SelfDestroy()
    {
        Destroyed = true;
        foreach (var pixel in PaintingPixelsCovered)
        {
            pixel.DestroyPixel(invokeEvent: false);
        }
        fountainTransform.DOScale(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    public void SelfDestroyGameobject()
    {
        if (Application.isPlaying) 
            GameObject.Destroy(gameObject);
        else 
            GameObject.DestroyImmediate(gameObject);
    }
    #endregion

    #region SUPPORTIVE
    private void RotateToTargetYOnly(Vector3 targetPos)
    {
        if (RotatePart == null) return;
        
        Vector3 direction = targetPos - RotatePart.position;
        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.0001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        RotatePart.rotation = Quaternion.RotateTowards(RotatePart.rotation, targetRotation, RotateSpeed * Time.deltaTime);
    }

    public string GetCurrentColorCode()
    {
        return CurrentColorCode;
    }
    #endregion
}
