using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarTest : MonoBehaviour
{
    [SerializeField] private HpBar hpbar;
    [SerializeField] float hp;
    [SerializeField] int loadTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        //hpbar = FindObjectOfType<HpBar>();
        hp = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(hp >= 0f)
        {
            Debug.Log("Im in it");
            StartCoroutine(MinsHPBar());

        }*/
    }

    IEnumerator MinsHPBar()
    {
        hpbar.SetSize(hp);
        hp -= 0.1f;
        Debug.Log("My hp is" + hp);
        yield return new WaitForSeconds(loadTime);


    }




}
