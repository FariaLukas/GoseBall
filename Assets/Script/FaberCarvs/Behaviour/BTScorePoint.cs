using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTScorePoint : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();
        CharacterBase characterBase = bt.GetComponent<CharacterBase>();
        Rigidbody body = bt.GetComponent<Rigidbody>();
        Dictionary<string, MoveTarget> target = new Dictionary<string, MoveTarget>{
            {"Ally",MoveTarget.Ally},
            {"Enemy",MoveTarget.Enemy}
        };
        AnimationManager.Instance.SetTrigger(characterBase.animator, "Scoring");
        while (true)
        {
            body.isKinematic = true;
            Manager.Instance.FillBar(target[characterBase.atributes.allyLabel], 1.5f);
            yield return null;
        }

    }
}
