using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BasePopupAnimation : PopupAnimationController
{
    [SerializeField] private FadingPanel fader;
    [SerializeField] private BasePopupAnimationConfig config;

    public override void Init()
    {
        fader.Refresh();
    }

    public override IEnumerator ShowAnimation()
    {
        fader.FadeIn(config.FadeInDuration, config.FadeInEase, config.FadeInDelay);
        yield return fader.FadeTween.WaitForCompletion();
    }

    public override IEnumerator HideAnimation()
    {
        fader.FadeOut(config.FadeOutDuration, config.FadeOutEase);
        yield return fader.FadeTween.WaitForCompletion();
    }
}
