using System;
using System.Collections;
using UnityEngine;

public class BaseAnimatedPopup : BasePopup
{
    [SerializeField] private PopupAnimationController animationController;

    public override void Initialize()
    {
        base.Initialize();
        if (animationController != null)
        {
            animationController.Init();
        }
    }

    public override void Show(Action onComplete = null)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowAnimate(onComplete));
    }
    
    private IEnumerator ShowAnimate(Action onComplete = null)
    {
        PrepareToShow();
        if (animationController != null)
        {
            yield return animationController.ShowAnimation();
        }
        OnAppeared(onComplete);
    }

    public override void Hide()
    {
        StartCoroutine(HideAnimate());
    }
    
    private IEnumerator HideAnimate()
    {
        if (animationController != null)
        {
            yield return animationController.HideAnimation();
        }
        gameObject.SetActive(false);
    }
}
