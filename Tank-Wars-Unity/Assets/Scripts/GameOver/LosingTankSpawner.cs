﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingTankSpawner : MonoBehaviour
{
    GameStatus currentState;

    [SerializeField] bool redTeamWon;
    [SerializeField] bool blueTeamWon;

    [SerializeField] GameObject[] tanksPreFabsBlue;
    [SerializeField] GameObject[] tanksPreFabsRed;
    [SerializeField] static GameObject losingTankSpawner;

    Vector3 tankSpawnLocation = new Vector3(6, 2, 7);

    // Start is called before the first frame update
    void Start()
    {
        currentState = FindObjectOfType<GameStatus>();
        redTeamWon = currentState.GetPlayer1WinLoseInfo();
        blueTeamWon = currentState.GetPlayer2WinLoseInfo();

        //blueTeamWon = true;       //blue won
        if (!blueTeamWon)
        {
            Instantiate(tanksPreFabsBlue[Random.Range(0, tanksPreFabsBlue.Length)], tankSpawnLocation, Quaternion.Euler(0, 300, 0));
        }
        else
        {
            Instantiate(tanksPreFabsRed[Random.Range(0, tanksPreFabsRed.Length)], tankSpawnLocation, Quaternion.Euler(0, 300, 0));
        }
    }
}
