using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMovement : Powerup
{
    public BonusMovement() {
        this.duration = 1;
        this.powerupValue = 1;
        this.name = "Bonus Movement";
    }

    public override void applyPowerupEffect(Tank tank) {
        tank.setMovement(tank.getMovement() + this.powerupValue);
    }

    public override void removePowerupEffect(Tank tank) {
        tank.setMovement(tank.getMovement() - this.powerupValue);
    }
}
