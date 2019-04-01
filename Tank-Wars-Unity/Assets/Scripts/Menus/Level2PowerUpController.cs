using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2PowerUpController : MonoBehaviour
{
    Turns getPowerUpInfo;

    [Header("Red Player's PowerUp GUI Controller Tank1")]
    [SerializeField] GameObject redTank1BonusDamageGUI;
    [SerializeField] GameObject redTank1BonusMovementGUI;
    [SerializeField] GameObject redTank1InvicibilityGUI;
    [SerializeField] GameObject redTank1HealingGUI;

    [Header("Red Player's PowerUp GUI Controller Tank2")]
    [SerializeField] GameObject redTank2BonusDamageGUI;
    [SerializeField] GameObject redTank2BonusMovementGUI;
    [SerializeField] GameObject redTank2InvicibilityGUI;
    [SerializeField] GameObject redTank2HealingGUI;

    [Header("Blue Player's PowerUp GUI Controller Tank1")]
    [SerializeField] GameObject blueTank1BonusDamageGUI;
    [SerializeField] GameObject blueTank1BonusMovementGUI;
    [SerializeField] GameObject blueTank1InvicibilityGUI;
    [SerializeField] GameObject blueTank1HealingGUI;

    [Header("Blue Player's PowerUp GUI Controller Tank2")]
    [SerializeField] GameObject blueTank2BonusDamageGUI;
    [SerializeField] GameObject blueTank2BonusMovementGUI;
    [SerializeField] GameObject blueTank2InvicibilityGUI;
    [SerializeField] GameObject blueTank2HealingGUI;

    // Start is called before the first frame update
    void Start()
    {
        getPowerUpInfo = FindObjectOfType<Turns>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Red Tank PowerUps " + getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[0]);
        //Debug.Log("Blue Tank PowerUps " + getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[0]);

        RedTanks1PowerUpGUIController();
        RedTanks2PowerUpGUIController();
        BlueTanks1PowerUpGUIController();
        BlueTanks2PowerUpGUIController();
    }

    //handles the PowerUp GUI for redTank1
    private void RedTanks1PowerUpGUIController()
    {

        if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[0] == "Bonus Damage")
        {
            redTank1BonusDamageGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[0] == "Bonus Movement")
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

    //handles the PowerUp GUI for redTank2
    private void RedTanks2PowerUpGUIController()
    {

        if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[1] == "Bonus Damage")
        {
            redTank2BonusDamageGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[1] == "Bonus Movement")
        {
            redTank2BonusMovementGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[1] == "Invincibility")
        {
            redTank2InvicibilityGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Red)[1] == "Healing")
        {
            redTank2HealingGUI.SetActive(true);
        }
        else
        {
            redTank2InvicibilityGUI.SetActive(false);
            redTank2BonusMovementGUI.SetActive(false);
            redTank2BonusDamageGUI.SetActive(false);
            redTank2HealingGUI.SetActive(false);
        }
    }

    //handles the PowerUp GUI for blueTank1
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

    //handles the PowerUp GUI for blueTank1
    private void BlueTanks2PowerUpGUIController()
    {
        if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[1] == "Bonus Damage")
        {
            blueTank2BonusDamageGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[1] == "Bonus Movement")
        {
            blueTank2BonusMovementGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[1] == "Invincibility")
        {
            blueTank2InvicibilityGUI.SetActive(true);
        }
        else if (getPowerUpInfo.getPlayerPowerups(PlayerColors.Blue)[1] == "Healing")
        {
            blueTank2HealingGUI.SetActive(true);
        }
        else
        {
            blueTank2InvicibilityGUI.SetActive(false);
            blueTank2BonusMovementGUI.SetActive(false);
            blueTank2BonusDamageGUI.SetActive(false);
            blueTank2HealingGUI.SetActive(false);
        }
    }
}
