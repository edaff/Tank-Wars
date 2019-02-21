﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScreen : MonoBehaviour
{

    [SerializeField] int whatLevelAmI = 1;                          //checks which diff
    int p1ArrayIdex = 0;                                            //keeps track of players 1 tank pick in the array
    int p2ArrayIdex = 0;                                            //keeps track of players 2 tank pick in the array

    [SerializeField] int[] player1TankPicks;                        //holds players 1 picks
    [SerializeField] int[] player2TankPicks;                        //holds players 1 picks


    LevelDifficuityInfo difficuity;


    void Start()
    {
        difficuity = FindObjectOfType<LevelDifficuityInfo>();
        whatLevelAmI = difficuity.GetDifficuity();

        player1TankPicks = new int[] {0, 0, 0};                     //players 1 picks, 0 = empty, 1 = normal tank, 2 = sniper tank, 3 = mortar tank
        player2TankPicks = new int[] {0, 0, 0};                     //checks which difficulty was picked, 1 = easy, 2 = medium , 3 = hard.
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q was hit");
            Player1Qpick();

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("w was hit");
            if (whatLevelAmI >= 2) { Player1Wpick(); }
             
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("e was hit");
            if(whatLevelAmI >= 3) { Player1Epick(); }
            
        }


    }

    private void Player1Qpick()
    {
        if(whatLevelAmI > p1ArrayIdex) 
        {
            player1TankPicks[p1ArrayIdex] = 1;
            p1ArrayIdex++;
            return;
        }
        else
        {
            return;
        }
    }

    private void Player1Wpick()
    {
        if (whatLevelAmI > p1ArrayIdex)
        {
            player1TankPicks[p1ArrayIdex] = 2;
            p1ArrayIdex++;
            return;
        }
        else
        {
            return;
        }
    }

    private void Player1Epick()
    {
        if (whatLevelAmI > p1ArrayIdex)
        {
            player1TankPicks[p1ArrayIdex] = 3;
            p1ArrayIdex++;
            return;
        }
        else
        {
            return;
        }
    }

    public void Player1RedoPick()
    {
        p1ArrayIdex = 0;
    }




    public int GetWhatLevelAmI()
    {
        return whatLevelAmI;
    }

}
