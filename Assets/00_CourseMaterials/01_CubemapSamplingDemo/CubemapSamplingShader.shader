Shader "01_CourseDemos/CubemapSamplingShader"
{
	Properties
	{
        // 1. Cube, not 2D
		_CubeMapTex ("Cubemap", Cube) = "white" {}
	}
	
    SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
            Cull Back
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;

                // 2. UV is not used
				// float2 uv : TEXCOORD0; 
			};

            struct v2f {
                float4 vertex : SV_POSITION;

                // 3. Instead, a 3 components vector
                float3 texcoord_3f : TEXCOORD0;
            };

            // 4. samplerCUBE, not sampler2D
			samplerCUBE _CubeMapTex; 
			
			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);

                // 5. Directly use vertex coordinates as sampling indices
                //    v.vertex.xyz is the cube's model space coordinates
                //    (0.5, 0.5, 0.5), (0.5, 0.5, -0.5), (-0.5, 0.5, 0.5) ... for a unit cube
                o.texcoord_3f = v.vertex.xyz;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                // 6. texCUBE, not tex2D
                half4 texCol = texCUBE(_CubeMapTex, i.texcoord_3f);

                // 7. Let's play around the coordinate values
                // half4 texCol = texCUBE(_CubeMapTex, float3(i.texcoord_3f.x * 2, i.texcoord_3f.y * 3, i.texcoord_3f.z * 4));

				return texCol;
			}
			ENDCG
		}
	}
}
