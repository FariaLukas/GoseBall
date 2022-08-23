using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class MoveY : MonoBehaviour
{
    public float height;
    public float duration;
    public Ease ease;
    void Start()
    {
        transform.DOLocalMoveY(height, duration).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }


}
