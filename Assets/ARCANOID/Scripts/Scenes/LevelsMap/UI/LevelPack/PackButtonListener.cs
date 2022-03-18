using System;
using UnityEngine;

[RequireComponent(typeof(TweenScaler))]
public class PackButtonListener : AnimatedButton
{
    [SerializeField] private TweenScaler tweenScaler;
    [SerializeField] private Vector3 onClickScale;
    [SerializeField] private float animationTime;
    public event Action OnClick;
    [HideInInspector] public bool hasCallback = false;

    protected override void OnPointerDown()
    {
        tweenScaler.DoScale(onClickScale, animationTime);
    }

    protected override void ReturnToNormalAnim()
    {
        tweenScaler.DoScale(Vector3.one, animationTime);
    }

    protected override void ExecuteClickEvent()
    {
        if (PointerEnter && interactable)
        {
            OnClick?.Invoke();
        }
    }
}
