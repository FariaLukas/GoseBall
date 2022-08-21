using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWithTime : MonoBehaviour
{
    [SerializeField] private float timeToDisable = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Disable), timeToDisable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
