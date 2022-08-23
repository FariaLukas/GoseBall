using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ManaCounter : MonoBehaviour
{
    public float maxMana = 10;
    public float currentMana { get; private set; }

    private bool _ended;

    private void Start()
    {
        Manager.Instance.OnEndGame += StopManaFlow;
    }

    private void Update()
    {
        if (_ended) return;

        if (currentMana > maxMana)
        {
            currentMana = maxMana;
            return;
        }

        currentMana += Time.deltaTime;

    }

    public void UseMana(float value)
    {
        currentMana -= value;
    }

    private void StopManaFlow()
    {
        _ended = true;

    }
}
