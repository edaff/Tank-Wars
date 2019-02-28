using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{
    [SerializeField] HpBar blueTank1Hp;
    [SerializeField] HpBar redTank1Hp;


    public void BlueTank1Hp(int x)
    {
        Debug.Log("Blue Tanks1hp was called and hp was passed in " + x);
        float y = (float)x;
        y = (x / 100f);
        Debug.Log("after x was normalized y is " + y);
        //Debug.Log("y is " + y);
        blueTank1Hp.SetSize((float)y);
    }

    public void RedTank1Hp(int x)
    {

        float y = (float)x;
        y = (x / 100f);
        //Debug.Log("y is " + y);
        redTank1Hp.SetSize((float)y);
    }



}
