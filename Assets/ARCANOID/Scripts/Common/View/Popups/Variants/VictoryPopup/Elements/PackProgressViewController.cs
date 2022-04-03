using System;
using DG.Tweening;
using UnityEngine;

public class PackProgressViewController : MonoBehaviour
{
    [SerializeField] private PackIcon packIcon;
    [SerializeField] private EnergyFxAnimator energyFxAnimator;
    [SerializeField] private Transform fxStartPoint;
    [SerializeField] private LocalizedTMPro packName;
    [SerializeField] private LocalizedTMPro buttonText;
    [SerializeField] private TweenScaler continueButtonScaler;
    [SerializeField] private PackIconAnimationSettings packIconAnimationSettings;
    [SerializeField] private ButtonAnimationSettings buttonAnimationSettings;
    [SerializeField] private NextButtonTranslationID nextButtonTranslationID;

    public void SetNextPackName(string packID)
    {
        packName.ChangeTranslationID(packID);
    }
    
    public void SetPackIcon(Sprite icon)
    {
        packIcon.SetIcon(icon);
    }

    public void InitProgressValues(int current, int max)
    {
        packIcon.Init(current, max);
    }

    public void InitSizes()
    {
        continueButtonScaler.Stop();
        continueButtonScaler.transform.localScale = Vector3.zero;
    }

    public void UpdateProgressAnimate(float nextLevel, TweenCallback onNextPack)
    {
        packIcon.UpdateProgressAnimate(nextLevel, onNextPack, () =>
        {
            PlayAnimationOfGettingApples(PlayContinueButtonAnimation);
        });
    }

    private void PlayAnimationOfGettingApples(TweenCallback onComplete = null)
    {
        energyFxAnimator.Play(fxStartPoint.localPosition, onComplete);
    }

    private void PlayContinueButtonAnimation()
    {
        continueButtonScaler.DoScale
        (
            Vector3.one,
            buttonAnimationSettings.buttonScaleDuration,
            buttonAnimationSettings.buttonAnimDelay,
            buttonAnimationSettings.buttonScaleEase
        );
    }

    public void PlayCompletePackAnimation(Sprite nextPackIcon,TweenCallback onComplete)
    {
        packIcon.PlayCompleteAnimation(packIconAnimationSettings.duration, packIconAnimationSettings.targetScale, nextPackIcon, onComplete);
    }

    public void UpdateButtonLevel(int level, bool isLastOrRepassed)
    {
        if (level > 1)
        {
            buttonText.SetInsertNumberActive(true);
            buttonText.ChangeTranslationID(nextButtonTranslationID.nextLevel);
            buttonText.InsertNumber(level.ToString());
            return;
        }
        
        buttonText.SetInsertNumberActive(false);
        string translationID = isLastOrRepassed ? nextButtonTranslationID.packSelection : nextButtonTranslationID.play;
        buttonText.ChangeTranslationID(translationID);
    }

    [Serializable]
    private class PackIconAnimationSettings
    {
        public Vector3 targetScale;
        public float duration;
    }
    
    [Serializable]
    private class ButtonAnimationSettings
    {
        public float buttonScaleDuration;
        public float buttonAnimDelay;
        public Ease buttonScaleEase;
    }
    
    [Serializable]
    private struct NextButtonTranslationID
    {
        public string nextLevel;
        public string play;
        public string packSelection;
    }
}
