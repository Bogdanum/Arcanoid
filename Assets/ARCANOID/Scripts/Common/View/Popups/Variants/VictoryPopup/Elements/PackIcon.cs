using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackIcon : MonoBehaviour
{
     [SerializeField] private Image packIcon;
     [SerializeField] private TMP_Text progressInPercent;
     [SerializeField] private ProgressSliderView progressSlider;
     [SerializeField] private ValueChangeAnimation progressAnimation;
     private float _previousLevel;
     private float _lastLevel;

     public void Init(int currentLevel, int lastLevel)
     {
          _previousLevel = currentLevel;
          _lastLevel = lastLevel;
          int percent = GetProgressPercent(_previousLevel - 1);
          progressInPercent.text = percent + "%";
          progressSlider.SetMaxValue(_lastLevel);
          progressSlider.SetMinValue(1);
          progressSlider.UpdateProgress(_previousLevel);
     }
     
     public void UpdateProgressAnimate(float nextLevel, TweenCallback onNextPack, TweenCallback onComplete)
     {
          progressAnimation.Play(_previousLevel, nextLevel, UpdateProgressBarWithPercent, () =>
          {
               _previousLevel = nextLevel;
               onNextPack?.Invoke();
               onComplete?.Invoke();
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
          var percent = (int)(100 * (progress / (_lastLevel - 1)));
          return percent > 100 ? 100 : percent;
     }

     public void SetIcon(Sprite icon)
     {
          packIcon.sprite = icon;
     }

     public void PlayCompleteAnimation(float duration, Vector3 targetScale, Sprite nextPackIcon, TweenCallback onComplete)
     {
          Vector3 rotation = new Vector3(0, 360, 0);
          transform.DORotate(rotation,duration, RotateMode.FastBeyond360);
          transform.DOScale(targetScale, duration / 2).SetEase(Ease.InCubic).OnComplete(() =>
          {
               packIcon.sprite = nextPackIcon;
               transform.DOScale(Vector3.one, duration / 2).SetDelay(duration / 2).SetEase(Ease.OutElastic).onComplete += onComplete;  
          });
     }
}