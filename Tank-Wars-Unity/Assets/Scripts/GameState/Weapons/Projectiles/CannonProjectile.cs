using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{ 
    public GameObject projectile;
    private GameObject clone;
    public Transform spawnpoint;
    public Transform gun;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private float startProgress = -1;
    private float endProgress = -1;
    [SerializeField] float speed = 10;
    private bool rotateBackOnce = false;
    private bool rotateTowardsOnce = false;


    // Update is called once per frame
    void Update()
    {
        if (rotateTowardsOnce == true)
        {
            rotateTowardsOnce = false;
            StartCoroutine(rotateTowards());
        }

        if (clone != null)
        {
            clone.transform.Translate(spawnpoint.transform.forward * Time.deltaTime * speed);
            rotateBackOnce = true;
        }

        if (rotateBackOnce == true)
        {
            rotateBackOnce = false;
            StartCoroutine(rotateBack());
        }
    }

    public void fire(Orientations orientation)
    {
        startRotation = gun.rotation;
        endRotation = Quaternion.Euler(0f, (float)orientation, 0f);
        rotateTowardsOnce = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        AudioSource sound = gameObject.GetComponent<AudioSource>();
        sound.Play();
        clone = Instantiate(projectile, spawnpoint.position, Quaternion.identity);
    }

    IEnumerator rotateTowards()
    {
        startProgress = Time.fixedDeltaTime * 5;
        float rotT = 0;
        while(rotT <= 1.0f)
        {
            rotT += startProgress;
            gun.rotation = Quaternion.Lerp(startRotation, endRotation, rotT);
            yield return new WaitForFixedUpdate();
        }
        gun.rotation = endRotation;
        StartCoroutine(Wait());
    }

    IEnumerator rotateBack()
    {
        yield return new WaitForSeconds(.5f);
        endProgress = Time.fixedDeltaTime * 5;
        float rotT = 0;
        while (rotT <= 1.0f)
        {
            rotT += endProgress;
            gun.rotation = Quaternion.Lerp(endRotation, startRotation, rotT);
            yield return new WaitForFixedUpdate();
        }
        gun.rotation = startRotation;
    }
}
