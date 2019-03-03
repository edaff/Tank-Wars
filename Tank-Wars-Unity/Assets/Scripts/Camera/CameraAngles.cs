using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngles : MonoBehaviour
{
    public int select = 0;
    public bool birdCam = false;
    public bool angleCam = false;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(4.5f, 10f, 4.5f);
        Camera.main.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }


    // Update is called once per frame
    void Update()
    {
        if (select == 4)
        {
            select = 0;
        }

        if (birdCam || angleCam)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                select++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && birdCam == false && angleCam == false)
        {
            birdCam = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && birdCam == true)
        {
            birdCam = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && angleCam == false && birdCam == false)
        {
            angleCam = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && angleCam == true)
        {
            angleCam = false;
        }

        if (birdCam)
        {
            if (select == 0)
            {
                Camera.main.transform.position = new Vector3(4.5f, 10f, 4.5f);
                Camera.main.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            }
            else if (select == 1)
            {
                Camera.main.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            }
            else if (select == 2)
            {
                Camera.main.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
            }
            else
            {
                Camera.main.transform.rotation = Quaternion.Euler(90f, 270f, 0f);
            }
        }

        else if (angleCam)
        {
            if (select == 0)
            {
                Camera.main.transform.position = new Vector3(9.5f, 9f, -0.5f);
                Camera.main.transform.rotation = Quaternion.Euler(60f, -45f, 0f);
            }
            else if (select == 1)
            {
                Camera.main.transform.position = new Vector3(9.5f, 9f, 9.5f);
                Camera.main.transform.rotation = Quaternion.Euler(60f, -135f, 0f);
            }
            else if (select == 2)
            {
                Camera.main.transform.position = new Vector3(-0.5f, 9f, 9.5f);
                Camera.main.transform.rotation = Quaternion.Euler(60f, 135f, 0f);
            }
            else
            {
                Camera.main.transform.position = new Vector3(-0.5f, 9f, -0.5f);
                Camera.main.transform.rotation = Quaternion.Euler(60f, 45f, 0f);
            }
        }
    }


    // get the camera to rotate around the center of the map at any 
    // camera position
    void rotateCamAtCurrentPosition()
    {
        transform.Rotate(0, -Input.GetAxis("Mouse X") * 50, 0);
    }

    // needs to follow the tank that is clicked only if its the current players tank
    void followPlayerCam()
    {
        if (player.getPlayerColor() == PlayerColors.Red)
        {
            //  player.getPlayerTanks()[0];
        }
        else if (player.getPlayerColor() == PlayerColors.Blue)
        {

        }
    }
}

