using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AnimatedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected bool Interactable = true;
    protected bool PointerEnter { get; private set; }

    public void OnPointerDown(PointerEventData eventData) => OnPointerDown();

    public void OnPointerUp(PointerEventData eventData) => OnPointerUp();

    public void OnPointerEnter(PointerEventData eventData) => PointerEnter = true;

    public void OnPointerExit(PointerEventData eventData) => PointerEnter = false;

    public void SetInteractable(bool state)
    {
        Interactable = state;
    }

    protected abstract void OnPointerDown();

    private void OnPointerUp()
    {
        ExecuteClickEvent();
        ReturnToNormalAnim();
    }

    protected abstract void ReturnToNormalAnim();
    protected abstract void ExecuteClickEvent();
}