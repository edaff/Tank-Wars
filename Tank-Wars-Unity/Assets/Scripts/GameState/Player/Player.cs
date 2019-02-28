using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    protected PlayerColors playerColor;
    protected Tank[] tanks;

    public Player(PlayerColors playerColor) {
        this.playerColor = playerColor;
        this.tanks = new Tank[3] { new EmptyTankSlot(), new EmptyTankSlot(), new EmptyTankSlot() };
    }

    public Player(PlayerColors playerColor, int[] playerTankArray) {
        this.playerColor = playerColor;
        this.tanks = createTankArray(playerTankArray, playerColor);
    }

    private Tank[] createTankArray(int [] playerTankArray, PlayerColors playerColor) {
        Tank[] tanks = new Tank[3];

        for(int i = 0; i < playerTankArray.Length; i++) {
            switch (playerTankArray[i]) {
                case 0:
                    tanks[i] = new EmptyTankSlot();
                    break;
                case 1:
                    if(playerColor == PlayerColors.Red) {
                        tanks[i] = new CannonTank(this, new CoordinateSet(4,0));
                    }
                    else {
                        tanks[i] = new CannonTank(this, new CoordinateSet(5,9));
                    }       
                    break;
                case 2:
                    if (playerColor == PlayerColors.Red) {
                        tanks[i] = new SniperTank(this, new CoordinateSet(4, 0));
                    }
                    else {
                        tanks[i] = new SniperTank(this, new CoordinateSet(5, 9));
                    }
                    break;
                // If three tanks, this needs to be implemented
                case 3:
                    if (playerColor == PlayerColors.Red) {
                        tanks[i] = new MortarTank(this, new CoordinateSet(4, 0));
                    }
                    else {
                        tanks[i] = new MortarTank(this, new CoordinateSet(5, 9));
                    }
                    break;
                    break;
                default:
                    tanks[i] = new EmptyTankSlot();
                    break;
            }
        }

        return tanks;
    }

    public PlayerColors getPlayerColor() {
        return this.playerColor;
    }

    public Tank[] getPlayerTanks() {
        return this.tanks;
    }

    public void setPlayerTanks(Tank[] tanks) {
        this.tanks = tanks;
    }

    public Tank getPlayerTankByCoordinates(CoordinateSet tankCoordinates) {

        // Iterate over the tank array
        for(int i=0; i < 3; i++) {
            Tank currentTank = this.tanks[i];

            // Check for empty tank slot
            if (currentTank is EmptyTankSlot) {
                continue;
            }
            else {
                // If the coordinates match, return that tank
                if(tankCoordinates == currentTank.getCoordinates()) {
                    return currentTank;
                }
            }
        }

        return new EmptyTankSlot();
    }

    public void updateAllTankPowerups() {
        for(int i = 0; i < 3; i++) {
            Tank currentTank = this.tanks[i];

            // Check for empty tank slot
            if (currentTank is EmptyTankSlot) {
                continue;
            }
            else {
                currentTank.updatePowerupState();
            }
        }
    }
}

public enum PlayerColors {
    Red = 1,
    Blue = 2
}