using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1PowerUpController : MonoBehaviour
{
    Turns getPowerUpInfo;

    [Header("Red Player's PowerUp GUI Controller")]
    [SerializeField] GameObject redTank1BonusDamageGUI;
    [SerializeField] GameObject redTank1BonusMovementGUI;
    [SerializeField] GameObject redTank1InvicibilityGUI;
    [SerializeField] GameObject redTank1HealingGUI;

    [Header("Blue Player's PowerUp GUI Controller")]
    [SerializeField] GameObject blueTank1BonusDamageGUI;
    [SerializeField] GameObject blueTank1BonusMovementGUI;
    [SerializeField] GameObject blueTank1InvicibilityGUI;
    [SerializeField] GameObject blueTank1HealingGUI;

    // Start is called before the first frame update
    void Start()
    {
        getPowerUpInfo = FindObjectOfType<Turns>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Red Tank PowerUps " + getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[0]);
        //Debug.Log("Blue Tank PowerUps " + getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[0]);

        RedTanks1PowerUpGUIController();
        BlueTanks1PowerUpGUIController();
    }

    //handles the PowerUp GUI for redTank
    private void RedTanks1PowerUpGUIController()
    {

        if(getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[0] == "Bonus Damage")
        {
            redTank1BonusDamageGUI.SetActive(true);
        }
        else if(getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[0] == "Bonus Movement")
        {
            redTank1BonusMovementGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[0] == "Invincibility")
        {
            redTank1InvicibilityGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[0] == "Healing")
        {
            redTank1HealingGUI.SetActive(true);
        }
        else
        {
            redTank1InvicibilityGUI.SetActive(false);
            redTank1BonusMovementGUI.SetActive(false);
            redTank1BonusDamageGUI.SetActive(false);
            redTank1HealingGUI.SetActive(false);
        }
    }

    //handles the PowerUp GUI for blueTank
    private void BlueTanks1PowerUpGUIController()
    {
        if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[0] == "Bonus Damage")
        {
            blueTank1BonusDamageGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[0] == "Bonus Movement")
        {
            blueTank1BonusMovementGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[0] == "Invincibility")
        {
            blueTank1InvicibilityGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[0] == "Healing")
        {
            blueTank1HealingGUI.SetActive(true);
        }
        else
        {
            blueTank1InvicibilityGUI.SetActive(false);
            blueTank1BonusMovementGUI.SetActive(false);
            blueTank1BonusDamageGUI.SetActive(false);
            blueTank1HealingGUI.SetActive(false);
        }
    }

}
