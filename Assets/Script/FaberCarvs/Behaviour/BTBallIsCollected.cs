using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBallIsCollected : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.FAILURE;

        if (Manager.Instance.ballIsHolded)
        {
            status = Status.SUCCESS;
        }

        yield break;
    }
}
