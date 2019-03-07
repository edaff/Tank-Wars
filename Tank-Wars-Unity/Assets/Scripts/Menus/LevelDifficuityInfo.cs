using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelDifficuityInfo : MonoBehaviour
{
   
    [SerializeField] int difficuity;                                //this will be passed in tankSelect
    [SerializeField] bool aiON = false;
    GameStatus setDifficuity;

    void Start()
    {
        setDifficuity = FindObjectOfType<GameStatus>();
    }

    //PvP
    public void Easy()
    {
        difficuity = 1;
        aiON = false;
        setDifficuity.SetDifficuity(difficuity, aiON);
    }

    public void Medium()
    {
        difficuity = 2;
        aiON = false;
        setDifficuity.SetDifficuity(difficuity, aiON);
    }

    public void Hard()
    {
        difficuity = 3;
        aiON = false;
        setDifficuity.SetDifficuity(difficuity, aiON);
    }

    //PvAI
    public void EasyAI()
    {
        difficuity = 1;
        aiON = true;
        setDifficuity.SetDifficuity(difficuity, aiON);
    }

    public void MediumAI()
    {
        difficuity = 2;
        aiON = true;
        setDifficuity.SetDifficuity(difficuity, aiON);
    }

    public void HardAI()
    {
        difficuity = 3;
        aiON = true;
        setDifficuity.SetDifficuity(difficuity, aiON);
    }

}
