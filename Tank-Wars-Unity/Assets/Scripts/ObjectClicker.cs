using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour {

    public GameObject objectClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    Rigidbody rb;
                    if (rb = hit.transform.GetComponent<Rigidbody>())
                    {
                    	objectClicked = hit.transform.gameObject;
                    	print(hit.transform.gameObject);
                    	/*
                        //print(hit.transform.gameObject);
                        if(hit.transform.gameObject.tag == "Tank"){
                            tankClicked = hit.transform.gameObject;
                        }
                           else if(hit.transform.gameObject.tag == "Tile" && tankClicked != null){
                               tileClicked = hit.transform.gameObject;
                              //print("tankClicked: ");
                              //print(tankClicked);
                              //print("tileClicked: ");
                              //print(tileClicked);
                              translateVector = new Vector3(tileClicked.transform.position.x, 1f,tileClicked.transform.position.z);
                              tankClicked.transform.position = translateVector;
                              tileClicked = null;
                              tankClicked = null;
                           }
                           */
                    }
                }
            }
        }
    }
}