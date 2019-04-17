using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionRenderer : MonoBehaviour
{
    [SerializeField]
    private Cubemap cubemap;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(RenderCubemap(0.032f));
    }

    // Update is called once per frame
    void Update()
    {
        doRenderCubeMap();
    }
    
    private void doRenderCubeMap()
    {
        // create temporary camera for rendering
        GameObject go = new GameObject("CubemapCamera");
        go.AddComponent<Camera>();




        var camera = go.GetComponent<Camera>();
        // camera.cullingMask |= 1 << LayerMask.NameToLayer("SomeLayer");
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("RaceCar"));
        // camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Trees"));

        camera.nearClipPlane = 0.001f;
        camera.farClipPlane = 1000.0f;
        camera.fieldOfView = 10.0f;

        // place it on the object
        camera.transform.position = transform.position;
        //  RenderToCubemap() has orientation issue.
        camera.transform.rotation = transform.rotation;

        // camera.transform.Translate(Vector3.up * 3.0f, Space.World);

        // render into cubemap
        camera.RenderToCubemap(cubemap);


        
        // destroy temporary camera
        DestroyImmediate(go);

    
        //Shader.globalMaximumLOD = 100;
        //Shader.globalMaximumLOD = int.MaxValue;
    }

    private IEnumerator RenderCubemap(float waitTime)
    {
        while (true)
        {
            doRenderCubeMap();

            yield return new WaitForSeconds(waitTime);
        }
    }
}
