using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDestroy : MonoBehaviour
{
    public GameObject boom;
    private GameObject clone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blue Tank")
        {
            Destroy(gameObject);
            clone = Instantiate(boom, other.transform.position, Quaternion.identity);
            Destroy(clone, 2f);
        }
    }
}
