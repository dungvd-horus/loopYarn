using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngameEffectSetting", menuName = "ColorPixelFlow/Ingame Effect Options", order = 1)]
public class InGameEffectOptions : ScriptableObject
{
    [Header("BULLET")]
    public float BulletSpeed = 25f;
    public float BulletScale = 1f;
    public bool ChangeBulletColor = true;
    public bool ChangeOutlineColor = false;

    [Header("ANIMATION: COLLECTOR")]
    public int IdleRate = 50;
    public bool RabbitRandomIdleAnimation = true;
    public bool RabbitEarAnimation = true;

    [Header("ANIMATION: BLOCK")]
    public bool ShakeNeighborBlocks = true;
    public float ShakeValue = 20;
    public float BlockScaleFactorWhenDestroyed = 1.5f;
}
