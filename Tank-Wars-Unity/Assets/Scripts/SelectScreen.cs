using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScreen : MonoBehaviour
{

    [SerializeField] int whatLevelAmI;                              //checks which diff
    int p1ArrayIdex = 0;                                            //keeps track of players 1 tank pick in the array
    int p2ArrayIdex = 0;                                            //keeps track of players 2 tank pick in the array

    [SerializeField] int[] player1TankPicks;                        //holds players 1 picks
    [SerializeField] int[] player2TankPicks;                        //holds players 1 picks



    void Start()
    {
        player1TankPicks = new int[] {0, 0, 0};                     //players 1 picks, 0 = empty, 1 = normal tank, 2 = sniper tank, 3 = mortar tank
        player2TankPicks = new int[] {0, 0, 0};

        //update wahtlevelami                                       //checks which difficulty was picked, 1 = easy, 2 = medium , 3 = hard.
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Player1pick();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Player1pick();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Player1pick();
        }


    }

    private void Player1pick()
    {
        if(p1ArrayIdex >= whatLevelAmI)
        {
            return;
        }
        else
        {

        }
    }

}
