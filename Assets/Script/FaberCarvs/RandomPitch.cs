using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    public Vector2 pitch;
    private AudioSource source;
    public bool playOnEnable = true;

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
        if (!source)
            source = gameObject.AddComponent<AudioSource>();
        source.mute = true;
        if (playOnEnable)
        {
            float random = Random.Range(pitch.x, pitch.y);
            SoundManager.Instance?.PlaySfx(random, source.volume, source.clip);
        }
    }
    public void Play()
    {
        float random = Random.Range(pitch.x, pitch.y);
        SoundManager.Instance?.PlaySfx(random, source.volume, source.clip);
    }
}
