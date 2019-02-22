
public class GameState {
    protected Grid grid;
    protected Player player1;
    protected Player player2;
    protected Levels level;

    public GameState(Levels level, int[] player1Tanks, int[] player2Tanks) {
        this.level = level;

        player1 = new Player(PlayerColors.Red, player1Tanks);
        player2 = new Player(PlayerColors.Blue, player2Tanks);

        // Create the grid
        switch (this.level) {
            case Levels.Level1:
                grid = new Grid("10x10 Grid Template", 10, player1, player2);
                break;
            case Levels.Level2:
                grid = new Grid("15x15 Grid Template", 15, player1, player2);
                break;
            case Levels.Level3:
                grid = new Grid("20x20 Grid Template", 20, player1, player2);
                break;
            default:
                grid = new Grid("10x10 Grid Template", 10, player1, player2);
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

    public bool checkValidMove(PlayerColors playerTurn, CoordinateSet tankCoordinates, CoordinateSet targetCoordinates) {
        Tank currentTank;

        if(playerTurn == PlayerColors.Red) {
            currentTank = player1.getPlayerTankByCoordinates(tankCoordinates);
        }
        else {
            currentTank = player2.getPlayerTankByCoordinates(tankCoordinates);
        }

        return currentTank.isValidMovement(this.grid, targetCoordinates);
    }

    public bool checkValidAttack(PlayerColors playerTurn, CoordinateSet currentTankCoordinates, CoordinateSet targetTankCoordinates) {
        Tank currentTank;

        if (playerTurn == PlayerColors.Red) {
            currentTank = player1.getPlayerTankByCoordinates(currentTankCoordinates);
        }
        else {
            currentTank = player2.getPlayerTankByCoordinates(currentTankCoordinates);
        }

        return currentTank.getWeapon().isValidAttack(this.grid, targetTankCoordinates);
    }

    public string playerGamble(PlayerColors playerTurn, CoordinateSet currentTankCoordinates) {
        Tank currentTank = getPlayerTank(playerTurn, currentTankCoordinates);
        Powerup powerup = Powerup.gamble();

        currentTank.setPowerup(powerup);

        return powerup.ToString();
    }

    private Tank getPlayerTank(PlayerColors playerTurn, CoordinateSet currentTankCoordinates) {
        if (playerTurn == PlayerColors.Red) {
            return player1.getPlayerTankByCoordinates(currentTankCoordinates);
        }
        else {
            return player2.getPlayerTankByCoordinates(currentTankCoordinates);
        }
    }
}

public enum Levels {
    Level1 = 1,
    Level2 = 2,
    Level3 = 3
}