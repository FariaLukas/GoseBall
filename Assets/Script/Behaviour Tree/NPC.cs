using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public SOAtributos atributos;
    BehaviorTree behavior;


    private void Start()
    {
        GetComponent<Renderer>().material.color = atributos.color;

        behavior = gameObject.AddComponent<BehaviorTree>();

        BTSequence combate = new BTSequence();
        combate.children.Add(new BTVeOponente());
        combate.children.Add(new BTCombateOponente());
        combate.children.Add(new BTEsquivaOponente());

        BTSelectorParalelo paralelo = new BTSelectorParalelo();
        paralelo.children.Add(new BTVeOponente());
        paralelo.children.Add(new BTMoveAteBola());

        BTSequence coleta = new BTSequence();
        coleta.children.Add(new BTTemBola());
        coleta.children.Add(paralelo);
        coleta.children.Add(new BTColetaBola());

        BTSelector selector = new BTSelector();
        selector.children.Add(combate);
        selector.children.Add(coleta);

        behavior.root = selector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projetil"))
        {
            Destroy(other);
            Destroy(gameObject);
        }
    }
}
