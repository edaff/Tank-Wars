using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSet
{
    // Data Members
    private int x;
    private int y;

    // Constructors
    public CoordinateSet() {
        this.x = 0;
        this.y = 0;
    }

    public CoordinateSet(int x, int y) {
        this.x = x;
        this.y = y;
    }

    // Member Functions
    public int getX() {
        return this.x;
    }

    public int getY() {
        return this.y;
    }

    public void setX(int x) {
        this.x = x;
    }

    public void setY(int y) {
        this.y = y;
    }

    // Operator Overload
    public static bool operator== (CoordinateSet set1, CoordinateSet set2) {
        if(set1.getX() == set2.getX() && set1.getY() == set2.getY()) {
            return true;
        }
        else {
            return false;
        }
    }

    public static bool operator!= (CoordinateSet set1, CoordinateSet set2) {
        if (set1.getX() != set2.getX() || set1.getY() != set2.getY()) {
            return true;
        }
        else {
            return false;
        }
    }
}
