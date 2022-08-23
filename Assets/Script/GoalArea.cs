using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    public MoveTarget target;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            Manager.Instance.FillBar(target, 1);

        }
    }

}
