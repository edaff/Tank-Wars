// https://answers.unity.com/questions/1257281/how-to-rotate-camera-orbit-around-a-game-object-on.html

using UnityEngine;
using System.Collections;

public class CameraAngles : MonoBehaviour
{
    public Vector3 middle;
    public float distance = 2.0f;
    public float xSpeed = 0.0f;
    public float ySpeed = 0.0f;
    public float yMinLimit = 0f;
    public float yMaxLimit = 90f;
    public float distanceMin = 10f;
    public float distanceMax = 10f;
    public float smoothTime = 2f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;

    void Start()
    {
        middle = new Vector3(4.5f, 0f, 4.5f);
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x + 15;

    }
    void LateUpdate()
    {
        velocityY += xSpeed * Input.GetAxis("Horizontal") * .01f;
        velocityX += ySpeed * Input.GetAxis("Vertical") * .01f;

        rotationYAxis += velocityY;
        rotationXAxis -= velocityX;

        if (rotationXAxis < yMinLimit)
        {
            rotationXAxis = yMinLimit;
        }
        if (rotationXAxis > yMaxLimit)
        {
            rotationXAxis = yMaxLimit;
        }
        Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
        Quaternion rotation = toRotation;

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
        RaycastHit hit;
        if (Physics.Linecast(middle, transform.position, out hit))
        {
            distance -= hit.distance;
        }
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + middle;

        transform.rotation = rotation;
        transform.position = position;
        velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
        velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
    }
}