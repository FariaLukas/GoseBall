using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Star : MonoBehaviour, IListener
{
    [SerializeField] private CustomEvent onEndGame;
    [SerializeField] private int maxLife = 5;
    public Health health;
    public float random;
    public NavMeshAgent agent;
    public Vector3 _target;

    private void Start()
    {
        SetTarget();
    }

    private void Update()
    {
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

    private void OnEnable()
    {
        health.SetupLife(maxLife);
        RegisterAsListener();
    }

    private void OnDisable()
    {
        UnregisterAsListener();
    }

    public void RegisterAsListener()
    {
        onEndGame?.RegisterListener(this);
    }

    public void UnregisterAsListener()
    {
        onEndGame?.UnregisterListener(this);
    }

    public void OnEventRaised(CustomEvent customEvent, object param)
    {
        if (customEvent.Equals(onEndGame))
            Stop();
    }

}
