﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank1 : Tank
{
    // Data Members

    // Constructors
    public Tank1(Player player, CoordinateSet coordinates) {
        this.health = 20;
        this.movement = 3;
        this.player = player;
        this.coordinates = coordinates;
        this.powerup = new EmptyPowerupSlot();
        this.weapon = new Cannon(this);
    }

    // Member Functions
    // Check that the desired movement is valid
    public override bool isValidMovement(Grid grid, CoordinateSet targetCoordinates) {
        bool validMovement = false;
        GridNode targetNode = grid.getGridNode(targetCoordinates);

        // Check for valid movement
        for(int i = 0;i < 4; i++) {
            validMovement = movementCheck(grid, i, this.coordinates, targetNode);
            if (validMovement) { break; }
        }

        // If all is well, update the game state and move the tank to the
        // targeted location.
        if (validMovement) {
            targetNode.setTank(grid.getGridNode(coordinates).getTank());
            grid.getGridNode(coordinates).setTank(new EmptyTankSlot());
            this.coordinates = new CoordinateSet(targetNode.getCoordinateSet().getX(), targetNode.getCoordinateSet().getY());

            Debug.Log("Player " + player.getPlayerNumber() + " moves to position " +
                      targetCoordinates.getX() + ", " + targetCoordinates.getY());

            return true;
        }
        else {
            Debug.Log("Invalid Movement!");

            return false;
        }
    }
}
