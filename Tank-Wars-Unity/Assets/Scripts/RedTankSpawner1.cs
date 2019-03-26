using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTankSpawner1 : MonoBehaviour
{
    GameStatus currentState;

    [SerializeField] int whatLevelAmI = 1;

    [SerializeField] private GameObject[] redTanksPreFabs;
    [SerializeField] public int[] getPicks;

    [SerializeField] GameObject redTank1;
    [SerializeField] Transform[] redTank1Childern = new Transform[7];      //holds the child objects for the first red tank that spawns

    [SerializeField] GameObject redTank2;
    [SerializeField] Transform[] redTank2Childern = new Transform[7];      //holds the child objects for the second red tank that spawns

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
            { redTank1 = Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationLv1, Quaternion.Euler(0, 0, 0)); }
            else { redTank1 = Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationDownLv1, Quaternion.Euler(0, 0, 0)); }
        }
        else if(whatLevelAmI == 2)
        {
            if ((getPicks[0] - 1) == 0)
            { redTank1 = Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationLv2, Quaternion.Euler(0, 0, 0)); }
            else { redTank1 = Instantiate(redTanksPreFabs[(getPicks[0] - 1)], tankSpawnLocationDownLv2, Quaternion.Euler(0, 0, 0)); }

            if ((getPicks[1] - 1) == 0)
            { redTank2 = Instantiate(redTanksPreFabs[(getPicks[1] - 1)], tankSpawnLocation2Lv2, Quaternion.Euler(0, 0, 0)); }
            else { redTank2 = Instantiate(redTanksPreFabs[(getPicks[1] - 1)], tankSpawnLocation2DownLv2, Quaternion.Euler(0, 0, 0)); }
        }

        //makes a red tank child array from its parent redtank1
        for(int i = 0; i < 7; i++)
        {
            redTank1Childern[i] = redTank1.transform.GetChild(i);
        }

        if(whatLevelAmI > 1) { 
        for (int i = 0; i < 7; i++)
        {
            redTank2Childern[i] = redTank2.transform.GetChild(i);
        }
        }
    }
    /*void Update()
    {
        //redTank10Per = GameObject.Find("normalTankFire10");
        //Debug.Log(redTank10Per);

        foreach (Transform child in redTank1.transform)
        {
            Debug.Log(child.gameObject.name);
            child.gameObject.SetActive(true);
        }
        
        //Debug.Log(redTankChildern[4].name);
        //redTankChildern[4].gameObject.SetActive(true);
    }*/

    public Transform[] getRedTank1Childern()                //returns the child array of the first red tank that spawns
    {
        return this.redTank1Childern;
    }

    public Transform[] getRedTank2Childern()                //returns the child array of the first red tank that spawns
    {
        return this.redTank2Childern;
    }
}
