using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelButtonHandler : MonoBehaviour
{

    [SerializeField] GameObject escMenu;            //controls the esc key meny object
    [SerializeField] GameObject optionMenu;         //controls the option menu object

    bool escButtonIsCliked = false;                 //used for to set all false if the key been hit in the middle of the menu

    void Start()
    {
        escMenu.SetActive(false);                   //will set the esc key menu false since it on for testing
    }

    // Update is called once per frame
    void Update()
    {
        //the escape key has not been hit yet
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(escButtonIsCliked == false)
            {
                escMenu.SetActive(true);            //turns on esc menu
                escButtonIsCliked = true;           //marked as pressed
            }

            //elese this is the second time it was pressed and will turn off all menu objects and set as button not clicked
            else
            {
                escMenu.SetActive(false);       
                optionMenu.SetActive(false);
                escButtonIsCliked = false;
            }

        }
    }

    //when returned pressed in game, it will reset as button not being clicked.
    public void EscButtonIsNotCliked()
    {

        escButtonIsCliked = false;
    }
}
