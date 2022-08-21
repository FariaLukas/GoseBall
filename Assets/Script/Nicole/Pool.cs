using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;

    private List<GameObject> _pooledObjects;

    private void Awake()
    {
        Instance = this;
        WarmPool();
    }

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

    public void DestroyOne()
    {
        if (_pooledObjects != null)
            foreach (var g in _pooledObjects)
            {
                if (g.activeInHierarchy)
                {
                    g.SetActive(false);
                    return;
                }
            }
    }


}

