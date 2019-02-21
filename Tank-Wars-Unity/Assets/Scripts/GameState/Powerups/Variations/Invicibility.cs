using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invicibility : Powerup
{
    public Invicibility() {
        this.duration = 1;
    }

    public override string ToString() {
        return "Invincibility";
    }
}
