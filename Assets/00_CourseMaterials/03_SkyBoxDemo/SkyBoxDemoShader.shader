Shader "01_CourseDemos/SkyBoxDemoShader"
{
	Properties
	{
		_CubeMapTex ("Cubemap", Cube) = "white" {}
	}
	
    SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
            Cull Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 texcoord_3f : TEXCOORD0;
            };

			samplerCUBE _CubeMapTex;

            v2f vert (appdata v)
			{
				v2f o;

                // 1. refactor UnityObjectToClipPos() to MV and P matrices
                //    model's relative position to the camera(eye) is recorded in MV matrix
                float4x4 matMV = mul(UNITY_MATRIX_V, unity_ObjectToWorld);
                o.vertex = mul(UNITY_MATRIX_P, mul(matMV, float4(v.vertex.xyz, 1.0)));
                
                o.texcoord_3f = v.vertex.xyz;

				return o;
                // 2. remove the translations from MV matrix
                // matMV[0][3] = 0.0;
                // matMV[1][3] = 0.0;
                // matMV[2][3] = 0.0;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                half4 texCol = texCUBE(_CubeMapTex, i.texcoord_3f);
				return texCol;
			}
			ENDCG
		}
	}
}
