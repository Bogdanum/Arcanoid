using System;
using DG.Tweening;
using UnityEngine;

public class ValueChangeAnimation : MonoBehaviour
{
     [SerializeField] private float duration;
     [SerializeField] private Ease ease;

     private bool _playing;

     public void Play(float startValue, float endValue, Action<float> updateValue, TweenCallback onComplete = null)
     {
          Resume();
          DOVirtual.Float(startValue, endValue, duration, value =>
          {
               if (_playing) updateValue(value);
          }).SetEase(ease).onComplete += onComplete;
     }

     public void Stop() => _playing = false;
     public void Resume() => _playing = true;
}
