public class PauseController : IPausePopupButtonsHandler
{
    private readonly PopupsManager _popupsManager;
    
    public PauseController(PopupsManager popupsManager)
    {
        _popupsManager = popupsManager;
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
        _popupsManager.HideLast();
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGameResumed());
        MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnRestartGame());
    }
}
