using System.Collections;
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
        StartCoroutine(ClearFieldAndStart());
    }

    private IEnumerator ClearFieldAndStart()
    {
        MessageBus.RaiseEvent<IClearGameFieldHandler>(handler => handler.OnClearGameField());
        MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnPrepare());
        MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnStartGame());
        yield return _popupsManager.HideAll();
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputActivation());
    }
    
    public void OnStartGame()
    {
        StartCoroutine(ClearFieldAndStart());
    }

    public void OnRestartGame()
    {
        StartCoroutine(ClearFieldAndStart());
    }

    public void OnContinue() => OnUseSecondChance();

    private void OnUseSecondChance()
    {
        MessageBus.RaiseEvent<IPlayerHealthChangeHandler>(handler => handler.OnAddHealth());
        MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnContinueGame());
    }
    
    #endregion
    
    #region GAME OVER

    public void OnVictory() => GameOver();
    public void OnLose() => GameOver();

    public void GameOver()
    {
        MessageBus.RaiseEvent<ILocalGameStateHandler>(handler => handler.OnEndGame());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputBlock());
    }

    #endregion
}
