using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallsOnSceneController : MonoBehaviour, IMainBallLifecycleHandler, ILaunchBallHandler, IComplexityIncreaseHandler, ILocalGameStateHandler, IClearGameFieldHandler
{
    [SerializeField] private Transform ballsContainer;
    [SerializeField] private BallPhysicsSettings ballPhysicsSettings;
    private PoolsManager _poolsManager;
    private List<Ball> _ballsList;
    private Ball _currentBallOnPlatform;
    private float _currentBallsVelocity;
    private float _additionalVelocity;

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    [Inject]
    public void Init(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
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

    private void ExecuteMethodForAllBalls(Action<Ball> method)
    {
        _ballsList.ForEach(method.Invoke);
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
            MessageBus.RaiseEvent<IPlayerHealthChangeHandler>(handler => handler.OnRemoveHealth());
            MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnContinueGame());
        }
    }

    public void OnEndGame() => RemoveAllBallsOnScene();
    public void OnClearGameField() => RemoveAllBallsOnScene();
    
    private void RemoveAllBallsOnScene()
    {
        _ballsList.ForEach(_poolsManager.ReturnItemToPool);
        _ballsList.Clear();
    }

    public void OnPrepare()
    {
        _currentBallsVelocity = ballPhysicsSettings.InitialVelocity;
    }
    public void OnStartGame() {}
    public void OnContinueGame() {}
}
