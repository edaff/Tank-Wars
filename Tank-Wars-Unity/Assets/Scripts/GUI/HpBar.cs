using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{

    [SerializeField] GameObject bar;
    float hp = 1f;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color invisibility;
    [SerializeField] private Color normalHp;

    private void Awake()
    {
        bar.transform.localScale = new Vector3(hp, 1f);
    }


    public void SetSize(float sizeNormalized)
    {
        if (sizeNormalized >= 0f && sizeNormalized <= 1f)
        {
            sr.color = normalHp;
            //Debug.Log("sizeNormalized is " + sizeNormalized);
            bar.transform.localScale = new Vector3(sizeNormalized, 1f);
            //Debug.Log("Hp is " + hp + " " + "Sizenormalized is " + sizeNormalized);
        }
        else if(sizeNormalized > 1f)
        {
            sr.color = invisibility;
            bar.transform.localScale = new Vector3(hp, 1f);
            //Debug.Log("Hp is " + hp + " " + "Sizenormalized is " + sizeNormalized);
        }
        else
        {
            bar.transform.localScale = new Vector3(0, 1f);
        }
    }

}
