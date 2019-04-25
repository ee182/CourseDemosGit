using UnityEngine;

[ExecuteInEditMode]
public class GrayedPostProcessing : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float grayScaleAmount = 1.0f;

    [SerializeField]
    private Material curMaterial;
    #endregion

    // 1. OnRenderImage() will be called after camera frame is rendered.
    //    Attaching this MonoBehavior to a camera will take the effect.
    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        curMaterial.SetFloat("_LuminosityAmount", grayScaleAmount);

        // 2. sourceTexture is the source needed to be processed.
        //    In this case, it is the camera's back buffer (the image of game so far).
        // 3. destTexture is the output texture (the image which is processed)
        //    If destTexture is null, Graphics.Blit() will directly modify the display's back buffer.
        //    This is equivelent to "render to screen"
        //    OnRenderImage()'s destTexture is null.
        // 4. Graphics.Blit() used curMaterial to modify the sourceTexture (camera image) and output to destTexture
        Graphics.Blit(sourceTexture, destTexture, curMaterial);
    }

    void Update()
    {
        grayScaleAmount = Mathf.Clamp(grayScaleAmount, 0.0f, 1.0f);
    }
}
