using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTextureScale : MonoBehaviour
{
    public bool Active = false;
    public Vector3 referenceScale = new Vector3(0.5f, 0.16f, 0.5f);
    public Vector2 referenceTiling = new Vector2(0.5f, 2.5f);

    public Renderer rend;

    private MaterialPropertyBlock block;
    private int frameCounter = 0;
    [Range(1, 60)] public int updateEveryNFrames = 10;

    void Awake()
    {
        if (rend == null) rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
    }

    void LateUpdate()
    {
        if (!Active) return;

        frameCounter++;
        if (frameCounter < updateEveryNFrames) return;
        frameCounter = 0;

        UpdateTextureUV();
    }

    void UpdateTextureUV()
    {
        if (rend == null) return;

        Vector3 s = transform.localScale;

        float ratioX = s.x / referenceScale.x;
        float ratioY = s.z / referenceScale.z;

        Vector2 tiling = new Vector2(
            referenceTiling.x * ratioX,
            referenceTiling.y * ratioY
        );
        if (rend == null) rend = GetComponent<Renderer>();
        if (block == null) block = new MaterialPropertyBlock();
        rend.GetPropertyBlock(block);
        block.SetVector("_MainTex_ST", new Vector4(tiling.x, tiling.y, 0, 0));
        rend.SetPropertyBlock(block);
    }

    public void ActiveScaler()
    {
        Active = true;
        UpdateTextureUV();
    }

    public void DeActiveTextureScaler()
    {
        Active = false; 
        UpdateTextureUV();
    }

}
