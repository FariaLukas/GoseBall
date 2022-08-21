using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMoveAteBola : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;

        Print();

        SOAtributos atributos = bt.GetComponent<NPC>().atributos;

        Transform npc = bt.transform;
        Transform bola = null;
        GameObject[] bolas = GameObject.FindGameObjectsWithTag(atributos.lable);
        float distancia = Mathf.Infinity;

        foreach (GameObject bo in bolas)
        {
            if (Vector3.Distance(npc.position, bo.transform.position) < distancia)
            {
                bola = bo.transform;
                distancia = Vector3.Distance(npc.position, bo.transform.position);
            }
        }

        while (true)
        {
            if (!bola)
            {
                status = Status.FAILURE;
                break;
            }

            if (Vector3.Distance(npc.position, bola.position) < 1) break;

            npc.LookAt(bola);
            npc.position += npc.forward * atributos.velocidade * Time.deltaTime;
            yield return null;
        }

        if (status == Status.RUNNING)
            status = Status.SUCCESS;

        Print();
        yield break;

    }
   
}
