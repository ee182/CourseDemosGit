Shader "01_CourseDemos/ReflectionDemoShader"
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
            // Cull Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
                float3 normal : NORMAL; // 1. We need the normal now.
			};

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 texcoord_3f : TEXCOORD0;
            };

			samplerCUBE _CubeMapTex;
			
			v2f vert (appdata v)
			{
				v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                // 2. Not the model's vertex coordinate.
                // o.texcoord_3f = v.vertex.xyz;
                
                // 3. Model normal to world normal.
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);

                // 4. Model position to world position, then to world view direction.
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
                
                // 5. Calculate the reflection vector, and use it to sample the cubemap.
                o.texcoord_3f = reflect(-worldViewDir, worldNormal);

				return o;
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
