using UnityEngine;

/// <summary>
/// Provides a globally assigned fallback material so Luna/HTML build has a registered asset reference
/// instead of ad-hoc instantiated Materials (which are not in the material registry and cause InternalErrorShader fallback).
/// Place one instance in the initial scene and assign a safe default pixel material (e.g. white base).
/// </summary>
public class PixelMaterialFallbackProvider : MonoBehaviour
{
    [Tooltip("Fallback material used when a pixel's material reference is missing. Should be a palette/shared asset, NOT an instanced material.")]
    public Material FallbackMaterial;

    private static PixelMaterialFallbackProvider _instance;
    public static Material GlobalFallback => _instance != null ? _instance.FallbackMaterial : null;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            // Keep first instance, ignore subsequent
            return;
        }
        _instance = this;
    }
}

