using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SceneObjects : Singleton<SceneObjects>
{
    [ShowInInspector]
    private Dictionary<string, List<GameObject>> _objectsInScene;

    protected override void Awake()
    {
        base.Awake();
        _objectsInScene = new Dictionary<string, List<GameObject>>();
    }

    public void AddObject(string key, GameObject value)
    {
        if (!_objectsInScene.ContainsKey(key))
        {
            _objectsInScene.Add(key, new List<GameObject>());
        }

        if (_objectsInScene[key].Contains(value)) return;

        _objectsInScene[key].Add(value);
    }

    public void RemoveObject(string key, GameObject value)
    {
        if (!_objectsInScene.ContainsKey(key)) return;

        if (!_objectsInScene[key].Contains(value)) return;

        _objectsInScene[key].Remove(value);
    }

    public List<GameObject> GetObjectsWithTag(string key)
    {
        if (!_objectsInScene.ContainsKey(key))
            return null;
        return _objectsInScene[key];
    }

    public GameObject GetObjectWithTag(string key)
    {
        if (!_objectsInScene.ContainsKey(key) ||
        _objectsInScene[key].Count == 0)
            return null;

        return _objectsInScene[key][0];
    }
}
