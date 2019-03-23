using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGUIController : MonoBehaviour
{
    Turns getTurnsInfo;
    string whatRoundAmi;
    string whoesTurnisIt;

    bool redPhase = true;
    bool bluePhase = true;
    bool aIOn = false;

    [Header("Red Player's GUI Controller")]
    [SerializeField] GameObject redTankMoveGUI;
    [SerializeField] GameObject redTankAttackGUI;
    [SerializeField] GameObject redTankGambleGUI;
    [SerializeField] Animator redPhaseAni;

    [Header("Blue Player's GUI Controller")]
    [SerializeField] GameObject blueTankMoveGUI;
    [SerializeField] GameObject blueTankAttackGUI;
    [SerializeField] GameObject blueTankGambleGUI;
    [SerializeField] Animator bluePhaseAni;

    [Header("Game State info")]
    [SerializeField] GameStatus currentState;


    // Start is called before the first frame update
    void Start()
    {
        getTurnsInfo = FindObjectOfType<Turns>();

        currentState = FindObjectOfType<GameStatus>();
        aIOn = currentState.GetAiON();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("AI is " + aIOn);
        whatRoundAmi = getTurnsInfo.round.ToString();
        whoesTurnisIt = getTurnsInfo.playerTurn.ToString();

        //Debug.Log("Inside GUI move is " + getTurnsInfo.round + " " + getTurnsInfo.playerTurn);
        //Debug.Log("whatRoundAmi " + whatRoundAmi);
        //Debug.Log("whoesTurnisIt " + whoesTurnisIt);

        RedTankGUIController();
        BlueTankGUIController();

    }

    //moves control for red tanks/player
    private void RedTankGUIController()
    {
        if (whatRoundAmi == "Move" && whoesTurnisIt == "Red")
        {
            redTankMoveGUI.SetActive(true);

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

            if (bluePhase == true && aIOn != true)
            {
                bluePhaseAni.Play("Phase_Blue");
                bluePhaseAni.Play("Idle");
                bluePhase = false;
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

}


