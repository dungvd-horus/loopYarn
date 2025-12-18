Shader "Custom/AutoAnim_SpriteSheet_3x2"
{
    Properties
    {
        _MainTex ("Texture (Sprite Sheet)", 2D) = "white" {}
        // Cố định kích thước lưới 3x2, nhưng vẫn khai báo cho dễ đọc
        _Rows ("Rows", Float) = 2
        _Cols ("Cols", Float) = 3
        // Tốc độ chuyển khung hình (Frames Per Second)
        _FPS ("Frames Per Second", Range(1, 60)) = 12 
        _Color ("Tint Color", Color) = (1,1,1,1) // Tùy chọn: Màu nhuộm
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc" 

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0; // Tọa độ UV mặc định
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Khai báo các thuộc tính
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Rows;
            float _Cols;
            float _FPS;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 1. Kích thước của 1 khung hình (Size)
                // (1/3, 1/2)
                float2 size = float2(1.0 / _Cols, 1.0 / _Rows);

                // 2. TÍNH TOÁN CHỈ MỤC KHUNG HÌNH (Index)
                
                float totalFrames = _Rows * _Cols; // 6 khung

                // floor((Thời gian * FPS) % Tổng số khung)
                // _Time.y là thời gian chạy game.
                float currentFrameIndex = floor(fmod(_Time.y * _FPS, totalFrames));

                // 3. Tọa độ Cột (C) và Hàng (R)
                
                // Cột (Col): Index % 3
                float col = fmod(currentFrameIndex, _Cols);
                // Hàng (Row): floor(Index / 3)
                float row = floor(currentFrameIndex / _Cols);

                // 4. Tính toán Offset (Tọa độ góc dưới bên trái của khung hình)
                float2 offset = float2(
                    col * size.x,
                    // Tính toán vị trí Y. Unity UV: 0=Dưới, 1=Trên. Cần đảo ngược thứ tự hàng.
                    1.0 - (row + 1.0) * size.y 
                );

                // 5. Tính toán UV mới: (UV_default * Size) + Offset
                float2 newUV = i.uv * size + offset;

                // 6. Lấy màu từ texture và áp dụng màu nhuộm
                fixed4 colTex = tex2D(_MainTex, newUV) * _Color;

                return colTex;
            }
            ENDCG
        }
    }
}