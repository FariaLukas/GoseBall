using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    public string label;

    private void Start()
    {
        SceneObjects.Instance.AddObject(label, gameObject);
    }

    private void OnDisable()
    {
        SceneObjects.Instance.RemoveObject(label, gameObject);
    }
}
