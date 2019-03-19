using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Powerup {
    public Healing() {
        this.duration = 1;
        this.powerupValue = 50;
        this.name = "Healing";
    }

    public override void applyPowerupEffect(Tank tank) {
        System.Random randomNumberGenerator = new System.Random();
        int healz = randomNumberGenerator.Next(2, 6) * 10;
        this.powerupValue = healz;

        if (tank.getHealth() + healz > 100) {
            tank.setHealth(100);
        }
        else {
            tank.setHealth(tank.getHealth() + healz);
        }
    }

    public override void removePowerupEffect(Tank tank) {
        // No-op
    }
}