using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperProjectile : MonoBehaviour
{
    public GameObject projectile;
    private GameObject clone;
    public Transform spawnpoint;

    private void Update()
    {
        if (clone != null)
        {
            clone.transform.Translate(spawnpoint.transform.forward * Time.deltaTime * 10);
        }
    }

    public void fire()
    {
        AudioSource sound = gameObject.GetComponent<AudioSource>();
        sound.Play();
        clone = Instantiate(projectile, spawnpoint.position, Quaternion.identity);
    }
}
