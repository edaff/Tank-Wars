using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTankSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] tanksPreFabs;
    [SerializeField] static GameObject tankSpawner;
  
    Vector3 tankSpawnLocation = new Vector3(5, 1, 9);


    //will spawn a random tank for the camera to rotate around
    void Start()
    {
        
        Instantiate(tanksPreFabs[Random.Range(0, tanksPreFabs.Length)], transform.position, Quaternion.Euler(0, 180, 0));
    }

}
