using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTColetaBola : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.FAILURE;
        Print();
        SOAtributos atributos = bt.GetComponent<NPC>().atributos;

        Transform npc = bt.transform;
        GameObject[] bolas = GameObject.FindGameObjectsWithTag(atributos.lable);
        foreach (GameObject bola in bolas)
        {
            if (Vector3.Distance(npc.position, bola.transform.position) < 1)
            {
                GameObject.Destroy(bola);
                status = Status.SUCCESS;
                Print();
            }
        }
        yield break;
    }
}
