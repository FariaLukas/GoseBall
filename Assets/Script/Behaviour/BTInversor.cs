using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInversor : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {

        foreach (BTNode node in children)
        {
            yield return bt.StartCoroutine(node.Run(bt));
            if (node.status == Status.FAILURE)
            {
                status = Status.SUCCESS;
                break;
            }
            if (node.status == Status.SUCCESS)
            {
                status = Status.FAILURE;
                break;
            }


        }
    }
}
