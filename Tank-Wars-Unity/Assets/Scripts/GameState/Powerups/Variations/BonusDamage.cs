using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDamage : Powerup
{
    public BonusDamage() {
        this.duration = 1;
        this.powerupValue = 10;
        this.name = "Bonus Damage";
    }

    public override void applyPowerupEffect(Tank tank) {
        Weapon weapon = tank.getWeapon();
        weapon.setDamage(weapon.getDamage() + this.powerupValue);
    }

    public override void removePowerupEffect(Tank tank) {
        Weapon weapon = tank.getWeapon();
        weapon.setDamage(weapon.getDamage() - this.powerupValue);
    }
}
