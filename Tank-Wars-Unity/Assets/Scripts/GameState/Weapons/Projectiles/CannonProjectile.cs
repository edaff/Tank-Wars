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


    // Update is called once per frame
    void Update()
    {
        if (startProgress < 1 && startProgress >= 0)
        {
            startProgress += Time.deltaTime * 5;
            gun.rotation = Quaternion.Lerp(startRotation, endRotation, startProgress);
        }

        if (clone != null)
        {
            clone.transform.Translate(spawnpoint.transform.forward * Time.deltaTime * speed);
            endProgress = 0;
        }

        if(endProgress < 1 && endProgress >= 0)
        {
            endProgress += Time.deltaTime * 5;
            gun.rotation = Quaternion.Lerp(endRotation, startRotation, endProgress);
        }
    }

    public void fire(Orientations orientation)
    {
        startRotation = gun.rotation;
        endRotation = Quaternion.Euler(0f, (float)orientation, 0f);
        startProgress = 0;
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
