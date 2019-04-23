using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionRenderer : MonoBehaviour
{
    [SerializeField]
    private Cubemap cubemap;

    void Start()
    {
        // StartCoroutine(RenderCubemap(0.032f)); // 0.032 second => 30 fps
    }

    private IEnumerator RenderCubemap(float waitTime)
    {
        while (true)
        {
            doRenderCubeMap();

            yield return new WaitForSeconds(waitTime);
        }
    }

    void Update()
    {
        doRenderCubeMap();
    }

    private void setupRenderCamera(Camera camera)
    {
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("RaceCar"));
        // camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Trees"));
        // camera.cullingMask |= 1 << LayerMask.NameToLayer("SomeLayer");

        camera.nearClipPlane = 0.001f;
        camera.farClipPlane = 1000.0f;
        camera.fieldOfView = 10.0f;
    }
    
    private void doRenderCubeMap()
    {
        GameObject go = new GameObject("CubemapCamera");
        go.AddComponent<Camera>();

        var camera = go.GetComponent<Camera>();

        setupRenderCamera(camera);

        // place it on the object
        camera.transform.position = transform.position;
        camera.transform.rotation = transform.rotation; // RenderToCubemap() has orientation issue.

        // Shader.globalMaximumLOD = 100;
        camera.RenderToCubemap(cubemap);
        // Shader.globalMaximumLOD = int.MaxValue;

        DestroyImmediate(go);
    }
}
