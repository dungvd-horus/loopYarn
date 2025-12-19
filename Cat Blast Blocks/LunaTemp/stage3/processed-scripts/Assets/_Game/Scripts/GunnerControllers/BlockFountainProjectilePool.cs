using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object pool for fountain projectiles
/// Add this to a GameObject in the scene (singleton)
/// </summary>
public class BlockFountainProjectilePool : MonoBehaviour
{
    public static BlockFountainProjectilePool Instance { get; private set; }

    [Header("Pool Setup")]
    [SerializeField] private FountainProjectileController fountainProjectilePrefab;
    [SerializeField] private int initialCount = 10; // Reduced for playable ads

    private readonly Queue<FountainProjectileController> normalProjectilePool = new Queue<FountainProjectileController>();

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        InitializePool();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialCount; i++)
            CreateProjectile(false);
    }

    private FountainProjectileController CreateProjectile(bool active = false)
    {
        if (fountainProjectilePrefab == null)
        {
            Debug.LogError("FountainProjectilePrefab is not assigned!");
            return null;
        }

        var proj = Instantiate(fountainProjectilePrefab, transform);
        proj.gameObject.SetActive(active);
        proj.SetReturnToPoolCallback(ReturnToPool);

        if (!active)
        {
            normalProjectilePool.Enqueue(proj);
        }

        return proj;
    }

    public FountainProjectileController GetProjectile()
    {
        if (normalProjectilePool.Count > 0)
        {
            var proj = normalProjectilePool.Dequeue();
            proj.gameObject.SetActive(true);
            return proj;
        }

        return CreateProjectile(true);
    }

    public void ReturnToPool(FountainProjectileController proj)
    {
        if (proj == null) return;
        proj.gameObject.SetActive(false);
        normalProjectilePool.Enqueue(proj);
    }
}
