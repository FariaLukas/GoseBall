using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
public class Manager : Singleton<Manager>
{
    public bool ballIsHolded;
    public string teamWithBall;
    public float maxPoints;
    public Image enemyBar;
    public Image allyBar;
    public GameObject loseScreen;
    public GameObject winScreen;

    private Rigidbody _ballRigidbody;
    private float _currentEnemyPoints;
    private float _currentAllyPoints;
    public Action OnEndGame;
    private GameObject _npc;

    private void Start()
    {
        allyBar.fillAmount = _currentAllyPoints / maxPoints;
        enemyBar.fillAmount = _currentEnemyPoints / maxPoints;
    }

    private void LateUpdate()
    {
        if (ballIsHolded && _npc == null)
        {
            DropBall();
        }
    }

    public void ChatchBall(GameObject ball, string team, GameObject npc)
    {
        _ballRigidbody = ball.GetComponent<Rigidbody>();
        _ballRigidbody.isKinematic = true;
        _ballRigidbody.velocity = Vector3.zero;
        ballIsHolded = true;
        teamWithBall = team;
        _npc = npc;
    }

    public void DropBall()
    {
        _ballRigidbody.isKinematic = false;
        _ballRigidbody.AddForce(_ballRigidbody.transform.forward * 200);
        ballIsHolded = false;
        teamWithBall = null;
        _npc = null;
    }

    public void FillBar(MoveTarget target, float multiplier)
    {
        if (target == MoveTarget.Ally)
        {
            if (_currentAllyPoints < maxPoints)
                _currentAllyPoints += Time.deltaTime * multiplier;

            allyBar.fillAmount = _currentAllyPoints / maxPoints;

        }

        if (target == MoveTarget.Enemy)
        {
            if (_currentEnemyPoints < maxPoints)
                _currentEnemyPoints += Time.deltaTime * multiplier;

            enemyBar.fillAmount = _currentEnemyPoints / maxPoints;
        }
        if (_currentEnemyPoints >= maxPoints || _currentAllyPoints >= maxPoints)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        OnEndGame?.Invoke();
        if (_currentEnemyPoints >= maxPoints || _currentEnemyPoints > _currentAllyPoints)
            loseScreen.SetActive(true);
        if (_currentAllyPoints >= maxPoints || _currentEnemyPoints < _currentAllyPoints)
            winScreen.SetActive(true);

    }
}
