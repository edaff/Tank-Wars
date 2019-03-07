using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [Header("Game Difficuity picked")]
    [SerializeField] int difficuityInfo;

    [Header("Tanks picked by players")]
    [SerializeField] int[] player1TankPicks;                        //holds players 1 picks
    [SerializeField] int[] player2TankPicks;                        //holds players 1 picks

    [Header("Which player won")]
    [SerializeField] bool player1Wins = false;
    [SerializeField] bool player2Wins = false;

    [Header("False = PvP : true = PvAI")]
    [SerializeField] bool aiON = false;


    SceneLoader nextLevel;

    void Start()
    {
        nextLevel = FindObjectOfType<SceneLoader>();
    }

    //sets difficuity from start screen
    public void SetDifficuity(int difficuity, bool aiON)
    {
        this.difficuityInfo = difficuity;
        this.aiON = aiON;
        nextLevel.Load10x10SelectScreen();
    }

    //sets players 1 picks from select screen
    public void SetPlayer1TanksPick(int[] player1TankPicks)
    {
        this.player1TankPicks = player1TankPicks;
    }

    //sets players 2 picks from select screen
    public void SetPlayer2TanksPick(int[] player2TankPicks)
    {
        this.player2TankPicks = player2TankPicks;
    }

    public int[] getPlayer1TankPicks() {
        return this.player1TankPicks;
    }

    public int[] getPlayer2TankPicks() {
        return this.player2TankPicks;
    }

    public void SetPlayer1Win()
    {
        player1Wins = true;
        nextLevel.LoadGameOverScreen();
    }

    public void SetPlayer2Win()
    {
        player2Wins = true;
        nextLevel.LoadGameOverScreen();
    }

    //gets Difficuiry from start screen
    public int GetDifficuity()
    {
        return this.difficuityInfo;
    }

    public bool GetPlayer1WinLoseInfo()
    {
        return this.player1Wins;
    }

    public bool GetPlayer2WinLoseInfo()
    {
        return this.player2Wins;
    }

    public bool GetAiON()
    {
        return this.aiON;
    }
}
