using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BasePopupAnimation : PopupAnimationController
{
    [SerializeField] private FadingPanel fader;
    [SerializeField] private BasePopupAnimationConfig config;
    [SerializeField] private OverrideFadeDelay overrideParams;

    private float _fadeInDelay;

    public override void Init()
    {
        fader.Refresh();
        _fadeInDelay = overrideParams.useOverrideParams ? overrideParams.fadeInDelay : config.FadeInDelay;
    }

    public override IEnumerator ShowAnimation()
    {
        fader.Refresh();
        fader.FadeIn(config.FadeInDuration, config.FadeInEase, _fadeInDelay);
        yield return fader.FadeTween.WaitForCompletion();
    }

    public override IEnumerator HideAnimation()
    {
        fader.FadeOut(config.FadeOutDuration, config.FadeOutEase);
        yield return fader.FadeTween.WaitForCompletion();
    }
}

[System.Serializable]
public struct OverrideFadeDelay
{
    public bool useOverrideParams;
    public float fadeInDelay;
}
