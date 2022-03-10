using UnityEngine;

public class MobileInputListener : MonoBehaviour, IInputBlockingHandler
{
    [SerializeField] private Camera cameraRaycaster;
    
    private bool _isBlocked;
    private bool _isHolding;

    private void OnEnable() => MessageBus.Subscribe(this);
    
    private void OnDisable() => MessageBus.Unsubscribe(this);

    private void Update()
    {
        if (_isBlocked) return;

        if (Input.GetMouseButtonDown(0))
        {
            _isHolding = true;
            return;
        }

        if (_isHolding && Input.GetMouseButtonUp(0))
        {
            MessageBus.RaiseEvent<ILaunchBallHandler>(handler => handler.OnLaunchCommand());
            _isHolding = false;
            return;
        }

        if (_isHolding)
        {
            SendCurrentPointerPosition();
        }
    }

    private void SendCurrentPointerPosition()
    {
        Vector3 pointerPos = cameraRaycaster.ScreenToWorldPoint(Input.mousePosition);
        MessageBus.RaiseEvent<IPointerPositionHandler>(handler => handler.OnUpdatePointerPosition(pointerPos));
    }

    public void OnInputActivation()
    {
        _isBlocked = false;
    }

    public void OnInputBlock()
    {
        _isBlocked = true;
        _isHolding = false;
    }
}
