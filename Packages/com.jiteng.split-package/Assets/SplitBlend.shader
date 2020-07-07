 Shader "Custom/Texture Blend"
 {
     Properties 
     {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {} 
        _BlendTex ("Blend (RGB)", 2D) = "white"
        _BlendAlpha ("Blend Alpha", float) = 0.5
     }
     SubShader 
     {
        Tags { "Queue"="Geometry-9" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Lighting Off
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
  
        CGPROGRAM
        #pragma target 3.0
        #pragma surface surf Lambert
        // #pragma surface surf Lambert noambient
  
        fixed4 _Color;
        sampler2D _MainTex;
        sampler2D _BlendTex;
        float _BlendAlpha;
  
        struct Input {
          float2 uv_MainTex;
        };
  
        // void surf (Input IN, inout SurfaceOutput o) {
        //   fixed4 c = ( ( 1 - _BlendAlpha ) * tex2D( _MainTex, IN.uv_MainTex ) + _BlendAlpha * tex2D( _BlendTex, IN.uv_MainTex ) * _Color);
        // //   fixed4 c = lerp (tex2D (_MainTex, IN.uv_MainTex), tex2D (_BlendTex, IN.uv_MainTex), _Blend) * _Color;
        //   // o.Emission = c.rgb;
        // half2 uv = IN.uv_MainTex;
        // if (uv.x < 100) {
        //     o.Albedo = c.rgb;
        //     o.Alpha = c.a;
        // }
        //   // o.Albedo = c.rgb;
        //   // o.Alpha = c.a;
        // }
        void surf(Input IN, inout SurfaceOutput o) {
            half2 uv = IN.uv_MainTex;
            // half2 size = textureSize(_MainTex, 0);
            bool p = fmod(uv.x, 2) < 0.5;
            float2 tmp = float2(IN.uv_MainTex.x * 2, IN.uv_MainTex.y);
            float2 tmp2 = float2((IN.uv_MainTex.x - 0.5) * 2, IN.uv_MainTex.y);
            fixed4 c = tex2D(_MainTex, tmp) * _Color;
            fixed4 c2 = tex2D(_BlendTex, tmp2) * _Color;

            if (p) {
                o.Albedo = c.rgb;
            }
            else {
                o.Albedo = c2.rgb;
            }
            o.Alpha = 1;
        }
        ENDCG
     }
  
    //  Fallback "Transparent/VertexLit"
 }