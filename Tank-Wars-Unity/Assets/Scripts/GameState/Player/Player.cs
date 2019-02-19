using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    protected int number;
    protected string color;
    protected Tank[] tanks;

    public Player(int playerNumber) {
        this.number = playerNumber;
        this.color = (playerNumber == 1) ? "Red" : "Blue";
        this.tanks = new Tank[3] { new EmptyTankSlot(), new EmptyTankSlot(), new EmptyTankSlot() };
    }

    public Player(int playerNumber, int[] playerTankArray) {
        this.number = playerNumber;
        this.color = (playerNumber == 1) ? "Red" : "Blue";
        this.tanks = createTankArray(playerTankArray, playerNumber);
    }

    private Tank[] createTankArray(int [] playerTankArray, int playerNumber) {
        Tank[] tanks = new Tank[3];

        for(int i = 0; i < playerTankArray.Length; i++) {
            switch (playerTankArray[i]) {
                case 0:
                    tanks[i] = new EmptyTankSlot();
                    break;
                case 1:
                    if(playerNumber == 1) {
                        tanks[i] = new Tank1(this, new CoordinateSet(4,0));
                    }
                    else {
                        tanks[i] = new Tank1(this, new CoordinateSet(5,9));
                    }       
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    tanks[i] = new EmptyTankSlot();
                    break;
            }
        }

        return tanks;
    }

    public int getPlayerNumber() {
        return this.number;
    }

    public string getPlayerColor() {
        return this.color;
    }

    public Tank[] getPlayerTanks() {
        return this.tanks;
    }

    public void setPlayerTanks(Tank[] tanks) {
        this.tanks = tanks;
    }
}

public enum Players {
    Red = 1,
    Blue = 2
}