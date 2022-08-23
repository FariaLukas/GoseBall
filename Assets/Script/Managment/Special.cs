using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class Special : Singleton<Special>, IListener
{
    public Star currentStar { get; private set; }
    [Title("Events")]
    [SerializeField] private CustomEvent onEndGame;
    [SerializeField] private CustomEvent onStunStarted, onStunFinished;

    public Action<string> OnStun;
    public Action<string> OnStunFinished;

    [Header("Spawn")]
    public GameObject starPFB;
    public float arenaSize;

    [SerializeField] public Vector2 timeToSpawn;

    [Header("Stun")]
    public AudioClip stun;
    [SerializeField] private float stunnedTime;


    [ReadOnly]
    [ShowInInspector]
    private float _timer;
    private bool _canSpawn = true;
    private string _teamStuned;

    private void Start()
    {
        _timer = Random();
    }

    private void Update()
    {
        if (_timer < 0)
        {
            if (_canSpawn)
                Spawn();
            return;
        }
        _timer -= Time.deltaTime;
    }

    public void TakeDamage(string enemyTeam)
    {
        currentStar.health.TakeDamage(1);

        if (currentStar.health.currentLife <= 0)
        {
            _teamStuned = enemyTeam;
            StartCoroutine(Stun());
        }
    }

    private void End()
    {
        _canSpawn = false;
        StopAllCoroutines();
    }

    private void Spawn()
    {
        _canSpawn = false;
        GameObject star = Instantiate(starPFB, GetNewPosition(), Quaternion.identity);
        currentStar = star.GetComponent<Star>();
    }

    private Vector3 GetNewPosition()
    {
        Vector2 area = UnityEngine.Random.insideUnitCircle;
        return new Vector3(area.x * arenaSize, 2, area.y * arenaSize);
    }

    private float Random()
    {
        return UnityEngine.Random.Range(timeToSpawn.x, timeToSpawn.y);
    }

    private IEnumerator Stun()
    {
        onStunStarted.Raise(_teamStuned);
        
        SoundManager.Instance?.PlaySfx(1, .5f, stun);
        
        yield return new WaitForSeconds(stunnedTime);
        
        onStunFinished.Raise(_teamStuned);
        
        _teamStuned = null;
        _timer = Random();
        _canSpawn = true;
    }

    private void OnEnable()
    {
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
            End();
    }
}
