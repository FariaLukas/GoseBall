using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEnemyInRange : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.FAILURE;

        CharacterAtributes atributes = bt.GetComponent<CharacterBase>().atributes;
        string label = atributes.enemyLabel;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(label);
        foreach (GameObject en in enemies)
        {
            if (bt.gameObject == en) continue;
            if (Vector3.Distance(bt.transform.position, en.transform.position) < atributes.range)
            {
                status = Status.SUCCESS;
                break;
            }
        }

        Print(bt.gameObject.name);
        bt.GetComponent<CharacterBase>().currentState = State();
        yield break;
    }
}
