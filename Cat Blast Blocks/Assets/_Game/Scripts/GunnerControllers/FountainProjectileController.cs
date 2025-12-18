using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// Controls the fountain projectile behavior
/// </summary>
public class FountainProjectileController : MonoBehaviour
{
    public InGameEffectOptions EffectOptions;

    public Transform MyTransform;

    [Header("REAL TIME DATA")]
    public Vector3 Target;

    [Header("PARTICLE SYSTEM(s)")]
    public List<ParticleSystem> FlyingFXs = new List<ParticleSystem>();
    public List<ParticleSystem> HitFXs = new List<ParticleSystem>();
    public TrailRenderer TrailFX;
    public ParticleSystem BulletMeshParticle;
    private Renderer blockRenderer;

    private Action<FountainProjectileController> returnToPoolCallback;
    private PaintingPixelComponent currentTarget;

    private void Awake()
    {
        if (MyTransform == null)
            MyTransform = transform;
            
        if (BulletMeshParticle != null)
            blockRenderer = BulletMeshParticle.GetComponent<Renderer>();
    }

    private void OnDisable()
    {
        currentTarget = null;
        Despawn();
    }

    public void RotateToTarget()
    {
        MyTransform.LookAt(Target);
    }

    public void StartProjectile(Transform origin, PaintingPixelComponent target, float speed = -1f)
    {
        if (EffectOptions != null)
        {
            if (speed <= 0) speed = EffectOptions.BulletSpeed;
            transform.localScale = new Vector3(EffectOptions.BulletScale, EffectOptions.BulletScale, EffectOptions.BulletScale);
        }
        else
        {
            if (speed <= 0) speed = 25f;
            transform.localScale = Vector3.one;
        }
        
        gameObject.SetActive(true);

        MyTransform.position = origin.position;
        
        if (target.CubeTransform != null)
            MyTransform.localScale = target.CubeTransform.localScale;
            
        Target = target.transform.position;
        currentTarget = target;

        // Play flying effects
        foreach (var fx in FlyingFXs)
        {
            if (fx != null) fx.Play();
        }
        
        // Stop hit effects
        foreach (var fx in HitFXs)
        {
            if (fx != null) fx.Stop();
        }

        // Animate projectile
        MyTransform.DOJump(Target, 5f, 1, 0.65f)
            .SetEase(Ease.Linear)
            .OnComplete(OnStop);
    }

    public void SetPosition(Vector3 pos)
    {
        MyTransform.position = pos;
    }

    public void SetColor(Color color)
    {
        // Set trail color
        if (TrailFX != null)
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
        }

        // Set particle colors
        foreach (var fx in FlyingFXs)
        {
            if (fx != null)
            {
                var main = fx.main;
                main.startColor = color;
            }
        }
        
        foreach (var fx in HitFXs)
        {
            if (fx != null)
            {
                var main = fx.main;
                main.startColor = color;
            }
        }
        
        SetBulletMeshColor(color);
    }

    private void SetBulletMeshColor(Color _color)
    {
        if (blockRenderer == null) return;
        
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        blockRenderer.GetPropertyBlock(block);
        
        if (EffectOptions != null)
        {
            if (EffectOptions.ChangeBulletColor) 
                block.SetColor("_Color", _color);
            if (EffectOptions.ChangeOutlineColor) 
                block.SetColor("_OutlineColor", _color);
        }
        else
        {
            block.SetColor("_Color", _color);
        }

        blockRenderer.SetPropertyBlock(block);
    }

    private void OnStop()
    {
        // Play hit effects
        foreach (var fx in HitFXs)
        {
            if (fx != null) fx.Play();
        }

        // Stop flying effects
        foreach (var fx in FlyingFXs)
        {
            if (fx != null) fx.Stop();
        }
        
        // Revive target block
        if (currentTarget != null && !currentTarget.IsDestroyed())
        {
            currentTarget.ReviveAnimate();
            currentTarget = null;
        }

        MyTransform.gameObject.SetActive(false);
    }

    public void SetReturnToPoolCallback(Action<FountainProjectileController> callback)
    {
        returnToPoolCallback = callback;
    }

    public void Despawn()
    {
        returnToPoolCallback?.Invoke(this);
    }
}
