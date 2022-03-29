using UnityEngine;
using Zenject;

public class PlayerHealthController : MonoBehaviour, IPlayerHealthChangeHandler, ILocalGameStateHandler
{
    [SerializeField] private PlayerHealthView view;
    [SerializeField] private HealthViewGridConfig config;
    private PoolsManager _poolsManager;
    private int _currentHeartID;

    [Inject]
    public void Init(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
    }
    
    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnPrepare()
    {
        view.Init(config, _poolsManager);
        _currentHeartID = config.InitHealthCount - 1;
    }

    public void OnAddHealth()
    {
        if (!IsFull())
        {
            _currentHeartID++;
            view.AddHealth(_currentHeartID);
        }
    }

    public void OnRemoveHealth(bool isBallDestroyed)
    {
        if (_currentHeartID >= 0)
        {
            view.RemoveHeart(_currentHeartID);
            _currentHeartID--;
            if (isBallDestroyed)
            {
                MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnContinueGame());   
            }
            return;
        }
        if (isBallDestroyed)
        {
            GameOver();   
        }
    }

    private void GameOver()
    {
        MessageBus.RaiseEvent<IGameResultHandler>(handler => handler.OnLose());
    }

    private bool IsFull()
    {
        return config.MaxHealthCount == _currentHeartID + 1;
    }

    public void OnStartGame() {}

    public void OnContinueGame() {}

    public void OnEndGame() {}
}
