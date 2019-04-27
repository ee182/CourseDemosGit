using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
    private Camera camera;

    private float scale = 0.8f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float wasdSpeed = 0.25f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        float mouseScrollY = Input.mouseScrollDelta.y * scale;

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if (camera != null)
        {
            camera.transform.Translate(new Vector3(xAxisValue * wasdSpeed, mouseScrollY, zAxisValue * wasdSpeed));
            camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
