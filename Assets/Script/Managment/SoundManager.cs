using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class SoundManager : Singleton<SoundManager>
{
    private List<AudioSource> _sources;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        _sources = GetComponents<AudioSource>().ToList();
    }

    public void PlaySfx(float pitch, float volume, AudioClip clip)
    {
        AudioSource source = GetAudioSource();
        source.mute = false;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        if (clip != null)
            source.PlayOneShot(clip);
    }

    private AudioSource GetAudioSource()
    {
        foreach (var s in _sources)
        {
            if (!s.isPlaying)
                return s;
        }

        AudioSource source = gameObject.AddComponent<AudioSource>();
        _sources.Add(source);
        return source;
    }

    public void StopSfx(AudioClip clip)
    {
        foreach (var s in _sources)
        {
            if (s != null && s.clip == clip)
                s.Stop();
        }
    }

}


