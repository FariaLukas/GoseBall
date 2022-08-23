using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
public class TimerManager : Singleton<TimerManager>
{

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float maxMatchTime = 20;
    [SerializeField] private AudioClip countdown;

    private float _timer;
    private bool _countdownStarted;

    private void Start()
    {
        _timer = maxMatchTime;
        Manager.Instance.OnEndGame += GameEnded;
    }


    private void OnDestroy()
    {
        Manager.Instance.OnEndGame -= GameEnded;
    }

    private void Update()
    {
        if (_timer <= 0) return;

        SetupUI();

        if (_timer <= 1)
        {
            _timer = 0;
            Manager.Instance.EndGame();

            return;
        }

        if (_timer <= 11 && !_countdownStarted)
        {
            _countdownStarted = true;
            SoundManager.Instance?.PlaySfx(1, 0.3f, countdown);
        }

        _timer -= Time.deltaTime;
    }

    private void SetupUI()
    {
        float minutes = Mathf.FloorToInt(_timer / 60);
        float seconds = Mathf.FloorToInt(_timer % 60);
        timerText.text = minutes.ToString("00") + ":"
        + seconds.ToString("00");
    }

    private void GameEnded()
    {
        _timer = 0;

        SoundManager.Instance?.StopSfx(countdown);
    }
}
