﻿using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AI
{
    public GameObject[] redTanks;
	public GameObject[] blueTanks;
	public GameState gs;
	GameObject[] closestTanks;
    ArrayList validMoves = new ArrayList();
    CoordinateSet greedyMove;

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
    	removeDeadTanks();
    	closestTanks = findClosestTanks();
    	validMoves = findValidMoves(closestTanks[0]);
    	greedyMove = findGreedyMove(closestTanks[1], validMoves);
    }

    //Remove dead tanks
    void removeDeadTanks()
    {
    	for(int i = 0; i < redTanks.Length; i++)
    	{
    		CoordinateSet tankCoordinates = new CoordinateSet((int)redTanks[i].transform.position.x, (int)redTanks[i].transform.position.z);
            Tank currentTank = gs.getPlayerTank(PlayerColors.Red, tankCoordinates);

            // If the player didn't choose one of their own tanks, or if that tank is dead, just ignore
            if (currentTank.isDead()) {
                GameObject[] tempTanks = new GameObject[redTanks.Length - 1];
                int count = 0;

                for(int j = 0; j < redTanks.Length; j++)
                {
                	if(j == i)
                	{
                		continue;
                	}
                	tempTanks[count] = redTanks[j];
                	count++;
                }
                redTanks = tempTanks;
            }
    	}

    	for(int i = 0; i < blueTanks.Length; i++)
    	{
    		CoordinateSet tankCoordinates = new CoordinateSet((int)blueTanks[i].transform.position.x, (int)blueTanks[i].transform.position.z);
            Tank currentTank = gs.getPlayerTank(PlayerColors.Blue, tankCoordinates);

            // If the player didn't choose one of their own tanks, or if that tank is dead, just ignore
            if (currentTank.isDead()) {
                GameObject[] tempTanks = new GameObject[blueTanks.Length - 1];
                int count = 0;

                for(int j = 0; j < blueTanks.Length; j++)
                {
                	if(j == i)
                	{
                		continue;
                	}
                	tempTanks[count] = blueTanks[j];
                	count++;
                }
                blueTanks = tempTanks;
            }
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
    			 if(gs.checkValidMove(PlayerColors.Blue, tankCoordinates, tileCoordinates, false) && !(gs.getGrid().getGridNode(tileCoordinates).getTerrain() is Lava))
    			 {
    			 	validMoves.Add(tileCoordinates);
    			 }
    		}
    	}
    	Debug.Log(validMoves.Count);
    	return validMoves;
    }

    
    public CoordinateSet findGreedyMove(GameObject red, ArrayList validMoves)
    {
    	double minDistance = 9999999;
    	double tempDis;
    	CoordinateSet ret = null;
    	CoordinateSet temp;

    	for(int i = 0; i < validMoves.Count; i++)
    	{
    		temp = (CoordinateSet)validMoves[i];
    		tempDis = Math.Pow((Math.Pow((red.transform.position.x - (double)temp.getX()),2) + Math.Pow((red.transform.position.z - (double)temp.getY()),2)),.5);
    		if(tempDis < minDistance)
			{
				minDistance = tempDis;
				ret = temp;
			}
    	}
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

    public CoordinateSet getGreedyMove()
    {
    	return greedyMove;
    }
	
    public string test() {
        return "Hi";
    }
}