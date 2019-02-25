using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup {
    private static int numberOfPowerups = 3;
    private static int emptyGambleSlots = 3;
    protected int duration;
    protected int powerupValue;
    protected string name = "Powerup";

    virtual public bool isEmptySlot() {
        return false;
    }

    public static Powerup gamble() {
        System.Random randomNumberGenerator = new System.Random();
        int randomNumber = randomNumberGenerator.Next(1, numberOfPowerups + emptyGambleSlots);

        switch (randomNumber) {
            case 1:
                return new Invicibility();
            case 2:
                return new BonusDamage();
            case 3:
                return new BonusMovement();
            default:
                return new EmptyPowerupSlot();
        }
    }

    public void decrementPowerupDuration() {
        this.duration--;
    }

    public int getPowerupDuration() {
        return this.duration;
    }

    public bool isExpired() {
        if(this.duration <= 0) {
            return true;
        }
        else {
            return false;
        }
    }

    public virtual void applyPowerupEffect(Tank tank) {
        Debug.Log("Nothing to do");
    }

    public virtual void removePowerupEffect(Tank tank) {
        Debug.Log("Nothing to do");
    }

    public override string ToString() {
        return this.name;
    }
}
