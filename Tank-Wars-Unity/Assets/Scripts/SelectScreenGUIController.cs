using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScreenGUIController : MonoBehaviour
{
    [SerializeField] GameObject p1Ready;
    [SerializeField] GameObject p1Redo;


    [SerializeField] bool player1FishedPicking = false;             //if this goes true, player 1 pick phase has ended
    [SerializeField] bool player2FishedPicking = false;             //if this goes true, player 1 pick phase has ended
    [SerializeField] bool bothPlayersReady = false;                 //if this goes true, both players are ready and it will load next level

    //object to get level index
    //object to get sence index


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bothPlayersReady)
        {
            //go to level index;
        }
    }


    public void P1Redo()
    {
        p1Redo.SetActive(false);
        p1Ready.SetActive(false);
        //call function to set p1 indexback to 0
    }

    public void P1Ready()
    {
        player1FishedPicking = true;
        p1Ready.SetActive(false);
        p1Redo.SetActive(false);
    }

    public void p1DonePicking()
    {
        p1Ready.SetActive(true);
        p1Redo.SetActive(true);
    }

}


