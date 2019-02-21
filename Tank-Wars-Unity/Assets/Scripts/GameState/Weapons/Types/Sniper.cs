using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon {
    // Data Members

    // Constructors
    public Sniper(Tank tank) {
        this.damage = 3;
        this.distance = 10;
        this.tank = tank;

        // Orientation is up by default for red and down by default for blue
        orientation = (tank.getPlayer().getPlayerNumber() == (int)Players.Red) ? (int)Orientations.Up : (int)Orientations.Down;
    }

    // Member Functions

    // SNIPER: Shoots very far in a straight line, only in one direction 
    public override bool isValidAttack(Grid grid, CoordinateSet targetCoordinates) {
        bool validAttack = false;
        GridNode targetNode = grid.getGridNode(targetCoordinates);

        // Check for valid attack
        validAttack = attackCheck(grid, tank.getCoordinates(), targetNode);

        // If all is well, decrement targeted player health and update the game state.
        if (validAttack) {
            Tank targetTank = targetNode.getTank();
            targetTank.decrementHealth(this.damage);

            Debug.Log("Player " + this.tank.getPlayer().getPlayerNumber() + " attacks Player " +
                      targetTank.getPlayer().getPlayerNumber() + " for " + this.damage + " damage!");

            return true;

        }
        else {
            Debug.Log("Invalid Attack!");
        }

        return false;
    }

    public bool attackCheck(Grid grid,
                            CoordinateSet currentTankCoordinates,
                            GridNode targetNode) {
        int gridSize = grid.getGridSize();
        int currentTankX = currentTankCoordinates.getX();
        int currentTankY = currentTankCoordinates.getY();
        int targetNodeX = targetNode.getCoordinateSet().getX();
        int targetNodeY = targetNode.getCoordinateSet().getY();

        switch (this.orientation) {
            case 0:
                for(int i = 0; i < this.distance; i++) {
                    // Check for index out of bounds
                    if (currentTankY + i >= gridSize) { break; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX, currentTankY + i).getTerrain() is Mountain) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if (currentTankX  == targetNodeX && (currentTankY + i) == targetNodeY) {
                        return true;
                    }
                }
                break;
            case 90:
                break;
            case 180:
                for (int i = 0; i < this.distance; i++) {
                    // Check for index out of bounds
                    if (currentTankY - i < 0) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX, currentTankY - i).getTerrain() is Mountain) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if ((currentTankX) == targetNodeX && (currentTankY - i) == targetNodeY) {
                        return true;
                    }
                }
                break;
            case 270:
                break;
        }

        return false;
    }

}