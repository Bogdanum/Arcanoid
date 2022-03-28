using UnityEngine;
using Zenject;

public class GameStateController : MonoBehaviour, IGlobalGameStateHandler, IGameResultHandler
{
    private PopupsManager _popupsManager;

    [Inject]
    public void Init(PopupsManager popupsManager)
    {
        _popupsManager = popupsManager;
    }
    
    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    #region START GAME

    private void Start()
    {
        ClearFieldAndStart();
    }

    private void ClearFieldAndStart()
    {
        MessageBus.RaiseEvent<IClearGameFieldHandler>(handler => handler.OnClearGameField());
        MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnPrepare());
        MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnStartGame());
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGameResumed());
        _popupsManager.HideAll();
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputActivation());
    }
    
    public void OnStartGame()
    {
        ClearFieldAndStart();
    }

    public void OnRestartGame()
    {
        ClearFieldAndStart();
    }

    public void OnContinue() => OnUseSecondChance();

    private void OnUseSecondChance()
    {
        _popupsManager.HideLast();
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGameResumed());
        MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnContinueGame());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputActivation());
    }
    
    #endregion
    
    #region GAME OVER

    public void OnVictory() => GameOver();
    public void OnLose() => GameOver();

    private void GameOver()
    {
        MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnEndGame());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputBlock());
    }

    #endregion
}
