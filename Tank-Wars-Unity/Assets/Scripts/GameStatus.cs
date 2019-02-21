using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField] int difficuityInfo;

    [SerializeField] int[] player1TankPicks;                        //holds players 1 picks
    [SerializeField] int[] player2TankPicks;                        //holds players 1 picks

    SceneLoader nextLevel;

    void Start()
    {
        nextLevel = FindObjectOfType<SceneLoader>();
    }


    public void SetDifficuity(int difficuity)
    {
        this.difficuityInfo = difficuity;
        nextLevel.Load10x10PlayerEasy();
    }

    public void SetPlayer1TanksPick(int[] player1TankPicks)
    {
        this.player1TankPicks = player1TankPicks;
    }

    public void SetPlayer2TanksPick(int[] player2TankPicks)
    {
        this.player2TankPicks = player2TankPicks;
    }

    public int GetDifficuity()
    {
        return this.difficuityInfo;
    }
}
