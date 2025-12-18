using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorProjectilePool : MonoBehaviour
{
    public static CollectorProjectilePool Instance { get; private set; }

    [Header("Pool Setup")]
    [SerializeField] private CollectorProjectileController normalProjectilePrefab;
    [SerializeField] private CollectorProjectileController superProjectilePrefab;
    [SerializeField] private int initialCount = 20;

    private readonly Queue<CollectorProjectileController> normalBulletPool = new Queue<CollectorProjectileController>();
    private readonly Queue<CollectorProjectileController> superBulletPool = new Queue<CollectorProjectileController>();

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialCount; i++)
            CreateProjectile(false, false);
    }

    private CollectorProjectileController CreateProjectile(bool super, bool active = false)
    {
        var prefabToSpawn = super ? superProjectilePrefab : normalProjectilePrefab;
        var proj = Instantiate(prefabToSpawn, transform);
        proj.gameObject.SetActive(active);

        proj.SetReturnToPoolCallback(ReturnToPool);

        if (!active)
        {
            if (super) superBulletPool.Enqueue(proj);
            else normalBulletPool.Enqueue(proj);
        }

        return proj;
    }

    public CollectorProjectileController GetProjectile(bool super)
    {
        if (super)
        {
            if (superBulletPool.Count > 0)
            {
                var proj = superBulletPool.Dequeue();
                proj.gameObject.SetActive(true);
                return proj;
            }
        }
        else
        {
            if (normalBulletPool.Count > 0)
            {
                var proj = normalBulletPool.Dequeue();
                proj.gameObject.SetActive(true);
                return proj;
            }
        }

        return CreateProjectile(super, true);
    }

    public void ReturnToPool(CollectorProjectileController proj, bool super)
    {
        proj.gameObject.SetActive(false);
        if (super) superBulletPool.Enqueue(proj);
        else normalBulletPool.Enqueue(proj);
    }
}
