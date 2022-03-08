using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallsOnSceneController : MonoBehaviour, IMainBallLifecycleHandler, ILaunchBallHandler, IComplexityIncreaseHandler
{
    [SerializeField] private Transform ballsContainer;
    [SerializeField] private BallPhysicsSettings ballPhysicsSettings;

    [Inject] private PoolsManager _poolsManager;
    private List<Ball> _ballsList;
    private Ball _currentBallOnPlatform;
    private float _currentBallsVelocity;
    private float _additionalVelocity;

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _ballsList = new List<Ball>();
        _currentBallsVelocity = ballPhysicsSettings.InitialVelocity;
    }
    
    public void OnCreateNewBallOnPlatform(Transform platform)
    {
        var ball = SpawnBallAtPosition(platform.position, platform);
        MessageBus.RaiseEvent<ISpawnBallHandler>(handler => handler.OnSpawnBallOnScene(ball));
        _currentBallOnPlatform = ball;
        _ballsList.Add(ball);
        SetBallsVelocity();
    }

    private Ball SpawnBallAtPosition(Vector3 position, Transform parent)
    {
        return _poolsManager.GetItem<Ball>(position, parent);
    }

    private void SetBallsVelocity()
    {
        float velocity = _currentBallsVelocity + _additionalVelocity;
        ExecuteMethodForAllBalls(ball => ball.SetVelocity(velocity));
    }

    public void ChangeAdditionalVelocity(float additionalVelocity)
    {
        _additionalVelocity = additionalVelocity;
        SetBallsVelocity();
    }

    
    public void OnIncreasingComplexity()
    {
        if (_currentBallsVelocity >= ballPhysicsSettings.MaxVelocity) return;

        _currentBallsVelocity += ballPhysicsSettings.VelocityIncreaseStep;
        SetBallsVelocity();
    }
    
    public void ExecuteMethodForAllBalls(Action<Ball> method)
    {
        foreach (var ball in _ballsList)
        {
            method.Invoke(ball);
        }
    }
    
    public void OnLaunchCommand()
    {
        if (_currentBallOnPlatform == null) return;
        
        _currentBallOnPlatform.PushBall(Vector2.up);
        _currentBallOnPlatform.transform.SetParent(ballsContainer);
        _currentBallOnPlatform = null;
    }

    public void OnDestroyBall(Ball ball)
    {
        _poolsManager.ReturnItemToPool(ball);
        _ballsList.Remove(ball);

        if (_ballsList.Count < 1)
        {
            MessageBus.RaiseEvent<IRemoveHealthHandler>(handler => handler.OnRemoveHealth());
        }
    }

    public void RemoveAllBallsOnScene()
    {
        foreach (var ball in _ballsList)
        {
            _poolsManager.ReturnItemToPool(ball);
        }
        _ballsList.Clear();
    }
}
