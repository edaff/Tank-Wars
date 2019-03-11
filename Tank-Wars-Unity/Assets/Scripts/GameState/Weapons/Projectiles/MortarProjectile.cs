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
            // Compute the next position, with arc added in
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
                // Rotate to face the next position, and then move there
                Vector2 forward = nextPos - clone.transform.position;
                //clone.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
                clone.transform.position = nextPos;
                endProgress = 0;
            }

            else if(y1 - y0 != 0)
            {
                float dist = y1 - y0;
                float nextY = Mathf.MoveTowards(clone.transform.position.z, y1, speed * Time.deltaTime);
                float baseY = Mathf.Lerp(start.y, end.y, (nextY - y0) / dist);
                float arc = height * (nextY - y0) * (nextY - y1) / (-0.25f * dist * dist);
                nextPos = new Vector3(clone.transform.position.x, baseY + arc, nextY);
                // Rotate to face the next position, and then move there
                Vector2 forward = nextPos - clone.transform.position;
                //clone.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
                clone.transform.position = nextPos;
                endProgress = 0;
            }
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
        yield return new WaitForSeconds(.25f);
        AudioSource sound = gameObject.GetComponent<AudioSource>();
        sound.Play();
        clone = Instantiate(projectile, spawnpoint.position, Quaternion.identity);
    }
}
