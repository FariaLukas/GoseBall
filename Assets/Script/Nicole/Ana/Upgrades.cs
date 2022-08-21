using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public ClickerManager clickerManager;
    public float upgradeCost;
    [Range(1, 2)]
    public float amountPercent = 1.5f;
    [Range(0, 1)]
    public float speedPercent = 0.8f;

    public void UpgradeClickValue()
    {
        if (clickerManager.score >= upgradeCost)
        {
            clickerManager.DebitScore(upgradeCost);
            clickerManager.amountAdded *= amountPercent;
            clickerManager.delayToScore *= speedPercent;
        }
    }

    public void UpgradeRockValue(float percentAdded)
    {
        clickerManager.moneyPerRock *= percentAdded;
    }

    public void UpgradePressedSpeed(float percentAdded)
    {

    }

}
