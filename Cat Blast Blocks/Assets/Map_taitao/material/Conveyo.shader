Shader "Custom/Lit_ConveyorArrowGlow"
{
    Properties
    {
        // Thuộc tính Lit cơ bản
        _Color ("Tint Color", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.0

        // Thuộc tính Glow
        _ArrowMaskTex ("Arrow Mask (White arrows, Black elsewhere)", 2D) = "white" {}
        _ScrollSpeed ("Overall Scroll Speed (UV Scroll)", Range(-2, 2)) = 0.5
        _GlowSpeed ("Glow Pulse Speed (Time-based shift)", Range(-2, 2)) = 1.0
        _GlowPulseCount ("Glow Pulse Count (Number of pulses from UV 0 to 1)", Range(1, 10)) = 3 
        _GlowDutyCycle ("Glow Duty Cycle (0.1 = short pulse, 0.9 = long pulse)", Range(0.1, 0.9)) = 0.5
        _GlowColor ("Glow Color (HDR)", Color) = (1, 0.5, 0, 1)
        _GlowIntensity ("Glow Max Intensity", Range(0, 50)) = 10.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Sử dụng lighting model Standard (PBR)
        #pragma surface surf Standard fullforwardshadows

        // Định nghĩa các biến toàn cục
        sampler2D _MainTex;
        sampler2D _ArrowMaskTex;
        fixed4 _Color;
        half _Glossiness;
        half _Metallic;

        // Các biến cho hiệu ứng Glow
        float _ScrollSpeed;
        float _GlowSpeed;
        float _GlowPulseCount;
        float _GlowDutyCycle;
        fixed4 _GlowColor;
        float _GlowIntensity;

        #define FIXED_FADE_PERCENTAGE 0.05 

        struct Input
        {
            float2 uv_MainTex; // Sử dụng uv_ + tên biến Texture
            // Các Surface Shader tự động tính toán các giá trị UV đã được áp dụng tiling/offset.
        };

        // Hàm tính toán Emission/Glow (để đơn giản hóa hàm surf)
        fixed3 calculateGlow(Input IN)
        {
            // --- 1. Tính toán UV Cuộn Chính ---
            float2 scrolledUV = IN.uv_MainTex;
            scrolledUV.y += _Time.y * _ScrollSpeed; 

            // Lấy mẫu Arrow Mask (cuộn theo tốc độ chính)
            fixed arrowMaskValue = tex2D(_ArrowMaskTex, scrolledUV).r;

            // --- 2. Tính toán Hiệu ứng Phát sáng Dần Dần Tuần Tự ---
            
            // Tính toán GlowPosition độc lập với Main Scroll
            float glowPosition = IN.uv_MainTex.y + (_Time.y * _GlowSpeed);

            // Tính toán Khoảng cách và Vùng Mờ
            float glowInterval = 1.0 / _GlowPulseCount;
            float glowFadeSize = glowInterval * FIXED_FADE_PERCENTAGE; 
            float glowEndPulse = glowInterval * _GlowDutyCycle; 
            
            // Vị trí hiện tại trong chu kỳ phát sáng
            float cyclePosition = glowPosition * _GlowPulseCount;
            float currentCyclePosition = fmod(cyclePosition, 1.0); 
            float currentPulsePosition = currentCyclePosition * glowInterval; 
            
            // Tính toán cường độ sáng cho vùng hiện tại (0 -> 1 -> 0)
            float pulseAlpha = 0;
            
            // Logic Sáng/Tắt Tuần Tự (Sử dụng Duty Cycle)
            if (currentPulsePosition < glowFadeSize)
            {
                pulseAlpha = smoothstep(0, glowFadeSize, currentPulsePosition);
            }
            else if (currentPulsePosition > (glowEndPulse - glowFadeSize) && currentPulsePosition < glowEndPulse)
            {
                pulseAlpha = smoothstep(glowEndPulse, glowEndPulse - glowFadeSize, currentPulsePosition);
            }
            else if (currentPulsePosition >= glowFadeSize && currentPulsePosition <= (glowEndPulse - glowFadeSize))
            {
                pulseAlpha = 1.0;
            }
            else
            {
                pulseAlpha = 0.0;
            }
            
            // Emission = Màu Glow x Cường độ Max x Alpha Sáng Tuần Tự x Arrow Mask
            fixed3 emission = _GlowColor.rgb * _GlowIntensity * pulseAlpha * arrowMaskValue;
            
            return emission;
        }

        // Hàm Surface Shader chính
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // --- 1. Tính toán Albedo và Texture Cuộn ---
            // Tính lại UV cuộn để lấy mẫu MainTex
            float2 scrolledUV = IN.uv_MainTex;
            scrolledUV.y += _Time.y * _ScrollSpeed; 
            
            fixed4 c = tex2D (_MainTex, scrolledUV) * _Color;
            
            // --- 2. Áp dụng các thuộc tính Lit ---
            o.Albedo = c.rgb; // Màu cơ bản (nhận ánh sáng và bóng)
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            // --- 3. Áp dụng Emission (Glow) ---
            // Emission sẽ được cộng thêm vào màu cuối cùng sau khi tính toán ánh sáng
            o.Emission = calculateGlow(IN);
        }
        ENDCG
    }
    FallBack "Diffuse"
}