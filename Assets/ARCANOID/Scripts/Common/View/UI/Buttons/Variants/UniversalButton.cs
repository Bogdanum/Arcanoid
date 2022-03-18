using UnityEngine;
using UnityEngine.Events;

public class UniversalButton : AnimatedButton
{
    [SerializeField] private Vector3 onClickScale;
    [SerializeField] private float animationTime;
    [SerializeField] private TweenScaler scaler;
    [SerializeField] private UnityEvent onClick;

    protected override void OnPointerDown()
    {
        scaler.DoScale(onClickScale, animationTime);
    }

    protected override void ReturnToNormalAnim()
    {
        scaler.DoScale(Vector3.one, animationTime);
    }

    protected override void ExecuteClickEvent()
    {
        if (PointerEnter && interactable)
        {
            onClick?.Invoke();   
        }
    }
}
