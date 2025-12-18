using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectorProjectileController : MonoBehaviour
{
    public InGameEffectOptions EffectOptions;

    public Transform MyTransform;

    [Header("STAT(s)")]
    public bool SuperAmmo = false;
    public float Speed = 0f;

    [Header("REAL TIME DATA")]
    public Vector3 Target;
    public float TimeExisted = 0f;
    public float TimeExistedAfterHit = 1f;
    public float CurrentTimer = 0;
    public bool Stopped = false;

    [Header("PARTICLE SYSTEM(s)")]
    public List<ParticleSystem> FlyingFXs = new List<ParticleSystem>();
    public List<ParticleSystem> HitFXs = new List<ParticleSystem>();
    public TrailRenderer TrailFX;
    public ParticleSystem BulletMeshParticle;
    private Renderer bulletRenderer;

    private Action<CollectorProjectileController, bool> returnToPoolCallback;

    private PaintingPixelComponent currentTarget;

    private void Awake()
    {
        bulletRenderer = BulletMeshParticle.GetComponent<Renderer>();
    }

    void Update()
    {
        CurrentTimer += Time.deltaTime;
        if (!Stopped && Speed > 0)
        {
            MyTransform.position += (MyTransform.forward) * (Speed * Time.deltaTime);
            MyTransform.LookAt(Target);

            if (CurrentTimer >= TimeExisted - TimeExistedAfterHit)
            {
                OnStop();
            }
            return;
        }
        if (CurrentTimer >= TimeExisted + TimeExistedAfterHit)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Speed = 0f;
        Stopped = true;
        TimeExisted = 0f;
        CurrentTimer = 0f;

        if (currentTarget)
        {
            currentTarget.DestroyPixelVisually();
        }

        currentTarget = null;

        this.Despawn(SuperAmmo);
    }

    public void RotateToTarget()
    {
        MyTransform.LookAt(Target);
    }

    public void StartProjectile(PaintingPixelComponent target, float speed = -1f)
    {
        RotateToTarget();

        if (speed <= 0) speed = EffectOptions.BulletSpeed;
        transform.localScale = new Vector3(EffectOptions.BulletScale, EffectOptions.BulletScale, EffectOptions.BulletScale);
        gameObject.SetActive(true);
        Speed  = speed;
        Target = target.transform.position;
        float distance   = Vector3.Distance(MyTransform.position, Target);
        float travelTime = distance / Speed;
        TimeExisted  = travelTime + TimeExistedAfterHit;
        CurrentTimer = 0;

        currentTarget = target;

        Stopped = false;
        foreach (var fx in FlyingFXs)
        {
            fx.Play();
        }
        foreach (var fx in HitFXs)
        {
            fx.Stop();
        }
    }

    public void StartProjectile(Vector3 target, float speed = -1f)
    {
        RotateToTarget();

        if (speed <= 0) speed = EffectOptions.BulletSpeed;
        transform.localScale = new Vector3(EffectOptions.BulletScale, EffectOptions.BulletScale, EffectOptions.BulletScale);
        gameObject.SetActive(true);
        Speed  = speed;
        Target = target;
        float distance   = Vector3.Distance(MyTransform.position, Target);
        float travelTime = distance / Speed;
        TimeExisted   = travelTime + TimeExistedAfterHit;
        CurrentTimer  = 0;
        currentTarget = null;
        Stopped       = false;
        foreach (var fx in FlyingFXs)
        {
            fx.Play();
        }
        foreach (var fx in HitFXs)
        {
            fx.Stop();
        }
    }

    public void SetPosition(Vector3 pos)
    {
        MyTransform.position = pos;
    }

    public void SetColor(Color color)
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                    new GradientColorKey(color, 0f),
                    new GradientColorKey(color, 1f)
            },
            new GradientAlphaKey[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(1f, 0.7f),
                    new GradientAlphaKey(0f, 1f)
            }
        );

        TrailFX.colorGradient = gradient;
        foreach (var fx in FlyingFXs)
        {
            var main = fx.main;
            main.startColor = color;
        }
        foreach (var fx in HitFXs)
        {
            var main = fx.main;
            main.startColor = color;
        }
        SetBulletMeshColor(color);
    }

    private void SetBulletMeshColor(Color _color)
    {
        if (bulletRenderer == null)
            bulletRenderer = BulletMeshParticle?.GetComponent<Renderer>();
        if (bulletRenderer == null)
            return;

        var mat = bulletRenderer.sharedMaterial;
        if (mat == null)
            return;

        // Kiểm tra shader name
        string shaderName = mat.shader != null ? mat.shader.name : "null";
        //Debug.Log($"[BulletColor] Shader: {shaderName}");

        // Dò property có thể dùng
        if (mat.HasProperty("_BaseColor"))
            mat.SetColor("_BaseColor", _color);
        else if (mat.HasProperty("_TintColor"))
            mat.SetColor("_TintColor", _color);
        else if (mat.HasProperty("_Color"))
            mat.SetColor("_Color", _color);

        // Nếu có Outline
        if (EffectOptions.ChangeOutlineColor && mat.HasProperty("_OutlineColor"))
            mat.SetColor("_OutlineColor", _color);

        // Nếu shader có emission — set luôn cho sáng hơn
        if (mat.HasProperty("_EmissionColor"))
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", _color * 0.5f);
        }

        // Vừa set cho particle main nữa để particle đồng bộ
        var main = BulletMeshParticle.main;
        main.startColor = new ParticleSystem.MinMaxGradient(_color);

        //Debug.Log($"[BulletColor] Applied color: {_color}");
    }


    private void OnStop()
    {
        Stopped = true;

        foreach (var fx in HitFXs)
        {
            fx.Play();
        }

        foreach (var fx in FlyingFXs)
        {
            fx.Stop();
        }

        currentTarget.DestroyPixelVisually();
        currentTarget = null;
    }

    public void SetReturnToPoolCallback(Action<CollectorProjectileController, bool> callback)
    {
        returnToPoolCallback = callback;
    }


    public void Despawn(bool super)
    {
        returnToPoolCallback?.Invoke(this, super);
    }
}
