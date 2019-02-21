using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScreen : MonoBehaviour
{
    [SerializeField] bool player1FishedPicking = false;             //if this goes true, player 1 pick phase has ended
    [SerializeField] bool player2FishedPicking = false;             //if this goes true, player 1 pick phase has ended
    [SerializeField] bool bothPlayersReady = false;                 //if this goes true, both players are ready and it will load next level

    [SerializeField] int whatLevelAmI = 1;                          //checks which diff

    int p1ArrayIdex = 0;                                            //keeps track of players 1 tank pick in the array
    int p2ArrayIdex = 0;                                            //keeps track of players 2 tank pick in the array

    [SerializeField] int[] player1TankPicks;                        //holds players 1 picks
    [SerializeField] int[] player2TankPicks;                        //holds players 1 picks

    [SerializeField] GameObject p1Ready;
    [SerializeField] GameObject p1Redo;

    GameStatus currentState;


    void Start()
    {
        currentState = FindObjectOfType<GameStatus>();
        whatLevelAmI = currentState.GetDifficuity();

        player1TankPicks = new int[] {0, 0, 0};                     //players 1 picks, 0 = empty, 1 = normal tank, 2 = sniper tank, 3 = mortar tank
        player2TankPicks = new int[] {0, 0, 0};                     //checks which difficulty was picked, 1 = easy, 2 = medium , 3 = hard.
    }


    void Update()
    {
        if(p1ArrayIdex == whatLevelAmI && !player1FishedPicking)
        {
            p1Redo.SetActive(true);
            p1Ready.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q was hit");
            Player1Qpick();

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("w was hit");
            Player1Wpick();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("e was hit");
            Player1Epick();
            
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
        p1Redo.SetActive(false);
        p1Ready.SetActive(false);
        p1ArrayIdex = 0;
    }

    public void p1DonePicking()
    {
        p1Ready.SetActive(false);
        p1Redo.SetActive(false);
        player1FishedPicking = true;
        currentState.SetPlayer1TanksPick(player1TankPicks);
    }




}
