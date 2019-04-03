using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGUIController : MonoBehaviour
{

    string whatRoundAmi;
    string whoesTurnisIt;

    bool redPhase = true;
    bool bluePhase = true;
    bool aIOn = false;

    int whatLevelIsThis = 1;

    [Header("Red Player's GUI Controller")]
    [SerializeField] GameObject redTankMoveGUI;
    [SerializeField] GameObject redTankAttackGUI;
    [SerializeField] GameObject redTankGambleGUI;
    [SerializeField] Animator redPhaseAni;
    [SerializeField] GameObject redOverlay;
    [SerializeField] GameObject redTeamIcon;

    [Header("Red Player's Hp text")]
    [SerializeField] TextMeshProUGUI redTank1;
    [SerializeField] TextMeshProUGUI redTank2;

    [Header("Blue Player's Hp text")]
    [SerializeField] TextMeshProUGUI blueTank1;
    [SerializeField] TextMeshProUGUI blueTank2;

    [Header("Blue Player's GUI Controller")]
    [SerializeField] GameObject blueTankMoveGUI;
    [SerializeField] GameObject blueTankAttackGUI;
    [SerializeField] GameObject blueTankGambleGUI;
    [SerializeField] Animator bluePhaseAni;
    [SerializeField] GameObject blueOverlay;
    [SerializeField] GameObject blueTeamIcon;

    [Header("Game State info")]
    GameStatus currentState;
    Turns getTurnsInfo;
    GameState getCurrentHp;
    RedTankSpawner1 redTankInfo;
    BlueTankSpawner blueTankInfo;


    //holds tank childern arrays for red tanks ************
    int[] redTanksHp = new int[3];
    [SerializeField] Transform[] redTank1Childern = new Transform[7];
    [SerializeField] Transform[] redTank2Childern = new Transform[7];
    //[SerializeField] Transform[] redTank3Childern = new Transform[7];

    int[] blueTanksHp = new int[3];
    [SerializeField] Transform[] blueTank1Childern = new Transform[7];
    [SerializeField] Transform[] blueTank2Childern = new Transform[7];
    //[SerializeField] Transform[] blueTank3Childern = new Transform[7];


    // Start is called before the first frame update
    void Start()
    {
        getTurnsInfo = FindObjectOfType<Turns>();

        currentState = FindObjectOfType<GameStatus>();
        aIOn = currentState.GetAiON();
        whatLevelIsThis = currentState.GetDifficuity();


        //box = RedTank1.GetComponent<TextMeshProUGUI>();
        //RedTank1 = FindObjectOfType<GameObject>();

        CreateChildArray();
    }

    // Update is called once per frame
    void Update()
    {
        getCurrentHp = getTurnsInfo.getGameState();
        redTanksHp = getCurrentHp.getHealthForPlayerTanks(PlayerColors.Red);
        blueTanksHp = getCurrentHp.getHealthForPlayerTanks(PlayerColors.Blue);

        //box = RedTank1.GetComponent<TextMeshPro>();
        //Debug.Log(redTank1);

        //redTank1.SetText("LeveL "+ whatLevelIsThis.ToString());
        //Debug.Log("Red Tanks 1 hp is " + redTanksHp[0]);                        //hp of red tank 1
        //Debug.Log("Red Tanks 2 hp is " + redTanksHp[1]);                        //hp og red tank 2
        //Debug.Log("blue Tanks 1 hp is " + blueTanksHp[0]);                        //hp of blue tank 1
        //Debug.Log("blue Tanks 2 hp is " + blueTanksHp[1]);                        //hp og blue tank 2

        //Debug.Log(redTank1Childern[4]);                                         //reads back the 4th child object of red tank 1, it should be fire10
        //redTankChildern[6].gameObject.SetActive(true);                        //turns on child 6 of red tank 1, it should be the normal fire pre fab

        /*if (redTanksHp[0] <= 90)
        {
            redTank1Childern[6].gameObject.SetActive(true);
        }
        if (blueTanksHp[0] <= 90)
        {
            blueTank1Childern[6].gameObject.SetActive(true);
        }
        */

        UpdateRedTanksLook();
        UpdateBlueTanksLook();

        //Debug.Log("AI is " + aIOn);
        whatRoundAmi = getTurnsInfo.round.ToString();
        whoesTurnisIt = getTurnsInfo.playerTurn.ToString();

        //Debug.Log("Inside GUI move is " + getTurnsInfo.round + " " + getTurnsInfo.playerTurn);
        //Debug.Log("whatRoundAmi " + whatRoundAmi);
        //Debug.Log("whoesTurnisIt " + whoesTurnisIt);

        RedTankGUIController();
        BlueTankGUIController();
        UpDateTanksHpNum();

    }

    //moves control for red tanks/player
    private void RedTankGUIController()
    {
        if (whatRoundAmi == "Move" && whoesTurnisIt == "Red")
        {
            redTankMoveGUI.SetActive(true);
            redOverlay.SetActive(true);
            blueOverlay.SetActive(false);
            redTeamIcon.SetActive(true);
            blueTeamIcon.SetActive(false);

            if (redPhase == true)
            {
                redPhaseAni.Play("Phase_Red");
                redPhaseAni.Play("Idle");
                redPhase = false;
            }

        }
        else { redTankMoveGUI.SetActive(false); }

        if (whatRoundAmi == "Attack" && whoesTurnisIt == "Red")
        {
            redTankAttackGUI.SetActive(true);
            redPhase = true;

        }
        else { redTankAttackGUI.SetActive(false); }

        if (whatRoundAmi == "Gamble" && whoesTurnisIt == "Red")
        {
            redTankGambleGUI.SetActive(true);
        }
        else { redTankGambleGUI.SetActive(false); }
    }

    //moves control for blue tanks/player
    private void BlueTankGUIController()
    {
        if (whatRoundAmi == "Move" && whoesTurnisIt == "Blue")
        {
            blueTankMoveGUI.SetActive(true);
            redOverlay.SetActive(false);

            if (bluePhase == true && aIOn != true)
            {
                bluePhaseAni.Play("Phase_Blue");
                bluePhaseAni.Play("Idle");
                bluePhase = false;
                blueOverlay.SetActive(true);
                redTeamIcon.SetActive(false);
                blueTeamIcon.SetActive(true);
            }
        }
        else { blueTankMoveGUI.SetActive(false); }

        if (whatRoundAmi == "Attack" && whoesTurnisIt == "Blue")
        {
            blueTankAttackGUI.SetActive(true);
            bluePhase = true;
        }
        else { blueTankAttackGUI.SetActive(false); }

        if (whatRoundAmi == "Gamble" && whoesTurnisIt == "Blue")
        {
            blueTankGambleGUI.SetActive(true);
        }
        else { blueTankGambleGUI.SetActive(false); }
    }

    private void CreateChildArray()
    {
        redTankInfo = FindObjectOfType<RedTankSpawner1>();
        blueTankInfo = FindObjectOfType<BlueTankSpawner>();

        if (whatLevelIsThis == 1)
        {
            redTank1Childern = redTankInfo.getRedTank1Childern();
            blueTank1Childern = blueTankInfo.getBlueTank1Childern();
        }
        else if(whatLevelIsThis == 2)
        {
            redTank1Childern = redTankInfo.getRedTank1Childern();
            redTank2Childern = redTankInfo.getRedTank2Childern();
            blueTank1Childern = blueTankInfo.getBlueTank1Childern();
            blueTank2Childern = blueTankInfo.getBlueTank2Childern();
        }
        else if (whatLevelIsThis == 3)
        {
            redTank1Childern = redTankInfo.getRedTank1Childern();
            redTank2Childern = redTankInfo.getRedTank2Childern();
            blueTank1Childern = blueTankInfo.getBlueTank1Childern();
            blueTank2Childern = blueTankInfo.getBlueTank2Childern();
            //redTank3Childern = redTankInfo.getRedTank3Childern();
            //blueTank3Childern = blueTankInfo.getBlueTank3Childern();
        }
    }

    private void UpdateRedTanksLook()
    {
        if (whatLevelIsThis == 1)                                                   //level 1 so it updates 1 tanks
        {
            if (redTanksHp[0] <= 90 && redTanksHp[0] > 0)
            {
                redTank1Childern[4].gameObject.SetActive(true);

                if (redTanksHp[0] <= 50)
                {
                    redTank1Childern[5].gameObject.SetActive(true);
                }
            }
            else if (redTanksHp[0] <= 0)
            {
                redTank1Childern[4].gameObject.SetActive(false);
                redTank1Childern[5].gameObject.SetActive(false);

                redTank1Childern[3].gameObject.SetActive(false);
                redTank1Childern[6].gameObject.SetActive(true);
            }
        }
        else if (whatLevelIsThis == 2)                                              //level 2 so it updates 2 tanks
        {
            if (redTanksHp[0] <= 90 && redTanksHp[0] > 0)
            {
                redTank1Childern[4].gameObject.SetActive(true);

                if (redTanksHp[0] <= 50)
                {
                    redTank1Childern[5].gameObject.SetActive(true);
                }
            }
            else if (redTanksHp[0] <= 0)
            {
                redTank1Childern[4].gameObject.SetActive(false);
                redTank1Childern[5].gameObject.SetActive(false);

                redTank1Childern[3].gameObject.SetActive(false);
                redTank1Childern[6].gameObject.SetActive(true);
            }

            if (redTanksHp[1] <= 90 && redTanksHp[1] > 0)
            {
                redTank2Childern[4].gameObject.SetActive(true);

                if (redTanksHp[1] <= 50)
                {
                    redTank2Childern[5].gameObject.SetActive(true);
                }
            }
            else if (redTanksHp[1] <= 0)
            {
                redTank2Childern[4].gameObject.SetActive(false);
                redTank2Childern[5].gameObject.SetActive(false);

                redTank2Childern[3].gameObject.SetActive(false);
                redTank2Childern[6].gameObject.SetActive(true);
            }
        }
        /*else if (whatLevelIsThis == 3)                                              //level 3 so it updates 3 tanks
        {
            if (redTanksHp[0] <= 90 && redTanksHp[0] > 0)
            {
                redTank1Childern[4].gameObject.SetActive(true);

                if (redTanksHp[0] <= 50)
                {
                    redTank1Childern[5].gameObject.SetActive(true);
                }
            }
            else if (redTanksHp[0] <= 0)
            {
                redTank1Childern[4].gameObject.SetActive(false);
                redTank1Childern[5].gameObject.SetActive(false);

                redTank1Childern[3].gameObject.SetActive(false);
                redTank1Childern[6].gameObject.SetActive(true);
            }

            if (redTanksHp[1] <= 90 && redTanksHp[1] > 0)
            {
                redTank2Childern[4].gameObject.SetActive(true);

                if (redTanksHp[1] <= 50)
                {
                    redTank2Childern[5].gameObject.SetActive(true);
                }
            }
            else if (redTanksHp[1] <= 0)
            {
                redTank2Childern[4].gameObject.SetActive(false);
                redTank2Childern[5].gameObject.SetActive(false);

                redTank2Childern[3].gameObject.SetActive(false);
                redTank2Childern[6].gameObject.SetActive(true);
            }
            if (redTanksHp[2] <= 90 && redTanksHp[2] > 0)
            {
                redTank3Childern[4].gameObject.SetActive(true);

                if (redTanksHp[2] <= 50)
                {
                    redTank3Childern[5].gameObject.SetActive(true);
                }
            }
            else if (redTanksHp[2] <= 0)
            {
                redTank3Childern[4].gameObject.SetActive(false);
                redTank3Childern[5].gameObject.SetActive(false);

                redTank3Childern[3].gameObject.SetActive(false);
                redTank3Childern[6].gameObject.SetActive(true);
            }
        }
        */
    }

    private void UpdateBlueTanksLook()
    {
        if (whatLevelIsThis == 1)                                                   //level 1 so it updates 1 tanks
        {
            if (blueTanksHp[0] <= 90 && blueTanksHp[0] > 0)
            {
                blueTank1Childern[4].gameObject.SetActive(true);

                if (blueTanksHp[0] <= 50)
                {
                    blueTank1Childern[5].gameObject.SetActive(true);
                }
            }
            else if (blueTanksHp[0] <= 0)
            {
                blueTank1Childern[4].gameObject.SetActive(false);
                blueTank1Childern[5].gameObject.SetActive(false);

                blueTank1Childern[3].gameObject.SetActive(false);
                blueTank1Childern[6].gameObject.SetActive(true);
            }
        }
        else if (whatLevelIsThis == 2)                                              //level 2 so it updates 2 tanks
        {
            if (blueTanksHp[0] <= 90 && blueTanksHp[0] > 0)
            {
                blueTank1Childern[4].gameObject.SetActive(true);

                if (blueTanksHp[0] <= 50)
                {
                    blueTank1Childern[5].gameObject.SetActive(true);
                }
            }
            else if (blueTanksHp[0] <= 0)
            {
                blueTank1Childern[4].gameObject.SetActive(false);
                blueTank1Childern[5].gameObject.SetActive(false);

                blueTank1Childern[3].gameObject.SetActive(false);
                blueTank1Childern[6].gameObject.SetActive(true);
            }

            if (blueTanksHp[1] <= 90 && blueTanksHp[1] > 0)
            {
                blueTank2Childern[4].gameObject.SetActive(true);

                if (blueTanksHp[1] <= 50)
                {
                    blueTank2Childern[5].gameObject.SetActive(true);
                }
            }
            else if (blueTanksHp[1] <= 0)
            {
                blueTank2Childern[4].gameObject.SetActive(false);
                blueTank2Childern[5].gameObject.SetActive(false);

                blueTank2Childern[3].gameObject.SetActive(false);
                blueTank2Childern[6].gameObject.SetActive(true);
            }
        }
        /*else if (whatLevelIsThis == 3)                                              //level 3 so it updates 3 tanks
        {
            if (blueTanksHp[0] <= 90 && blueTanksHp[0] > 0)
            {
                blueTank1Childern[4].gameObject.SetActive(true);

                if (blueTanksHp[0] <= 50)
                {
                    blueTank1Childern[5].gameObject.SetActive(true);
                }
            }
            else if (blueTanksHp[0] <= 0)
            {
                blueTank1Childern[4].gameObject.SetActive(false);
                blueTank1Childern[5].gameObject.SetActive(false);

                blueTank1Childern[3].gameObject.SetActive(false);
                blueTank1Childern[6].gameObject.SetActive(true);
            }

            if (blueTanksHp[1] <= 90 && blueTanksHp[1] > 0)
            {
                blueTank2Childern[4].gameObject.SetActive(true);

                if (blueTanksHp[1] <= 50)
                {
                    blueTank2Childern[5].gameObject.SetActive(true);
                }
            }
            else if (blueTanksHp[1] <= 0)
            {
                blueTank2Childern[4].gameObject.SetActive(false);
                blueTank2Childern[5].gameObject.SetActive(false);

                blueTank2Childern[3].gameObject.SetActive(false);
                blueTank2Childern[6].gameObject.SetActive(true);
            }
            if (blueTanksHp[2] <= 90 && blueTanksHp[2] > 0)
            {
                blueTank3Childern[4].gameObject.SetActive(true);

                if (blueTanksHp[2] <= 50)
                {
                    blueTank3Childern[5].gameObject.SetActive(true);
                }
            }
            else if (blueTanksHp[2] <= 0)
            {
                blueTank3Childern[4].gameObject.SetActive(false);
                blueTank3Childern[5].gameObject.SetActive(false);

                blueTank3Childern[3].gameObject.SetActive(false);
                blueTank3Childern[6].gameObject.SetActive(true);
            }
        }
        */

    }

    private void UpDateTanksHpNum()
    {
        if (whatLevelIsThis == 1)
        {
            if (redTanksHp[0] <= 100)
            {
                redTank1.SetText("HP:" + redTanksHp[0]);
            }
            else if (redTanksHp[0] > 100)
            {
                redTank1.SetText("HP: ITS OVER 9000!!!");
            }
            if (blueTanksHp[0] <= 100)
            {
                blueTank1.SetText("HP:" + blueTanksHp[0]);
            }
            else if (blueTanksHp[0] > 100)
            {
                blueTank1.SetText("HP: ITS OVER 9000!!!");
            }

        }
        else if (whatLevelIsThis == 2)
        {
            if (redTanksHp[0] <= 100)
            {
                redTank1.SetText("HP:" + redTanksHp[0]);
            }
            else if (redTanksHp[0] > 100)
            {
                redTank1.SetText("HP: ITS OVER 9000!!!");
            }
            if (redTanksHp[1] <= 100)
            {
                redTank2.SetText("HP:" + redTanksHp[1]);
            }
            else if (redTanksHp[1] > 100)
            {
                redTank2.SetText("HP: ITS OVER 9000!!!");
            }
            if (blueTanksHp[0] <= 100)
            {
                blueTank1.SetText("HP:" + blueTanksHp[0]);
            }
            else if (blueTanksHp[0] > 100)
            {
                blueTank1.SetText("HP: ITS OVER 9000!!!");
            }
            if (blueTanksHp[1] <= 100)
            {
                blueTank2.SetText("HP:" + blueTanksHp[1]);
            }
            else if (blueTanksHp[1] > 100)
            {
                blueTank2.SetText("HP: ITS OVER 9000!!!");
            }
        }
    }

}


