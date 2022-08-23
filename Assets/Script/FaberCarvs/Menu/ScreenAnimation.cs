using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ScreenAnimation : MonoBehaviour
{
    public Direction direction;
    public float duration = 0.2f;
    public Ease easeIn = Ease.OutBack;
    public Ease easeOut = Ease.InBack;
    public Image image;

    public UnityEvent OnCompleteIn;
    public UnityEvent OnCompleteOut;
    private float _width;
    private float _height;

    private void OnEnable()
    {
        _width = image.GetComponent<RectTransform>().rect.width;
        _height = image.GetComponent<RectTransform>().rect.height;
    }

    public void MoveIn()
    {

        image.gameObject.SetActive(true);
        switch (direction)
        {
            case Direction.Top:
                image.transform.DOLocalMoveY(_height, duration).SetEase(easeIn).From().OnComplete(() => CompleteIn());
                break;
            case Direction.Bottom:
                image.transform.DOLocalMoveY(-_height, duration).SetEase(easeIn).From().OnComplete(() => CompleteIn());
                break;
            case Direction.Left:
                image.transform.DOLocalMoveX(-_width, duration).SetEase(easeIn).From().OnComplete(() => CompleteIn());
                break;
            case Direction.Rigt:
                image.transform.DOLocalMoveX(_width, duration).SetEase(easeIn).From().OnComplete(() => CompleteIn());
                break;
        }
    }

    public void MoveOut()
    {
        switch (direction)
        {
            case Direction.Top:
                image.transform.DOLocalMoveY(_height, duration).SetEase(easeOut).OnComplete(() => ResetPos());
                break;
            case Direction.Bottom:
                image.transform.DOLocalMoveY(-_height, duration).SetEase(easeOut).OnComplete(() => ResetPos());
                break;
            case Direction.Left:
                image.transform.DOLocalMoveX(-_width, duration).SetEase(easeOut).OnComplete(() => ResetPos());
                break;
            case Direction.Rigt:
                image.transform.DOLocalMoveX(_width, duration).SetEase(easeOut).OnComplete(() => ResetPos());
                break;
        }
    }

    private void ResetPos()
    {
        OnCompleteOut?.Invoke();
        image.transform.DOLocalMove(Vector3.zero, 0);
        image.gameObject.SetActive(false);
    }

    public void CompleteIn()
    {
        OnCompleteIn?.Invoke();
    }


}
public enum Direction
{
    Top,
    Bottom,
    Left,
    Rigt
}