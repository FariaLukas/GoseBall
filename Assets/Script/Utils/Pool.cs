using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;

    private List<GameObject> _pooledObjects;

    public void WarmPool()
    {
        if (!_prefab) return;

        _pooledObjects = new List<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            AddGameObject();
        }

    }

    private GameObject AddGameObject()
    {
        if (!_prefab) return null;

        GameObject newGO = (GameObject)Instantiate(_prefab);
        newGO.SetActive(false);

        newGO.transform.SetParent(transform);

        newGO.name = _prefab.name + "-" + _pooledObjects.Count;

        _pooledObjects.Add(newGO);

        return newGO;
    }

    public GameObject GetPooledGameObject()
    {
        foreach (var g in _pooledObjects)
        {
            if (!g.activeInHierarchy)
                return g;
        }

        return AddGameObject();
    }

    public void DestroyOne(GameObject destroied)
    {
        if (_pooledObjects == null) return;

        _pooledObjects.Find(g => g == destroied).SetActive(false);
    }

}

