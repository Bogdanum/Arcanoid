
public class GameResultController : IGameResultHandler
{
    private readonly PopupsManager _popupsManager;
    private readonly LevelPacksManager _levelPacksManager;

    public GameResultController(PopupsManager popupsManager, LevelPacksManager levelPacksManager)
    {
        _popupsManager = popupsManager;
        _levelPacksManager = levelPacksManager;
        MessageBus.Subscribe(this);
    }

    ~GameResultController() => MessageBus.Unsubscribe(this);

    public void OnVictory()
    {
        _levelPacksManager.OnLevelComplete();
        _popupsManager.HideAll();
        _popupsManager.Show<VictoryPopup>();
    }

    public void OnLose()
    {
        _popupsManager.Show<LosePopup>();
    }
}
