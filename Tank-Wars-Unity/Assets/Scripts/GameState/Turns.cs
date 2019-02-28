using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    public GameObject gameStatus;
	public ObjectClicker oc;
	public GameState gs;
	public GameObject objectClicked = null;
	public GameObject tankClicked = null;
	public GameObject tankClicked2 = null;
	public GameObject tileClicked = null;
	public int[] tankSet1;
	public int[] tankSet2;
    public GameObject[] redTanks;
    public GameObject[] blueTanks;
	public Rounds round;
	public PlayerColors playerTurn;
    public Levels currentLevel;
	public bool gamblePressed = false;
    public bool aiON;
    public AI ai;
    ClickItems items;

    [SerializeField] HpController hpController;

    void Start()
    {
        // Grab the game status object which holds information from the main menu
        gameStatus = GameObject.Find("GameStatus");
        currentLevel = (Levels)gameStatus.GetComponent<GameStatus>().GetDifficuity();

        // Set the tanks
        tankSet1 = gameStatus.GetComponent<GameStatus>().getPlayer1TankPicks();
        tankSet2 = gameStatus.GetComponent<GameStatus>().getPlayer2TankPicks();

        //Check if ai mode is on
        aiON = false;
        //Gameobjects used by ai
        redTanks = GameObject.FindGameObjectsWithTag("Red Tank");
        blueTanks = GameObject.FindGameObjectsWithTag("Blue Tank");

        // For development purposes, if the main menu isn't used, then use
        // a default level.
        if(currentLevel != 0) {
            gs = new GameState(currentLevel, tankSet1, tankSet2);
        }
        else {
            gs = new GameState(Levels.Level1, new int[] {3,0,0}, new int[] {1,0,0});
        }

        // Set up the turns and start
        oc = GetComponent<ObjectClicker>();
    	round = Rounds.Move;
    	playerTurn = PlayerColors.Red;
        printTurn();

        hpController.BlueTank1Hp(20);           //delete this only here for testing
        hpController.RedTank1Hp(75);            //delete this only here for testing

    }

    // Update is called once per frame
    void Update()
    {
        // Run if ai's turn
        if(playerTurn == PlayerColors.Blue && aiON){
            handleGreedyAi();
        }
        // Skip turn if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            round++;

            if(round > Rounds.Gamble) {
                round = Rounds.Move;

                changeTurns();
            }

            printTurn();
        }

        // If nothing has been clicked, return
        if (oc.objectClicked == null) {
            return;
        }

        // Assign variables
        objectClicked = oc.objectClicked;
        oc.objectClicked = null;
        ClickItems items = getItemClicked();
        RaycastHit hit = items.getRaycastHit();

        if (!items.isValid()) {
            return;
        }

        switch (round) {
            case Rounds.Move:
                handleMove(hit);
                break;
            case Rounds.Attack:
                assignTanksClicked(hit);
                handleAttack();
                break;
            case Rounds.Gamble:
                handleGamble(hit);
                break;
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

    private ClickItems getItemClicked() {
        RaycastHit hit;
        Rigidbody rb;
        ClickItems items;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f)) {
            if (hit.transform != null) {
                if(rb = hit.transform.GetComponent<Rigidbody>()) {
                    items = new ClickItems(hit, rb, true);
                    return items;
                }
            }
        }

        items = new ClickItems(new RaycastHit(), new Rigidbody(), false);
        return items;
     }

    private void handleMove(RaycastHit hit) {
        if ((hit.transform.gameObject.tag == "Red Tank" && playerTurn == PlayerColors.Red) || (hit.transform.gameObject.tag == "Blue Tank" && playerTurn == PlayerColors.Blue)) {
            tankClicked = hit.transform.gameObject;
        }
        else if (tankClicked != null) {
            tileClicked = hit.transform.gameObject;

            CoordinateSet tankCoordinates = new CoordinateSet((int)tankClicked.transform.position.x, (int)tankClicked.transform.position.z);
            CoordinateSet tileCoordinates = new CoordinateSet((int)tileClicked.transform.position.x, (int)tileClicked.transform.position.z);

            if (gs.checkValidMove(playerTurn, tankCoordinates, tileCoordinates, true)) {
                tankClicked.transform.position = new Vector3(tileClicked.transform.position.x, 1, tileClicked.transform.position.z);
                tileClicked = null;
                tankClicked = null;
                round = Rounds.Attack;
                printTurn();
            }
        }
    }

    private void assignTanksClicked(RaycastHit hit) {
        // Assign tanks clicked
        if ((hit.transform.gameObject.tag == "Red Tank")) {
            if (playerTurn == PlayerColors.Red) {
                tankClicked = hit.transform.gameObject;
            }
            else {
                tankClicked2 = hit.transform.gameObject;
            }
        }
        else if (hit.transform.gameObject.tag == "Blue Tank") {
            if (playerTurn == PlayerColors.Blue) {
                tankClicked = hit.transform.gameObject;
            }
            else {
                tankClicked2 = hit.transform.gameObject;
            }
        }
    }

    private void handleAttack() {
        // If both tanks have been clicked, orchestrate the attack
        if (tankClicked != null && tankClicked2 != null) {

            // Create coordinates
            CoordinateSet currentPlayerTankCoordinates = new CoordinateSet((int)tankClicked.transform.position.x, (int)tankClicked.transform.position.z);
            CoordinateSet targetPlayerTankCoordinates = new CoordinateSet((int)tankClicked2.transform.position.x, (int)tankClicked2.transform.position.z);

            // Check if the attack is valid
            if (gs.checkValidAttack(playerTurn, currentPlayerTankCoordinates, targetPlayerTankCoordinates)) {
                //print("Good attack!");
            }
            else {
                //print("Bad attack!");
            }

            // Update player powerup state
            gs.updatePlayerPowerupState(playerTurn);

            // Update the state information
            round = Rounds.Gamble;
            printTurn();
            tankClicked = null;
            tankClicked2 = null;
        }
    }

    private void handleGamble(RaycastHit hit) {
        // Do Gamble
        tankClicked = hit.transform.gameObject;
        CoordinateSet currentTankCoordinates = new CoordinateSet((int)tankClicked.transform.position.x, (int)tankClicked.transform.position.z);
        string powerup = gs.playerGamble(playerTurn, currentTankCoordinates);

        // Print log message
        Debug.Log("Player " + playerTurn + "'s gamble results in: " + powerup);

        // Update the state
        tankClicked = null;
        round = Rounds.Move;
        changeTurns();
        printTurn();
    }

    private void handleGreedyAi() {
        ai = new AI(redTanks, blueTanks, gs);
        CoordinateSet aiLocation;
        CoordinateSet playerLocation;
        CoordinateSet targetLocation;
        GameObject blue;
        GameObject red;

        ai.greedyTurn();
        blue = ai.getAITank();
        red = ai.getPlayerTank();
        aiLocation = new CoordinateSet((int)blue.transform.position.x, (int)blue.transform.position.z);
        targetLocation = ai.getGreedyMove();

        blue.transform.position = new Vector3(targetLocation.getX(), 1, targetLocation.getY());
        gs.checkValidMove(PlayerColors.Blue, aiLocation, targetLocation, true);

        tankClicked = blue;
        tankClicked2 = red;
        handleAttack();
        //gs.checkValidAttack(PlayerColors.Blue, aiLocation, playerLocation);

        changeTurns();
        round = Rounds.Move;
    }
}

public enum Rounds {
    Move = 1,
    Attack = 2,
    Gamble = 3
}

public struct ClickItems {
    RaycastHit hit;
    Rigidbody rb;
    bool valid;

    public ClickItems(RaycastHit hit, Rigidbody rb, bool valid) {
        this.hit = hit;
        this.rb = rb;
        this.valid = valid;
    }

    public RaycastHit getRaycastHit() {
        return this.hit;
    }

    public Rigidbody getRigidbody() {
        return this.rb;
    }

    public bool isValid() {
        return this.valid;
    }
}