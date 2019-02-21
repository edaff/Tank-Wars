
public class GameState {
    Grid grid;
    Player player1;
    Player player2;

    public GameState(int level, int[] player1Tanks, int[] player2Tanks) {
        player1 = new Player(1, player1Tanks);
        player2 = new Player(2, player2Tanks);

        // Create the grid
        switch (level) {
            case 1:
                grid = new Grid("10x10 Grid Template", 10, player1, player2);
                break;
            case 2:
                grid = new Grid("15x15 Grid Template", 15, player1, player2);
                break;
            case 3:
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

    public bool checkValidMove(int playerTurn, CoordinateSet tankCoordinates, CoordinateSet targetCoordinates) {
        Tank currentTank;

        if(playerTurn == (int)Players.Red) {
            currentTank = player1.getPlayerTankByCoordinates(tankCoordinates);
        }
        else {
            currentTank = player2.getPlayerTankByCoordinates(tankCoordinates);
        }

        return currentTank.isValidMovement(this.grid, targetCoordinates);
    }

    public bool checkValidAttack(int playerTurn, CoordinateSet currentTankCoordinates, CoordinateSet targetTankCoordinates) {
        Tank currentTank;

        if (playerTurn == (int)Players.Red) {
            currentTank = player1.getPlayerTankByCoordinates(currentTankCoordinates);
        }
        else {
            currentTank = player2.getPlayerTankByCoordinates(currentTankCoordinates);
        }

        return currentTank.getWeapon().isValidAttack(this.grid, targetTankCoordinates);
    }

    public string playerGamble(int playerTurn, CoordinateSet currentTankCoordinates) {
        Tank currentTank = getPlayerTank(playerTurn, currentTankCoordinates);
        Powerup powerup = Powerup.gamble();

        currentTank.setPowerup(powerup);

        return powerup.ToString();
    }

    private Tank getPlayerTank(int playerTurn, CoordinateSet currentTankCoordinates) {
        if (playerTurn == (int)Players.Red) {
            return player1.getPlayerTankByCoordinates(currentTankCoordinates);
        }
        else {
            return player2.getPlayerTankByCoordinates(currentTankCoordinates);
        }
    }
}
