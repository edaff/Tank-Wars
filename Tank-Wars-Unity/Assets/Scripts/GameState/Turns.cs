﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
	public ObjectClicker oc;
	public GameState gs;
	public GameObject objectClicked = null;
	public GameObject tankClicked = null;
	public GameObject tankClicked2 = null;
	public GameObject tileClicked = null;
	public int[] tankSet1;
	public int[] tankSet2;
	public int round;
	public int playerTurn;
	public int redPlayerTurn = 1;
	public int bluePlayerTurn = 2;
	public bool gamblePressed = false;
	
    void Start()
    {
    	oc = GetComponent<ObjectClicker>();
    	tankSet1 = new int[] {1,0,0};
    	tankSet2 = new int[] {1,0,0};
    	gs = new GameState(1,tankSet1,tankSet2);
    	round = (int)Rounds.Move;
    	playerTurn = 1;
        printTurn();
    }

    // Update is called once per frame
    void Update()
    {
        // Skip turn if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            round++;

            if(round > (int)Rounds.Gamble) {
                round = (int)Rounds.Move;

                playerTurn = (playerTurn >= 2) ? 1 : 2;
            }

            printTurn();
        }

        if(oc.objectClicked != null)
        {
        	objectClicked = oc.objectClicked;
        	oc.objectClicked = null;

        	if(round == (int)Rounds.Move)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform != null)
                    {
                        Rigidbody rb;
                        if (rb = hit.transform.GetComponent<Rigidbody>())
                        {                        
                            //print(hit.transform.gameObject);
                            if((hit.transform.gameObject.tag == "Red Tank" && playerTurn == redPlayerTurn) || (hit.transform.gameObject.tag == "Blue Tank" && playerTurn == bluePlayerTurn))
                            {
                                tankClicked = hit.transform.gameObject;
                            }
                            else if(tankClicked != null)
                            {
                                tileClicked = hit.transform.gameObject;

                                CoordinateSet tankCoordinates = new CoordinateSet((int)tankClicked.transform.position.x, (int)tankClicked.transform.position.z);
                                CoordinateSet tileCoordinates = new CoordinateSet((int)tileClicked.transform.position.x, (int)tileClicked.transform.position.z);

                                if(gs.checkValidMove(playerTurn, tankCoordinates, tileCoordinates))
                                {
                                    tankClicked.transform.position = new Vector3(tileClicked.transform.position.x, 1, tileClicked.transform.position.z);
                                    tileClicked = null;
                                    tankClicked = null;
                                    round = (int)Rounds.Attack;
                                    printTurn();
                                }
                            }
                        }
                    }
                }
            }
            else if(round == (int)Rounds.Attack)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform != null)
                    {
                        Rigidbody rb;
                        if (rb = hit.transform.GetComponent<Rigidbody>())
                        {
                            if((hit.transform.gameObject.tag == "Red Tank"))
                            {    
                                if(playerTurn == (int)Players.Red) {
                                    tankClicked = hit.transform.gameObject;
                                }
                                else {
                                    tankClicked2 = hit.transform.gameObject;
                                }
                            }
                            else if(hit.transform.gameObject.tag == "Blue Tank")
                            {
                                if (playerTurn == (int)Players.Blue) {
                                    tankClicked = hit.transform.gameObject;
                                }
                                else {
                                    tankClicked2 = hit.transform.gameObject;
                                }
                            }
                            if(tankClicked != null && tankClicked2 != null)
                            {
                                CoordinateSet currentPlayerTankCoordinates = new CoordinateSet((int)tankClicked.transform.position.x, (int)tankClicked.transform.position.z);
                                CoordinateSet targetPlayerTankCoordinates = new CoordinateSet((int)tankClicked2.transform.position.x, (int)tankClicked2.transform.position.z);

                                if (gs.checkValidAttack(playerTurn, currentPlayerTankCoordinates, targetPlayerTankCoordinates))
                                {
                                    print("Good attack!");
                                }
                                else
                                {
                                    print("Bad attack!");
                                }

                                round = (int)Rounds.Gamble;

                                printTurn();
                                tankClicked = null;
                                tankClicked2 = null;
                            }
                        }
                    }
                }
            }
            else if(round == (int)Rounds.Gamble)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f)) {
                    if (hit.transform != null) {
                        Rigidbody rb;
                        if (rb = hit.transform.GetComponent<Rigidbody>()) {
                            tankClicked = hit.transform.gameObject;

                            CoordinateSet currentTankCoordinates = new CoordinateSet((int)tankClicked.transform.position.x, (int)tankClicked.transform.position.z);
                            string powerup = gs.playerGamble(playerTurn, currentTankCoordinates);

                            Debug.Log("Player " + playerTurn + "'s gamble results in: " + powerup);

                            gamblePressed = true;
                        }
                    }
                }

                if (gamblePressed)
                {
                    tankClicked = null;
                    gamblePressed = false;

                    round = (int)Rounds.Move;

                    playerTurn++;

                    if(playerTurn > 2)
                    {
                        playerTurn = 1;
                    }

                    printTurn();
                }
            }
        }

    }

    private void printTurn() {
        Debug.Log("Player " + playerTurn + ": " + (Rounds)round);
    }
}

public enum Rounds {
    Move = 1,
    Attack = 2,
    Gamble = 3
}
