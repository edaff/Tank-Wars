using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPowerupSlot : Powerup
{
    override public bool isEmptySlot() {
        return true;
    }
}
