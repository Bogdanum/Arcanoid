using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HeaderUIController : MonoBehaviour, ILocalGameStateHandler
{
    [SerializeField] private Image currentPackIcon;
    [SerializeField] private LocalizedText currentLevel;
    private PauseController _pauseController;
    private LevelPacksManager _levelPacksManager;

    [Inject]
    public void Init(PauseController pauseController, LevelPacksManager levelPacksManager)
    {
        _pauseController = pauseController;
        _levelPacksManager = levelPacksManager;
    }
    
    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OpenPauseView()
    {
        _pauseController.Pause();
    }

    public void OnPrepare()
    {
        var packInfo = _levelPacksManager.GetCurrentPackInfo();
        currentPackIcon.sprite = packInfo.Pack.Icon;
        currentLevel.InsertNumber(packInfo.CurrentLevel.ToString());
    }

    public void OnStartGame() {}

    public void OnContinueGame() {}

    public void OnEndGame() {}
}
