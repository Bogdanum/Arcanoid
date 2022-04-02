using System;
using UnityEngine;

public abstract class BasePopup : MonoBehaviour
{
    public virtual void Initialize()
    {
        SetupScaleAndPosition();
        gameObject.SetActive(false);
    }

    private void SetupScaleAndPosition()
    {
        var rect = (RectTransform) transform;
        rect.RefreshScaleAndPosition();
    }

    public abstract void Show(Action onComplete = null);
    public abstract void Hide();
    
    protected virtual void PrepareToShow() {}
    protected virtual void OnAppeared(Action onAppeared = null) {}
}
