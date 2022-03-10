using UnityEngine;

public class PausePopup : BasePopup
{
    [SerializeField] private UniversalButton resumeButton;
    [SerializeField] private UniversalButton restartButton;
    [SerializeField] private UniversalButton backToMapButton;
    [SerializeField] private GameObject restartButtonLocker;

    public void OnResumeClicked()
    {
        MessageBus.RaiseEvent<IResumeButtonHandler>(handler => handler.OnResumeButtonClicked());
    }

    public void OnRestartClicked()
    {
        MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnRestartGame());
    }

    public void OnBackToMapClicked()
    {
        
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
