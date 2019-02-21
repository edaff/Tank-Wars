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

    virtual public bool isValidMovement(Grid grid, CoordinateSet coordinates) {
        return true;
    }

    public int getHealth() {
        return this.health;
    }
    public void decrementHealth(int amount) {
        this.health -= amount;
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

    public void setPowerup(Powerup powerup) {
        this.powerup = powerup;
    }

    public Player getPlayer() {
        return this.player;
    }

    public CoordinateSet getCoordinates() {
        return this.coordinates;
    }

    public void updatePowerupState() {
        if(this.powerup is EmptyPowerupSlot) {
            return;
        }

        this.powerup.decrementPowerupDuration();

        if (this.powerup.isExpired()) {
            this.powerup = new EmptyPowerupSlot();
        }
    }
}
