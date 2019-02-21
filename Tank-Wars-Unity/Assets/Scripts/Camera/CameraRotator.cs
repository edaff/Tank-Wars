using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] float speed = 10f;                 //speed that the camera will rotate
    
    //the function that drives the rotation of the camera
    void Update()
    {
        transform.Rotate(0,speed * Time.deltaTime, 0);
    }
}
