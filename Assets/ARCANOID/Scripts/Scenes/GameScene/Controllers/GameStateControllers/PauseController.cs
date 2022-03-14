public class PauseController : IResumeButtonHandler
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
        _popupsManager.StartCoroutine(_popupsManager.Show<PausePopup>());
    }

    public void OnResumeButtonClicked()
    {
        _popupsManager.StartCoroutine(_popupsManager.HideAll());
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGameResumed());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputActivation());
    }
}
