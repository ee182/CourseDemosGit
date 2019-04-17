using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PostProcessingDemo : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float grayScaleAmount = 1.0f;

    [SerializeField]
    private Material curMaterial;
    #endregion

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        curMaterial.SetFloat("_LuminosityAmount", grayScaleAmount);

        Graphics.Blit(sourceTexture, destTexture, curMaterial);
    }
    
    void Update()
    {
        grayScaleAmount = Mathf.Clamp(grayScaleAmount, 0.0f, 1.0f);
    }
}
