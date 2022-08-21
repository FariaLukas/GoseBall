using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public float random;
    public NavMeshAgent agent;
    public Vector3 _target;
    public Image lifeDisplay;
    
    private void Start()
    {
        SetTarget();
        Manager.Instance.OnEndGame += Stop;
    }

    private void Update()
    {
        lifeDisplay.fillAmount = Special.Instance.currentStarLife / Special.Instance.starMaxLife;
        if (agent.remainingDistance < 3)
        {
            SetTarget();
        }
    }

    private void SetTarget()
    {
        float zPos = Random.Range(-random, random);
        float xPos = Random.Range(-random, random);
        Vector3 target = new Vector3(xPos, transform.position.y, zPos);
        _target = target;
        agent.SetDestination(target);
    }

    private void Stop()
    {
        agent.speed = 0;
    }

    private void OnDestroy()
    {
        Manager.Instance.OnEndGame -= Stop;
    }

}
