
public class GameResultController : IGameResultHandler
{
    private readonly PopupsManager _popupsManager;
    
    public GameResultController(PopupsManager popupsManager)
    {
        _popupsManager = popupsManager;
        MessageBus.Subscribe(this);
    }

    ~GameResultController() => MessageBus.Unsubscribe(this);

    public void OnVictory()
    {
        _popupsManager.StartCoroutine(_popupsManager.Show<VictoryPopup>());
    }

    public void OnLose()
    {
        _popupsManager.StartCoroutine(_popupsManager.Show<LosePopup>());
    }
}
