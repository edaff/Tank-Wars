using System.Collections;
using System.Collections.Generic;
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

    public string test() {
        return "Hi";
    }
}