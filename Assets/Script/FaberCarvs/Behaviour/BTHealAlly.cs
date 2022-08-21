using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BTHealAlly : BTNode
{

    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.FAILURE;
        Support support = bt.GetComponent<Support>();

        Transform character = bt.transform;
        GameObject ally = support.allyToHeal;

        if (ally)
        {
            if (ally.TryGetComponent(out CharacterBase characterBase))
            {
                AnimationManager.Instance.SetTrigger(support.animator, "Attack");

                yield return new WaitForSeconds(support.atributes.attackSync);
                if (ally)
                {
                    bt.transform.DOLookAt(ally.transform.position, 0.1f);

                    GameObject pfb = GameObject.Instantiate(support.atributes.attackPFB, ally.transform.position, support.atributes.attackPFB.transform.rotation);
                    GameObject.Destroy(pfb, 3);
                    characterBase.Heal(support.heal);
                    status = Status.SUCCESS;
                }
            }

            yield return new WaitForSeconds(support.atributes.attackDelay);
            if (ally)
                Print(bt.gameObject.name + " : " + Vector3.Distance(character.position, ally.transform.position).ToString() + " : " + ally.name);
            support.currentState = State();
        }
        yield break;
    }
}
