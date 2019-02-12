using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Terrain : MonoBehaviour
{
    // Static Members
    public static int numberOfTerrains = 5;

    // Data Members

    // Stati Functions
    public static Terrain getRandomTerrain() {
        System.Random randomNumberGenerator = new System.Random();
        int randomNumber = randomNumberGenerator.Next(1,numberOfTerrains);

        switch (randomNumber) {
            case 1:
                return new Desert();
            case 2:
                return new Forest();
            case 3:
                return new Water();
            case 4:
                return new Lava();
            case 5:
                return new Mountain();
            default:
                return new Desert();
        }
    }
}
