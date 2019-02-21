using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelDifficuityInfo : MonoBehaviour
{
   
    [SerializeField] int difficuity;                                //this will be passed in tankSelect
    GameStatus setDifficuity;

    void Start()
    {
        setDifficuity = FindObjectOfType<GameStatus>();
    }



    public void Easy()
    {
        difficuity = 1;
        setDifficuity.SetDifficuity(difficuity);
    }

    public void Medium()
    {
        difficuity = 2;
        setDifficuity.SetDifficuity(difficuity);
    }

    public void Hard()
    {
        difficuity = 3;
        setDifficuity.SetDifficuity(difficuity);
    }

}
