using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    // Members
    protected int distance;
    protected int damage;
    protected int orientation;
    protected Tank tank;

    // Functions
    virtual public bool isValidAttack(Grid grid, CoordinateSet targetCoordinates) {
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

    public int getOrientation() {
        return this.orientation;
    }
}

public enum Orientations {
    Up = 0,
    Right = 90,
    Down = 180,
    Left = 270
}
