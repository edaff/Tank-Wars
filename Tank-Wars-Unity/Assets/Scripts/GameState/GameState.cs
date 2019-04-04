
using System.Collections;
using UnityEngine;

public class GameState {
    protected Grid grid;
    public int gridSize;
    protected Player player1;
    protected Player player2;
    public Levels level;

    public GameState(Levels level, int[] player1Tanks, int[] player2Tanks) {
        this.level = level;

        player1 = new Player(PlayerColors.Red, player1Tanks, level);
        player2 = new Player(PlayerColors.Blue, player2Tanks, level);

        // Create the grid
        switch (this.level) {
            case Levels.Level1:
                gridSize = 10;
                grid = new Grid("10x10 Grid Template", gridSize, player1, player2);
                break;
            case Levels.Level2:
                gridSize = 15;
                grid = new Grid("15x15 Grid Template", gridSize, player1, player2);
                break;
            case Levels.Level3:
                gridSize = 20;
                grid = new Grid("20x20 Grid Template", gridSize, player1, player2);
                break;
            default:
                gridSize = 10;
                grid = new Grid("10x10 Grid Template", gridSize, player1, player2);
                break;
        }
    }

    public PlayerColors isGameOver() {
        if (areAllPlayerTanksDead(player1.getPlayerTanks())) {
            return PlayerColors.Red;
        }
        else if (areAllPlayerTanksDead(player2.getPlayerTanks())){
            return PlayerColors.Blue;
        }
        else {
            return PlayerColors.None;
        }
    }

    private bool areAllPlayerTanksDead(Tank[] tanks) {
        bool allTanksDead = true;
        for (int i = 0; i < tanks.Length; i++) {
            if (tanks[i] is EmptyTankSlot) {
                continue;
            }

            if (tanks[i].getHealth() > 0) {
                allTanksDead = false;
            }
        }

        return allTanksDead;
    }

    public bool checkValidMove(PlayerColors playerTurn, CoordinateSet tankCoordinates, CoordinateSet targetCoordinates, bool updateState) {
        Tank currentTank;

        if(playerTurn == PlayerColors.Red) {
            currentTank = player1.getPlayerTankByCoordinates(tankCoordinates);
        }
        else {
            currentTank = player2.getPlayerTankByCoordinates(tankCoordinates);
        }

        return currentTank.isValidMovement(this.grid, targetCoordinates, updateState);
    }

    public bool checkValidAttack(PlayerColors playerTurn, CoordinateSet currentTankCoordinates, CoordinateSet targetTankCoordinates,bool updateState) {
        Tank currentTank;

        if (playerTurn == PlayerColors.Red) {
            currentTank = player1.getPlayerTankByCoordinates(currentTankCoordinates);
        }
        else {
            currentTank = player2.getPlayerTankByCoordinates(currentTankCoordinates);
        }
        return currentTank.getWeapon().isValidAttack(this.grid, targetTankCoordinates, updateState);
    }

    public string playerGamble(PlayerColors playerTurn, CoordinateSet currentTankCoordinates) {
        Tank currentTank = getPlayerTank(playerTurn, currentTankCoordinates);
        Powerup powerup = Powerup.gamble();

        if (powerup.ToString() == "Nothing") {
            currentTank.decrementHealth(10);
        }

        currentTank.setPowerup(powerup);

        return powerup.ToString();
    }

    public Tank getPlayerTank(PlayerColors playerTurn, CoordinateSet currentTankCoordinates) {
        if (playerTurn == PlayerColors.Red) {
            return player1.getPlayerTankByCoordinates(currentTankCoordinates);
        }
        else {
            return player2.getPlayerTankByCoordinates(currentTankCoordinates);
        }
    }

    public void updatePlayerPowerupState(PlayerColors player) {
        if(player == PlayerColors.Red) {
            player1.updateAllTankPowerups();
        }
        else {
            player2.updateAllTankPowerups();
        }
    }

    public void updatePlayerHealthBars(HpController hpController) {
        player1.updateAllTankHealthBars(hpController);
        player2.updateAllTankHealthBars(hpController);
    }

    public Grid getGrid() {
        return this.grid;
    }

    public void toggleTankRings(GameObject[] tanks, CoordinateSet targetTankCoordinates, bool toggle) {
        foreach (GameObject tank in tanks) {
            if (targetTankCoordinates.getX() != -1 && targetTankCoordinates.getY() != -1) {
                foreach (Transform t in tank.GetComponent<Transform>()) {
                    if ((t.name == "Ring_Red" || t.name == "Ring_Bleu") && new CoordinateSet((int)tank.transform.position.x, (int)tank.transform.position.z) == targetTankCoordinates) {
                        t.gameObject.SetActive(toggle);
                    }
                    else if ((t.name == "Ring_Red" || t.name == "Ring_Bleu")) {
                        t.gameObject.SetActive(!toggle);
                    }
                }
                continue;
            }

            foreach (Transform t in tank.GetComponent<Transform>()) {
                if(t.name == "Ring_Red" || t.name == "Ring_Bleu") {
                    t.gameObject.SetActive(toggle);
                }
            }
        }
    }

    public string[] getPlayerPowerups(PlayerColors player) {
        string[] powerups = new string[3];
        Tank[] tanks;

        if(player == PlayerColors.Red) {
            tanks = player1.getPlayerTanks();
        }
        else {
            tanks = player2.getPlayerTanks();
        }

        for(int i = 0; i < 3; i++) {
            if(tanks[i] is EmptyTankSlot) {
                powerups[i] = "Nothing";
                continue;
            }

            powerups[i] = tanks[i].getPowerupAsString();
        }

        return powerups;

    }

    // Returns an array of size 3. Each index corresponds to a tank
    // and holds an integer for it's health. If that index is an 
    // empty tank slot, the health will be -9999.
    public int[] getHealthForPlayerTanks(PlayerColors player) {
        int[] health = new int[3];
        Tank[] tanks;
        const int emptyTankSlotHealth = -9999;

        if(player == PlayerColors.Red) {
            tanks = player1.getPlayerTanks();
        }
        else {
            tanks = player2.getPlayerTanks();
        }

        for(int i = 0; i < 3; i++) {
            if(tanks[i] is EmptyTankSlot) {
                health[i] = emptyTankSlotHealth;
            }
            else {
                health[i] = tanks[i].getHealth();
            }
        }

        return health;
    }

    public bool playerHasValidAttack(PlayerColors player, CoordinateSet playerCoordinates) {
        Tank playerTank;
        Tank[] enemyTanks;

        if (player == PlayerColors.Red) {
            playerTank = player1.getPlayerTankByCoordinates(playerCoordinates);
            enemyTanks = player2.getPlayerTanks();
        }
        else {
            playerTank = player2.getPlayerTankByCoordinates(playerCoordinates);
            enemyTanks = player1.getPlayerTanks();
        }

        for (int i = 0; i < 3; i++) {
            if (enemyTanks[i] is EmptyTankSlot) { continue; }

            if (checkValidAttack(player, playerCoordinates, enemyTanks[i].getCoordinates(), false)) {
                return true;
            }
        }

        return false;
    }
}

public enum Levels {
    Level1 = 1,
    Level2 = 2,
    Level3 = 3
}