using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Terrain
{
    // Static Members
    public static int numberOfTerrains = 5;

    // Data Members

    // Static Functions
    public static Terrain getRandomTerrain() {
        System.Random randomNumberGenerator = new System.Random();
        int randomNumber = randomNumberGenerator.Next(1,numberOfTerrains);

        switch (randomNumber) {
            case 1:
                return new Neutral();
            case 2:
                return new Forest();
            case 3:
                return new Water();
            case 4:
                return new Lava();
            case 5:
                return new Mountain();
            default:
                return new Neutral();
        }
    }

    public static Terrain getTerrainFromTerrainMapValue(int terrainMapValue) {
        switch (terrainMapValue) {
            case 0:
                return new Neutral();
            case 1:
                return new Forest();
            case 2:
                return new Water();
            case 3:
                return new Lava();
            case 4:
                return new Mountain();
            default:
                return new Neutral();
        }
    }

    public static Terrain getTerrainFromTag(string tag) {
        switch (tag) {
            case "Neutral":
                return new Neutral();
            case "Forest":
                return new Forest();
            case "Water":
                return new Water();
            case "Lava":
                return new Lava();
            case "Mountain":
                return new Mountain();
            default:
                return new Neutral();
        }
    }
}

public enum Terrains {
    Neutral = 0,
    Forest = 1,
    Water = 2,
    Lava = 3,
    Rock = 4
}