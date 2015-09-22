Shader "Custom/GeometricMirror"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.98
        _Metallic ("Metallic", Range(0,1)) = 1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }

        cull off

        CGPROGRAM

        #pragma multi_compile _ FLIP_X
        #pragma multi_compile _ FLIP_Y
        #pragma multi_compile _ FLIP_Z

        #pragma surface surf Standard
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf_base(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            #ifndef FLIP_X
            clip(IN.worldPos.x);
            #else
            clip(-IN.worldPos.x);
            #endif

            #ifndef FLIP_Y
            clip(IN.worldPos.y);
            #else
            clip(-IN.worldPos.y);
            #endif

            #ifndef FLIP_Z
            clip(IN.worldPos.z);
            #else
            clip(-IN.worldPos.z);
            #endif

            surf_base(IN, o);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
