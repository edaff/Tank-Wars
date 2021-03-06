﻿using System;
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
    Tank[] redTunks;
    Tank[] blueTunks;
    bool move = true;

    public MinMaxAI(GameObject[] rt, GameObject[] bt, GameState gamestate, int[] player1Tanks, int[] player2Tanks)
    {
        redTanks = rt;
        blueTanks = bt;
        gs = gamestate;
        Player player = new Player(PlayerColors.Red, player1Tanks, gs.level);
        redTunks = player.createTankArray(player1Tanks, PlayerColors.Red, gs.level);
        player = new Player(PlayerColors.Blue, player2Tanks, gs.level);
        blueTunks = player.createTankArray(player2Tanks, PlayerColors.Red, gs.level);
    }

    //Remove dead tanks
    void removeDeadTanks()
    {
        for (int i = 0; i < redTanks.Length; i++)
        {
            CoordinateSet tankCoordinates = new CoordinateSet((int)redTanks[i].transform.position.x, (int)redTanks[i].transform.position.z);
            Tank currentTank = gs.getPlayerTank(PlayerColors.Red, tankCoordinates);

            // If the player didn't choose one of their own tanks, or if that tank is dead, just ignore
            if (currentTank.isDead())
            {
                GameObject[] tempTanks = new GameObject[redTanks.Length - 1];
                Tank[] tempTunks = new Tank[redTunks.Length - 1];
                int count = 0;

                for (int j = 0; j < redTanks.Length; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }
                    tempTanks[count] = redTanks[j];
                    tempTunks[count] = redTunks[j];
                    count++;
                }
                redTanks = tempTanks;
            }
        }

        for (int i = 0; i < blueTanks.Length; i++)
        {
            CoordinateSet tankCoordinates = new CoordinateSet((int)blueTanks[i].transform.position.x, (int)blueTanks[i].transform.position.z);
            Tank currentTank = gs.getPlayerTank(PlayerColors.Blue, tankCoordinates);

            // If the player didn't choose one of their own tanks, or if that tank is dead, just ignore
            if (currentTank.isDead())
            {
                GameObject[] tempTanks = new GameObject[blueTanks.Length - 1];
                Tank[] tempTunks = new Tank[blueTunks.Length - 1];
                int count = 0;

                for (int j = 0; j < blueTanks.Length; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }
                    tempTanks[count] = blueTanks[j];
                    tempTunks[count] = blueTunks[j];
                    count++;
                }
                blueTanks = tempTanks;
            }
        }
    }

    public CoordinateSet MinMaxTurn()
    {
        //This while loop is used to allow the continue code to retry the MinMaxTurn
        CoordinateSet lastAiCoordinates = null;
        while (true) {
            removeDeadTanks();
            ArrayList val = new ArrayList();
            CoordinateSet aiCoordinates;
            int highestWeight = -99999;
            double minDis = 99999;
            int row = 100;
            int column = blueTanks.Length;
            int[,] min = new int[column, row];
            int[,] max = new int[column, row];
            int[,] net = new int[column, row];
            double[,] zero = new double[column, row];

            Debug.Log("Row: " + row);
            Debug.Log("Column: " + column);
            //For each blue tank
            for (int i = 0; i < column; i++)
            {
                //find the valid moves
                CoordinateSet tempCoordinates = new CoordinateSet((int)blueTanks[i].transform.position.x, (int)blueTanks[i].transform.position.z);
                validMoves = findValidMoves(tempCoordinates, (PlayerColors)PlayerColors.Blue);
                Debug.Log("validMoves.Count: " + validMoves.Count);
                //and for each valid move, find the min outcome for the player
                for (int j = 0; j < validMoves.Count; j++)
                {
                    min[i, j] = findMinForPlayers((CoordinateSet)validMoves[j]);
                }

                //and for each valid move, find the max outcome for the AI
                for (int j = 0; j < validMoves.Count; j++)
                {
                    max[i, j] = findMaxForAI((CoordinateSet)validMoves[j]);
                }
            }

            //For each blue tank
            for (int i = 0; i < column; i++)
            {
                //For each move calculate the net gamestate change for the AI
                for (int j = 0; j < row; j++)
                {
                    net[i, j] = min[i, j] + max[i, j];
                }
            }

            //Create matrix that shows the possible valid moves
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (net[i, j] > highestWeight)
                    {
                        zero = new double[column, row];
                        highestWeight = net[i, j];
                        zero[i, j] = 1;
                    }
                    else if (net[i, j] == highestWeight)
                    {
                        zero[i, j] = 1;
                    }
                }
            }

            //Find Which valid move is closer to the player
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (zero[i, j] == 1)
                    {
                        aiCoordinates = new CoordinateSet((int)blueTanks[i].transform.position.x, (int)blueTanks[i].transform.position.z);
                        validMoves = findValidMoves(aiCoordinates, (PlayerColors)PlayerColors.Blue);
                        //Debug.Log("# of val moves: " + validMoves.Count);

                        // THIS HAS A BUG. GOES OUT OF RANGE OF THE INDEX.
                        try {
                            zero[i, j] = findDisToPlayer((CoordinateSet)validMoves[j]);
                        }
                        catch (Exception ex) {
                            continue;
                        }
                        //Debug.Log(findDisToPlayer((CoordinateSet)validMoves[j]));
                    }
                }
            }
            int i_ = 0;
            int j_ = 0;
            int j__ = 0;
            double maxDis = 0;

            //Gives you i and j for min dis
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (zero[i, j] != 0)
                    {
                        if (zero[i, j] < minDis)
                        {
                            i_ = i;
                            j_ = j;
                            minDis = zero[i, j];
                        }
                    }
                }
            }

            if (blueTanks.Length == 0)
            {
                move = false;
                return lastAiCoordinates;
            }

            bool snipCanAttack = false;
            CoordinateSet aiSave = null;
            aiCoordinates = new CoordinateSet((int)blueTanks[i_].transform.position.x, (int)blueTanks[i_].transform.position.z);
            validMoves = findValidMoves(aiCoordinates, (PlayerColors)PlayerColors.Blue);

            for (int j = 0; j < validMoves.Count + 1; j++)
            {
                if (zero[i_, j] != 0) {
                    aiCoordinates = new CoordinateSet((int)blueTanks[i_].transform.position.x, (int)blueTanks[i_].transform.position.z);
                    aiSave = aiCoordinates;
                    if (gs.getPlayerTank((PlayerColors)PlayerColors.Blue, aiCoordinates) is SniperTank)
                    {
                        Debug.Log("HELLO1");
                        validMoves = findValidMoves(aiCoordinates, (PlayerColors)PlayerColors.Blue);
                        aiTank = blueTanks[i_];
                        //blueTunks[i_].getWeapon();


                        // THIS HAS A BUG. DOESN'T FIND CLOSEST TANK SOMETIMES
                        bool canAt = false;
                        try {
                            closestTank = findClosestTanks((CoordinateSet)validMoves[j]);
                            CoordinateSet closestCoordinate = new CoordinateSet((int)closestTank.transform.position.x, (int)closestTank.transform.position.z);
                            //checkValidMove(PlayerColors playerTurn, CoordinateSet tankCoordinates, CoordinateSet targetCoordinates, bool updateState)
                            Debug.Log(gs.checkValidMove((PlayerColors)PlayerColors.Blue, aiCoordinates, (CoordinateSet)validMoves[j], true));
                            canAt = handleAttack(i_, (CoordinateSet)validMoves[j], closestCoordinate, (PlayerColors)PlayerColors.Blue, false);
                            Debug.Log(gs.checkValidMove((PlayerColors)PlayerColors.Blue, (CoordinateSet)validMoves[j], aiCoordinates, true));
                        }
                        catch (Exception ex) {
                            Debug.Log("Caught exception finding closest tank");
                        }

                        if (canAt)
                        {
                            Debug.Log("HELLO2");
                            if (zero[i_, j] > maxDis)
                            {
                                j__ = j;
                                maxDis = zero[i_, j];
                                snipCanAttack = true;
                            }
                        }
                    }
                }
            }

            bool isSnip = false;


            aiCoordinates = new CoordinateSet((int)blueTanks[i_].transform.position.x, (int)blueTanks[i_].transform.position.z);
            aiTank = blueTanks[i_];
            closestTank = findClosestTanks((CoordinateSet)validMoves[j_]);
            lastAiCoordinates = aiCoordinates;
            if (gs.getPlayerTank((PlayerColors)PlayerColors.Blue, aiCoordinates) is SniperTank)
            {
                j_ = j__;
                isSnip = true;
            }
            if (!snipCanAttack && isSnip)
            {
                deleteStagTank(i_);
                continue;
            }
            else if (isSnip)
            {
                Debug.Log("I'm fed up");
                return (CoordinateSet)validMoves[j_];
            }

            //Now return the best move
            aiCoordinates = new CoordinateSet((int)blueTanks[i_].transform.position.x, (int)blueTanks[i_].transform.position.z);
            validMoves = findValidMoves(aiCoordinates, (PlayerColors)PlayerColors.Blue);
            aiTank = blueTanks[i_];
            //blueTunks[i_].getWeapon();
            closestTank = findClosestTanks((CoordinateSet)validMoves[j_]);
            CoordinateSet closestCoordinates = new CoordinateSet((int)closestTank.transform.position.x, (int) closestTank.transform.position.z);
            bool canAtt = handleAttack(i_, aiCoordinates, closestCoordinates, (PlayerColors)PlayerColors.Blue, false);
            //bool canAtt = true;
            if (j_ == 0 && blueTanks.Length == 1)
            {
                move = false;
            }
            else if (j_ == 0 && blueTanks.Length > 1 && canAtt == false)
            {
                deleteStagTank(i_);
                continue;
                Debug.Log("I'm fed up");
            }
            return (CoordinateSet)validMoves[j_];
        }
    }

    void deleteStagTank(int i)
    {
        GameObject[] tempTanks = new GameObject[blueTanks.Length - 1];
        Tank[] tempTunks = new Tank[blueTunks.Length - 1];
        int count = 0;

        for (int j = 0; j < blueTanks.Length; j++)
        {
            if (j == i)
            {
                continue;
            }
            tempTanks[count] = blueTanks[j];
            tempTunks[count] = blueTunks[j];
            count++;
        }
        blueTanks = tempTanks;
    }

    public double findDisToPlayer(CoordinateSet aiCoor)
    {
        double minDistance = 999999999;
        double tempDis;

        for (int i = 0; i < redTanks.Length; i++)
        {
            tempDis = Math.Pow((Math.Pow((redTanks[i].transform.position.x - aiCoor.getX()), 2) + Math.Pow((redTanks[i].transform.position.z - aiCoor.getY()), 2)), .5);
            if (tempDis < minDistance)
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
        for (int i = 0; i < redTanks.Length; i++)
        {
            //Find the valid moves
            CoordinateSet tempCoordinates = new CoordinateSet((int)redTanks[i].transform.position.x, (int)redTanks[i].transform.position.z);
            validMovesPlayer = findValidMoves(tempCoordinates, PlayerColors.Red);

            //and for each valid move calculate the weight and check if it's min
            for (int j = 0; j < validMovesPlayer.Count; j++)
            {
                int temp = 0;

                if (handleAttack(i, (CoordinateSet)validMovesPlayer[j], valMoves, (PlayerColors)PlayerColors.Red, false))
                {
                    temp--;
                }

                if (gs.getGrid().getGridNode((CoordinateSet)validMovesPlayer[j]).getTerrain() is Water)
                {
                    temp++;
                }

                if (temp < min)
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
        for (int i = 0; i < redTanks.Length; i++)
        {
            int temp = 0;
            CoordinateSet tempCoordinates = new CoordinateSet((int)redTanks[i].transform.position.x, (int)redTanks[i].transform.position.z);

            if (handleAttack(i, tempCoordinates, valMoves, (PlayerColors)PlayerColors.Blue, false))
            {
                temp += 3;
            }
            if((handleAttack(i, valMoves, tempCoordinates, (PlayerColors)PlayerColors.Red, false))){
                temp -= 1;
            }
            if (gs.getGrid().getGridNode(valMoves).getTerrain() is Water)
            {
                temp--;
            }
            if (temp > max)
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

        for (int i = 0; i < redTanks.Length; i++)
        {
            tempDis = Math.Pow((Math.Pow((redTanks[i].transform.position.x - c.getX()), 2) + Math.Pow((redTanks[i].transform.position.z - c.getY()), 2)), .5);
            if (tempDis < minDistance)
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

        validMoves.Add(tankPos);

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                tileCoordinates = new CoordinateSet(i, j);
                if (gs.checkValidMove(turn, tankCoordinates, tileCoordinates, false) && !(gs.getGrid().getGridNode(tileCoordinates).getTerrain() is Lava))
                {
                    validMoves.Add(tileCoordinates);
                }
            }
        }
        //Debug.Log(validMoves.Count);
        return validMoves;
    }

    private bool handleAttack(int index, CoordinateSet attackingTankCoordinates, CoordinateSet targetTankCoordinates, PlayerColors turn, bool updateState)
    {
        //CoordinateSet currentPlayerTankCoordinates = new CoordinateSet((int)tank1.transform.position.x, (int)tank1.transform.position.z);
        //CoordinateSet targetPlayerTankCoordinates = new CoordinateSet((int)tank1.transform.position.x, (int)tank1.transform.position.z);
        if (turn == (PlayerColors)PlayerColors.Red)
        {
            redTunks[index].coordinates = attackingTankCoordinates;
            return redTunks[index].getWeapon().isValidAttack(gs.getGrid(), targetTankCoordinates, updateState);
        }

        if (turn == (PlayerColors)PlayerColors.Blue)
        {
            blueTunks[index].coordinates = attackingTankCoordinates;
            return blueTunks[index].getWeapon().isValidAttack(gs.getGrid(), targetTankCoordinates, updateState);
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

    public string test()
    {
        return "Hi";
    }
}