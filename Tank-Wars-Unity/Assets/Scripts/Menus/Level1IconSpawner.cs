using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1IconSpawner : MonoBehaviour
{
    GameStatus getTankInfo;

    [Header("Red Player's GUI Controller")]
    [SerializeField] GameObject redTankNormal;
    [SerializeField] GameObject redTankSniper;
    [SerializeField] GameObject redTankMortar;

    [Header("Blue Player's GUI Controller")]
    [SerializeField] GameObject blueTankNormal;
    [SerializeField] GameObject blueTankSniper;
    [SerializeField] GameObject blueTankMortar;

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

        //turn all blue UI off
        blueTankNormal.SetActive(false);
        blueTankSniper.SetActive(false);
        blueTankMortar.SetActive(false);

    }

    private void TurnTheCorrectIconOn()
    {
        if(getRedPicks[0] == 1)
        {
            redTankNormal.SetActive(true);
        }
        else if(getRedPicks[0] == 2)
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
    }
}
