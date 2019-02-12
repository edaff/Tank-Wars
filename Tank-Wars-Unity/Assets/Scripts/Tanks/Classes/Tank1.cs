using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank1 : Tank
{
    // Data Members

    // Constructors
    public Tank1() {
        this.health = 5;
        this.baseMovement = 2;
        this.weapon = new Cannon();
        this.powerup = new EmptyPowerupSlot();
    }

    // Member Functions
    public override bool isValidMovement() {
        return true;
    }
}
