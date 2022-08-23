using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemySpawner : PlayerBase
{
    [Title("Spawn")]
    [SerializeField] private float rangeX;
    [SerializeField] private Vector2 rangeZ;
    [SerializeField] private float coolDown;

    private void Start()
    {
        Manager.Instance.OnEndGame += End;
        InvokeRepeating(nameof(CreateEnemy), coolDown, coolDown);
    }

    private void CreateEnemy()
    {
        int index = Random.Range(0, 3);
        Vector2 pos2 = new Vector2(Random.Range(-rangeX, rangeX), Random.Range(rangeZ.x, rangeZ.y));
        Spawn(index, new Vector3(pos2.x, transform.position.y, pos2.y));
    }

    private void End()
    {
        CancelInvoke();
    }
}
