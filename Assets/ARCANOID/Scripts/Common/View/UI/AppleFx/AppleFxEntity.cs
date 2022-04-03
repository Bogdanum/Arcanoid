using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AppleFxEntity : PoolableUIEntity
{
    [SerializeField] private Image apple;
    private Tween _moveTween;
    private Tween _scaleTween;
    private Tween _rotationTween;

    public override void OnSpawned()
    {
        base.OnSpawned();
        CheckTween();
    }

    public void SetRectParams(Vector3 localPosition, Vector2 sizeDelta, Transform parent)
    {
        apple.rectTransform.SetParent(parent);
        apple.rectTransform.SetLocalPositionAndRotation(localPosition, Quaternion.identity);
        apple.rectTransform.sizeDelta = sizeDelta;
        
    }

    public void Play(RectTransform target, EnergyFxConfig config, TweenCallback onComplete)
    {
        CheckTween();

        float duration = config.EntityFlightDuration;
        RotationAnim(config.MaxRotationAngle, duration);
        _moveTween = apple.rectTransform.DOMove(target.position, duration).SetEase(config.MoveEaseType);
        float sizeAnimDuration = duration - duration * 0.7f;
        float sizeAnimDelay = sizeAnimDuration * 3;
        _scaleTween = apple.rectTransform.DOSizeDelta(Vector2.zero, sizeAnimDuration).SetDelay(sizeAnimDelay);
        _scaleTween.onComplete += onComplete;
    }

    private void RotationAnim(float angle, float duration)
    {
        var targetAngle = Random.Range(-angle, angle);
        _rotationTween = apple.rectTransform.DORotate(new Vector3(0, 0,  targetAngle), duration);
    }

    private void CheckTween()
    {
        if (_moveTween == null && _scaleTween == null && _rotationTween == null) return;

        _moveTween = null;
        _scaleTween = null;
        _rotationTween = null;
    }
}
