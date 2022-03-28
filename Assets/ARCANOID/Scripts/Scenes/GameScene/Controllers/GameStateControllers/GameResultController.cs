
public class GameResultController : IGameResultHandler
{
    private readonly PopupsManager _popupsManager;
    private readonly LevelPacksManager _levelPacksManager;
    private readonly EnergyManager _energyManager;
    
    public GameResultController(PopupsManager popupsManager, LevelPacksManager levelPacksManager, EnergyManager energyManager)
    {
        _popupsManager = popupsManager;
        _levelPacksManager = levelPacksManager;
        _energyManager = energyManager;
        MessageBus.Subscribe(this);
    }

    ~GameResultController() => MessageBus.Unsubscribe(this);

    public void OnVictory()
    {
        _levelPacksManager.OnLevelComplete();
        _popupsManager.HideAll();
        _popupsManager.Show<VictoryPopup>(GetEnergyForWinning);
    }

    private void GetEnergyForWinning()
    {
        _energyManager.AddEnergyForAction(ActionWithEnergy.Victory);
    }

    public void OnLose()
    {
        _popupsManager.Show<LosePopup>();
    }
}
