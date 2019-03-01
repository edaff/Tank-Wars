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
    	for(int i = 0; i < blueTanks.Length; i++)
    	{
    		validMoves = findValidMoves(blueTanks[i], PlayerColors.Blue);
    	}
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

    public ArrayList findValidMoves(GameObject tank, PlayerColors turn)
    {
    	int gridSize = gs.gridSize;
    	CoordinateSet tankCoordinates = new CoordinateSet((int)tank.transform.position.x, (int)tank.transform.position.z);
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