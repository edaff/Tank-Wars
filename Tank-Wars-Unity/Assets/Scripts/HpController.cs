using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{
    [SerializeField] HpBar blueTank1Hp;
    [SerializeField] HpBar redTank1Hp;


    // Start is called before the first frame update
    void Start()
    {
        blueTank1Hp = FindObjectOfType<HpBar>();
        redTank1Hp = FindObjectOfType<HpBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlueTank1Hp(int x)
    {
        float y = (float)(x / 100);
        blueTank1Hp.SetSize((float)y);
    }



}
