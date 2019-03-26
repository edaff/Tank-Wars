using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTankSpawner : MonoBehaviour
{
    GameStatus currentState;

    [SerializeField] int whatLevelAmI = 1;

    [SerializeField] private GameObject[] redTanksPreFabs;
    [SerializeField] public int[] getPicks;

    [SerializeField] GameObject blueTank1;
    [SerializeField] Transform[] blueTank1Childern = new Transform[7];      //holds the child objects for the first blue tank that spawns

    [SerializeField] GameObject blueTank2;
    [SerializeField] Transform[] blueTank2Childern = new Transform[7];      //holds the child objects for the second blue tank that spawns

    Vector3 tankSpawnLocationLv1 = new Vector3(5, 1, 9);               //this is the spawn location for the first normal tank since its heigh is not the same as the others
    Vector3 tankSpawnLocationDownLv1 = new Vector3(5, .8f, 9);            //this spawns location for the sinper and mortar tank

    Vector3 tankSpawnLocationLv2 = new Vector3(3, 1, 14);               //this is the spawn location for the first normal tank since its heigh is not the same as the others
    Vector3 tankSpawnLocationDownLv2 = new Vector3(3, .8f, 14);            //this spawns location for the sinper and mortar tank

    Vector3 tankSpawnLocation2Lv2 = new Vector3(11, 1, 14);               //this is the spawn location for the first normal tank since its heigh is not the same as the others
    Vector3 tankSpawnLocation2DownLv2 = new Vector3(11, .8f, 14);            //this spawns location for the sinper and mortar tank

    // Start is called before the first frame update
    void Start()
    {
        currentState = FindObjectOfType<GameStatus>();
        whatLevelAmI = currentState.GetDifficuity();

        getPicks = new int[] { 0, 0, 0 };
        getPicks = currentState.getPlayer2TankPicks();

        if(whatLevelAmI == 1)
        {
            if ((getPicks[0] - 1) == 0)
            { blueTank1 = Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationLv1, Quaternion.Euler(0, 180, 0)); }
            else { blueTank1 = Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationDownLv1, Quaternion.Euler(0, 180, 0)); }
        }
        else if(whatLevelAmI == 2)
        {
            if ((getPicks[0] - 1) == 0)
            { blueTank1 = Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationLv2, Quaternion.Euler(0, 180, 0)); }
            else { blueTank1 = Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationDownLv2, Quaternion.Euler(0, 180, 0)); }

            if ((getPicks[1] - 1) == 0)
            { blueTank2 = Instantiate(redTanksPreFabs[(getPicks[1] - 1)], tankSpawnLocation2Lv2, Quaternion.Euler(0, 180, 0)); }
            else { blueTank2 = Instantiate(redTanksPreFabs[(getPicks[1] - 1)], tankSpawnLocation2DownLv2, Quaternion.Euler(0, 180, 0)); }
        }

        //makes a blue tank child array from its parent redtank1
        for (int i = 0; i < 7; i++)
        {
            blueTank1Childern[i] = blueTank1.transform.GetChild(i);
        }

        if (whatLevelAmI > 1)
        {
            for (int i = 0; i < 7; i++)
            {
                blueTank2Childern[i] = blueTank2.transform.GetChild(i);
            }
        }
    }

    public Transform[] getBlueTank1Childern()                //returns the child array of the first blue tank that spawns
    {
        return this.blueTank1Childern;
    }

    public Transform[] getBlueTank2Childern()                //returns the child array of the first blue tank that spawns
    {
        return this.blueTank2Childern;
    }

}
