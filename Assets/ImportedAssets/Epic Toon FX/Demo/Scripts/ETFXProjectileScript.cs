using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using DG.Tweening;

public class ETFXProjectileScript : MonoBehaviour
{
 
    private bool hasCollided = false;

    public GameObject projectileParticle;

    void OnCollisionEnter(Collision hit)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            Debug.Log("Dont use this script. Use ProjectileController instead");

        }
    }

}