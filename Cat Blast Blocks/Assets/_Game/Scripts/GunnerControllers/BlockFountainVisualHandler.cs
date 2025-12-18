using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles visual effects for BlockFountain
/// </summary>
public class BlockFountainVisualHandler : MonoBehaviour
{
    #region PROPERTIES
    [Header("RENDERERS")]
    public List<Renderer> FontainMeshes;
    public Material BaseMaterial;
    public ParticleSystem MuzzleParticle;
    private Material cloneMaterial;
    public TMP_Text BulletText;

    [Header("ANIMATOR")]
    public Animator FountainAnimator;
    public string ShootStateName = "Shoot";
    #endregion

    #region UNITY CORE
    private void Awake()
    {
        if (BaseMaterial != null)
        {
            cloneMaterial = new Material(BaseMaterial);
            foreach (var rend in FontainMeshes)
            {
                if (rend != null)
                    rend.material = cloneMaterial;
            }
        }
    }

    private void OnDestroy()
    {
        if (cloneMaterial != null)
        {
            Destroy(cloneMaterial);
        }
    }
    #endregion

    #region MAIN
    public void ChangeMeshColor(Color newColor)
    {
        if (FontainMeshes == null) return;
        
        foreach (var rend in FontainMeshes)
        {
            if (rend != null && rend.sharedMaterial != null)
                rend.sharedMaterial.color = newColor;
        }
        
        if (MuzzleParticle != null)
        {
            var main = MuzzleParticle.main;
            main.startColor = newColor;
        }
    }

    public void PlayMuzzleParticle()
    {
        if (MuzzleParticle != null)
            MuzzleParticle.Play();
    }

    public void SetBulletText(int bulletLeft)
    {
        if (BulletText != null)
            BulletText.text = bulletLeft.ToString();
    }

    public void PlayShootAnimation()
    {
        if (FountainAnimator != null)
            FountainAnimator.Play(ShootStateName);
    }
    #endregion
}
