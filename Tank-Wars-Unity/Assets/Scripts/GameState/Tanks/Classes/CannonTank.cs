using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTank : Tank
{
    // Data Members

    // Constructors
    public CannonTank(Player player, CoordinateSet coordinates) {
        this.health = 100;
        this.movement = 3;
        this.player = player;
        this.coordinates = coordinates;
        this.powerup = new EmptyPowerupSlot();
        this.weapon = new Cannon(this);
    }

    // Member Functions
    // Check that the desired movement is valid
    public override bool isValidMovement(Grid grid, CoordinateSet targetCoordinates, bool updateState) {
        bool validMovement = false;

        GridNode targetNode = grid.getGridNode(targetCoordinates);

        // If this coordinate set is (-1,-1), the target coordinates were out of bounds.
        if (targetNode.getCoordinateSet().getX() == -1 && targetNode.getCoordinateSet().getY() == -1) {
            return false;
        }

        // Check for valid movement
        for(int i = 0;i < 4; i++) {
            validMovement = movementCheck(grid, i, this.coordinates, targetNode);
            if (validMovement) { break; }
        }

        // If all is well, update the game state and move the tank to the
        // targeted location.
        if (validMovement) {
            if(updateState){
                targetNode.setTank(grid.getGridNode(coordinates).getTank());
                grid.getGridNode(coordinates).setTank(new EmptyTankSlot());
                this.coordinates = new CoordinateSet(targetNode.getCoordinateSet().getX(), targetNode.getCoordinateSet().getY());

                Debug.Log("Player " + player.getPlayerColor() + " moves to position " +
                          targetCoordinates.getX() + ", " + targetCoordinates.getY());

                if (grid.getGridNode(coordinates).getTerrain() is Water) {
                    this.health -= 10;
                }
                else if (grid.getGridNode(coordinates).getTerrain() is Lava) {
                    this.health = 0;
                }
            }

            return true;
        }
        else {
            if(updateState){
                Debug.Log("Invalid Movement!");
            }

            return false;
        }
    }
}
