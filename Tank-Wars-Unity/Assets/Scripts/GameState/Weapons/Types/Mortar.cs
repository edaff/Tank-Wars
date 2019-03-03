using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Weapon
{
    // Data Members

    // Constructors
    public Mortar(Tank tank) {
        this.damage = 20;
        this.distance = 3;
        this.tank = tank;

        // Orientation is up by default for red and down by default for blue
        this.orientation = (this.tank.getPlayer().getPlayerColor() == PlayerColors.Red) ? Orientations.Up : Orientations.Down;
    }

    // Member Functions
    public override bool isValidAttack(Grid grid, CoordinateSet targetCoordinates, bool updateState) {
        bool validAttack = false;
        GridNode targetNode = grid.getGridNode(targetCoordinates);

        // Check for valid attack
        for (int i = 0; i < 4; i++) {
            validAttack = attackCheck(grid, i, tank.getCoordinates(), targetNode);

            if (validAttack) {
                break;
            }
        }

        // If all is well, decrement targeted player health and update the game state.
        if (validAttack) {
            if(updateState){
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

    private bool attackCheck(Grid grid, int currentIteration,
                            CoordinateSet currentTankCoordinates,
                            GridNode targetNode) {
        int gridSize = grid.getGridSize();
        int currentTankX = currentTankCoordinates.getX();
        int currentTankY = currentTankCoordinates.getY();
        int targetNodeX = targetNode.getCoordinateSet().getX();
        int targetNodeY = targetNode.getCoordinateSet().getY();

        for (int i = 1; i <= this.distance; i++) {
            switch (currentIteration) {
                case 0:
                    // Check for index out of bounds
                    if (currentTankX + i >= gridSize) { continue; }

                    // Do final check to see if this is the target node
                    if ((currentTankX + i) == targetNodeX && currentTankY == targetNodeY) {
                        return true;
                    }

                    break;
                case 1:
                    // Check for index out of bounds
                    if (currentTankX - i < 0) { continue; }

                    // Do final check to see if this is the target node
                    if ((currentTankX - i) == targetNodeX && currentTankY == targetNodeY) {
                        return true;
                    }

                    break;
                case 2:
                    // Check for index out of bounds
                    if (currentTankY + i >= gridSize) { continue; }

                    // Do final check to see if this is the target node
                    if ((currentTankX) == targetNodeX && (currentTankY + i) == targetNodeY) {
                        return true;
                    }

                    break;
                case 3:
                    // Check for index out of bounds
                    if (currentTankY - i < 0) { continue; }

                    // Do final check to see if this is the target node
                    if ((currentTankX) == targetNodeX && (currentTankY - i) == targetNodeY) {
                        return true;
                    }

                    break;
            }
        }

        return false;
    }

    public override ArrayList getValidAttacks(Grid grid) {
        int gridSize = grid.getGridSize();
        int currentTankX = this.tank.getCoordinates().getX();
        int currentTankY = this.tank.getCoordinates().getY();
        ArrayList validAttacks = new ArrayList();

        for (int i = 0; i < 4; i++) {
            for (int j = 1; j <= this.distance; j++) {
                switch (i) {
                    case 0:
                        // Check for index out of bounds
                        if (currentTankX + j >= gridSize) { continue; }

                        // Do final check to see if this is the target node
                        validAttacks.Add(new CoordinateSet(currentTankX + j, currentTankY));

                        break;
                    case 1:
                        // Check for index out of bounds
                        if (currentTankX - j < 0) { continue; }

                        validAttacks.Add(new CoordinateSet(currentTankX - j, currentTankY));

                        break;
                    case 2:
                        // Check for index out of bounds
                        if (currentTankY + j >= gridSize) { continue; }

                        validAttacks.Add(new CoordinateSet(currentTankX, currentTankY + j));
                        break;
                    case 3:
                        // Check for index out of bounds
                        if (currentTankY - j < 0) { continue; }

                        validAttacks.Add(new CoordinateSet(currentTankX, currentTankY - j));

                        break;
                }
            }
        }

        return validAttacks;
    }
}
