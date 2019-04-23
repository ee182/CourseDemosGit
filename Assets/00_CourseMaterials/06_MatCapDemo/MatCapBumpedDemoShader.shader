// MatCap Shader, (c) 2015-2019 Jean Moreno

Shader "01_CourseDemos/MatCap/Bumped"
{
    Properties
    {
        _Color("Main Color", Color) = (0.5, 0.5, 0.5, 1)

        // 1. Two more textures
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

                    // 2. TRANSFORM_TEX is used to include tiling and offset of textures
                    o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.uv.zw = TRANSFORM_TEX(v.texcoord, _BumpMap);

                    // 3. TANGENT_SPACE_ROTATION provides normal, tangent, and binormal vectors in Model space
                    TANGENT_SPACE_ROTATION;

                    // 4. Concatenate rotation with IT_MV to form transform from tangent space to view space
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

                    // 5. UnpackNormal maps from [0, 1] to [-1, 1]
                    float3 normal = UnpackNormal(tex2D(_BumpMap, i.uv.zw));

                    // 6. Calculate the normal vector by Pythagorean theorem
                    normal.z = sqrt(1.0 - saturate(dot(normal.xy ,normal.xy)));
                    normal = normalize(normal);

                    half2 vn;
                    // 7. Normal vector calculated from bumpmap is in tangent space
                    //    Transform it to view space
                    vn.x = dot(i.TtoV0, normal);
                    vn.y = dot(i.TtoV1, normal);

                    // 8. Sample it as usual
                    fixed4 matcapLookup = tex2D(_MatCap, vn * 0.5 + 0.5);

                    fixed4 finalColor = matcapLookup * c * unity_ColorSpaceDouble;
                    return finalColor;
                }
            ENDCG
        }
    }
    Fallback "VertexLit"
}
