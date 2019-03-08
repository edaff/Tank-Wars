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
    	double minDis = 99999;
    	int row = 20;
    	int column = blueTanks.Length;
    	int[,] min = new int[column,row];
    	int[,] max = new int[column,row];
    	int[,] net = new int[column,row];
    	double[,] zero = new double[column,row];

    	Debug.Log("Row: " + row);
    	Debug.Log("Column: " + column);
    	//For each blue tank
    	for(int i = 0; i < column; i++)
    	{
    		//find the valid moves
    		CoordinateSet tempCoordinates = new CoordinateSet((int)blueTanks[i].transform.position.x, (int)blueTanks[i].transform.position.z);
    		validMoves = findValidMoves(tempCoordinates, (PlayerColors)PlayerColors.Blue);
    		Debug.Log("validMoves.Count: " + validMoves.Count);
    		//and for each valid move, find the min outcome for the player
    		for(int j = 0; j < validMoves.Count; j++)
    		{
	    		min[i,j] = findMinForPlayers((CoordinateSet)validMoves[j]);
    		}

    		//and for each valid move, find the max outcome for the AI
    		for(int j = 0; j < validMoves.Count; j++)
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
    				zero = new double[column,row];
    				highestWeight = net[i,j];
    				zero[i,j] = 1;
    			}
    			else if(net[i,j] == highestWeight)
    			{
    				zero[i,j] = 1;
    			}
    		}
    	}

    	/*
		DEBUG STUFF---------------------------------------------------------------------------
    	*/
    	string tempS;

    	for(int i = 0; i < column; i++)
    	{
    		tempS = "";
    		for(int j = 0; j < row; j++)
    		{
    			tempS = string.Concat(tempS, min[i,j]);
    		}
    		Debug.Log(tempS);
    	}

    	for(int i = 0; i < column; i++)
    	{
    		tempS = "";
    		for(int j = 0; j < row; j++)
    		{
    			tempS = string.Concat(tempS, max[i,j]);
    		}
    		Debug.Log(tempS);
    	}

    	for(int i = 0; i < column; i++)
    	{
    		tempS = "";
    		for(int j = 0; j < row; j++)
    		{
    			tempS = string.Concat(tempS, net[i,j]);
    		}
    		Debug.Log(tempS);
    	}

    	for(int i = 0; i < column; i++)
    	{
    		tempS = "";
    		for(int j = 0; j < row; j++)
    		{
    			tempS = string.Concat(tempS, zero[i,j]);
    		}
    		Debug.Log(tempS);
    	}
    	/*
		DEBUG STUFF---------------------------------------------------------------------------
    	*/

    	//Find Which valid move is closer to the player
    	for(int i = 0; i < column; i++)
    	{
    		for(int j = 0; j < row; j++)
    		{
    			if(zero[i,j] == 1)
    			{
    				aiCoordinates = new CoordinateSet((int)blueTanks[i].transform.position.x, (int)blueTanks[i].transform.position.z);
    				validMoves = findValidMoves(aiCoordinates, (PlayerColors)PlayerColors.Blue);
    				//Debug.Log("# of val moves: " + validMoves.Count);
    				zero[i,j] = findDisToPlayer((CoordinateSet)validMoves[j]);
    				//Debug.Log(findDisToPlayer((CoordinateSet)validMoves[j]));
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
    					minDis = zero[i,j];
    				}
    			}
    		}
    	}

    	/*
		DEBUG STUFF---------------------------------------------------------------------------
    	*/

		for(int i = 0; i < column; i++)
    	{
    		tempS = "";
    		for(int j = 0; j < row; j++)
    		{
    			tempS = string.Concat(tempS, zero[i,j]);
    		}
    		Debug.Log(tempS);
    	}

    	Debug.Log("i_: " + i_ + "\nj_: " + j_);
    	/*
		DEBUG STUFF---------------------------------------------------------------------------
    	*/

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
    				min = temp + 10;
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
    			max = temp + 10;
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
    	return true;
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