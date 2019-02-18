using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    // Data Members

    // Constructors 
    public Cannon(Tank tank) {
        this.damage = 2;
        this.distance = 3;
        this.tank = tank;
    }

    // Member Functions
    public override bool isValidAttack(Grid grid, CoordinateSet targetCoordinates) {
        bool validAttack = false;
        GridNode targetNode = grid.getGridNode(targetCoordinates);

        // Check for valid attack
        for(int i = 0; i < 4; i++) {
            validAttack = attackCheck(grid, i, tank.getCoordinates(), targetNode);
        }

        // If all is well, decrement targeted player health and update the game state.
        if (validAttack) {
            Tank targetTank = targetNode.getTank();
            targetTank.decrementHealth(this.damage);

            Debug.Log("Player " + this.tank.getPlayer().getPlayerNumber() + " attacks player " + 
                      targetTank.getPlayer().getPlayerNumber() + " for " + this.damage + " damage!");

        }
        else {
            Debug.Log("Invalid Attack!");
        }

        return false;
    }

    public bool attackCheck(Grid grid, int currentIteration, 
                            CoordinateSet currentTankCoordinates,
                            GridNode targetNode) {
        int gridSize = grid.getGridSize();
        int currentTankX = currentTankCoordinates.getX();
        int currentTankY = currentTankCoordinates.getY();
        int targetNodeX = targetNode.getCoordinateSet().getX();
        int targetNodeY = targetNode.getCoordinateSet().getY();

        for(int i = 1; i < this.distance; i++) {
            switch (currentIteration) {
                case 0:
                    // Check for index out of bounds
                    if(currentTankX + i >= gridSize) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX + i, currentTankY).getTerrain() is Mountain) {
                        return false;
                    }

                    // Check if a tank is in the path or on the target node
                    // TODO: Hit tank that is in the way
                    if (!grid.getGridNode(currentTankX + i, currentTankY).getTank().isEmptySlot()) {
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