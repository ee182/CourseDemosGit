using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    void Start()
    {
        var camera = GetComponent<Camera>();
        camera.aspect = 1.0f;
    }
}
