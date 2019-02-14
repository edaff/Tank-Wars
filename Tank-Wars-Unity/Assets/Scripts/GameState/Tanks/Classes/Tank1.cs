using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank1 : Tank
{
    // Data Members

    // Constructors
    public Tank1(Player player, CoordinateSet coordinates) {
        this.health = 5;
        this.movement = 2;
        this.weapon = new Cannon();
        this.powerup = new EmptyPowerupSlot();
        this.player = player;
        this.coordinates = coordinates;
    }

    // Member Functions
    public override bool isValidMovement() {
        return true;
    }
}
