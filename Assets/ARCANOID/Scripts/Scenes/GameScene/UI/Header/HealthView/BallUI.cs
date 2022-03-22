using System;
using UnityEngine;

public class BallUI : PoolableUIEntity
{
     [SerializeField] private TweenScaler scaler;

     public override void OnSpawned() {}

     public void Show()
     {
          gameObject.SetActive(true);
     }
    
     public void Show(float duration)
     {
          gameObject.SetActive(true);
          transform.localScale = Vector3.zero;
          SmoothAppearance(duration);
     }

     private void SmoothAppearance(float duration)
     {
          scaler.DoScale(Vector3.one, duration);
     }

     public void Hide(float duration, Action onComplete = null)
     {
          scaler.DoScale(Vector3.zero, duration, () =>
          {
               gameObject.SetActive(false);
               onComplete?.Invoke();
          });
     }
}
