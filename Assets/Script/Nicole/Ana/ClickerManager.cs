using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
{
    public Text dollarDisplay;
    private float currentTimer;
    public bool pressed;

    public float score;
    public float rockQuantity;
    public float amountAdded = 1;
    public float moneyPerRock = 1;
    public float delayToScore;

    public void AddScore()
    {
        rockQuantity += amountAdded;
        rockQuantity += amountAdded;
        score += amountAdded * moneyPerRock;

        dollarDisplay.text = score.ToString("0.00") + " $";

    }

    private void Update()
    {
        if (pressed)
        {
            if (currentTimer <= 0)
            {
                currentTimer = delayToScore;
                AddScore();
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }

    }

    public void Pressed()
    {
        pressed = true;
    }
    public void Released()
    {
        currentTimer = 0;
        pressed = false;
    }

    public void DebitScore(float debit)
    {
        score = score - debit;

        if (score < 0)
        {
            score = 0;
        }

    }


}
