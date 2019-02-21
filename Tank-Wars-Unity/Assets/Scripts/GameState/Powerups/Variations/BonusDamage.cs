using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDamage : Powerup
{
    public BonusDamage() {
        this.duration = 1;
    }

    public override string ToString() {
        return "Bonus Damage";
    }
}
