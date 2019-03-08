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

    public Player(PlayerColors playerColor, int[] playerTankArray, Levels level) {
        this.playerColor = playerColor;
        this.tanks = createTankArray(playerTankArray, playerColor, level);
    }

    public Tank[] createTankArray(int [] playerTankArray, PlayerColors playerColor, Levels level) {
        Tank[] tanks = new Tank[3];
        CoordinateSet[] level1Red = { new CoordinateSet(4, 0) };
        CoordinateSet[] level1Blue = { new CoordinateSet(5, 9) };
        CoordinateSet[] level2Red = { new CoordinateSet(3, 0), new CoordinateSet(11,0) };
        CoordinateSet[] level2Blue = { new CoordinateSet(3, 14), new CoordinateSet(11,14) };

        for (int i = 0; i < playerTankArray.Length; i++) {
            switch (playerTankArray[i]) {
                // Empty Slot
                case 0:
                    tanks[i] = new EmptyTankSlot();
                    break;
                // Cannon Tank
                case 1:
                    if(playerColor == PlayerColors.Red) {
                        if (level == Levels.Level1) {
                            tanks[i] = new CannonTank(this, level1Red[i]);
                        }
                        else if (level == Levels.Level2) {
                            tanks[i] = new CannonTank(this, level2Red[i]);
                        }
                    }
                    else {
                        if (level == Levels.Level1) {
                            tanks[i] = new CannonTank(this, level1Blue[i]);
                        }
                        else if (level == Levels.Level2) {
                            tanks[i] = new CannonTank(this, level2Blue[i]);
                        }
                    }
                    break;
                // Sniper Tank
                case 2:
                    if (playerColor == PlayerColors.Red) {
                        if (level == Levels.Level1) {
                            tanks[i] = new SniperTank(this, level1Red[i]);
                        }
                        else if (level == Levels.Level2) {
                            tanks[i] = new SniperTank(this, level2Red[i]);
                        }
                    }
                    else {
                        if (level == Levels.Level1) {
                            tanks[i] = new SniperTank(this, level1Blue[i]);
                        }
                        else if (level == Levels.Level2) {
                            tanks[i] = new SniperTank(this, level2Blue[i]);
                        }
                    }
                    break;
                // Mortar Tank
                case 3:
                    if (playerColor == PlayerColors.Red) {
                        if (level == Levels.Level1) {
                            tanks[i] = new MortarTank(this, level1Red[i]);
                        }
                        else if (level == Levels.Level2) {
                            tanks[i] = new MortarTank(this, level2Red[i]);
                        }
                    }
                    else {
                        if (level == Levels.Level1) {
                            tanks[i] = new MortarTank(this, level1Blue[i]);
                        }
                        else if (level == Levels.Level2) {
                            tanks[i] = new MortarTank(this, level2Blue[i]);
                        }
                    }
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

    public void updateAllTankHealthBars(HpController hpController) {
        for (int i = 0; i < 3; i++) {
            Tank currentTank = this.tanks[i];

            if (currentTank is EmptyTankSlot) {
                continue;
            }
            else {
                switch (i) {
                    case 0:
                        if (this.playerColor == PlayerColors.Red) {
                            hpController.RedTank1Hp(currentTank.getHealth());
                        }
                        else {
                            hpController.BlueTank1Hp(currentTank.getHealth());
                        }
                        break;
                    case 1:
                        if (this.playerColor == PlayerColors.Red) {
                            hpController.RedTank2Hp(currentTank.getHealth());
                        }
                        else {
                            hpController.BlueTank2Hp(currentTank.getHealth());
                        }
                        break;
                    case 2:
                        if (this.playerColor == PlayerColors.Red) {
                            hpController.RedTank3Hp(currentTank.getHealth());
                        }
                        else {
                            hpController.BlueTank3Hp(currentTank.getHealth());
                        }
                        break;
                }
            }
        }
    }

    public ArrayList getAllTankCoordinates() {
        ArrayList tankCoordinates = new ArrayList();

        for (int i = 0; i < 3; i++) {
            if (this.tanks[i] is EmptyTankSlot || this.tanks[i].isDead()) {
                continue;
            }

            tankCoordinates.Add(this.tanks[i].getCoordinates());
        }

        return tankCoordinates;
    }
}

public enum PlayerColors {
    Red = 1,
    Blue = 2,
    None = 3
}