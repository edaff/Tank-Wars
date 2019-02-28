using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{

    [SerializeField] GameObject bar;
    float hp = 1f;

    private void Awake()
    {
        bar.transform.localScale = new Vector3(hp, 1f);
    }


    public void SetSize(float sizeNormalized)
    {
        if (hp >= 0f)
        {
            Debug.Log("sizeNormalized is " + sizeNormalized);
            bar.transform.localScale = new Vector3(sizeNormalized, 1f);
        }
    }

}
