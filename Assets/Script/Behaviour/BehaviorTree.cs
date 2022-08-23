using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    public BTNode root;

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        StartCoroutine(Execute());
    }
    public void StopAll()
    {
        StopAllCoroutines();
        StopCoroutine(Execute());
    }

    IEnumerator Execute()
    {
        while (true)
        {
            if (root != null) yield return StartCoroutine(root.Run(this));
            else yield return null;
        }
    }
}
