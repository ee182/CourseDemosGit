// MatCap Shader, (c) 2015-2019 Jean Moreno

Shader "MatCap/Demo/Texture and Bumped"
{
    Properties
    {
        _Color("Main Color", Color) = (0.5, 0.5, 0.5, 1)

        _MainTex("Diffuse Map", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}

        _MatCap("MatCap Map (RGB)", 2D) = "white" {}
    }

        Subshader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #pragma multi_compile_fog
                #include "UnityCG.cginc"

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float4 uv : TEXCOORD0;
                    float3 TtoV0 : TEXCOORD1;
                    float3 TtoV1 : TEXCOORD2;
                };

                uniform float4 _BumpMap_ST;
                uniform float4 _MainTex_ST;

                v2f vert(appdata_tan v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.uv.zw = TRANSFORM_TEX(v.texcoord, _BumpMap);

                    TANGENT_SPACE_ROTATION;
                    o.TtoV0 = normalize(mul(rotation, UNITY_MATRIX_IT_MV[0].xyz));
                    o.TtoV1 = normalize(mul(rotation, UNITY_MATRIX_IT_MV[1].xyz));

                    return o;
                }

                uniform float4 _Color;
                uniform sampler2D _MainTex;
                uniform sampler2D _BumpMap;
                uniform sampler2D _MatCap;

                float4 frag(v2f i) : COLOR
                {
                    fixed4 c = tex2D(_MainTex, i.uv.xy);

                    float3 normal = UnpackNormal(tex2D(_BumpMap, i.uv.zw));
                    normal.z = sqrt(1.0 - saturate(dot(normal.xy ,normal.xy)));
                    normal = normalize(normal);

                    half2 vn;
                    vn.x = dot(i.TtoV0, normal);
                    vn.y = dot(i.TtoV1, normal);

                    fixed4 matcapLookup = tex2D(_MatCap, vn * 0.5 + 0.5);

                    fixed4 finalColor = matcapLookup * c * unity_ColorSpaceDouble;
                    return finalColor;
                }
            ENDCG
        }
    }
    Fallback "VertexLit"
}
