using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HeaderUIController : MonoBehaviour, ILocalGameStateHandler
{
    [Inject] private PauseController _pauseController;
    [SerializeField] private Image currentPackIcon;
    [SerializeField] private LocalizedText currentLevel;

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OpenPauseView()
    {
        _pauseController.Pause();
    }

    public void OnPrepare()
    {
        // set current pack values
        currentPackIcon.sprite = currentPackIcon.sprite;
        currentLevel.InsertNumber("1");
    }

    public void OnStartGame() {}

    public void OnContinueGame() {}

    public void OnEndGame() {}
}
