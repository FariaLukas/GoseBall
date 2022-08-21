using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class Special : Singleton<Special>
{
    public GameObject starPFB;
    public float starMaxLife;
    public Vector2 timeToSpawn;
    public float arenaSize;
    public float stunnedTime;
    public float currentStarLife;
    public AudioClip stun;
    private string _teamStuned;
    [ReadOnly]
    [ShowInInspector]
    private float _timer;
    private bool _canSpawn = true;
    public Action<string> OnStun;
    public Action<string> OnStunFinished;
    private GameObject _currentStar;

    private void Start()
    {
        _timer = Random();
        Manager.Instance.OnEndGame += End;
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
        currentStarLife--;

        if (currentStarLife <= 0)
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
        currentStarLife = starMaxLife;
        Vector2 area = UnityEngine.Random.insideUnitCircle;
        Vector3 position = new Vector3(area.x * arenaSize, 2, area.y * arenaSize);
        GameObject star = Instantiate(starPFB, position, Quaternion.identity);
        _currentStar = star;
    }

    private float Random()
    {
        return UnityEngine.Random.Range(timeToSpawn.x, timeToSpawn.y);
    }

    IEnumerator Stun()
    {
        OnStun?.Invoke(_teamStuned);
        Destroy(_currentStar);
        SoundManager.Instance?.PlaySfx(1, .5f, stun);
        yield return new WaitForSeconds(stunnedTime);
        OnStunFinished.Invoke(_teamStuned);
        _teamStuned = null;
        _timer = Random();
        _canSpawn = true;
    }

}
