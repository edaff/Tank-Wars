using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invicibility : Powerup
{
    public Invicibility() {
        this.duration = 1;
        this.powerupValue = 0;
        this.name = "Invincibility";
    }

    public override void applyPowerupEffect(Tank tank) {
        this.powerupValue = tank.getHealth();
        tank.setHealth(10000);
    }

    public override void removePowerupEffect(Tank tank) {
        tank.setHealth(this.powerupValue);
    }
}
