using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    // Data Members
    private Terrain terrain;
    private Tank tank;
    private CoordinateSet coordinates;

    // Constructors
    public GridNode() {
        this.terrain = Terrain.getRandomTerrain();
        this.tank = new EmptyTankSlot();
        this.coordinates = new CoordinateSet(0,0);
    }

    public GridNode(CoordinateSet coordinates) {
        this.terrain = Terrain.getRandomTerrain();
        this.tank = new EmptyTankSlot();
        this.coordinates = coordinates;
    }

    public GridNode(Terrain terrain, Tank tank) {
        this.terrain = terrain;
        this.tank = tank;
        this.coordinates = new CoordinateSet(0,0);
    }

    public GridNode(Terrain terrain, CoordinateSet coordinates) {
        this.terrain = terrain;
        this.tank = new EmptyTankSlot();
        this.coordinates = coordinates;
    }

    public GridNode(Terrain terrain, Tank tank, CoordinateSet coordinates) {
        this.terrain = terrain;
        this.tank = tank;
        this.coordinates = coordinates;
    }

    // Member Functions
    public Tank getTank() {
        return this.tank;
    }

    public Terrain getTerrain() {
        return this.terrain;
    }

    public CoordinateSet getCoordinateSet() {
        return this.coordinates;
    }

    public void setTank(Tank tank) {
        this.tank = tank;
    }
}
