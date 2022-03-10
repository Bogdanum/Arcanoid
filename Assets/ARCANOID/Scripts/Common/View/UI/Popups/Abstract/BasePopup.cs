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
