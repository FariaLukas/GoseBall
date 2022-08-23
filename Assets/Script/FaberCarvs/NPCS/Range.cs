using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : CharacterBase
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

        BTSelectorParalelo paralelo = new BTSelectorParalelo();
        BTInversor inversor = new BTInversor();

        inversor.children.Add(new BTBallIsCollected());

        paralelo.children.Add(inversor);
        //TODO:Mudar apenas no inspector
        paralelo.children.Add(new BTMoveToObject(atributes.targets[2], atributes.range - 2f));

        combat.children.Add(paralelo);
        combat.children.Add(new BTEnemyInRange());
        combat.children.Add(new BTAttackEnemy());


        AddRoot();
    }
}
