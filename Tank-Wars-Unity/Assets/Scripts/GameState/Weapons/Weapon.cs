using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Members
    public int baseDistance;
    public int baseDamage;

    // Functions
    public int getBaseDamage() {
        return this.baseDamage;
    }

    public int getBaseDistance() {
        return this.baseDistance;
    }

    public void setBaseDamage(int baseDamage) {
        this.baseDamage = baseDamage;
    }

    public void setBaseDistance(int baseDistance) {
        this.baseDistance = baseDistance;
    }
}
