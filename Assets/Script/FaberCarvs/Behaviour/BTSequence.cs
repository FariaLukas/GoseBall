using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();
        foreach (BTNode node in children)
        {
            yield return bt.StartCoroutine(node.Run(bt));
            if (node.status == Status.FAILURE)
            {
                status = Status.FAILURE;
                break;
            }

        }

        if (status == Status.RUNNING) status = Status.SUCCESS;
        Print();
    }
}


