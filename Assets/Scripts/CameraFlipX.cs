using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlipX : MonoBehaviour
{
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        var camera = GetComponent<Camera>();

        camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
