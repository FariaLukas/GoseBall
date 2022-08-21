using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
public class TimerManager : Singleton<TimerManager>
{

    public TextMeshProUGUI timerText;
    public float maxMatchTime = 20;
    [HideInInspector]
    public float timer;
    public AudioClip countdown;
    private bool ended;
    private bool tenSec;
    private void Start()
    {
        timer = maxMatchTime;
        Manager.Instance.OnEndGame += GameEnded;
    }

    private void Update()
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = minutes.ToString("00") + ":";
        timerText.text = timerText.text + seconds.ToString("00");

        if (timer <= 1)
        {
            timer = 0;
            if (!ended)
            {
                Manager.Instance.EndGame();
                ended = true;
            }
            return;
        }
        if (timer <= 11 && !tenSec)
        {
            tenSec = true;
            SoundManager.Instance.PlaySfx(1, 0.3f, countdown);
        }
        timer -= Time.deltaTime;
    }
    private void GameEnded()
    {
        ended = true;
        timer = 0;
    }
}
