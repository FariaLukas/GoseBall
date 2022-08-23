using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SearchTarget
{
    Ball,
    Star
}

public class BTSearchObject : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        CharacterBase characterBase = bt.GetComponent<CharacterBase>();
        string tag = characterBase.atributes.searchTarget.ToString();

        if (SceneObjects.Instance.GetObjectWithTag(tag))
        {
            status = Status.SUCCESS;
            Print();
            bt.GetComponent<CharacterBase>().currentState = State();
        }
        else
        {
            status = Status.FAILURE;
            Print();
            bt.GetComponent<CharacterBase>().currentState = State();
        }

        yield break;

    }
}
