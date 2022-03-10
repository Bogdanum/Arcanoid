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

    public void DoScale(Vector3 endValue, float duration)
    {
        CheckTween();
        _tween = transform.DOScale(endValue, duration);
    }

    private void CheckTween()
    {
        if (_tween != null)
        {
            _tween.Kill(false);
        }
    }
}