using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
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

    // Create a default grid of custom size with randomly generated terrain
    public Grid(int size) {
        grid = new GridNode[size,size];
        this.gridSize = size;
        randomlyGenerateGridTerrain();
    }

    // Create a grid based off a terrain map. Used the dimensions of the terrain
    // map for the size.
    public Grid(int[,] terrainMap) {
        this.gridSize = terrainMap.GetLength(0);
        this.grid = new GridNode[gridSize, gridSize];

        // Set up the grid
        assignTerrains(terrainMap);
        assignTanks();
    }

    // ************************
    //      Member Functons
    // ************************

    // Assign terrains to the grid based on the terrain map    
    private void assignTerrains(int[,] terrainMap) {

        int terrainX = 0;
        int terrainY = gridSize - 1;

        for(int i = 0;i < gridSize; i++) {
            terrainY = gridSize - 1;
            for(int j = 0;j < gridSize; j++) {
                grid[i, j] = new GridNode(Terrain.getTerrainFromTerrainMapValue(terrainMap[terrainX, terrainY]),
                                          new CoordinateSet(i,j));
                terrainY--;
            }
            terrainX++;
        }
    }



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

    public GridNode getGrideNode(int x, int y) {
        return this.grid[x,y];
    }
}
