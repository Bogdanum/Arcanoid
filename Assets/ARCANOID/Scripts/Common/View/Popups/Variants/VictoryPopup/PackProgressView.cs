using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackProgressView : MonoBehaviour
{
    [SerializeField] private Image packIcon;
    [SerializeField] private TMP_Text progressInPercent;
    [SerializeField] private LocalizedTMPro packName;
    [SerializeField] private LocalizedTMPro buttonText;
    [SerializeField] private ProgressSliderView progressSlider;
    [SerializeField] private ValueChangeAnimation progressAnimation;
    [SerializeField] private FadingPanel continueButtonFader;
    [SerializeField] private AnimationSettings animationSettings;
    [SerializeField] private NextButtonTranslationID nextButtonTranslationID;

    private float _previousLevel;
    private float _levelsCount;

    public void SetNextPackName(string packID)
    {
        packName.ChangeTranslationID(packID);
    }
    
    public void SetPackIcon(Sprite icon)
    {
        packIcon.sprite = icon;
    }

    public void UpdateButtonLevel(int level)
    {
        if (level > 1)
        {
            buttonText.SetInsertNumberActive(true);
            buttonText.ChangeTranslationID(nextButtonTranslationID.nextLevel);
            buttonText.InsertNumber(level.ToString());
        } else
        {
            buttonText.SetInsertNumberActive(false);
            buttonText.ChangeTranslationID(nextButtonTranslationID.nextPack);
        }
    }

    public void InitProgressValues(int current, int max)
    {
        _previousLevel = current;
        _levelsCount = max;
        int percent = GetProgressPercent(_previousLevel - 1);
        progressInPercent.text = percent + "%";
        progressSlider.SetMaxValue(_levelsCount);
        progressSlider.SetMinValue(1);
        progressSlider.UpdateProgress(_previousLevel);
    }

    public void OnPrepareView()
    {
        continueButtonFader.Refresh();
    }

    public void UpdateProgressAnimate(float nextLevel, TweenCallback onNextPack, Action onComplete)
    {
        progressAnimation.Play(_previousLevel, nextLevel, UpdateProgressBarWithPercent, () =>
        {
            _previousLevel = nextLevel;
            onNextPack?.Invoke();
            continueButtonFader.FadeIn
            (
                animationSettings.buttonFadeInDuration, 
                animationSettings.buttonFadeInEase, 
                animationSettings.buttonFadeInDelay,
                onComplete
            );
        });
    }

    private void UpdateProgressBarWithPercent(float value)
    {
        progressSlider.UpdateProgress(value);
        int progressPercent = GetProgressPercent(value - 1);
        progressInPercent.text = progressPercent > 99 ? string.Empty : progressPercent + "%";
    }

    private int GetProgressPercent(float progress)
    {
        var percent = (int)(100 * (progress / (_levelsCount - 1)));
        return percent > 100 ? 100 : percent;
    }
    
    [Serializable]
    public class AnimationSettings
    {
        public float buttonFadeInDuration;
        public float buttonFadeInDelay;
        public Ease buttonFadeInEase;
    }
    
    [Serializable]
    public struct NextButtonTranslationID
    {
        public string nextLevel;
        public string nextPack;
    }
}
