using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallsOnSceneController : MonoBehaviour, IMainBallLifecycleHandler, ILaunchBallHandler
{
    [SerializeField] private BallPhysicsSettings ballPhysicsSettings;
    [SerializeField] private BallsOnSceneContainer ballsContainer;
    private BallsOnSceneVelocityController _velocityController;
    private BallsSpawner _spawner;
    private Ball _ballOnPlatform;

    [Inject]
    public void Init(PoolsManager poolsManager)
    {
        _spawner = new BallsSpawner(poolsManager);
        ballsContainer.Init(_spawner);
        _velocityController = new BallsOnSceneVelocityController(ballPhysicsSettings, ballsContainer);
    }
    
    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public List<Ball> GetBallsOnSceneList() => ballsContainer.GetBallsOnSceneList();
    
    public void OnCreateNewBallOnPlatform(Transform platform)
    {
        SpawnBall(platform.position, platform);   
        _velocityController.UpdateBallsVelocity();
    }

    public void CreateBallAtPositionAndPushInDirection(Vector2 position, Vector2 direction)
    {
        var ball = SpawnBall(position, ballsContainer.transform);
        _velocityController.UpdateBallsVelocity();
        ball.PushBall(direction);
    }

    private Ball SpawnBall(Vector2 position, Transform parent = null)
    {
        var ball = _spawner.SpawnBallAtPosition(position, parent);
        MessageBus.RaiseEvent<ISpawnBallHandler>(handler => handler.OnSpawnBallOnScene(ball));
        _ballOnPlatform = ball;
        ballsContainer.Add(ball);
        return ball;
    }
    
    public void ChangeAdditionalVelocity(float additionalVelocity)
    {
        _velocityController.ChangeAdditionalVelocity(additionalVelocity);
    }

    public void OnLaunchCommand()
    {
        if (_ballOnPlatform == null) return;
        
        _ballOnPlatform.PushBall(Vector2.up);
        ballsContainer.Put(_ballOnPlatform.transform);
        _ballOnPlatform = null;
    }

    public void OnDestroyBall(Ball ball)
    {
        ballsContainer.Remove(ball);

        if (ballsContainer.IsEmpty)
        {
            MessageBus.RaiseEvent<IPlayerHealthChangeHandler>(handler => handler.OnRemoveHealth(true));
        }
    }
}
