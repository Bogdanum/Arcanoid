using UnityEngine;

public class MobileInputListener : MonoBehaviour, IInputBlockingHandler
{
    [SerializeField] private Camera cameraRaycaster;
    [SerializeField] private float headerOffset;

    private Vector3 _pointerPos;
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
            if (InPermittedArea())
            {
                MessageBus.RaiseEvent<ILaunchBallHandler>(handler => handler.OnLaunchCommand());
            }
            _isHolding = false;
            return;
        }
        if (_isHolding)
        {
            SendCurrentPointerPosition();
        }
        MessageBus.RaiseEvent<IPointerPositionHandler>(handler => handler.OnUpdateHoldingState(_isHolding));
    }

    private void SendCurrentPointerPosition()
    {
         _pointerPos= cameraRaycaster.ScreenToWorldPoint(Input.mousePosition);
        if (InPermittedArea())
        {
            MessageBus.RaiseEvent<IPointerPositionHandler>(handler => handler.OnUpdatePointerPosition(_pointerPos));   
        }
    }

    private bool InPermittedArea()
    {
        return _pointerPos.y < cameraRaycaster.orthographicSize - headerOffset;
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
