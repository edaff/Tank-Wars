
using System.Collections;

public class GameState {
    protected Grid grid;
    public int gridSize;
    protected Player player1;
    protected Player player2;
    protected Levels level;

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

    public bool isGameOver() {
        Tank[] player1Tanks = player1.getPlayerTanks();
        Tank[] player2Tanks = player2.getPlayerTanks();

        if (areAllPlayerTanksAreDead(player1.getPlayerTanks()) || areAllPlayerTanksAreDead(player2.getPlayerTanks())) {
            return true;
        }
        else {
            return false;
        }
    }

    private bool areAllPlayerTanksAreDead(Tank[] tanks) {
        bool allTanksDead = true;
        for (int i = 0; i < tanks.Length; i++) {
            if (tanks[i] is EmptyTankSlot) {
                continue;
            }

            if (tanks[i].getHealth() >= 0) {
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
        currentTank.decrementHealth(10);

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

    public void highlightPlayerTiles(PlayerColors player, Rounds round) {
        if (player == PlayerColors.Red) {
            TileHighlighter.highlightValidTiles(player1.getAllTankCoordinates(), round);
        }
        else {
            TileHighlighter.highlightValidTiles(player2.getAllTankCoordinates(), round);
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
            powerups[i] = tanks[i].getPowerupAsString();
        }

        return powerups;

    }
}

public enum Levels {
    Level1 = 1,
    Level2 = 2,
    Level3 = 3
}