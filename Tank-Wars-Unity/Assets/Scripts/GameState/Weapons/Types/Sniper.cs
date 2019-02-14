using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon {
    // Data Members

    // Constructors
    public Sniper() {
        this.damage = 1;
        this.distance = 10;
    }

    // Member Functions
    public override bool isValidAttack(Grid grid) {
        return true;
    }
}
