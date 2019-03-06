using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLosingTankSpawner : MonoBehaviour
{
    GameStatus currentState;

    [SerializeField] bool redTeamWon;
    [SerializeField] bool blueTeamWon;

    [SerializeField] GameObject[] tanksPreFabsBlue;
    [SerializeField] GameObject[] tanksPreFabsRed;
    [SerializeField] static GameObject losingTankSpawner;

    Vector3 tankSpawnLocation = new Vector3(5, 1, 9);

    // Start is called before the first frame update
    void Start()
    {
        currentState = FindObjectOfType<GameStatus>();
        redTeamWon = currentState.GetPlayer1WinLoseInfo();
        blueTeamWon = currentState.GetPlayer2WinLoseInfo();

        if (!blueTeamWon)
        {
            Instantiate(tanksPreFabsBlue[Random.Range(0, tanksPreFabsBlue.Length)], transform.position, Quaternion.Euler(0, 180, 0));
        }
        else
        {

        }
    }
}
