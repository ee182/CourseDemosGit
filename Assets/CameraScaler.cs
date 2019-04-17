using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        var camera = GetComponent<Camera>();

        camera.aspect = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
