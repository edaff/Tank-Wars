using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDestroy : MonoBehaviour
{
    public GameObject boom;
    private GameObject clone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Red Tank")
        {
            Destroy(gameObject);
            clone = Instantiate(boom, other.transform.position, Quaternion.identity);
            Destroy(clone, 2f);
        }
    }
}
