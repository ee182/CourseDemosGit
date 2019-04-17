using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PostProcessingDemo2 : MonoBehaviour
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
            // Get 2 down-sampled RenderTextures.
            RenderTexture temp1 = RenderTexture.GetTemporary(source.width >> downSample, source.height >> downSample, 0, source.format);
            RenderTexture temp2 = RenderTexture.GetTemporary(source.width >> downSample, source.height >> downSample, 0, source.format);

            // Down-sample the source into temp1.
            Graphics.Blit(source, temp1);

            _Material.SetVector("_colorThreshold", colorThreshold);
            // Only use pass 0 of the shader.
            Graphics.Blit(temp1, temp2, _Material, 0);

            // Gaussian Blur twice (x and y directions)
            // Only use pass 1 of the shader.
            _Material.SetVector("_offsets", new Vector4(0, samplerScale, 0, 0));
            Graphics.Blit(temp2, temp1, _Material, 1);

            _Material.SetVector("_offsets", new Vector4(samplerScale, 0, 0, 0));
            Graphics.Blit(temp1, temp2, _Material, 1);

            // Bloom
            _Material.SetTexture("_BlurTex", temp2);
            _Material.SetVector("_bloomColor", bloomColor);
            _Material.SetFloat("_bloomFactor", bloomFactor);

            // Only use pass 2 of the shader.
            // source to MainTex, destination is null.
            Graphics.Blit(source, destination, _Material, 2);

            // Release RenderTextures.
            RenderTexture.ReleaseTemporary(temp1);
            RenderTexture.ReleaseTemporary(temp2);
        }
    }
}
