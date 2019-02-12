using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    public int health;
    public int baseMovement;
    public Weapon weapon;
    public Powerup powerup;

    virtual public bool isEmptySlot() {
        return false;
    }

    virtual public bool isValidMovement() {
        return true;
    }
}
