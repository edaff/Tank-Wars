﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    // Members
    protected int distance;
    protected int damage;
    public Orientations orientation;
    protected Tank tank;
    protected int knockback;

    // Functions
    virtual public bool isValidAttack(Grid grid, CoordinateSet targetCoordinates, bool updateState) {
        return true;
    }
    
    public int getDamage() {
        return this.damage;
    }

    public int getDistance() {
        return this.distance;
    }

    public void setDamage(int damage) {
        this.damage = damage;
    }

    public void setDistance(int distance) {
        this.distance = distance;
    }

    public Orientations getOrientation() {
        return this.orientation;
    }

    public void setOrientation(Orientations orientation ) {
        this.orientation = orientation;
    }

    public void setKnockback(int knockback) {
        this.knockback = knockback;
    }

    public int getKnockback() {
        return this.knockback;
    }

    virtual public ArrayList getValidAttacks(Grid grid) {
        return new ArrayList();
    }

}

public enum Orientations {
    Up = 0,
    Right = 90,
    Down = 180,
    Left = 270
}
