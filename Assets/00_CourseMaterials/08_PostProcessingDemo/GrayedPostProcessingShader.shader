Shader "01_CourseDemos/GrayedPostEffect"
{
    Properties
    {
        // 1. Do not set _MainTex while using this shader in OnRenderImage() callback
        //    _MainTex will be automatically set to sourceTexture
        _MainTex("Base (RGB)", 2D) = "white" {}
        _LuminosityAmount("GrayScale Amount", Range(0.0, 1.0)) = 1.0
    }
    
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            fixed _LuminosityAmount;

            fixed4 frag(v2f_img i) : COLOR
            {
                // 2. Sampling the image of game
                fixed4 renderTex = tex2D(_MainTex, i.uv);

                // 3. Combine R G B colors into a gray scale
                float luminosity = 0.299 * renderTex.r + 0.587 * renderTex.g + 0.114 * renderTex.b;

                // 4. Output the gray scale, interpolated by _LuminosityAmount
                fixed4 finalColor = lerp(renderTex, luminosity, _LuminosityAmount);

                return finalColor;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
