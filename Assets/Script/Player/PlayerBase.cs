using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IListener
{
    [SerializeField] protected CustomEvent onEndGame;
    [Title("Setup")]
    [SerializeField] protected List<UnitSetup> units;
    public ManaCounter manaCounter;
    [SerializeField] protected bool debug;

    protected virtual void Update()
    {
        units.ForEach(u => u.counter.Update());
    }

    protected virtual void Spawn(int index, Vector3 position)
    {
        if (!CanSpawn(index)) return;

        Instantiate(units[index].unityPFB, position, Quaternion.identity);

        manaCounter.UseMana(units[index].manaCost);

        units[index].counter.StartCooldown();
    }

    protected virtual bool CanSpawn(int index)
    {
        if (debug)
            Debug.Log(units[index].manaCost + " : " + manaCounter.currentMana + " > " + units[index].counter.onCooldown + " : " + index);
        return units[index].manaCost <= manaCounter.currentMana &&
        !units[index].counter.onCooldown;
    }

    protected virtual void End()
    {

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
            End();
    }
}

[System.Serializable]
public class UnitSetup
{
    public GameObject unityPFB;
    public float manaCost;
    public CooldownCounter counter;
}