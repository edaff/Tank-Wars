using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGUIController : MonoBehaviour
{
    Turns getTurnsInfo;
    string whatRoundAmi;
    string whoesTurnisIt;

    [Header("Red Player's GUI Controller")]
    [SerializeField] GameObject redTankMoveGUI;
    [SerializeField] GameObject redTankAttackGUI;
    [SerializeField] GameObject redTankGambleGUI;

    [Header("Blue Player's GUI Controller")]
    [SerializeField] GameObject blueTankMoveGUI;
    [SerializeField] GameObject blueTankAttackGUI;
    [SerializeField] GameObject blueTankGambleGUI;


    // Start is called before the first frame update
    void Start()
    {
        getTurnsInfo = FindObjectOfType<Turns>();
    }

    // Update is called once per frame
    void Update()
    {
        whatRoundAmi = getTurnsInfo.round.ToString();
        whoesTurnisIt = getTurnsInfo.playerTurn.ToString();

        //Debug.Log("Inside GUI move is " + getTurnsInfo.round + " " + getTurnsInfo.playerTurn);
        //Debug.Log("whatRoundAmi " + whatRoundAmi);
        //Debug.Log("whoesTurnisIt " + whoesTurnisIt);


        if(whatRoundAmi == "Move" && whoesTurnisIt == "Red")
        {
            redTankMoveGUI.SetActive(true);
        }
        else {redTankMoveGUI.SetActive(false);}

        if (whatRoundAmi == "Attack" && whoesTurnisIt == "Red")
        {
            redTankAttackGUI.SetActive(true);
        }
        else {redTankAttackGUI.SetActive(false);}

        if (whatRoundAmi == "Gamble" && whoesTurnisIt == "Red")
        {
            redTankGambleGUI.SetActive(true);
        }
        else {redTankGambleGUI.SetActive(false);}






        if (whatRoundAmi == "Move" && whoesTurnisIt == "Blue")
        {
            blueTankMoveGUI.SetActive(true);
        }
        else {blueTankMoveGUI.SetActive(false);}

        if (whatRoundAmi == "Attack" && whoesTurnisIt == "Blue")
        {
            blueTankAttackGUI.SetActive(true);
        }
        else {blueTankAttackGUI.SetActive(false);}

        if (whatRoundAmi == "Gamble" && whoesTurnisIt == "Blue")
        {
            blueTankGambleGUI.SetActive(true);
        }
        else {blueTankGambleGUI.SetActive(false);}


    }


}
