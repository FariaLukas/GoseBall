using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTParallelSelector : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        Dictionary<BTNode, Coroutine> coroutines = new Dictionary<BTNode, Coroutine>();

        foreach (BTNode node in children)
        {
            coroutines.Add(node, bt.StartCoroutine(node.Run(bt)));
        }

        while (status == Status.RUNNING)
        {
            status = Status.FAILURE;

            foreach (BTNode node in children)
            {
                if (node.status == Status.RUNNING)
                    status = Status.RUNNING;

                else if (node.status == Status.SUCCESS)
                {
                    status = Status.SUCCESS;
                    break;
                }
            }

            if (status == Status.RUNNING)
            {
                foreach (BTNode node in children)
                {
                    if (node.status == Status.FAILURE)
                        coroutines[node] = bt.StartCoroutine(node.Run(bt));
                }
            }
            else if (status == Status.SUCCESS)
            {
                foreach (var pair in coroutines)
                {
                    if (pair.Value != null)
                        bt.StopCoroutine(pair.Value);
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

        Print();
    }
}
