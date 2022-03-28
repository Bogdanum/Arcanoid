using System.Collections.Generic;
using UnityEngine;

public class BallsOnSceneContainer : MonoBehaviour, IClearGameFieldHandler, ILocalGameStateHandler
{
    private BallsSpawner _spawner;
    private List<Ball> _ballsList;
    public bool IsEmpty => _ballsList.Count < 1;

    public void Init(BallsSpawner spawner)
    {
        _ballsList = new List<Ball>();
        _spawner = spawner;
    }

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void Add(Ball ball)
    {
        _ballsList.Add(ball);
    }

    public void Remove(Ball ball)
    {
        _ballsList.Remove(ball);
        _spawner.ReturnToPool(ball);
    }

    public void Put(Transform ball)
    {
        ball.SetParent(transform);
    }

    public List<Ball> GetBallsOnSceneList() => _ballsList;

    public void OnClearGameField() => ClearAll();

    public void OnEndGame() => ClearAll();

    private void ClearAll()
    {
        _ballsList.ForEach(_spawner.ReturnToPool);
        _ballsList.Clear();
    }
    
    public void OnPrepare(){}
    public void OnStartGame(){}
    public void OnContinueGame(){}
}
