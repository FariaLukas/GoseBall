using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : CharacterBase
{
    public int heal;
    public GameObject allyToHeal;

    protected override void Init()
    {
        base.Init();

        parallel.children.Add(new BTSearchObject());
        parallel.children.Add(new BTMoveToHeal(atributes.targets[1], atributes.range - .5f));

        BTInversor inversor = new BTInversor();

        inversor.children.Add(new BTSearchObject());

        movement.children.Add(inversor);
        movement.children.Add(parallel);
        movement.children.Add(new BTHealAlly());

        combat.children.Add(new BTSearchObject());
        combat.children.Add(new BTMoveToObject(atributes.targets[0], atributes.range - .5f));
        combat.children.Add(new BTAttackStar());

        AddRoot();
    }

    public override void Attack(GameObject enemy)
    {
        if (!enemy.TryGetComponent(out Health enemyHealth)) return;

        base.Attack(enemy);

        GameObject pfb = GameObject.Instantiate(atributes.attackPFB, enemy.transform.position,
        atributes.attackPFB.transform.rotation);
        
        GameObject.Destroy(pfb, 3);

        enemyHealth.Heal(heal);

    }
}
