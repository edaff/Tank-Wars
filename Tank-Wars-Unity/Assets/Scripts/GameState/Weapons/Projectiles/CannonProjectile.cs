using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{ 
    public GameObject projectile;
    public Transform spawnpoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject clone;
            clone = Instantiate(projectile, spawnpoint.position, Quaternion.identity);
            clone.transform.Translate(Vector3.forward * Time.deltaTime * 20);
            AudioSource sound = gameObject.GetComponent<AudioSource>();
            sound.Play();
        }
    }
}
