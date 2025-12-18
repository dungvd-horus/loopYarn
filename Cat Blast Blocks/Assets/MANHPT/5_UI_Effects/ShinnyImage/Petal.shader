Shader "UI/Petal"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _RaySmoothStep("Ray SmoothStep", Vector) = (0.11, 0.26, 0, 0)
        _GlowSmoothStep("Glow SmoothStep", Vector) = (1, 0.1, 0, 0)
        _Speed("Speed", Float) = -1
        _Petal("Petal", Integer) = 8

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha One
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float3 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                half4 color : COLOR;
                float2 uv : TEXCOORD0;
                float3 vertex : TEXCOORD1;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float2 _RaySmoothStep;
            float2 _GlowSmoothStep;
            float _Speed;
            int _Petal;

            float CustomGet2DClipping(float2 position, float4 clipRect)
            {
                return step(clipRect.x, position.x) * step(position.x, clipRect.z) * step(clipRect.y, position.y) *
                    step(position.y, clipRect.w);
            }

            void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale,
                            out float2 Out)
            {
                float2 delta = UV - Center;
                float radius = length(delta) * 2 * RadialScale;
                float angle = atan2(delta.x, delta.y) * 1.0 / 6.28 * LengthScale;
                Out = float2(radius, angle);
            }

            v2f vert(appdata_t v)
            {
                v2f OUT;
                OUT.vertex = v.vertex;
                OUT.pos = UnityObjectToClipPos(OUT.vertex);
                OUT.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 polarUV = float2(0, 0);
                Unity_PolarCoordinates_float(IN.uv, 0.5, 1, 1, polarUV);

                float ray = smoothstep(_RaySmoothStep.x, _RaySmoothStep.y,
                   abs(frac(polarUV.y * _Petal + _Time.y * _Speed) - 0.5));
                float glow = smoothstep(_GlowSmoothStep.x, _GlowSmoothStep.y, polarUV.x);

                fixed4 color = (ray * glow).xxxx;

                color *= IN.color;

                #ifdef UNITY_UI_CLIP_RECT
                            color.a *= CustomGet2DClipping(IN.vertex.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                            clip (color.a - 0.001);
                #endif

                return color;
            }
            ENDCG
        }
    }
    Fallback "UI/Default"
}