using UnityEngine;

public class BallsSpawner
{
    private readonly PoolsManager _poolsManager;

    public BallsSpawner(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
    }
    
    public Ball SpawnBallAtPosition(Vector3 position, Transform parent = null)
    {
        return _poolsManager.GetItem<Ball>(position, parent);
    }

    public void ReturnToPool(Ball ball)
    {
        _poolsManager.ReturnItemToPool(ball);
    }
}
