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
    GameObject closestTank;
 	GameObject aiTank;

    public MinMaxAI(GameObject[] rt, GameObject[] bt, GameState gamestate)
    {
    	redTanks = rt;
    	blueTanks = bt;
    	gs = gamestate;
    }

    
    public CoordinateSet MinMaxTurn()
    {
 		ArrayList val = new ArrayList();
 		CoordinateSet aiCoordinates;
    	int highestWeight = -99999;
    	int minDis = 99999;
    	int row = validMoves.Count;
    	int column = blueTanks.Length;
    	int[,] min = new int[row,column];
    	int[,] max = new int[row,column];
    	int[,] net = new int[row,column];
    	double[,] zero = new double[row,column];

    	//For each blue tank
    	for(int i = 0; i < column; i++)
    	{
    		//find the valid moves
    		CoordinateSet tempCoordinates = new CoordinateSet((int)blueTanks[i].transform.position.x, (int)blueTanks[i].transform.position.z);
    		validMoves = findValidMoves(tempCoordinates, (PlayerColors)PlayerColors.Blue);

    		//and for each valid move, find the min outcome for the player
    		for(int j = 0; j < row; j++)
    		{
	    		min[i,j] = findMinForPlayers((CoordinateSet)validMoves[j]);
    		}

    		//and for each valid move, find the max outcome for the AI
    		for(int j = 0; j < row; j++)
    		{
	    		max[i,j] = findMaxForAI((CoordinateSet)validMoves[j]);
    		}
    	}

    	//For each blue tank
    	for(int i = 0; i < column; i++)
    	{
    		//For each move calculate the net gamestate change for the AI
    		for(int j = 0; j < row; j++)
    		{
    			net[i,j] = min[i,j] + max[i,j];
    		}
    	}


    	//Create matrix that shows the possible valid moves
    	for(int i = 0; i < column; i++)
    	{
    		for(int j = 0; j < row; j++)
    		{
    			if(net[i,j] > highestWeight)
    			{
    				zero = new double[row,column];
    				zero[i,j] = 1;
    			}
    			if(net[i,j] == highestWeight)
    			{
    				zero[i,j] = 1;
    			}
    		}
    	}

    	//Find Which valid move is closer to the player
    	for(int i = 0; i < column; i++)
    	{
    		for(int j = 0; j < row; j++)
    		{
    			if(zero[i,j] == 1)
    			{
    				aiCoordinates = new CoordinateSet((int)blueTanks[i].transform.position.x, (int)blueTanks[i].transform.position.z);
    				validMoves = findValidMoves(aiCoordinates, (PlayerColors)PlayerColors.Blue);
    				zero[i,j] = findDisToPlayer((CoordinateSet)validMoves[j]);
    			}
    		}
    	}
    	int i_ = 0;
    	int j_ = 0;
    	for(int i = 0; i < column; i++)
    	{
    		for(int j = 0; j < row; j++)
    		{
    			if(zero[i,j] != 0)
    			{
    				if(zero[i,j] < minDis)
    				{
    					i_ = i;
    					j_ = j;
    				}
    			}
    		}
    	}

    	//Now return the best move
    	aiCoordinates = new CoordinateSet((int)blueTanks[i_].transform.position.x, (int)blueTanks[i_].transform.position.z);
    	validMoves = findValidMoves(aiCoordinates, (PlayerColors)PlayerColors.Blue);
    	aiTank = blueTanks[i_];
    	closestTank = findClosestTanks((CoordinateSet) validMoves[j_]);
    	return (CoordinateSet) validMoves[j_];
    }

    public double findDisToPlayer(CoordinateSet aiCoor)
    {
    	double minDistance = 999999999;
    	double tempDis;

    	for(int i = 0; i < redTanks.Length; i++)
		{
			tempDis = Math.Pow((Math.Pow((redTanks[i].transform.position.x - aiCoor.getX()),2) + Math.Pow((redTanks[i].transform.position.z - aiCoor.getY()),2)),.5);
			if(tempDis < minDistance)
			{
				minDistance = tempDis;
			}
		}
		return minDistance;
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

    			if(handleAttack((CoordinateSet)validMovesPlayer[j], valMoves, (PlayerColors)PlayerColors.Red, false))
    			{
    				temp--;
    			}

    			if(gs.getGrid().getGridNode((CoordinateSet)validMovesPlayer[j]).getTerrain() is Water)
    			{
    				temp++;
    			}

    			if(temp < min)
    			{
    				min = temp;
    			}
    		}
       	}
       	return min;
    }

    public int findMaxForAI(CoordinateSet valMoves)
    {
    	int max = -99999999;

    	//For each red tank
    	for(int i = 0; i < redTanks.Length; i++)
    	{
    		int temp = 0;
    		CoordinateSet tempCoordinates = new CoordinateSet((int)redTanks[i].transform.position.x, (int)redTanks[i].transform.position.z);

    		if(handleAttack(tempCoordinates, valMoves, (PlayerColors)PlayerColors.Blue, false))
    		{
    			temp++;
    		}
    		if(gs.getGrid().getGridNode(valMoves).getTerrain() is Water)
    		{
    			temp--;
    		}
    		if(temp > max)
    		{
    			max = temp;
    		}
    	}
       	return max;
    }

    //Gameobject[] holds [AI gameobject, player closest gameobject]
    public GameObject findClosestTanks(CoordinateSet c)
    {
    	double minDistance = 9999999;
    	double tempDis;
    	GameObject ret = null;

		for(int i = 0; i < redTanks.Length; i++)
		{
			tempDis = Math.Pow((Math.Pow((redTanks[i].transform.position.x - c.getX()),2) + Math.Pow((redTanks[i].transform.position.z - c.getY()),2)),.5);
			if(tempDis < minDistance)
			{
				minDistance = tempDis;
				ret = redTanks[i];
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
    			 if(gs.checkValidMove(turn, tankCoordinates, tileCoordinates, false) && !(gs.getGrid().getGridNode(tileCoordinates).getTerrain() is Lava))
    			 {
    			 	validMoves.Add(tileCoordinates);
    			 }
    		}
    	}
    	//Debug.Log(validMoves.Count);
    	return validMoves;
    }

    private bool handleAttack(CoordinateSet tank1, CoordinateSet tank2, PlayerColors turn, bool updateState) {
        //CoordinateSet currentPlayerTankCoordinates = new CoordinateSet((int)tank1.transform.position.x, (int)tank1.transform.position.z);
        //CoordinateSet targetPlayerTankCoordinates = new CoordinateSet((int)tank1.transform.position.x, (int)tank1.transform.position.z);

        if (gs.checkValidAttack(turn, tank1, tank2, updateState)) {
            if(!updateState){
                return true;
            }
        }
        else {
            if(!updateState){
                return false;
            }
        }

        return true;
    }

    public GameObject getAITank()
    {
    	return aiTank;
    }

    public GameObject getPlayerTank()
    {
    	return closestTank;
    }

    public CoordinateSet getMinMaxMove()
    {
    	return minMaxMove;
    }
	
    public string test() {
        return "Hi";
    }
}