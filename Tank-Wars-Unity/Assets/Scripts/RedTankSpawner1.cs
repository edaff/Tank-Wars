using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTankSpawner1 : MonoBehaviour
{
    GameStatus currentState;
    [SerializeField] int whatLevelAmI = 1;

    [SerializeField] private GameObject[] redTanksPreFabs;
    [SerializeField] public int[] getPicks;

    Vector3 tankSpawnLocationLv1 = new Vector3(4, 1, 0);               //this is the spawn location for the first normal tank since its heigh is not the same as the others
    Vector3 tankSpawnLocationDownLv1 = new Vector3(4, .8f, 0);            //this spawns location for the sinper and mortar tank

    Vector3 tankSpawnLocationLv2 = new Vector3(3, 1, 0);               //this is the spawn location for the first normal tank since its heigh is not the same as the others
    Vector3 tankSpawnLocationDownLv2 = new Vector3(3, .8f, 0);            //this spawns location for the sinper and mortar tank

    Vector3 tankSpawnLocation2Lv2 = new Vector3(11, 1, 0);               //this is the spawn location for the first normal tank since its heigh is not the same as the others
    Vector3 tankSpawnLocation2DownLv2 = new Vector3(11, .8f, 0);            //this spawns location for the sinper and mortar tank

    // Start is called before the first frame update
    void Start()
    {
        currentState = FindObjectOfType<GameStatus>();
        whatLevelAmI = currentState.GetDifficuity();

        getPicks = new int[] {0,0,0};
        getPicks = currentState.getPlayer1TankPicks();

        if(whatLevelAmI == 1)
        {
            if ((getPicks[0] - 1) == 0)
            { Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationLv1, Quaternion.Euler(0, 0, 0)); }
            else { Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationDownLv1, Quaternion.Euler(0, 0, 0)); }
        }
        else if(whatLevelAmI == 2)
        {
            if ((getPicks[0] - 1) == 0)
            { Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationLv2, Quaternion.Euler(0, 0, 0)); }
            else { Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationDownLv2, Quaternion.Euler(0, 0, 0)); }

            if ((getPicks[1] - 1) == 0)
            { Instantiate(redTanksPreFabs[(getPicks[1] - 1)], tankSpawnLocation2Lv2, Quaternion.Euler(0, 0, 0)); }
            else { Instantiate(redTanksPreFabs[(getPicks[1] - 1)], tankSpawnLocation2DownLv2, Quaternion.Euler(0, 0, 0)); }
        }
    }
}
