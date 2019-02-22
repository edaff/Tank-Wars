using System.Collections;
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
	public Rounds round;
	public PlayerColors playerTurn;
	public bool gamblePressed = false;
	
    void Start()
    {
    	oc = GetComponent<ObjectClicker>();
    	tankSet1 = new int[] {1,0,0};
    	tankSet2 = new int[] {1,0,0};
    	gs = new GameState(1,tankSet1,tankSet2);
    	round = Rounds.Move;
    	playerTurn = PlayerColors.Red;
        printTurn();
    }

    // Update is called once per frame
    void Update()
    {
        // Skip turn if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            round++;

            if(round > Rounds.Gamble) {
                round = Rounds.Move;

                changeTurns();
            }

            printTurn();
        }

        if(oc.objectClicked != null)
        {
        	objectClicked = oc.objectClicked;
        	oc.objectClicked = null;

        	if(round == Rounds.Move)
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
                            if((hit.transform.gameObject.tag == "Red Tank" && playerTurn == PlayerColors.Red) || (hit.transform.gameObject.tag == "Blue Tank" && playerTurn == PlayerColors.Blue))
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
                                    round = Rounds.Attack;
                                    printTurn();
                                }
                            }
                        }
                    }
                }
            }
            else if(round == Rounds.Attack)
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
                                if(playerTurn == PlayerColors.Red) {
                                    tankClicked = hit.transform.gameObject;
                                }
                                else {
                                    tankClicked2 = hit.transform.gameObject;
                                }
                            }
                            else if(hit.transform.gameObject.tag == "Blue Tank")
                            {
                                if (playerTurn == PlayerColors.Blue) {
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

                                round = Rounds.Gamble;

                                printTurn();
                                tankClicked = null;
                                tankClicked2 = null;
                            }
                        }
                    }
                }
            }
            else if(round == Rounds.Gamble)
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

                    round = Rounds.Move;

                    changeTurns();
                    printTurn();
                }
            }
        }

    }

    private void printTurn() {
        Debug.Log("Player " + playerTurn + ": " + (Rounds)round);
    }

    private void changeTurns() {
        if(playerTurn == PlayerColors.Red) {
            playerTurn = PlayerColors.Blue;
        }
        else {
            playerTurn = PlayerColors.Red;
        }
    }
}

public enum Rounds {
    Move = 1,
    Attack = 2,
    Gamble = 3
}
