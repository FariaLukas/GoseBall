using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTemBola : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        string tag = bt.GetComponent<NPC>().atributos.lable;
        if (GameObject.FindGameObjectWithTag(tag))
        {
            status = Status.SUCCESS;
            Print();
        }
        else
        {
            status = Status.FAILURE;
            Print();
        }

        yield break;

    }
}
