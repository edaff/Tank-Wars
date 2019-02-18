using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    Grid grid;
    Player player1;
    Player player2;
    bool player1Turn;
    bool player2Turn;
    bool move, attack, gamble;
    // Start is called before the first frame update
    void Start()
    {
        player1 = new Player(1);
        player2 = new Player(2);

        grid = new Grid("10x10 Grid Template", 10, player1, player2);

        player1Turn = true;
        player2Turn = false;
        move = true;
        attack = false;
        gamble = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Turn) {
            takeTurn(player1);
        }
        else if(player2Turn) {
            takeTurn(player2);
        }
    }

    void takeTurn(Player player) {
        if (move) { 

        }
        else if (attack) {

        }
        else if (gamble) {

        }
    }

    public bool isGameOver() {
        Tank[] player1Tanks = player1.getPlayerTanks();
        Tank[] player2Tanks = player2.getPlayerTanks();

        if (areAllPlayerTanksAreDead(player1.getPlayerTanks()) || areAllPlayerTanksAreDead(player2.getPlayerTanks())) {
            return true;
        }
        else {
            return false;
        }
    }

    private bool areAllPlayerTanksAreDead(Tank[] tanks) {
        bool allTanksDead = true;
        for(int i = 0;i < tanks.Length; i++) {
            if(tanks[i] is EmptyTankSlot) {
                continue;
            }

            if(tanks[i].getHealth() >= 0) {
                allTanksDead = false;
            }
        }

        return allTanksDead;
    }
}
