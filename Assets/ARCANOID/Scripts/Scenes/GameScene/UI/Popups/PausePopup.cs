using UnityEngine;
using Zenject;

public class PausePopup : BasePopup
{
    [SerializeField] private UniversalButton restartButton;
    [SerializeField] private GameObject restartButtonLocker;
    private SceneLoader _sceneLoader;

    [Inject]
    public void Init(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void OnResumeClicked()
    {
        MessageBus.RaiseEvent<IResumeButtonHandler>(handler => handler.OnResumeButtonClicked());
    }

    public void OnRestartClicked()
    {
        MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnRestartGame());
    }

    public void BackToLevelsMap()
    {
        MessageBus.RaiseEvent<IClearGameFieldHandler>(handler => handler.OnClearGameField());
        _sceneLoader.LoadScene(Scene.LevelSelection, () =>
        {
            StartCoroutine(Hide());
        });
    }
    
    public void LockRestartButton()
    {
        restartButton.SetActive(false);
        restartButtonLocker.SetActive(true);
    }

    public void UnlockRestartButton()
    {
        restartButtonLocker.SetActive(false);
        restartButton.SetActive(true);
    }
}
