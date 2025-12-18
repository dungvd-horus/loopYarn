using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelObjectPrefabs", menuName = "ColorPixelFlow/Level Object Prefabs", order = 0)]
public class LevelMechanicObjectPrefabs : ScriptableObject
{
    public GameObject PipeObjectPrefab;
    public GameObject PipeHeadPrefab;
    public GameObject PipeBodyPrefab;
    public GameObject PipeTailPrefab;
    public GameObject KeyObjectPrefab;

    [Space]
    public GameObject LockPrefab;
    public GameObject BigBlockPrefab;
    public GameObject DefaultBlockPrefab;

    [Space]
    public GameObject GunnerPrefab;

    [Space]
    public ColorPalleteData ColorPallete;

    [Header("BLOCK FOUNTAIN")]
    public GameObject BlockFountainPrefab;
    public GameObject FountainProjectilePrefab;
}