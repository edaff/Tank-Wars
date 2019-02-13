using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour {
    public static int numberOfPowerups = 3;
    public static int emptyGambleSlots = 3;

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
}
