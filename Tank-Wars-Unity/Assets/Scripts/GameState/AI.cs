using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AI
{
    public GameObject[] redTanks;
	public GameObject[] blueTanks;
	public GameState gs;

    public AI(GameObject[] rt, GameObject[] bt, GameState gamestate)
    {
    	redTanks = rt;
    	blueTanks = bt;
    	gs = gamestate;
    }

    /*
	1) Find tank closest to player
	2) Finds all valid moves
	3) Calculates weights to each move
		- +How close to player tank
		- +If it can shoot a tank
		- -Can it get shot by a tank
	4) Pick the move with the highest weight
    */
    public void greedyTurn()
    {
    	GameObject[] closestTanks;
    	ArrayList validMoves = new ArrayList();

    	closestTanks = findClosestTanks();
    	validMoves = findValidMoves(closestTanks[0]);
    }

    //Gameobject[] holds [AI gameobject, player closest gameobject]
    public GameObject[] findClosestTanks()
    {
    	double minDistance = 9999999;
    	double tempDis;
    	GameObject[] ret = new GameObject[2];

    	for(int i = 0; i < blueTanks.Length; i++)
    	{
    		for(int j = 0; j < redTanks.Length; j++)
    		{
    			tempDis = Math.Pow((Math.Pow((redTanks[j].transform.position.x - blueTanks[i].transform.position.x),2) + Math.Pow((redTanks[j].transform.position.y - blueTanks[i].transform.position.y),2)),.5);
    			if(tempDis < minDistance)
    			{
    				minDistance = tempDis;
    				ret[0] = blueTanks[i];
    				ret[1] = redTanks[j];
    			}
    		}
    	}
    	return ret;
    }

    public ArrayList findValidMoves(GameObject blue)
    {
    	int gridSize = gs.gridSize;
    	CoordinateSet tankCoordinates = new CoordinateSet((int)blue.transform.position.x, (int)blue.transform.position.z);
    	CoordinateSet tileCoordinates;
    	ArrayList validMoves = new ArrayList();

    	for(int i = 0; i < gridSize; i++)
    	{
    		for(int j = 0; j < gridSize; j++)
    		{
    			 tileCoordinates = new CoordinateSet(i, j);
    			 if(gs.checkValidMove(PlayerColors.Blue, tankCoordinates, tileCoordinates))
    			 {
    			 	validMoves.Add(tileCoordinates);
    			 }
    		}
    	}

    	return validMoves;
    }

    public string test() {
        return "Hi";
    }
}