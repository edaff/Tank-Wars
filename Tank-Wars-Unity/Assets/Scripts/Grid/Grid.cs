using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Data Members
    private GridNode[,] grid;
    private int gridSize;

    // Constructors
    public Grid() {
        grid = new GridNode[10,10];
        gridSize = 10;
        randomlyGenerateGridTerrain();
    }

    public Grid(int size) {
        grid = new GridNode[size,size];
        this.gridSize = size;
        randomlyGenerateGridTerrain();
    }

    // Member Functions
    public void randomlyGenerateGridTerrain() {
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
}
