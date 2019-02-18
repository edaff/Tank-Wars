using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank1 : Tank
{
    // Data Members

    // Constructors
    public Tank1(Player player, CoordinateSet coordinates) {
        this.health = 5;
        this.movement = 2;
        this.weapon = new Cannon(this);
        this.powerup = new EmptyPowerupSlot();
        this.player = player;
        this.coordinates = coordinates;
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

    // Iterate 4 times in all directions and check that the target node is within range
    private bool movementCheck(Grid grid, int currentIteration, 
                               CoordinateSet currentTankCoordinates, 
                               GridNode targetNode) {
        int gridSize = grid.getGridSize();
        int currentTankX = currentTankCoordinates.getX();
        int currentTankY = currentTankCoordinates.getY();
        int targetNodeX = targetNode.getCoordinateSet().getX();
        int targetNodeY = targetNode.getCoordinateSet().getY();

        for(int i = 1;i <= this.movement; i++) {
            switch (currentIteration) {
                case 0:
                    // Check for index out of bounds
                    if(currentTankX + i >= gridSize) { continue;  }

                    // Check for mountains
                    if(grid.getGridNode(currentTankX + i, currentTankY).getTerrain() is Mountain) {
                        return false;
                    }

                    // Check if a tank is in the path or on the target node
                    if(!grid.getGridNode(currentTankX + i, currentTankY).getTank().isEmptySlot()) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if ((currentTankX + i) == targetNodeX && currentTankY == targetNodeY) {
                        return true;
                    }

                    break;
                case 1:
                    // Check for index out of bounds
                    if (currentTankX - i < 0) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX - i, currentTankY).getTerrain() is Mountain) {
                        return false;
                    }

                    // Check if a tank is in the path or on the target node
                    if (!grid.getGridNode(currentTankX - i, currentTankY).getTank().isEmptySlot()) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if ((currentTankX - i) == targetNodeX && currentTankY == targetNodeY) {
                        return true;
                    }
                    
                    break;
                case 2:
                    // Check for index out of bounds
                    if (currentTankY + i >= gridSize) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX, currentTankY + i).getTerrain() is Mountain) {
                        return false;
                    }

                    // Check if a tank is in the path or on the target node
                    if (!grid.getGridNode(currentTankX, currentTankY + i).getTank().isEmptySlot()) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if ((currentTankX) == targetNodeX && (currentTankY + i) == targetNodeY) {
                        return true;
                    }

                    break;
                case 3:
                    // Check for index out of bounds
                    if (currentTankY - i < 0) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX, currentTankY - i).getTerrain() is Mountain) {
                        return false;
                    }

                    // Check if a tank is in the path or on the target node
                    if (!grid.getGridNode(currentTankX, currentTankY - i).getTank().isEmptySlot()) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if ((currentTankX) == targetNodeX && (currentTankY - i) == targetNodeY) {
                        return true;
                    }

                    break;
            }
        }

        return false;
    }
}
