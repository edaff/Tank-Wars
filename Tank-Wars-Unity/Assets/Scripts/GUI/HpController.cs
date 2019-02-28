using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{
    [SerializeField] HpBar redTank1Hp;
    [SerializeField] HpBar redTank2Hp;
    [SerializeField] HpBar redTank3Hp;
    [SerializeField] HpBar blueTank1Hp;
    [SerializeField] HpBar blueTank2Hp;
    [SerializeField] HpBar blueTank3Hp;

    // *******************************
    //      Blue Tank Health Bars
    // *******************************

    public void BlueTank1Hp(int x)
    {
        //Debug.Log("Blue Tanks1hp was called and hp was passed in " + x);
        float y = (float)x;
        y = (x / 100f);
        //Debug.Log("after x was normalized y is " + y);
        //Debug.Log("y is " + y);
        blueTank1Hp.SetSize((float)y);
    }

    public void BlueTank2Hp(int x) {
        //Debug.Log("Blue Tanks1hp was called and hp was passed in " + x);
        float y = (float)x;
        y = (x / 100f);
        //Debug.Log("after x was normalized y is " + y);
        //Debug.Log("y is " + y);
        blueTank2Hp.SetSize((float)y);
    }

    public void BlueTank3Hp(int x) {
        //Debug.Log("Blue Tanks1hp was called and hp was passed in " + x);
        float y = (float)x;
        y = (x / 100f);
        //Debug.Log("after x was normalized y is " + y);
        //Debug.Log("y is " + y);
        blueTank3Hp.SetSize((float)y);
    }

    // *******************************
    //      Red Tank Health Bars
    // *******************************

    public void RedTank1Hp(int x)
    {

        float y = (float)x;
        y = (x / 100f);
        //Debug.Log("y is " + y);
        redTank1Hp.SetSize((float)y);
    }

    public void RedTank2Hp(int x) {

        float y = (float)x;
        y = (x / 100f);
        //Debug.Log("y is " + y);
        redTank2Hp.SetSize((float)y);
    }

    public void RedTank3Hp(int x) {

        float y = (float)x;
        y = (x / 100f);
        //Debug.Log("y is " + y);
        redTank3Hp.SetSize((float)y);
    }



}
