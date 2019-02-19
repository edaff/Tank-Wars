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
	public int round;
	public int playerTurn;
	public int redPlayerTurn = 1;
	public int bluePlayerTurn = 2;
	public bool gamblePressed = false;
	
    void Start()
    {
    	oc = GetComponent<ObjectClicker>();
    	tankSet1 = new int[] {1,0,0};
    	tankSet2 = new int[] {1,0,0};
    	gs = new GameState(1,tankSet1,tankSet2);
    	round = 1;
    	playerTurn = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(oc.objectClicked != null)
        {
        	objectClicked = oc.objectClicked;
        	oc.objectClicked = null;
        	if(round == 1)
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
                            if((hit.transform.gameObject.tag == "Red Tank" && playerTurn == redPlayerTurn) || (hit.transform.gameObject.tag == "Blue Tank" && playerTurn == bluePlayerTurn))
                            {
                                tankClicked = hit.transform.gameObject;
                            }
                            else if(tankClicked != null)
                            {
                                tileClicked = hit.transform.gameObject;
                                CoordinateSet cs = new CoordinateSet((int)tileClicked.transform.position.x, (int)tileClicked.transform.position.z);
                                if(gs.checkValidMove(playerTurn, 0, cs))
                                {
                                    tankClicked.transform.position = new Vector3(tileClicked.transform.position.x, 1,tileClicked.transform.position.z);
                                    tileClicked = null;
                                    tankClicked = null;
                                    round++;
                                }
                            }
                        }
                    }
                }
            }
            else if(round == 2)
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
                            if(hit.transform.gameObject.tag == "Red Tank")
                            {
                                tankClicked = hit.transform.gameObject;
                            }
                            else if(hit.transform.gameObject.tag == "Blue Tank")
                            {
                                tankClicked2 = hit.transform.gameObject;
                            }
                            if(tankClicked != null && tankClicked2 != null)
                            {
                                if(gs.checkValidAttack())
                                {
                                    print("Good attack");
                                }
                                else
                                {
                                    print("Bad attack");
                                }
                                round = 1;
                                tankClicked = null;
                                tankClicked2 = null;
                                if(++playerTurn > 2)
                                {
                                	playerTurn = 1;
                                }
                            }
                        }
                    }
                }
            }
            else if(round == 3)
            {
                if(gamblePressed)
                {
                    gamblePressed = false;
                    round = 1;
                    if(++playerTurn > 2)
                    {
                        playerTurn = 1;
                    }
                }
            }
        }

    }
}
