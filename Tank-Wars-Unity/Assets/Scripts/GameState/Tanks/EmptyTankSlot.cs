using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTankSlot : Tank
{
    override public bool isEmptySlot() {
        return true;
    }
}
