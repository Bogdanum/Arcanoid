using System;
using System.Collections;
using UnityEngine;

public abstract class BasePopup : PoolableUIEntity
{
    [SerializeField] private PopupAnimationController animationController;
    
    public override void Init()
    {
        gameObject.SetActive(false);
        if (animationController != null)
        {
            animationController.Init();
        }
    }

    public IEnumerator Show(Action onComplete)
    {
        gameObject.SetActive(true);
        PrepareToShow();

        if (animationController != null)
        {
            yield return animationController.ShowAnimation();
        }
        OnAppeared(onComplete);
    }

    public IEnumerator Hide()
    {
        if (animationController != null)
        {
            yield return animationController.HideAnimation();
        }
        gameObject.SetActive(false);
    }
    
    public virtual void PrepareToShow() {}
    public virtual void OnAppeared(Action onAppeared) {}
}
