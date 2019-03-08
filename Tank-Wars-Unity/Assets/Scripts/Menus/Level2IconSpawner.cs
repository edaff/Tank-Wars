using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2IconSpawner : MonoBehaviour
{
    GameStatus getTankInfo;

    [Header("Red Player's GUI Controller Tank1")]
    [SerializeField] GameObject redTankNormal;
    [SerializeField] GameObject redTankSniper;
    [SerializeField] GameObject redTankMortar;

    [Header("Red Player's GUI Controller Tank2")]
    [SerializeField] GameObject redTank2Normal;
    [SerializeField] GameObject redTank2Sniper;
    [SerializeField] GameObject redTank2Mortar;

    [Header("Blue Player's GUI Controller Tank1")]
    [SerializeField] GameObject blueTankNormal;
    [SerializeField] GameObject blueTankSniper;
    [SerializeField] GameObject blueTankMortar;

    [Header("Blue Player's GUI Controller Tank2")]
    [SerializeField] GameObject blueTank2Normal;
    [SerializeField] GameObject blueTank2Sniper;
    [SerializeField] GameObject blueTank2Mortar;

    [SerializeField] public int[] getRedPicks;
    [SerializeField] public int[] getBluePicks;


    // Start is called before the first frame update
    void Start()
    {
        getTankInfo = FindObjectOfType<GameStatus>();

        getRedPicks = new int[] { 0, 0, 0 };
        getBluePicks = new int[] { 0, 0, 0 };

        getRedPicks = getTankInfo.getPlayer1TankPicks();
        getBluePicks = getTankInfo.getPlayer2TankPicks();
        TurnALLOff();
        TurnTheCorrectIconOn();
    }


    private void TurnALLOff()
    {
        //turns all red UI off
        redTankNormal.SetActive(false);
        redTankSniper.SetActive(false);
        redTankMortar.SetActive(false);

        redTank2Normal.SetActive(false);
        redTank2Sniper.SetActive(false);
        redTank2Mortar.SetActive(false);

        //turn all blue UI off
        blueTankNormal.SetActive(false);
        blueTankSniper.SetActive(false);
        blueTankMortar.SetActive(false);

        blueTank2Normal.SetActive(false);
        blueTank2Sniper.SetActive(false);
        blueTank2Mortar.SetActive(false);


    }

    private void TurnTheCorrectIconOn()
    {
        if (getRedPicks[0] == 1)
        {
            redTankNormal.SetActive(true);
        }
        else if (getRedPicks[0] == 2)
        {
            redTankSniper.SetActive(true);
        }
        else if (getRedPicks[0] == 3)
        {
            redTankMortar.SetActive(true);
        }
        else
        {
            //wrong item in the array do nothing
        }

        if (getRedPicks[1] == 1)
        {
            redTank2Normal.SetActive(true);
        }
        else if (getRedPicks[1] == 2)
        {
            redTank2Sniper.SetActive(true);
        }
        else if (getRedPicks[1] == 3)
        {
            redTank2Mortar.SetActive(true);
        }
        else
        {
            //wrong item in the array do nothing
        }

        if (getBluePicks[0] == 1)
        {
            blueTankNormal.SetActive(true);
        }
        else if (getBluePicks[0] == 2)
        {
            blueTankSniper.SetActive(true);
        }
        else if (getBluePicks[0] == 3)
        {
            blueTankMortar.SetActive(true);
        }
        else
        {
            //wrong item in the array do nothing
        }

        if (getBluePicks[1] == 1)
        {
            blueTank2Normal.SetActive(true);
        }
        else if (getBluePicks[1] == 2)
        {
            blueTank2Sniper.SetActive(true);
        }
        else if (getBluePicks[1] == 3)
        {
            blueTank2Mortar.SetActive(true);
        }
        else
        {
            //wrong item in the array do nothing
        }
    }
}
