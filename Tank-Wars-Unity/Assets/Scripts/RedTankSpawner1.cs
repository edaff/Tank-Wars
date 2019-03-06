using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTankSpawner1 : MonoBehaviour
{

    GameStatus currentState;
    [SerializeField] int whatLevelAmI = 1;

    [SerializeField] private GameObject[] redTanksPreFabs;
    [SerializeField] public int[] getPicks;

    Vector3 tankSpawnLocation = new Vector3(4, 1, 0);               //this is the spawn location for the first normal tank since its heigh is not the same as the others
    Vector3 tankSpawnLocation2 = new Vector3(4, .8f, 0);            //this spawns location for the sinper and mortar tank

    // Start is called before the first frame update
    void Start()
    {
        currentState = FindObjectOfType<GameStatus>();
        whatLevelAmI = currentState.GetDifficuity();

        getPicks = new int[] {0,0,0};
        getPicks = currentState.getPlayer1TankPicks();

        if((getPicks[0] - 1) == 0)
        {Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocation, Quaternion.Euler(0, 0, 0));}
        else{Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocation2, Quaternion.Euler(0, 0, 0)); }
        

    }
}
