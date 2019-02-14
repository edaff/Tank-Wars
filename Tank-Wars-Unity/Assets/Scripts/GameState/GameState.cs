using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    Grid grid;
    Player player1;
    Player player2;
    // Start is called before the first frame update
    void Start()
    {
        player1 = new Player(1);
        player2 = new Player(2);

        grid = new Grid("10x10 Grid Template", 10);
        GridNode node;

        for(int i = 0; i < grid.getGridSize(); i++) {
            for(int j=0;j < grid.getGridSize(); j++) {
                node = grid.getGridNode(i, j);
                print("(" + node.getCoordinateSet().getX() + ", " + node.getCoordinateSet().getY() + ") - " + node.getTerrain());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
