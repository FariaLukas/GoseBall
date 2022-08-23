using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : CharacterBase
{
    protected override void Init()
    {
        base.Init();

        parallel.children.Add(new BTBallIsCollected());
        parallel.children.Add(new BTMoveToObject(atributes.targets[0], 1));

        BTInversor inversor_ = new BTInversor();

        inversor_.children.Add(new BTBallIsCollected());

        movement.children.Add(inversor_);
        movement.children.Add(parallel);
        movement.children.Add(new BTCollectBall());
        movement.children.Add(new BTMoveToObject(atributes.targets[1], 1 - .3f));
        movement.children.Add(new BTScorePoint());

        BTParallelSelector paralelo = new BTParallelSelector();
        BTInversor inversor = new BTInversor();

        inversor.children.Add(new BTBallIsCollected());

        paralelo.children.Add(inversor);
        paralelo.children.Add(new BTMoveToObject(atributes.targets[2], atributes.movementRange));

        combat.children.Add(paralelo);
        combat.children.Add(new BTEnemyInRange());
        combat.children.Add(new BTAttackEnemy());

        AddRoot();
    }

    public override void Attack(GameObject enemy)
    {
        base.Attack(enemy);
        Vector3 position = transform.position + transform.forward;

        GameObject projectile = GameObject.Instantiate(atributes.attackPFB, position, transform.rotation);
        GameObject.Destroy(projectile, 5);

        if (atributes.characterType == CharacterType.Ranged)
        {
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * atributes.projectileSpeed);
        }
        else
        {
            enemy.GetComponent<Health>().TakeDamage(1);

        }
    }
}