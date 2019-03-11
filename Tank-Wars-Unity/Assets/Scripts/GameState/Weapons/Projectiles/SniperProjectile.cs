using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperProjectile : MonoBehaviour
{
    public GameObject projectile;
    private GameObject clone;
    public Transform spawnpoint;
    [SerializeField] float speed = 10;

    private void Update()
    {
        if (clone != null)
        {
            clone.transform.Translate(spawnpoint.transform.forward * Time.deltaTime * speed);
        }
    }

    public void fire()
    {
        AudioSource sound = gameObject.GetComponent<AudioSource>();
        sound.Play();
        clone = Instantiate(projectile, spawnpoint.position, Quaternion.identity);
    }
}
