using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AppleFxEntity : PoolableUIEntity
{
    [SerializeField] private Image apple;
    private Tween _moveTween;
    private Tween _scaleTween;

    public override void OnSpawned()
    {
        base.OnSpawned();
        CheckTween();
    }

    public void SetRectParams(Vector3 localPosition, Vector2 sizeDelta, Transform parent)
    {
        apple.rectTransform.SetParent(parent);
        apple.rectTransform.localPosition = localPosition;
        apple.rectTransform.sizeDelta = sizeDelta;
    }

    public void Play(RectTransform target, float duration, Ease ease, TweenCallback onComplete)
    {
        CheckTween();
        
        _moveTween = apple.rectTransform.DOMove(target.position, duration).SetEase(ease);
        float sizeAnimDuration = duration - duration * 0.7f;
        float sizeAnimDelay = sizeAnimDuration * 3;
        _scaleTween = apple.rectTransform.DOSizeDelta(Vector2.zero, sizeAnimDuration).SetDelay(sizeAnimDelay);
        _scaleTween.onComplete += onComplete;
    }

    private void CheckTween()
    {
        if (_moveTween == null && _scaleTween == null) return;

        _moveTween = null;
        _scaleTween = null;
    }
}
