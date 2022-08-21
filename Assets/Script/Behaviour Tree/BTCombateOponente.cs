using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCombateOponente : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject oponente = null;
        GameObject[] oponentes = GameObject.FindGameObjectsWithTag("NPC");
        float distancia = Mathf.Infinity;

        foreach (GameObject op in oponentes)
        {
            if (bt.gameObject == op) continue;
            if (Vector3.Distance(bt.transform.position, op.transform.position) < distancia)
            {
                oponente = op;
                distancia = Vector3.Distance(bt.transform.position, op.transform.position);
            }
        }

        if (oponente)
        {
            bt.transform.LookAt(oponente.transform);
            GameObject prefab = bt.GetComponent<NPC>().atributos.projetil;
            float speed = bt.GetComponent<NPC>().atributos.projetilVel;
            Vector3 posicao = bt.transform.position + bt.transform.forward;

            GameObject tiro = GameObject.Instantiate(prefab, posicao, Quaternion.identity);
            
            tiro.GetComponent<Rigidbody>().AddForce(bt.transform.forward * speed);
            GameObject.Destroy(tiro, 5);

            status = Status.SUCCESS;

        }
        else
        {
            status = Status.FAILURE;
        }

        Print();

        yield break;

    }
}
