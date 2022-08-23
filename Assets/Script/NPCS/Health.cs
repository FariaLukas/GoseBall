using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class Health : MonoBehaviour
{
    public Action onTakeDamage, onHeal, onDie;

    [ShowInInspector]
    public int maxLife { get; private set; }
    [ShowInInspector]
    public int currentLife { get; private set; }

    [SerializeField] protected float delayToDie;

    public virtual void SetupLife(float maxLife)
    {
        this.maxLife = currentLife = (int)maxLife;
    }

    public virtual void TakeDamage(int value)
    {
        currentLife -= value;
        onTakeDamage?.Invoke();

        if (currentLife <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int heal)
    {
        if (currentLife >= maxLife) return;
        onTakeDamage?.Invoke();
        currentLife += heal;
    }

    public virtual void Die()
    {
        onDie?.Invoke();
        Destroy(gameObject, delayToDie);
    }
}

