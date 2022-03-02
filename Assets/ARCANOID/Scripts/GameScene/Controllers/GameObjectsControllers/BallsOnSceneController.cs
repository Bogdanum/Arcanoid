using System;
using System.Collections.Generic;
using UnityEngine;

public class BallsOnSceneController : MonoBehaviour, IMainBallLifecycleHandler, ILaunchBallHandler
{
    [SerializeField] private Transform ballsContainer;
    [SerializeField] private BallPhysicsSettings ballPhysicsSettings;

    private List<Ball> _ballsList;
    private Ball _currentBallOnPlatform;
    private float _currentBallslVelocity;
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
        _currentBallslVelocity = ballPhysicsSettings.InitialVelocity;
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
        return PoolsManager.Instance.GetItem<Ball>(position, parent);
    }

    private void SetBallsVelocity()
    {
        float velocity = _currentBallslVelocity + _additionalVelocity;
        ExecuteMethodForAllBalls(ball => ball.SetVelocity(velocity));
    }

    public void ChangeAdditionalVelocity(float additionalVelocity)
    {
        _additionalVelocity = additionalVelocity;
        SetBallsVelocity();
    }

    public void IncreaseBallsVelocityByStep()
    {
        if (_currentBallslVelocity >= ballPhysicsSettings.MaxVelocity) return;

        _currentBallslVelocity += ballPhysicsSettings.VelocityIncreaseStep;
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
        PoolsManager.Instance.ReturnItemToPool(ball);
        _ballsList.Remove(ball);

        if (_ballsList.Count < 1)
        {
            MessageBus.RaiseEvent<IRemoveHealthHandler>(handler => handler.OnRemoveHealth());
        }
    }

    public void RemoveAllBallsOnScene()
    {
        var poolManager = PoolsManager.Instance;
        foreach (var ball in _ballsList)
        {
            poolManager.ReturnItemToPool(ball);
        }
        _ballsList.Clear();
    }
}
