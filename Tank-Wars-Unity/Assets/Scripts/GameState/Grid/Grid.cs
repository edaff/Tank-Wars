using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    // **********************
    //      Data Members
    // **********************
    private GridNode[,] grid;
    private int gridSize;

    // **********************
    //      Constructors
    // **********************

    // Create a default 10x10 grid with randomly generated terrain 
    public Grid() {
        grid = new GridNode[10,10];
        gridSize = 10;
        randomlyGenerateGridTerrain();
    }

    // Pass in the game object name so the grid can be constructed off of the pre-built gameobject
    public Grid(string sceneGridName, int gridSize, Player player1, Player player2) {
        this.gridSize = gridSize;

        // Create Grid
        grid = new GridNode[gridSize,gridSize];
        GameObject sceneGrid = GameObject.Find(sceneGridName);

        int tileX, tileY;
        foreach (Transform child in sceneGrid.transform) {
            tileX = (int)child.position.x;
            tileY = (int)child.position.z;
            grid[tileX, tileY] = new GridNode(Terrain.getTerrainFromTag(child.tag), new CoordinateSet(tileX, tileY));
        }

        // Assign tanks
        assignTanks(player1);
        assignTanks(player2);
    }

    // Create a default grid of custom size with randomly generated terrain
    public Grid(int size) {
        grid = new GridNode[size,size];
        this.gridSize = size;
        randomlyGenerateGridTerrain();
    }

    // ************************
    //      Member Functons
    // ************************

    // TODO: Assign tanks to the grid based on the player arrays
    private void assignTanks(Player player) {
        Tank[] playerTanks = player.getPlayerTanks();

        for(int i = 0;i < playerTanks.Length; i++) {
            Tank currentTank = playerTanks[i];

            if(playerTanks[i] is EmptyTankSlot) {
                continue;
            }

            int currentTankX = currentTank.getCoordinates().getX();
            int currentTankY = currentTank.getCoordinates().getY();

            grid[currentTankX, currentTankY].setTank(currentTank);
        }

        Debug.Log("Player " + player.getPlayerColor() + "'s tanks have been assigned to the grid...");
    }

    // Code to randomly genereate terrains
    private void randomlyGenerateGridTerrain() {
        for(int i = 0; i < gridSize; i++) {
            for(int j = 0; j < gridSize; j++) {
                grid[i, j] = new GridNode(new CoordinateSet(i,j));
            }
        }
    }

    public int getGridSize() {
        return this.gridSize;
    }

    public GridNode[,] getGrid() {
        return this.grid;
    }

    public GridNode getGridNode(int x, int y) {
        return this.grid[x,y];
    }

    public GridNode getGridNode(CoordinateSet coordinates) {
        return this.grid[coordinates.getX(), coordinates.getY()];
    }
}
