using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{ 
    public GameObject projectile;
    private GameObject clone;
    public Transform spawnpoint;
    public Transform gun;
    public Quaternion startRotation;
    public Quaternion endRotation;
    public float rotationProgress = -1;

    // Update is called once per frame
    void Update()
    {
        if (rotationProgress < 1 && rotationProgress >= 0)
        {
            rotationProgress += Time.deltaTime * 5;
            gun.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
        }

        if (clone != null)
        {
            clone.transform.Translate(spawnpoint.transform.forward * Time.deltaTime * 10);
        }
    }

    public void fire(Orientations orientation)
    {
        startRotation = gun.rotation;
        endRotation = Quaternion.Euler(0f, (float)orientation, 0f);
        rotationProgress = 0;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.25f);
        AudioSource sound = gameObject.GetComponent<AudioSource>();
        sound.Play();
        clone = Instantiate(projectile, spawnpoint.position, Quaternion.identity);
    }
}
