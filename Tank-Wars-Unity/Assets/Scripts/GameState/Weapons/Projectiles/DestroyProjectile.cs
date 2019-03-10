using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Red Tank")
        {
            Destroy(gameObject);
        }
        else if(other.tag == "Blue Tank")
        {
            Destroy(gameObject);
        }
    }
}
