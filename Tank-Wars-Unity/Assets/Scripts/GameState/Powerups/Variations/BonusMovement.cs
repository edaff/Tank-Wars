using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMovement : Powerup
{
    public BonusMovement() {
        this.duration = 1;
    }

    public override string ToString() {
        return "Bonus Movement";
    }
}
