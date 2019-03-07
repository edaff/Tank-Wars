using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningTankSpawn : MonoBehaviour
{
    GameStatus currentState;

    [SerializeField] bool redTeamWon;
    [SerializeField] bool blueTeamWon;

    [SerializeField] GameObject[] tanksPreFabsBlue;
    [SerializeField] GameObject[] tanksPreFabsRed;
    [SerializeField] static GameObject winningTankSpawner;

    Vector3 tankSpawnLocation = new Vector3(2, 2, 1);

    // Start is called before the first frame update
    void Start()
    {
        currentState = FindObjectOfType<GameStatus>();
        redTeamWon = currentState.GetPlayer1WinLoseInfo();
        blueTeamWon = currentState.GetPlayer2WinLoseInfo();

        //blueTeamWon = true;       //blue won
        if (blueTeamWon)
        {
            Instantiate(tanksPreFabsBlue[Random.Range(0, tanksPreFabsBlue.Length)], tankSpawnLocation, Quaternion.Euler(0, 180, 0));
        }
        else
        {
            Instantiate(tanksPreFabsRed[Random.Range(0, tanksPreFabsRed.Length)], tankSpawnLocation, Quaternion.Euler(0, 180, 0));
        }
    }
}
