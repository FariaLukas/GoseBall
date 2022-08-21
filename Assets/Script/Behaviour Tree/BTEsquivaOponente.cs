using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEsquivaOponente : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        SOAtributos atributos = bt.GetComponent<NPC>().atributos;

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
            float tempo = Random.Range(0.5f, 1f);
            float sinal = Mathf.Sign(Random.Range(-1f, 1f));

            while (tempo > 0)
            {
                tempo -= Time.deltaTime;

                if (oponente == null)
                {
                    status = Status.FAILURE;
                    break;
                }

                bt.transform.LookAt(oponente.transform);
                bt.transform.Translate(Vector3.right * sinal * atributos.velocidade * Time.deltaTime);
                yield return null;
            }
            if (status == Status.RUNNING)
                status = Status.SUCCESS;
        }
        else
        {
            status = Status.FAILURE;
        }

        Print();

    }
}
