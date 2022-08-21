using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    public Transform item1, item2, item3;
    void Start()

    {

        Vector2 pos1 = new Vector2(3, 7);

        Vector2 pos2 = new Vector2(1, 5);

        item3.transform.position = pos1 - pos2;

        item2.transform.position = pos1 + pos2;

        item1.transform.position = pos2;

        float valor = item3.transform.position.x + item3.transform.position.y;

        print(valor);

    }
    // private void Update()
    // {
    //     Vector3 angle = item3.position - item1.position;
    //     float a = Vector3.Angle(item3.position, item1.forward);
    //     float b = Vector3.Angle(angle, item1.forward);
    //     print(a + " : " + b);
    // }
}
// public class BTInimigoAvistado : BTNode
// {
//     enum Status { SUCCESS, FAILURE, RUNNING }
//     Status status;

//     IEnumerator Run(GameObject npc)
//     {
//         status = Status.FAILURE;

//         GameObject[] armadilhas = GameObject.FindGameObjectWithTag("Armadilha");
//         float angulo = 70;

//         for (int i = 0; i < armadilhas.Length; i++)
//         {
//             if (Vector3.Angle(armadilhas[i].transform.position, npc.transform.forward) < angulo)
//             {
//                 status = Status.SUCCESS;
//                 yield break;
//             }
//         }
//         yield break;
//     }
// }
// public class BTMedicamentoEncontrado : BTNode
// {
//     enum Status { SUCCESS, FAILURE, RUNNING }
//     Status status;

//     IEnumerator Run(GameObject npc)
//     {
//         status = Status.FAILURE;

//         GameObject[] medicamentos = GameObject.FindGameObjectsWithTag("Medicamento");

//         foreach (var m in medicamentos)
//         {
//             if (Vector3.Angle(m.transform.position, npc.transform.forward) < 45)
//             {
//                 status = Status.SUCCESS;
//                 yield break;
//             }
//         }

//         foreach (GameObject t in trap)
//         {
//             if (Vector3.Angle(t.transform.position, npc.transform.forward) < 70f)
//             {
//                 status = Status.SUCCESS;
//                 yield break;
//             }
//         }
//         yield break;
//     }
// }