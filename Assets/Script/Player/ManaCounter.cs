using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ManaCounter : MonoBehaviour, IListener
{
    [SerializeField] protected CustomEvent onEndGame;
    public float maxMana = 10;
    public float currentMana { get; private set; }

    private bool _ended;

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

    private void OnEnable()
    {
        RegisterAsListener();
    }

    private void OnDisable()
    {
        UnregisterAsListener();
    }

    public void RegisterAsListener()
    {
        onEndGame?.RegisterListener(this);
    }

    public void UnregisterAsListener()
    {
        onEndGame?.UnregisterListener(this);
    }

    public void OnEventRaised(CustomEvent customEvent, object param)
    {
        if (customEvent.Equals(onEndGame))
            StopManaFlow();
    }
}
