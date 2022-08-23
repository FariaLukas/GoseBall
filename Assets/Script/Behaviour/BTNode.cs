using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode
{
    public enum Status { RUNNING, SUCCESS, FAILURE }
    public Status status;
    public List<BTNode> children = new List<BTNode>();

    public abstract IEnumerator Run(BehaviorTree bt);
    private CharacterBase character;

    public void Print(string texto = "")
    {
        // string cor = "lightblue";
        // if (status == Status.SUCCESS) cor = "green";
        // if (status == Status.FAILURE) cor = "orange";

        //        Debug.Log("<color=" + cor + ">" + this.ToString()
        //  + " - " + status.ToString() + " : " + texto + "</color>");
    }

    public string State(string text = "")
    {
        return this.ToString() + " - " + status.ToString() + " : " + text;
    }

}
