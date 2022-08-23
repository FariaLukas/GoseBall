using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CooldownCounter
{
    public float maxCooldown;
    public float currentCooldown { get; set; }
    public bool onCooldown { get; set; }

    public void StartCooldown()
    {
        currentCooldown = 0;
        onCooldown = true;
    }

    public void Update()
    {
        if (!onCooldown) return;

        if (currentCooldown >= maxCooldown)
        {
            currentCooldown = maxCooldown;
            onCooldown = false;
        }

        currentCooldown += Time.deltaTime;
    }
}
