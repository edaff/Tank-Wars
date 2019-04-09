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
    public MinMaxAI mmai;
    ClickItems items;
    public CameraAngles camera;
    public GameObject mainCamera;
    public float tankMoveSpeed = 3f;
    GameObject tooltip;
    List<GameObject> tooltips;
    int roundCounter = 0;
    int numGambles = 0;
    private bool aiMove = false;

    [SerializeField] HpController hpController;

    void Start()
    {
        // Grab the game status object which holds information from the main menu
        gameStatus = GameObject.Find("GameStatus");
        currentLevel = (Levels)gameStatus.GetComponent<GameStatus>().GetDifficuity();

        // Set the tanks
        tankSet1 = gameStatus.GetComponent<GameStatus>().getPlayer1TankPicks();
        tankSet2 = gameStatus.GetComponent<GameStatus>().getPlayer2TankPicks();

        // Check if the AI is on or off, based on what the user chose in the main menu
        aiON = (bool)gameStatus.GetComponent<GameStatus>().GetAiON();

        // Gameobjects used by ai
        redTanks = GameObject.FindGameObjectsWithTag("Red Tank");
        blueTanks = GameObject.FindGameObjectsWithTag("Blue Tank");

        // Set a default level if this isn't initialized so that the game can still be played
        if (currentLevel != 0)
        {
            gs = new GameState(currentLevel, tankSet1, tankSet2);
        }
        else
        {
            gs = new GameState(Levels.Level1, new int[] { 1, 0, 0 }, new int[] { 1, 0, 0 });
        }

        // Set up the turns and start
        oc = GetComponent<ObjectClicker>();
        round = Rounds.Move;
        playerTurn = PlayerColors.Red;
        printTurn();

        // Get main camera object for CameraAngles functions
        mainCamera = GameObject.Find("Main Camera");
        camera = mainCamera.GetComponent<CameraAngles>();
        camera.translateToPlayer(PlayerColors.Red);
        //gs.toggleTankRings(redTanks, new CoordinateSet(-1, -1), true);

        tooltip = GameObject.FindGameObjectWithTag("ToolTips");
        tooltips = new List<GameObject>();
        foreach (Transform obj in tooltip.transform)
        {
            tooltips.Add(obj.gameObject);
        }

        updateTooltips("Move1");
    }

    // Update is called once per frame
    void Update()
    {
        if(aiON && aiMove) {
            return;
        }

        if(numGambles == 4) {
            updateTooltips("GLHF");
        }

        // Check if gameover
        gameOver();

        // Run if ai's turn
        if(playerTurn == PlayerColors.Blue && aiON){
            gs.toggleTankRings(redTanks, new CoordinateSet(-1, -1), false);
            gs.toggleTankRings(blueTanks, new CoordinateSet(-1, -1), false);
            //handleGreedyAi();
            if (aiMove == false)
            {
                aiMove = true;

                if(currentLevel == Levels.Level1) {
                    StartCoroutine(handleGreedyAi());
                }
                else {
                    StartCoroutine(handleMinMaxAi());
                }
            }
        }

        // Skip turn if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !aiMove) {
            round++;
            TileHighlighter.resetTiles();

            if (round == Rounds.Gamble) {
                gs.toggleTankRings(playerTurn == PlayerColors.Red ? redTanks : blueTanks, new CoordinateSet(-1, -1), true);
                gs.toggleTankRings(playerTurn == PlayerColors.Red ? blueTanks : redTanks, new CoordinateSet(-1, -1), false);
                updateTooltips("Gamble");
                gs.updatePlayerPowerupState(playerTurn);
            }

            if(round == Rounds.Attack) {
                gs.toggleTankRings(redTanks, new CoordinateSet(-1, -1), false);
                gs.toggleTankRings(blueTanks, new CoordinateSet(-1, -1), false);
                updateTooltips("Attack1");
            }

            if(round > Rounds.Gamble) {
                round = Rounds.Move;
                updateTooltips("Move1");
                numGambles++;

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
                handleAttack(true);
                break;
            case Rounds.Gamble:
                handleGamble(hit);
                break;
        }

        gs.updatePlayerHealthBars(hpController);
    }

    private void printTurn() {
        Debug.Log("Player " + playerTurn + ": " + (Rounds)round);
    }

    private void changeTurns() {
        if (playerTurn == PlayerColors.Red) {
            playerTurn = PlayerColors.Blue;
            if (aiON == false)
            {
                camera.translateToPlayer(PlayerColors.Blue);
            }
        }
        else {
            playerTurn = PlayerColors.Red;
            if (aiON == false)
            {
                camera.translateToPlayer(PlayerColors.Red);
            }
        }
        TileHighlighter.resetTiles();
        gs.toggleTankRings(redTanks, new CoordinateSet(-1, -1), false);
        gs.toggleTankRings(blueTanks, new CoordinateSet(-1, -1), false);
        roundCounter++;
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
            CoordinateSet tankCoordinates = new CoordinateSet((int)hit.transform.position.x, (int)hit.transform.position.z);
            Tank currentTank = gs.getPlayerTank(playerTurn, tankCoordinates);

            // If the player didn't choose one of their own tanks, or if that tank is dead, just ignore
            if (currentTank.isDead() || currentTank.getPlayer().getPlayerColor() != playerTurn) {
                tankClicked = null;
                return;
            }

            TileHighlighter.resetTiles();
            TileHighlighter.highlightValidTiles(currentTank.getValidMovements(gs.getGrid(), tankCoordinates), round);
            gs.toggleTankRings(playerTurn == PlayerColors.Red ? redTanks : blueTanks, tankCoordinates, true);
            gs.toggleTankRings(playerTurn == PlayerColors.Red ? blueTanks : redTanks, new CoordinateSet(-1, -1), false);
            updateTooltips("Move2");
        }
        else if (tankClicked != null) {
            tileClicked = hit.transform.gameObject;

            CoordinateSet tankCoordinates = new CoordinateSet((int)tankClicked.transform.position.x, (int)tankClicked.transform.position.z);
            CoordinateSet tileCoordinates = new CoordinateSet((int)tileClicked.transform.position.x, (int)tileClicked.transform.position.z);

            if (gs.checkValidMove(playerTurn, tankCoordinates, tileCoordinates, true)) {
                TileHighlighter.resetTiles();
                Tank currentTank = gs.getPlayerTank(playerTurn, tileCoordinates);

                // Tank moving animation
                StartCoroutine(moveTank(tileClicked, tankClicked, currentTank));

                tileClicked = null;
                tankClicked = null;
                round = Rounds.Attack;
                TileHighlighter.resetTiles();
                printTurn();
                updateTooltips("Attack1");
                gs.toggleTankRings(redTanks, new CoordinateSet(-1, -1), false);
                gs.toggleTankRings(blueTanks, new CoordinateSet(-1, -1), false);
            }
        }
    }

    IEnumerator moveTank(GameObject tileClicked, GameObject tankClicked, Tank currentTank)
    {
        Vector3 startPos = tankClicked.transform.position;
        Vector3 endPos = new Vector3(0, 0, 0);
        Quaternion startRot = tankClicked.transform.rotation;
        Quaternion endRot = Quaternion.Euler(0f, 0f, 0f);
        if (currentTank is CannonTank)
        {
            endPos = new Vector3(tileClicked.transform.position.x, 1f, tileClicked.transform.position.z);
        }
        else
        {
            endPos = new Vector3(tileClicked.transform.position.x, 0.8f, tileClicked.transform.position.z);
        }

        if (endPos.x > startPos.x)
        {
            endRot = Quaternion.Euler(0f, 90f, 0f);
        }
        else if (endPos.x < startPos.x)
        {
            endRot = Quaternion.Euler(0f, 270f, 0f);
        }
        else if (endPos.z > startPos.z)
        {
            endRot = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (endPos.z < startPos.z)
        {
            endRot = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            endRot = tankClicked.transform.rotation;
        }
        float rotStep = tankMoveSpeed * Time.fixedDeltaTime;
        float rotT = 0;
        while(rotT <= 1.0f)
        {
              rotT += rotStep;
              tankClicked.transform.rotation = Quaternion.Lerp(startRot, endRot, rotT);
              yield return new WaitForFixedUpdate();
        }
        tankClicked.transform.rotation = endRot;

        float step = (tankMoveSpeed / (startPos - endPos).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            tankClicked.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return new WaitForFixedUpdate();
        }
        tankClicked.transform.position = endPos;

        rotT = 0;
        if(currentTank is SniperTank)
        {
            while(rotT <= 1.0f)
            {
                rotT += rotStep;
                tankClicked.transform.rotation = Quaternion.Lerp(endRot, startRot, rotT);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    private void assignTanksClicked(RaycastHit hit) {
        // Assign tanks clicked
        Tank currentTank;
        if (hit.transform.gameObject.tag == "Red Tank") {
            currentTank = gs.getPlayerTank(PlayerColors.Red, new CoordinateSet((int)hit.transform.position.x, (int)hit.transform.position.z));

            if (playerTurn == PlayerColors.Red && !currentTank.isDead()) {
                tankClicked = hit.transform.gameObject;
                TileHighlighter.resetTiles();
                highlightAttackTiles(new CoordinateSet((int)hit.transform.position.x, (int)hit.transform.position.z));
                
                if (gs.playerHasValidAttack(playerTurn, new CoordinateSet((int)hit.transform.position.x, (int)hit.transform.position.z))) {
                    updateTooltips("Attack2");
                }
                else {
                    updateTooltips("Attack3");
                }
            }
            else if (!currentTank.isDead()) {
                tankClicked2 = hit.transform.gameObject;
                TileHighlighter.resetTiles();
            }
        }
        else if ((hit.transform.gameObject.tag == "Blue Tank")) {
            currentTank = gs.getPlayerTank(PlayerColors.Blue, new CoordinateSet((int)hit.transform.position.x, (int)hit.transform.position.z));

            if (playerTurn == PlayerColors.Blue && !currentTank.isDead()) {
                tankClicked = hit.transform.gameObject;
                TileHighlighter.resetTiles();
                highlightAttackTiles(new CoordinateSet((int)hit.transform.position.x, (int)hit.transform.position.z));
                if (gs.playerHasValidAttack(playerTurn, new CoordinateSet((int)hit.transform.position.x, (int)hit.transform.position.z))) {
                    updateTooltips("Attack2");
                }
                else {
                    updateTooltips("Attack3");
                }
            }
            else if (!currentTank.isDead()) {
                tankClicked2 = hit.transform.gameObject;
                TileHighlighter.resetTiles();
            }
        }
    }

    private void highlightAttackTiles(CoordinateSet currentTankCoordinates) {
        gs.toggleTankRings(playerTurn == PlayerColors.Red ? redTanks : blueTanks, currentTankCoordinates, true);
        gs.toggleTankRings(playerTurn == PlayerColors.Red ? blueTanks : redTanks, new CoordinateSet(-1, -1), true);


        Tank currentTank = gs.getPlayerTank(playerTurn, currentTankCoordinates);
        TileHighlighter.highlightValidTiles(currentTank.getWeapon().getValidAttacks(gs.getGrid()), round);
    }

    private bool handleAttack(bool updateState) {
        // If both tanks have been clicked, orchestrate the attack
        if (tankClicked != null && tankClicked2 != null) {
            // Create coordinates
            CoordinateSet currentPlayerTankCoordinates = new CoordinateSet((int)tankClicked.transform.position.x, (int)tankClicked.transform.position.z);
            CoordinateSet targetPlayerTankCoordinates = new CoordinateSet((int)tankClicked2.transform.position.x, (int)tankClicked2.transform.position.z);

            // Check if the attack is valid
            if (gs.checkValidAttack(playerTurn, currentPlayerTankCoordinates, targetPlayerTankCoordinates, updateState)) {
                if(!updateState){
                    return true;
                }

                // Do knockback
                handleKnockback(currentPlayerTankCoordinates, targetPlayerTankCoordinates);
            }
            else {
                if(!updateState){
                    return false;
                }
            }

            // Update player powerup state
            gs.updatePlayerPowerupState(playerTurn);

            // Update the state information
            round = Rounds.Gamble;
            TileHighlighter.resetTiles();
            gs.toggleTankRings(playerTurn == PlayerColors.Red ? redTanks : blueTanks, new CoordinateSet(-1, -1), true);
            gs.toggleTankRings(playerTurn == PlayerColors.Red ? blueTanks : redTanks, new CoordinateSet(-1, -1), false);
            printTurn();
            tankClicked = null;
            tankClicked2 = null;
            updateTooltips("Gamble");
        }

        return true;
    }

    private void handleKnockback(CoordinateSet currentTankCoordinates, CoordinateSet targetTankCoordinates) {
        Tank currentTank;
        Tank targetTank;
        ArrayList knockbackArray = new ArrayList();
        CoordinateSet knockbackCoordinates = new CoordinateSet();
        PlayerColors knockbackPlayer;
        bool validKnockback = true;
        int validKnockbackIndex = 0;

        if (playerTurn == PlayerColors.Red) {
            currentTank = gs.getPlayerTank(PlayerColors.Red, currentTankCoordinates);
            targetTank = gs.getPlayerTank(PlayerColors.Blue, targetTankCoordinates);
            knockbackPlayer = PlayerColors.Blue;
        }
        else {
            currentTank = gs.getPlayerTank(PlayerColors.Blue, currentTankCoordinates);
            targetTank = gs.getPlayerTank(PlayerColors.Red, targetTankCoordinates);
            knockbackPlayer = PlayerColors.Red;
        }

        int knockback = currentTank.getWeapon().getKnockback();

        if(knockback == 0) {
            return;
        }

        // Figure out which direction to knockback the target tank
        if (currentTankCoordinates.getX() == targetTankCoordinates.getX()) {
            // Up
            if (currentTankCoordinates.getY() < targetTankCoordinates.getY()) {
                for(int i = targetTankCoordinates.getY() + 1; i <= targetTankCoordinates.getY() + knockback; i++) {
                    knockbackArray.Add(new CoordinateSet(targetTankCoordinates.getX(), i));
                }
            }
            // Down
            else {
                for (int i = targetTankCoordinates.getY() - 1; i >= targetTankCoordinates.getY() - knockback; i--) {
                    knockbackArray.Add(new CoordinateSet(targetTankCoordinates.getX(), i));
                }
            }
        }
        else if (currentTankCoordinates.getY() == targetTankCoordinates.getY()) {
            // Right
            if (currentTankCoordinates.getX() < targetTankCoordinates.getX()) {
                for (int i = targetTankCoordinates.getX() + 1; i <= targetTankCoordinates.getX() + knockback; i++) {
                    knockbackArray.Add(new CoordinateSet(i, targetTankCoordinates.getY()));
                }
            }
            // Left
            else {
                for (int i = targetTankCoordinates.getX() - 1; i >= targetTankCoordinates.getX() - knockback; i--) {
                    knockbackArray.Add(new CoordinateSet(i, targetTankCoordinates.getY()));
                }
            }
        }

        for(int i = 0; i < knockbackArray.Count; i++) {
            validKnockback = gs.checkValidMove(knockbackPlayer, targetTankCoordinates, (CoordinateSet)knockbackArray[i], false);

            if (validKnockback) {
                knockbackCoordinates = (CoordinateSet)knockbackArray[i];
                validKnockbackIndex = i;
            }
            else {
                if(i == 0) {
                    targetTank.decrementHealth(5);
                    return;
                }
            }
        }

        if (gs.checkValidMove(knockbackPlayer, targetTankCoordinates, knockbackCoordinates, true)) {

            if (targetTank is CannonTank) {
                tankClicked2.transform.position = new Vector3(knockbackCoordinates.getX(), 1, knockbackCoordinates.getY());
            }
            else {
                tankClicked2.transform.position = new Vector3(knockbackCoordinates.getX(), 0.8f, knockbackCoordinates.getY());
            }

            // If the knockback was less than the number of tiles, the target tank must have hit an obstacle along the way.
            // Take damage.
            if (validKnockbackIndex < knockbackArray.Count - 1) {
                targetTank.decrementHealth(5);
            }
        }
        else {
            // If the move was invalid, take damage
            targetTank.decrementHealth(5);
        }
    }

    private void handleGamble(RaycastHit hit) {
        // Do Gamble
        tankClicked = hit.transform.gameObject;
        CoordinateSet currentTankCoordinates = new CoordinateSet((int)tankClicked.transform.position.x, (int)tankClicked.transform.position.z);
        Tank currentTank = gs.getPlayerTank(playerTurn, currentTankCoordinates);

        // If the player didn't choose one of their own tanks, or if that tank is dead, just ignore
        if (currentTank.isDead() || currentTank.getPlayer().getPlayerColor() != playerTurn) {
            tankClicked = null;
            return;
        }

        string powerup = gs.playerGamble(playerTurn, currentTankCoordinates);

        // Print log message
        Debug.Log("Player " + playerTurn + "'s gamble results in: " + powerup);

        // Update the state
        tankClicked = null;
        round = Rounds.Move;
        changeTurns();
        printTurn();
        gs.updatePlayerHealthBars(hpController);
        updateTooltips("Move1");
        numGambles++;
        gs.toggleTankRings(redTanks, new CoordinateSet(-1, -1), false);
        gs.toggleTankRings(blueTanks, new CoordinateSet(-1, -1), false);
    }

    private IEnumerator handleGreedyAi() {
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
        tankClicked = blue;
        tankClicked2 = red;

        // The greedy AI will stay still if there is already a valid attack
        // Otherwise, it will try and move closer to the closest player.
        if(!handleAttack(false))
        {
            Tank aiTank = gs.getPlayerTank(PlayerColors.Blue, aiLocation);
            
            Vector3 startPos = blue.transform.position;
            Vector3 endPos = new Vector3(0, 0, 0);
            Quaternion startRot = blue.transform.rotation;
            Quaternion endRot = Quaternion.Euler(0f, 0f, 0f);
            if (aiTank is CannonTank)
            {
                endPos = new Vector3(targetLocation.getX(), 1f, targetLocation.getY());
            }
            else
            {
                endPos = new Vector3(targetLocation.getX(), 0.8f, targetLocation.getY());
            }

            if (endPos.x > startPos.x)
            {
                endRot = Quaternion.Euler(0f, 90f, 0f);
            }
            else if (endPos.x < startPos.x)
            {
                endRot = Quaternion.Euler(0f, 270f, 0f);
            }
            else if (endPos.z > startPos.z)
            {
                endRot = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (endPos.z < startPos.z)
            {
                endRot = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                endRot = blue.transform.rotation;
            }
            float rotStep = tankMoveSpeed * Time.fixedDeltaTime;
            float rotT = 0;
            while (rotT <= 1.0f)
            {
                rotT += rotStep;
                blue.transform.rotation = Quaternion.Lerp(startRot, endRot, rotT);
                yield return new WaitForFixedUpdate();
            }
            blue.transform.rotation = endRot;

            float step = (tankMoveSpeed / (startPos - endPos).magnitude) * Time.fixedDeltaTime;
            float t = 0;
            while (t <= 1.0f)
            {
                t += step;
                blue.transform.position = Vector3.Lerp(startPos, endPos, t);
                yield return new WaitForFixedUpdate();
            }
            blue.transform.position = endPos;

            rotT = 0;
            if (aiTank is SniperTank)
            {
                while (rotT <= 1.0f)
                {
                    rotT += rotStep;
                    blue.transform.rotation = Quaternion.Lerp(endRot, startRot, rotT);
                    yield return new WaitForFixedUpdate();
                }
            }

            gs.checkValidMove(PlayerColors.Blue, aiLocation, targetLocation, true);

            // AI will game 1/5 of the time if it moves
            System.Random randomNumberGenerator = new System.Random();
            int randomNumber = randomNumberGenerator.Next(1, 4);
            if(randomNumber == 1)
            {
                string powerup = gs.playerGamble(playerTurn, targetLocation);
                Debug.Log("Player " + playerTurn + "'s gamble results in: " + powerup);
            }

        }

        handleAttack(true);
        changeTurns();
        round = Rounds.Move;
        gs.updatePlayerHealthBars(hpController);
        numGambles++;
        updateTooltips("Move1");
        aiMove = false;
    }

    private IEnumerator handleMinMaxAi(){
        mmai = new MinMaxAI(redTanks, blueTanks, gs, tankSet1, tankSet2);
        CoordinateSet aiLocation;
        CoordinateSet playerLocation;
        CoordinateSet targetLocation;
        GameObject blue;
        GameObject red;

        targetLocation = mmai.MinMaxTurn();
        blue = mmai.getAITank();
        red = mmai.getPlayerTank();
        aiLocation = new CoordinateSet((int)blue.transform.position.x, (int)blue.transform.position.z);
        tankClicked = blue;
        tankClicked2 = red;

        if(!handleAttack(false))
        {
            Tank aiTank = gs.getPlayerTank(PlayerColors.Blue, aiLocation);

            Vector3 startPos = blue.transform.position;
            Vector3 endPos = new Vector3(0, 0, 0);
            Quaternion startRot = blue.transform.rotation;
            Quaternion endRot = Quaternion.Euler(0f, 0f, 0f);
            if (aiTank is CannonTank)
            {
                endPos = new Vector3(targetLocation.getX(), 1f, targetLocation.getY());
            }
            else
            {
                endPos = new Vector3(targetLocation.getX(), 0.8f, targetLocation.getY());
            }

            if (endPos.x > startPos.x)
            {
                endRot = Quaternion.Euler(0f, 90f, 0f);
            }
            else if (endPos.x < startPos.x)
            {
                endRot = Quaternion.Euler(0f, 270f, 0f);
            }
            else if (endPos.z > startPos.z)
            {
                endRot = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (endPos.z < startPos.z)
            {
                endRot = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                endRot = blue.transform.rotation;
            }
            float rotStep = tankMoveSpeed * Time.fixedDeltaTime;
            float rotT = 0;
            while (rotT <= 1.0f)
            {
                rotT += rotStep;
                blue.transform.rotation = Quaternion.Lerp(startRot, endRot, rotT);
                yield return new WaitForFixedUpdate();
            }
            blue.transform.rotation = endRot;

            float step = (tankMoveSpeed / (startPos - endPos).magnitude) * Time.fixedDeltaTime;
            float t = 0;
            while (t <= 1.0f)
            {
                t += step;
                blue.transform.position = Vector3.Lerp(startPos, endPos, t);
                yield return new WaitForFixedUpdate();
            }
            blue.transform.position = endPos;

            rotT = 0;
            if (aiTank is SniperTank)
            {
                while (rotT <= 1.0f)
                {
                    rotT += rotStep;
                    blue.transform.rotation = Quaternion.Lerp(endRot, startRot, rotT);
                    yield return new WaitForFixedUpdate();
                }
            }

            gs.checkValidMove(PlayerColors.Blue, aiLocation, targetLocation, true);

            // AI will game 1/5 of the time if it moves
            System.Random randomNumberGenerator = new System.Random();
            int randomNumber = randomNumberGenerator.Next(1, 4);
            if(randomNumber == 1)
            {
                //string powerup = gs.playerGamble(playerTurn, targetLocation);
                //Debug.Log("Player " + playerTurn + "'s gamble results in: " + powerup);
            }

        }

        handleAttack(true);
        changeTurns();
        round = Rounds.Move;
        gs.updatePlayerHealthBars(hpController);
        numGambles++;
        updateTooltips("Move1");
        aiMove = false;
    }
    
    public string[] getPlayerPowerups(PlayerColors player) {
        return gs.getPlayerPowerups(player);
    }

    private void gameOver() {
        PlayerColors player = gs.isGameOver();

        if(player == PlayerColors.Red) {
            Debug.Log("PLAYER 1 DEAD!!");
            gameStatus.GetComponent<GameStatus>().SetPlayer2Win();
        }
        else if(player == PlayerColors.Blue){
            Debug.Log("PLAYER 2 DEAD!!");
            gameStatus.GetComponent<GameStatus>().SetPlayer1Win();
        }
        else {
            // Game's not over fool!
        }
    }

    public GameState getGameState() {
        return this.gs;
    }

    public void updateTooltips(string name) {
        if(numGambles >= 4) {
            name = "GLHF";
        }

        foreach (GameObject obj in tooltips) {
            if (obj.name == name && numGambles <= 5) {
                obj.SetActive(true);
            }
            else {
                obj.SetActive(false);
            }
        }
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