using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected int number;
    protected string color;
    protected Tank[] tanks;

    public Player(int playerNumber) {
        this.number = playerNumber;
        this.color = (playerNumber == 1) ? "Red" : "Blue";
        this.tanks = new Tank[3];
    }
}

public enum Players {
    Red = 1,
    Blue = 2
}