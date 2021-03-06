﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BloomPostProcessing : MonoBehaviour
{
    public int downSample = 1;
    public int samplerScale = 1;
    public Color colorThreshold = Color.gray;
    public Color bloomColor = Color.white;

    [Range(0.0f, 50.0f)]
    public float bloomFactor = 0.5f;

    public Material _Material;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_Material)
        {
            // 1. Get 2 down-sampled RenderTextures.
            RenderTexture temp1 = RenderTexture.GetTemporary(source.width >> downSample, source.height >> downSample, 0, source.format);
            RenderTexture temp2 = RenderTexture.GetTemporary(source.width >> downSample, source.height >> downSample, 0, source.format);

            // 2. Down-sample the source (game image) into temp1.
            //    No material means coping image.
            Graphics.Blit(source, temp1);

            _Material.SetVector("_colorThreshold", colorThreshold);
            // 3. Only use pass 0 of the shader.
            //    pass 0: Extract high-lighting region
            Graphics.Blit(temp1, temp2, _Material, 0);

            // 4. Gaussian Blur twice (x and y directions)
            //    pass 1: Gaussian Blur
            _Material.SetVector("_offsets", new Vector4(0, samplerScale, 0, 0));
            Graphics.Blit(temp2, temp1, _Material, 1);

            _Material.SetVector("_offsets", new Vector4(samplerScale, 0, 0, 0));
            Graphics.Blit(temp1, temp2, _Material, 1);

            // 5. Set bloom parameters
            //    The previous result temp2 is set to _BlurTex texture
            _Material.SetTexture("_BlurTex", temp2);
            _Material.SetVector("_bloomColor", bloomColor);
            _Material.SetFloat("_bloomFactor", bloomFactor);

            // 6. Only use pass 2 of the shader.
            //    pass 2: Bloom
            //    source is the original game image, destination is null (directly to the screen).
            Graphics.Blit(source, destination, _Material, 2);

            // 7. Release RenderTextures.
            RenderTexture.ReleaseTemporary(temp1);
            RenderTexture.ReleaseTemporary(temp2);
        }
    }
}
