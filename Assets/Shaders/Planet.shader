Shader "Custom/Planet"
{
    Properties
    {
        _Albedo ("Albedo (RGB)", 2D) = "white" {}
        _Tint ("Colour", Color) = (1,1,1,1)
        _AtmosphereColor("Atmosphere Colour", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf SimpleLambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _Albedo;

        struct Input
        {
            float2 uv_Albedo;
        };

        fixed4 _Tint;
        fixed _LightExp;
        fixed3 _AtmosphereColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
            half NdotL = dot(s.Normal, lightDir);
            half NdotV = dot(s.Normal, viewDir);
            half4 c;
            c.rgb = saturate(lerp(s.Albedo, _AtmosphereColor, 1.0 - NdotV) * _LightColor0 * NdotL * atten);
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_Albedo, IN.uv_Albedo) * _Tint;
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
