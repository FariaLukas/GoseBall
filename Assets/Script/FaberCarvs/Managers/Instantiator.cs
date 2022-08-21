using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Instantiator : MonoBehaviour
{
    [Title("Setup")]
    public List<UnitSetup> units;

    [Title("Mana")]
    public float maxMana;
    public float rangeX;
    public Vector2 rangeZ;
    public float coolDown;

    [ShowInInspector]
    private float _currentMana;
    private bool ended;
    private void Start()
    {
        Manager.Instance.OnEndGame += End;
        StartCoroutine(Create());
    }


    private void Update()
    {
        if (ended) return;
        if (_currentMana > maxMana)
        {
            _currentMana = maxMana;
            return;
        }

        _currentMana += Time.deltaTime;

    }

    IEnumerator Create()
    {

        while (true)
        {
            yield return new WaitForSeconds(coolDown);

            if (ended) break;
            int index = Random.Range(0, 3);

            Vector2 pos2 = new Vector2(Random.Range(-rangeX, rangeX), Random.Range(rangeZ.x, rangeZ.y));

            if (units[index].manaCost <= _currentMana)
            {
                Instantiate(units[index].unityPFB, new Vector3(pos2.x, transform.position.y, pos2.y), Quaternion.identity);
            }
        }
        yield break;
    }

    private void End()
    {
        ended = true;
        StopAllCoroutines();
    }
}
