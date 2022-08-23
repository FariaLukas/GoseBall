using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [Title("Setup")]
    [SerializeField] protected List<UnitSetup> units;
    public ManaCounter manaCounter;

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
        return units[index].manaCost <= manaCounter.currentMana && 
        !units[index].counter.onCooldown;
    }
}

[System.Serializable]
public class UnitSetup
{
    public GameObject unityPFB;
    public float manaCost;
    public CooldownCounter counter;
}