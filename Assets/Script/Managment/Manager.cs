using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Manager : Singleton<Manager>
{
    public bool ballIsHolded;
    public string teamWithBall;
    public float maxPoints;
    public Image allyBar, enemyBar;

    public GameObject loseScreen;
    public GameObject winScreen;

    private Rigidbody _ballRigidbody;
    public CustomEvent onEndGame;
    public Action OnEndGame;
    private GameObject _npc;
    private bool _gameEnded;

    private Dictionary<MoveTarget, ScoreSetup> _scores
    = new Dictionary<MoveTarget, ScoreSetup>();

    private void Start()
    {
        _scores.Add(MoveTarget.Enemy, new ScoreSetup(enemyBar));
        _scores.Add(MoveTarget.Ally, new ScoreSetup(allyBar));

        foreach (KeyValuePair<MoveTarget, ScoreSetup> s in _scores)
        {
            s.Value.SetupBar(maxPoints);
        }
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
        var score = _scores[target];

        if (score.score < maxPoints)
            score.score += Time.deltaTime * multiplier;

        score.SetupBar(maxPoints);

        if (score.score >= maxPoints)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        if (_gameEnded) return;

        onEndGame.Raise();

        if (_scores[MoveTarget.Enemy].score >= maxPoints ||
        _scores[MoveTarget.Enemy].score > _scores[MoveTarget.Ally].score)
            loseScreen.SetActive(true);
        else
            winScreen.SetActive(true);

        _gameEnded = true;
    }
}

public class ScoreSetup
{
    public Image bar;
    public float score;

    public ScoreSetup(Image bar)
    {
        this.bar = bar;
    }

    public void SetupBar(float maxValue)
    {
        bar.fillAmount = score / maxValue;
    }
}