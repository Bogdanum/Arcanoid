public class PauseController : IPausePopupButtonsHandler
{
    private readonly PopupsManager _popupsManager;
    private readonly EnergyManager _energyManager;
    
    public PauseController(PopupsManager popupsManager, EnergyManager energyManager)
    {
        _popupsManager = popupsManager;
        _energyManager = energyManager;
        MessageBus.Subscribe(this);
    }
    
    ~PauseController() => MessageBus.Unsubscribe(this);

    public void Pause()
    {
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGamePaused());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputBlock());
        _popupsManager.Show<PausePopup>();
    }

    public void OnResumeButtonClicked()
    {
        _popupsManager.HideLast();
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGameResumed());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputActivation());
    }

    public void OnRestartButtonClicked()
    {
        _energyManager.RemoveEnergy(ActionWithEnergy.RestartGame);
        _popupsManager.HideLast();
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGameResumed());
        MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnRestartGame());
    }
}
