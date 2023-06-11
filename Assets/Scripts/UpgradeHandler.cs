using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    [Header("Upgrade List")]
    public int multiplier = 1;
    public float timeBetweenRolls = 0;
    public DiceType diceType;

    public void UpgradeTimeBetweenRolls(float percentTimeDecrease)
    {
        timeBetweenRolls *= percentTimeDecrease;
    }

    public void UpgradeRollMultiplier()
    {
        multiplier += 1;
    }

    public void AddDiceUpgrades()
    {
        
    }
}
