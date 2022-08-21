using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTVeOponente : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.FAILURE;

       SOAtributos atributos = bt.GetComponent<NPC>().atributos;
        GameObject[] oponentes = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject oponente in oponentes)
        {
            if (bt.gameObject == oponente) continue;
            if (Vector3.Distance(bt.transform.position, oponente.transform.position) < atributos.alcance)
            {
                status = Status.SUCCESS;
                break;
            }
        }

        Print();
        yield break;
    }
}
