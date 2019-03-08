// https://answers.unity.com/questions/1257281/how-to-rotate-camera-orbit-around-a-game-object-on.html

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraAngles : MonoBehaviour
{
    Vector3 middle;
    float distance = 10.0f;
    float xSpeed = 10.0f;
    float ySpeed = 10.0f;
    float yMinLimit = 0f;
    float yMaxLimit = 90f;
    float distanceMin = 10f;
    float distanceMax = 10f;
    float smoothTime = 2f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;

    void Start()
    {
        middle = new Vector3(4.5f, 0f, 4.5f);
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x + 50;
    }

    void LateUpdate()
    {
        velocityY += xSpeed * Input.GetAxis("Horizontal") * .01f;
        velocityX += ySpeed * Input.GetAxis("Vertical") * .01f;

        rotationYAxis -= velocityY;
        rotationXAxis += velocityX;

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
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + middle;

        transform.rotation = rotation;
        transform.position = position;
        velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
        velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
    }

    public void translateToPlayer(PlayerColors playerTurn)
    {
        if (playerTurn == PlayerColors.Red)
        {
            rotationXAxis = 50f;
            rotationYAxis = 0f;
        }
        else if(playerTurn == PlayerColors.Blue)
        {
            rotationXAxis = 50f;
            rotationYAxis = 180f;
        }
    }
}