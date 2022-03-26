using System;
using System.Collections;
using UnityEngine;

public class PlatformMovement : MonoBehaviour, IPointerPositionHandler
{
    [SerializeField] private Rigidbody2D platformRigidbody;

    private Transform _platformTransform;
    private Vector3 _startPosition;
    private Vector2 _targetPosition;
    private Vector2 _velocity;
    private Vector2 _direction;
    private float _platformHalfSize;
    private float _speed;
    private float _boundX;
    private float _gameBoundarySize;
    private float _targetPosAccuracy;
    private double _absDistanceToTarget;
    private bool _controlLock;
    
    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);
    public void LockControl() => _controlLock = true;
    public void UnlockControl() => _controlLock = false;

    public void Init(float targetPosAccuracy, float gameBoundarySize)
    {
        _platformTransform = transform;
        _startPosition = _platformTransform.position;
        _targetPosition = _startPosition;
        _targetPosAccuracy = targetPosAccuracy;
        _gameBoundarySize = gameBoundarySize;
        _boundX = _gameBoundarySize / 2;
    }

    public void RefreshParameters()
    {
        transform.position = _startPosition;
        _targetPosition = _startPosition;
        _controlLock = false;
    }
    
    public void SetNewPhysicsSize(float size)
    {
        _platformHalfSize = _gameBoundarySize * size / 2;
    }
    
    public void SetNewSpeed(float speed)
    {
        _speed = speed;
    }
    
    public void OnUpdatePointerPosition(Vector3 position)
    {
        if (_controlLock) return;

        _targetPosition = CalculateTargetMovePosition(position);
    }
    
    private Vector3 CalculateTargetMovePosition(Vector3 pointerPos)
    {
        pointerPos.y = _platformTransform.position.y;
        float absPosX = Mathf.Abs(pointerPos.x) + _platformHalfSize;

        if (absPosX > _boundX)
        {
            pointerPos.x = pointerPos.x > 0 ? _boundX - _platformHalfSize : _platformHalfSize - _boundX;
        }
        return pointerPos;
    }

    private void FixedUpdate()
    {
        _absDistanceToTarget = Math.Round(Mathf.Abs(platformRigidbody.position.x - _targetPosition.x), 2);

        _direction = _targetPosition - platformRigidbody.position;
        if (_absDistanceToTarget > _targetPosAccuracy)
        {
            _velocity = _direction.normalized * _speed;
        }
        else
        {
            _velocity = _direction.normalized * _speed * (float)_absDistanceToTarget;
        }
        platformRigidbody.velocity = _velocity;
    }
    
    public IEnumerator BackToInitialPosition(Action onComplete = null)
    {
        LockControl();
        _targetPosition = _startPosition;
        while (_absDistanceToTarget > _targetPosAccuracy)
        {
            yield return new WaitForEndOfFrame();
        }
        UnlockControl();
        onComplete?.Invoke();
    }
}
