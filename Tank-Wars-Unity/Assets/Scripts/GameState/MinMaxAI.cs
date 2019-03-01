using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MinMaxAI
{
    public GameObject[] redTanks;
	public GameObject[] blueTanks;
	public GameState gs;
	GameObject[] closestTanks;
    ArrayList validMoves = new ArrayList();
    CoordinateSet minMaxMove;

    public MinMaxAI(GameObject[] rt, GameObject[] bt, GameState gamestate)
    {
    	redTanks = rt;
    	blueTanks = bt;
    	gs = gamestate;
    }

    
    public void MinMaxTurn()
    {
    	//closestTanks = findClosestTanks();
    	//validMoves = findValidMoves(closestTanks[0]);
    	//greedyMove = findGreedyMove(closestTanks[1], validMoves);
    	int[] max = new int[validMoves.Count + 1];

    	//For each blue tank
    	for(int i = 0; i < blueTanks.Length; i++)
    	{
    		//find the valid moves
    		CoordinateSet tempCoordinates = new CoordinateSet((int)blueTanks[i].transform.position.x, (int)blueTanks[i].transform.position.z);
    		validMoves = findValidMoves(tempCoordinates, (PlayerColors)PlayerColors.Blue);

    		//and for each valid move, find the min outcome for the player
    		for(int j = 0; j < validMoves.Count; j++)
    		{
	    		max[i] = findMinForPlayers((CoordinateSet)validMoves[j]);
    		}
    	}
    }

    public int findMinForPlayers(CoordinateSet valMoves)
    {
    	int min = 99999999;
    	ArrayList validMovesPlayer = new ArrayList();

    	//For each red tank
    	for(int i = 0; i < redTanks.Length; i++)
    	{
    		//Find the valid moves
    		CoordinateSet tempCoordinates = new CoordinateSet((int)redTanks[i].transform.position.x, (int)redTanks[i].transform.position.z);
    		validMovesPlayer = findValidMoves(tempCoordinates, PlayerColors.Red);

    		//and for each valid move calculate the weight and check if it's min
    		for(int j = 0; j < validMovesPlayer.Count; j++)
    		{
    			int temp = 0;
    		}
       	}
       	return 0;
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
    			tempDis = Math.Pow((Math.Pow((redTanks[j].transform.position.x - blueTanks[i].transform.position.x),2) + Math.Pow((redTanks[j].transform.position.z - blueTanks[i].transform.position.z),2)),.5);
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

    public ArrayList findValidMoves(CoordinateSet tankPos, PlayerColors turn)
    {
    	int gridSize = gs.gridSize;
    	CoordinateSet tankCoordinates = tankPos;
    	CoordinateSet tileCoordinates;
    	ArrayList validMoves = new ArrayList();

    	for(int i = 0; i < gridSize; i++)
    	{
    		for(int j = 0; j < gridSize; j++)
    		{
    			 tileCoordinates = new CoordinateSet(i, j);
    			 if(gs.checkValidMove(turn, tankCoordinates, tileCoordinates, false))
    			 {
    			 	validMoves.Add(tileCoordinates);
    			 }
    		}
    	}
    	Debug.Log(validMoves.Count);
    	return validMoves;
    }

    
    public CoordinateSet findMinMaxMove(GameObject red, ArrayList validMoves)
    {
    	CoordinateSet ret = null;
    	return ret;
    }

    public GameObject getAITank()
    {
    	return closestTanks[0];
    }

    public GameObject getPlayerTank()
    {
    	return closestTanks[1];
    }

    public CoordinateSet getMinMaxMove()
    {
    	return minMaxMove;
    }
	
    public string test() {
        return "Hi";
    }
}