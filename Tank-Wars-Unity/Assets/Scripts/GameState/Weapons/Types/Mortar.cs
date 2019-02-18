using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Weapon
{
    // Data Members

    // Constructors
    public Mortar() {
        this.damage = 3;
        this.distance = 5;
    }

    // Member Functions
    public override bool isValidAttack(Grid grid, CoordinateSet targetCoordinates) {
        return true;
    }
}
