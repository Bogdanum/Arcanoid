using UnityEngine;

public class PlatformMovement : MonoBehaviour, IPointerPositionHandler
{
    [SerializeField] private Rigidbody2D rigidbody;
    
    private Transform _platformTransform;
    private Vector3 _startPosition;
    private float _size;
    private float _speed;
    private float _boundX;
    private float _gameBoundarySize;
    private float _targetPosAccuracy;
    private bool _controlLock;
    
    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);
    
    public void LockControl() => _controlLock = true;
    
    public void UnlockControl() => _controlLock = false;
    
    
    public void Init(float targetPosAccuracy, float gameBoundarySize)
    {
        _platformTransform = transform;
        _startPosition = _platformTransform.position;
        _targetPosAccuracy = targetPosAccuracy;
        _gameBoundarySize = gameBoundarySize;
        _boundX = _gameBoundarySize / 2;
    }

    public void RefreshParameters()
    {
        transform.position = _startPosition;
        _controlLock = false;
    }
    
    public void SetNewPhysicsSize(float size)
    {
        _size = _gameBoundarySize * size;
    }
    
    public void SetNewSpeed(float speed)
    {
        _speed = speed;
    }
    
    public void OnUpdatePointerPosition(Vector3 position)
    {
        if (_controlLock) return;

        Vector3 target = CalculateTargetMovePosition(position);
        SmoothMoveToTarget(target);
    }
    
    private Vector3 CalculateTargetMovePosition(Vector3 pointerPos)
    {
        pointerPos.y = _platformTransform.position.y;
        float platformHalfSize = _size / 2;
        float absPosX = Mathf.Abs(pointerPos.x) + platformHalfSize;

        if (absPosX > _boundX)
        {
            pointerPos.x = pointerPos.x > 0 ? _boundX - platformHalfSize : platformHalfSize - _boundX;
        }
        return pointerPos;
    }
    
    private void SmoothMoveToTarget(Vector2 target)
    {
        float absDistanceToTarget = Mathf.Abs(_platformTransform.position.x - target.x);

        if (absDistanceToTarget > _targetPosAccuracy)
        {
            var directionVector = target - (Vector2)transform.position;
            float velocity = directionVector.normalized.x * _speed;
            target.x = _platformTransform.position.x + velocity * Time.fixedDeltaTime;
            
            rigidbody.MovePosition(target);
        }
    }
}
