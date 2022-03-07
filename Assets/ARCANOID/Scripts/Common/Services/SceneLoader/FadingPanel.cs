using System;
using DG.Tweening;
using UnityEngine;

public class FadingPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween _fadeTween;

    private void OnDestroy()
    {
        CheckTween();
    }

    public void FadeIn(float duration, Ease ease, float delay = 0, Action onComplete = null)
    {
        _fadeTween = canvasGroup.DOFade(1, duration).SetDelay(delay).SetEase(ease);
        _fadeTween.OnComplete(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            onComplete?.Invoke();
            _fadeTween.Kill(true);
        });
    }

    public void FadeOut(float duration, Ease ease, float delay = 0, Action onComplete = null)
    {
        _fadeTween = canvasGroup.DOFade(0, duration).SetDelay(delay).SetEase(ease);
        _fadeTween.OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            onComplete?.Invoke();
            _fadeTween.Kill(true);
        });
    }
    
    private void CheckTween()
    {
        if (_fadeTween != null)
        {
            _fadeTween.Kill();
        }
    }

}
