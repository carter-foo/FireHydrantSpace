Shader "Custom/Ring"
{
    Properties
    {
        _Albedo ("Albedo", 2D) = "white" {}
        _Tint ("Tint", Color) = (1, 1, 1, 1)
        _Thickness ("Thickness", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf SimpleLambert alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _Albedo;

        struct Input
        {
            float2 uv_Albedo;
        };

        fixed4 _Tint;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten) {
            // half NdotL = dot(s.Normal, lightDir);
            half4 c;
            c.rgb = s.Albedo * _LightColor0;
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 uv = (float2(0.5, 0.5) - IN.uv_Albedo) * 2.0;
            float angle = atan2(uv.y, uv.x);
            float dist = saturate(2.0 * length(uv) - 1.0);
            half4 col = tex2D(_Albedo, float2(dist, angle / 3.141592 * 64));
            half NdotL = saturate(dot(uv, float2(0, 1)) + 0.6);
            o.Albedo = col.rgb * _Tint.rgb * NdotL;
            o.Alpha = col.a * _Tint.a;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
