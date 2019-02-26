using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{

    [SerializeField] GameObject bar;
    float hp = 1f;


    // Start is called before the first frame update
    void Start()
    {

        bar.transform.localScale = new Vector3(hp, 1f);
        //Transform bar = transform.Find("Bar");
        //bar.localScale = new Vector3(.75f, 1f);
    }


    public void SetSize(float sizeNormalized)
    {
        if (hp >= 0f)
        {
            bar.transform.localScale = new Vector3(sizeNormalized, 1f);
        }
    }

}
