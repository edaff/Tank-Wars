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

    public int getPlayerNumber() {
        return this.number;
    }

    public string getPlayerColor() {
        return this.color;
    }

    public Tank[] getPlayerTanks() {
        return this.tanks;
    }
}

public enum Players {
    Red = 1,
    Blue = 2
}