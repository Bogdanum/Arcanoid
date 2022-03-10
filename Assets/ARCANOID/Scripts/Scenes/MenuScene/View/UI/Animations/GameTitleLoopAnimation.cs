using DG.Tweening;
using UnityEngine;

public class GameTitleLoopAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 minPosition;
    [SerializeField] private Vector3 maxPosition;
    [SerializeField] private float moveTime;
    [SerializeField] private Ease ease;
    private Tween _tween;
    private bool _isPause = true;

    public void Play()
    {
        _tween = null;
        _isPause = false;
        StartLoopAnim();
    }

    public void Stop()
    {
        _tween?.Kill();
        _tween = null;
        _isPause = true;
    }

    private void StartLoopAnim()
    {
        if (_tween != null || _isPause) return;

        transform.localPosition = minPosition;
        MinMaxMove();
    }

    private void MinMaxMove()
    {
        _tween = transform.DOLocalMove(maxPosition, moveTime).SetEase(ease);
        _tween.OnComplete(() =>
        {
            _tween = null;
            MaxMinMove();
        });
    }

    private void MaxMinMove()
    {
        _tween = transform.DOLocalMove(minPosition, moveTime).SetEase(ease);
        _tween.OnComplete(() =>
        {
            _tween = null;
            StartLoopAnim();
        });
    }
}
