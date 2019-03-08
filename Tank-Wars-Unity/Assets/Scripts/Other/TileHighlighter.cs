using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlighter
{
    public static void highlightValidTiles(ArrayList listOfTileCoordinates, Rounds round) {
        GameObject grid = GameObject.FindGameObjectWithTag("Grid");
        GameObject child;
        MeshRenderer meshRenderer;
        CoordinateSet currentCoordinates;

        for (int i = 0; i < grid.transform.childCount; i++) {
            child = grid.transform.GetChild(i).gameObject;
            meshRenderer = child.GetComponent<MeshRenderer>();

            for(int j = 0; j < listOfTileCoordinates.Count; j++) {
                currentCoordinates = (CoordinateSet)listOfTileCoordinates[j];

                if ((int)child.transform.position.x == currentCoordinates.getX() && (int)child.transform.position.z == currentCoordinates.getY()){
                    if (round == Rounds.Move) {
                        meshRenderer.material.color = Color.yellow;
                    }
                    else if(round == Rounds.Attack){
                        meshRenderer.material.color = Color.red;
                    }
                    else {
                        meshRenderer.material.color = Color.magenta;
                    }
                }
            }
        }
    }

    public static void resetTiles() {

        GameObject grid = GameObject.FindGameObjectWithTag("Grid");
        GameObject child;
        MeshRenderer meshRenderer;

        for (int i = 0; i < grid.transform.childCount; i++) {
            child = grid.transform.GetChild(i).gameObject;
            meshRenderer = child.GetComponent<MeshRenderer>();

            if(child.tag == "Water") {
                meshRenderer.material.color = new Color(1.000f, 1.000f, 1.000f, 0.820f);
            }
            else {
                meshRenderer.material.color = new Color(1.000f, 1.000f, 1.000f, 1.000f);
            }
        }
    }
}
