using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank
{
    // **********************
    //      Data Members
    // **********************
    protected int health;
    protected int movement;
    protected Weapon weapon;
    protected Powerup powerup;
    protected Player player;
    protected CoordinateSet coordinates;

    // ************************
    //      Member Functons
    // ************************
    virtual public bool isEmptySlot() {
        return false;
    }

    virtual public bool isValidMovement(Grid grid, CoordinateSet coordinates) {
        return true;
    }

    public int getHealth() {
        return this.health;
    }
    public void decrementHealth(int amount) {
        this.health -= amount;
    }

    public int getMovement() {
        return this.movement;
    }

    public Weapon getWeapon() {
        return this.weapon;
    }

    public Powerup getPowerup() {
        return this.powerup;
    }

    public void setPowerup(Powerup powerup) {
        this.powerup = powerup;
    }

    public Player getPlayer() {
        return this.player;
    }

    public CoordinateSet getCoordinates() {
        return this.coordinates;
    }

    public void updatePowerupState() {
        if(this.powerup is EmptyPowerupSlot) {
            return;
        }

        this.powerup.decrementPowerupDuration();

        if (this.powerup.isExpired()) {
            this.powerup = new EmptyPowerupSlot();
        }
    }

    // Iterate 4 times in all directions and check that the target node is within range
    protected bool movementCheck(Grid grid, int currentIteration,
                               CoordinateSet currentTankCoordinates,
                               GridNode targetNode) {
        int gridSize = grid.getGridSize();
        int currentTankX = currentTankCoordinates.getX();
        int currentTankY = currentTankCoordinates.getY();
        int targetNodeX = targetNode.getCoordinateSet().getX();
        int targetNodeY = targetNode.getCoordinateSet().getY();

        for (int i = 1; i <= this.movement; i++) {
            switch (currentIteration) {
                case 0:
                    // Check for index out of bounds
                    if (currentTankX + i >= gridSize) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX + i, currentTankY).getTerrain() is Mountain) {
                        return false;
                    }

                    // Check if a tank is in the path or on the target node
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
