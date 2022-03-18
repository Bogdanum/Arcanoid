using UnityEngine;
using UnityEngine.Events;

public class LoadSceneButton : AnimatedButton
{
    [SerializeField] private Vector3 onClickScale;
    [SerializeField] private float animationTime;
    [SerializeField] private TweenScaler scaler;
    [SerializeField] private Scene targetScene;
    [SerializeField] private UnityEvent<Scene> onClick;
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
            onClick?.Invoke(targetScene);
        }
    }
}
