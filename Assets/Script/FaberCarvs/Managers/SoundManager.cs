using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class SoundManager : Singleton<SoundManager>
{
    private List<AudioSource> sources;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        sources = GetComponents<AudioSource>().ToList();
    }

    public void PlaySfx(float pitch, float volume, AudioClip clip)
    {
        foreach (var s in sources)
        {
            if (s.isPlaying) continue;
            if (!s.isPlaying)
            {
                s.volume = volume;
                s.pitch = pitch;
                if (clip != null)
                    s.PlayOneShot(clip);
                return;
            }
        }

        AudioSource source = gameObject.AddComponent<AudioSource>();
        sources.Add(source);
        source.volume = volume;
        source.pitch = pitch;
        if (clip != null)
            source.PlayOneShot(clip);
    }


}


