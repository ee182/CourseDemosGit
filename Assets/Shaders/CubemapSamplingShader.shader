Shader "CourseDemos/CubemapSamplingShader"
{
	Properties
	{
		_CubeMapTex ("Cubemap", Cube) = "white" {} // Not 2D
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
				// float2 uv : TEXCOORD0; // Not as used as in 2D texture sampling
			};

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 texcoord_3f : TEXCOORD0; // Not float2 uv : TEXCOORD0;
            };

			samplerCUBE _CubeMapTex; // Not sampler2D
			
			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);

                o.texcoord_3f = v.vertex.xyz;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the cubemap
                half4 texCol = texCUBE(_CubeMapTex, i.texcoord_3f); // Not tex2D(_MainTex, i.uv);
                // half4 texCol = texCUBE(_CubeMapTex, float3(i.texcoord_3f.x * 20, i.texcoord_3f.y * 3, i.texcoord_3f.z * 4)); // Not tex2D(_MainTex, i.uv);

				return texCol;
			}
			ENDCG
		}
	}
}
