using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon {
    // Data Members

    // Constructors
    public Sniper(Tank tank) {
        this.damage = 10;
        this.distance = 10;
        this.tank = tank;

        // Orientation is up by default for red and down by default for blue
        orientation = (tank.getPlayer().getPlayerColor() == PlayerColors.Red) ? Orientations.Up : Orientations.Down;
    }

    // Member Functions

    // SNIPER: Shoots very far in a straight line, only in one direction 
    public override bool isValidAttack(Grid grid, CoordinateSet targetCoordinates, bool updateState) {
        bool validAttack = false;
        GridNode targetNode = grid.getGridNode(targetCoordinates);

        // Check for valid attack
        validAttack = attackCheck(grid, tank.getCoordinates(), targetNode);

        // If all is well, decrement targeted player health and update the game state.
        if (validAttack) {
            if(updateState)
            {
                Tank targetTank = targetNode.getTank();
                targetTank.decrementHealth(this.damage);

                Debug.Log("Player " + this.tank.getPlayer().getPlayerColor() + " attacks Player " +
                          targetTank.getPlayer().getPlayerColor() + " for " + this.damage + " damage!");
                Debug.Log("Player " + targetTank.getPlayer().getPlayerColor() + "'s health is now at: " + targetTank.getHealth());
            }
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
            case Orientations.Up:
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
            case Orientations.Right:
                break;
            case Orientations.Down:
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
            case Orientations.Left:
                break;
        }

        return false;
    }

    public override ArrayList getValidAttacks(Grid grid) {
        int gridSize = grid.getGridSize();
        int currentTankX = this.tank.getCoordinates().getX();
        int currentTankY = this.tank.getCoordinates().getY();
        ArrayList validAttacks = new ArrayList();

        switch (this.orientation) {
            case Orientations.Up:
                for (int i = 0; i < this.distance; i++) {
                    // Check for index out of bounds
                    if (currentTankY + i >= gridSize) { break; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX, currentTankY + i).getTerrain() is Mountain) {
                        break;
                    }

                    validAttacks.Add(new CoordinateSet(currentTankX, currentTankY + i));
                }
                break;
            case Orientations.Right:
                break;
            case Orientations.Down:
                for (int i = 0; i < this.distance; i++) {
                    // Check for index out of bounds
                    if (currentTankY - i < 0) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX, currentTankY - i).getTerrain() is Mountain) {
                        break;
                    }

                    validAttacks.Add(new CoordinateSet(currentTankX, currentTankY - i));
                }
                break;
            case Orientations.Left:
                break;
        }

        return validAttacks;
    }

}