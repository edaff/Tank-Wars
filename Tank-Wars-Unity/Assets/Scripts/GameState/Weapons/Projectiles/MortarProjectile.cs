//http://luminaryapps.com/blog/arcing-projectiles-in-unity/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarProjectile : MonoBehaviour
{
    public GameObject projectile;
    private GameObject clone;
    public Transform spawnpoint;
    public Transform gun;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private Vector3 start;
    private Vector3 end;
    private Vector3 nextPos;
    [SerializeField] float height = 3;
    [SerializeField] float speed = 3;
    private float startProgress = -1;
    private float endProgress = -1;
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
            float x0 = start.x;
            float x1 = end.x;
            float y0 = start.z;
            float y1 = end.z;
            if (x1 - x0 != 0)
            {
                float dist = x1 - x0;
                float nextX = Mathf.MoveTowards(clone.transform.position.x, x1, speed * Time.deltaTime);
                float baseY = Mathf.Lerp(start.y, end.y, (nextX - x0) / dist);
                float arc = height * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
                nextPos = new Vector3(nextX, baseY + arc, clone.transform.position.z);
                Vector2 forward = nextPos - clone.transform.position;
                clone.transform.position = nextPos;
                rotateBackOnce = true;
            }

            else if(y1 - y0 != 0)
            {
                float dist = y1 - y0;
                float nextY = Mathf.MoveTowards(clone.transform.position.z, y1, speed * Time.deltaTime);
                float baseY = Mathf.Lerp(start.y, end.y, (nextY - y0) / dist);
                float arc = height * (nextY - y0) * (nextY - y1) / (-0.25f * dist * dist);
                nextPos = new Vector3(clone.transform.position.x, baseY + arc, nextY);
                Vector2 forward = nextPos - clone.transform.position;
                clone.transform.position = nextPos;
                rotateBackOnce = true;
            }
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

    public void setStart(int currentTankX, int currentTankY)
    {
        start = new Vector3(currentTankX, 0f, currentTankY);
    }
    
    public void setEnd(int targetNodeX, int targetNodeY)
    {
        end = new Vector3(targetNodeX, 0f, targetNodeY);
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
