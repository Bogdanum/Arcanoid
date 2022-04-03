using DG.Tweening;
using UnityEngine;

public class TweenScaler : MonoBehaviour
{
    private Tween _tween;

    private void OnDestroy()
    {
        CheckTween();
    }

    public void DoScale(Vector3 endValue, float duration, TweenCallback onEnd)
    {
        CheckTween();
        _tween = transform.DOScale(endValue, duration);
        _tween.onComplete += onEnd;
    }

    public void DoScale(Vector3 endValue, float duration, float delay = 0, Ease ease = Ease.Unset)
    {
        CheckTween();
        _tween = transform.DOScale(endValue, duration).SetDelay(delay).SetEase(ease);
    }

    public void Stop() => CheckTween();

    private void CheckTween()
    {
        if (_tween != null)
        {
            _tween.Kill();
        }
    }
}