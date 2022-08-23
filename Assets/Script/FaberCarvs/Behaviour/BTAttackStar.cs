using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BTAttackStar : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.FAILURE;

        Support support = bt.GetComponent<Support>();

        var star = Special.Instance.currentStar;

        if (!star) yield break;

        bt.transform.DOLookAt(star.transform.position, 0.1f);

        AnimationManager.Instance.SetTrigger(support.animator, "Attack");

        yield return new WaitForSeconds(support.atributes.attackDelay);

        if (!star) yield break;

        Special.Instance.TakeDamage(support.atributes.enemyLabel);
        GameObject pfb = GameObject.Instantiate(support.atributes.attackPFB, star.transform.position, support.atributes.attackPFB.transform.rotation);
        GameObject.Destroy(pfb, 3);
        status = Status.SUCCESS;
        support.currentState = State();


        yield return new WaitForSeconds(1.2f);
        yield break;
    }
}
