using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngles : MonoBehaviour
{
    public Vector3 middle;
    private float initialFOV;
    public float minZoomLimit = .1f;
    public float maxZoomLimit = 10f;
    public float distance = 0f;
    private float xRotate;
    private float yRotate;
    public float xRotateSpeed = 3.0f;
    public float yRotateSpeed = 1.5f;
    public float maxYRotate = 90.0f;
    public float minYRotate = 0.0f;
    public float xMoveSpeed = 100.0f;
    public float zMoveSpeed = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        middle = new Vector3(4.5f, 0f, 4.5f);
        Vector3 angles = transform.eulerAngles;
        xRotate = angles.x;
        yRotate = angles.y + 45;
        Camera.main.fieldOfView += Camera.main.fieldOfView - 115;
        initialFOV = Camera.main.fieldOfView;
        transform.position = new Vector3(0, 0, -distance) + middle;
    }

    // LateUpdate is called once at the end of a frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            float zoom = Camera.main.fieldOfView - Input.GetAxis("Vertical") * yRotateSpeed;
            if (zoom >= initialFOV / maxZoomLimit && zoom <= initialFOV / minZoomLimit)
            {
                Camera.main.fieldOfView -= Input.GetAxis("Vertical") * yRotateSpeed;
            }
        }
        else
        {
            xRotate += Input.GetAxis("Horizontal") * xRotateSpeed;
            yRotate += Input.GetAxis("Vertical") * yRotateSpeed;
            if (yRotate > 360) yRotate -= 360;
            if (yRotate < -360) yRotate += 360;
            yRotate = Mathf.Clamp(yRotate, minYRotate, maxYRotate);
        }

        var rotation = Quaternion.Euler(yRotate, xRotate, 0);
        var position = rotation * (new Vector3(0, 0, -distance)) + middle;
        transform.rotation = rotation;
        transform.position = position;
    }
}

