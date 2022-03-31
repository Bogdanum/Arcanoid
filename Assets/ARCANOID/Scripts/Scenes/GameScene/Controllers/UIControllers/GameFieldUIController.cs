using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameFieldUIController : MonoBehaviour, ILocalGameStateHandler
{
    [SerializeField] private Image gameBackground;
    private LevelPacksManager _levelPacksManager;
    
    [Inject]
    public void Init(LevelPacksManager levelPacksManager)
    {
        _levelPacksManager = levelPacksManager;
    }

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnPrepare()
    {
        var packInfo = _levelPacksManager.GetCurrentPackInfo();
        var background = packInfo.Pack.GameBackground;
        if (background != null)
        {
            gameBackground.sprite = packInfo.Pack.GameBackground;
        }
    }

    public void OnStartGame() {}
    public void OnContinueGame() {}
    public void OnEndGame() {}
}
