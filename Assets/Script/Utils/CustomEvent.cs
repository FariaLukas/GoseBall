using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Custom Event", menuName = "TCC/Custom Event")]
public class CustomEvent : ScriptableObject
{
    public bool hasDebug;

    private List<IListener> _listeners =
        new List<IListener>();


    public void Raise(object param)
    {
        Debugging();
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            if (_listeners[i] == null) continue;
            _listeners[i].OnEventRaised(this, param);
        }

    }

    [Button]
    public void Raise()
    {
        Raise(null);
    }

    private void Debugging()
    {
        if (!hasDebug) return;

        if (_listeners.Count == 0)
        {
            Debug.LogWarning("There are no registered listeners to the: " + this.name + " event");
        }

        _listeners.ForEach(l => Debug.Log(l.ToString()));
    }

    public void RegisterListener(IListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(IListener listener)
    {
        _listeners.Remove(listener);
    }
}
