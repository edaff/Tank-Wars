using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScreen : MonoBehaviour
{
    [Header("Player fished picking state")]
    [SerializeField] bool player1FishedPicking = false;             //if this goes true, player 1 pick phase has ended
    [SerializeField] bool player2FishedPicking = false;             //if this goes true, player 1 pick phase has ended
    [SerializeField] bool bothPlayersReady = false;                 //if this goes true, both players are ready and it will load next level

    [Header("Current Difficuity")]
    [SerializeField] int whatLevelAmI = 1;                          //checks which diff

    int p1ArrayIdex = 0;                                            //keeps track of players 1 tank pick in the array
    int p2ArrayIdex = 0;                                            //keeps track of players 2 tank pick in the array

    [Header("Holds Player picks")]
    [SerializeField] int[] player1TankPicks;                        //holds players 1 picks
    [SerializeField] int[] player2TankPicks;                        //holds players 1 picks

    [Header("GUI controller for p1")]
    [SerializeField] GameObject p1Ready;
    [SerializeField] GameObject p1Redo;
    [SerializeField] GameObject p1PickingText;
    [SerializeField] GameObject p1Background;

    [Header("p1 boxes & tanks")]
    [SerializeField] GameObject p1Box2;
    [SerializeField] GameObject p1Box3;
    [SerializeField] GameObject p1Tank2Border;
    [SerializeField] GameObject p1Tank2Background;
    [SerializeField] GameObject p1Tank2;
    [SerializeField] GameObject p1Tank2Key;
    [SerializeField] GameObject p1Tank3Border;
    [SerializeField] GameObject p1Tank3Background;
    [SerializeField] GameObject p1Tank3;
    [SerializeField] GameObject p1Tank3Key;

    [Header("GUI controller for p2")]
    [SerializeField] GameObject p2Ready;
    [SerializeField] GameObject p2Redo;
    [SerializeField] GameObject p2PickingText;
    [SerializeField] GameObject p2Background;

    [Header("p2 boxes & tanks")]
    [SerializeField] GameObject p2Box2;
    [SerializeField] GameObject p2Box3;
    [SerializeField] GameObject p2Tank2Border;
    [SerializeField] GameObject p2Tank2Background;
    [SerializeField] GameObject p2Tank2;
    [SerializeField] GameObject p2Tank2Key;
    [SerializeField] GameObject p2Tank3Border;
    [SerializeField] GameObject p2Tank3Background;
    [SerializeField] GameObject p2Tank3;
    [SerializeField] GameObject p2Tank3Key;

    GameStatus currentState;


    void Start()
    {
        currentState = FindObjectOfType<GameStatus>();
        whatLevelAmI = currentState.GetDifficuity();



        player1TankPicks = new int[] {0, 0, 0};                     //players 1 picks, 0 = empty, 1 = normal tank, 2 = sniper tank, 3 = mortar tank
        player2TankPicks = new int[] {0, 0, 0};                     //checks which difficulty was picked, 1 = easy, 2 = medium , 3 = hard.

        //makes sure all the UI for p1 is turned off at start
        p1Redo.SetActive(false);
        p1Ready.SetActive(false);
        p1PickingText.SetActive(false);
        p1Background.SetActive(false);

        //turns off all player 1 space
        p1Box2.SetActive(false);
        p1Box3.SetActive(false);
        p1Tank2Border.SetActive(false);
        p1Tank2Background.SetActive(false);
        p1Tank2.SetActive(false);
        p1Tank2Key.SetActive(false);
        p1Tank3Border.SetActive(false);
        p1Tank3Background.SetActive(false);
        p1Tank3.SetActive(false);
        p1Tank3Key.SetActive(false);


        //makes sure all the UI for p2 is turned off at start
        p2Redo.SetActive(false);
        p2Ready.SetActive(false);
        p2PickingText.SetActive(false);
        p2Background.SetActive(false);

        //turns off all player 2 space
        p2Box2.SetActive(false);
        p2Box3.SetActive(false);
        p2Tank2Border.SetActive(false);
        p2Tank2Background.SetActive(false);
        p2Tank2.SetActive(false);
        p2Tank2Key.SetActive(false);
        p2Tank3Border.SetActive(false);
        p2Tank3Background.SetActive(false);
        p2Tank3.SetActive(false);
        p2Tank3Key.SetActive(false);

        //checks to see how many UI elements need to be turned on
        if(whatLevelAmI == 2)
        {
            //set actives for p1 if medium
            p1Box2.SetActive(true);
            p1Tank2Border.SetActive(true);
            p1Tank2Background.SetActive(true);
            p1Tank2.SetActive(true);
            p1Tank2Key.SetActive(true);

            //sets active for p2 if medium
            p2Box2.SetActive(true);
            p2Tank2Border.SetActive(true);
            p2Tank2Background.SetActive(true);
            p2Tank2.SetActive(true);
            p2Tank2Key.SetActive(true);
        }
        else if(whatLevelAmI == 3)
        {
            //set actives for p1 if medium
            p1Box2.SetActive(true);
            p1Box3.SetActive(true);
            p1Tank2Border.SetActive(true);
            p1Tank2Background.SetActive(true);
            p1Tank2.SetActive(true);
            p1Tank2Key.SetActive(true);
            p1Tank3Border.SetActive(true);
            p1Tank3Background.SetActive(true);
            p1Tank3.SetActive(true);
            p1Tank3Key.SetActive(true);

            //sets active for p2 if medium
            p2Box2.SetActive(true);
            p2Box3.SetActive(true);
            p2Tank2Border.SetActive(true);
            p2Tank2Background.SetActive(true);
            p2Tank2.SetActive(true);
            p2Tank2Key.SetActive(true);
            p2Tank3Border.SetActive(true);
            p2Tank3Background.SetActive(true);
            p2Tank3.SetActive(true);
            p2Tank3Key.SetActive(true);
        }
        else
        {
            //well its not easy, medium or hard, so dont do nothing.
        }


    }


    void Update()
    {
        if(player1FishedPicking && player2FishedPicking)
        {
            if(whatLevelAmI == 1) { SceneManager.LoadScene("Level-1");}
            if(whatLevelAmI == 2) { SceneManager.LoadScene("Level-2");}
            if(whatLevelAmI == 3) { SceneManager.LoadScene("Level-3");}

        }

        //this will pop up redo? and ready GUI for player 1
        if(p1ArrayIdex == whatLevelAmI && !player1FishedPicking)
        {
            p1Redo.SetActive(true);
            p1Ready.SetActive(true);
            p1PickingText.SetActive(true);
            p1Background.SetActive(true);
        }

        //this will pop up redo? and ready GUI for player 2
        if (p2ArrayIdex == whatLevelAmI && !player2FishedPicking)
        {
            p2Redo.SetActive(true);
            p2Ready.SetActive(true);
            p2PickingText.SetActive(true);
            p2Background.SetActive(true);
        }

        //player 1 keys
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q was hit");
            Player1Qpick();

        }

        if (Input.GetKeyDown(KeyCode.W) && whatLevelAmI >= 2)
        {
            Debug.Log("w was hit");
            Player1Wpick();

        }

        if (Input.GetKeyDown(KeyCode.E) && whatLevelAmI >= 3)
        {
            Debug.Log("e was hit");
            Player1Epick();
            
        }

        //player 2 keys
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("i was hit");
            Player2KeyPad1pick();

        }

        if (Input.GetKeyDown(KeyCode.O) && whatLevelAmI >= 2)
        {
            Debug.Log("o was hit");
            Player2KeyPad2pick();

        }

        if (Input.GetKeyDown(KeyCode.P) && whatLevelAmI >= 3)
        {
            Debug.Log("p was hit");
            Player2KeyPad3pick();

        }


    }
    //player1 pick functions
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
    //end of player 1 functions

    //player 2 pick functions
    private void Player2KeyPad1pick()
    {
        if (whatLevelAmI > p2ArrayIdex)
        {
            player2TankPicks[p2ArrayIdex] = 1;
            p2ArrayIdex++;
            return;
        }
        else
        {

            return;
        }
    }

    private void Player2KeyPad2pick()
    {
        if (whatLevelAmI > p2ArrayIdex)
        {
            player2TankPicks[p2ArrayIdex] = 2;
            p2ArrayIdex++;
            return;
        }
        else
        {
            return;
        }
    }

    private void Player2KeyPad3pick()
    {
        if (whatLevelAmI > p2ArrayIdex)
        {
            player2TankPicks[p2ArrayIdex] = 3;
            p2ArrayIdex++;
            return;
        }
        else
        {
            return;
        }
    }
    //end of player 2 functions

    //player 1 GUI control functions
    public void Player1RedoPick()
    {
        p1Redo.SetActive(false);
        p1Ready.SetActive(false);
        p1PickingText.SetActive(false);
        p1Background.SetActive(false);
        p1ArrayIdex = 0;
    }

    public void p1DonePicking()
    {
        p1Ready.SetActive(false);
        p1Redo.SetActive(false);
        p1PickingText.SetActive(false);
        p1Background.SetActive(false);
        player1FishedPicking = true;
        currentState.SetPlayer1TanksPick(player1TankPicks);
    }
    //end of player 1 GUI control functions

    //player 2 GUI control functions
    public void Player2RedoPick()
    {
        p2Redo.SetActive(false);
        p2Ready.SetActive(false);
        p2PickingText.SetActive(false);
        p2Background.SetActive(false);
        p2ArrayIdex = 0;
    }

    public void p2DonePicking()
    {
        p2Ready.SetActive(false);
        p2Redo.SetActive(false);
        p2PickingText.SetActive(false);
        p2Background.SetActive(false);
        player2FishedPicking = true;
        currentState.SetPlayer2TanksPick(player2TankPicks);
    }
    //end of player 2 GUI control functions



}
