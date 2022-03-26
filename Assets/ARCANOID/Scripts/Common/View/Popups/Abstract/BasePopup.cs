using System;
using System.Collections;
using UnityEngine;

public abstract class BasePopup : MonoBehaviour
{
    [SerializeField] private PopupAnimationController animationController;
    
    public void Init()
    {
        SetupScaleAndPosition();
        gameObject.SetActive(false);
        
        if (animationController != null)
        {
            animationController.Init();
        }
    }

    private void SetupScaleAndPosition()
    {
        var rect = (RectTransform) transform;
        rect.RefreshScaleAndPosition();
    }

    public void Show(Action onComplete = null)
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

    public void Hide()
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

    protected virtual void PrepareToShow() {}
    protected virtual void OnAppeared(Action onAppeared = null) {}
}
