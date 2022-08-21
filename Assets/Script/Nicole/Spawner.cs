using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnFrequency = 2;
    [SerializeField] private float _spawnRadius = 10;
    [SerializeField] private bool useCoroutine;
    private int _x = 0, _y = 1;
    private int _fibonacci;
    private int currentValue = 0;

    private void Start()
    {

        if (useCoroutine)
            StartCoroutine(WaitToSpawn());
        else
            InvokeRepeating(nameof(AddValue), 0, _spawnFrequency);
    }

    private void AddValue()
    {
        _fibonacci = _x + _y;
        _y = _x;
        _x = _fibonacci;

        Debug.Log($"{_fibonacci}");
        
        currentValue = 0;
        SpawnObj();
    }

    private void SpawnObj()
    {
        currentValue ++;

        if (currentValue < _fibonacci)
        {
            Vector2 area = Random.insideUnitCircle * _spawnRadius;
            Vector3 position = new Vector3(area.x, 2, area.y);

            GameObject spawned = Pool.Instance?.GetPooledGameObject();

            spawned.transform.position = position;
            spawned.SetActive(true);

            SpawnObj();
        }

    }

    IEnumerator Spawn()
    {

        AddValue();

        yield return null;
        StartCoroutine(WaitToSpawn());
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(_spawnFrequency);
        StartCoroutine(Spawn());
    }

}
