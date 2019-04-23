using UnityEngine;

public class CameraFlipX : MonoBehaviour
{
    void Start()
    {
        var camera = GetComponent<Camera>();
        camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }
}
