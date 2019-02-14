using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank
{
    // **********************
    //      Data Members
    // **********************
    protected int health;
    protected int movement;
    protected Weapon weapon;
    protected Powerup powerup;
    protected Player player;
    protected CoordinateSet coordinates;

    // ************************
    //      Member Functons
    // ************************
    virtual public bool isEmptySlot() {
        return false;
    }

    virtual public bool isValidMovement() {
        return true;
    }

    public int getHealth() {
        return this.health;
    }

    public int getMovement() {
        return this.movement;
    }

    public Weapon getWeapon() {
        return this.weapon;
    }

    public Powerup getPowerup() {
        return this.powerup;
    }

    public Player getPlayer() {
        return this.player;
    }
}
