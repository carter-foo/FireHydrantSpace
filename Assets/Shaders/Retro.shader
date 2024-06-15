Shader "Custom/RetroSurface"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BeginFadeDist("Begin Fade Distance", Float) = 40
        _EndFadeDist("End Fade Distance", Float) = 50
        _MinBrightness("Minimum Brightness", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf SimpleLambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        fixed4 _Color;
        fixed _BeginFadeDist;
        fixed _EndFadeDist;
        fixed _MinBrightness;

        half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
            return half4(s.Albedo, s.Alpha);
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float inv_lerp(float a, float b, float v) {
            return (v - a) / (b - a);
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            float dist = distance(_WorldSpaceCameraPos, IN.worldPos);
            o.Albedo = c.rgb * saturate(max(_MinBrightness, 1.0 - inv_lerp(_BeginFadeDist, _EndFadeDist, dist)));
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
