
using UnityEngine;

public class LosePopup : BasePopup
{
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject restartButtonLocker;
    
    public void OnRestartButtonClicked()
    {
        MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnRestartGame());
    }

    public void OnSecondChanceButtonClicked()
    {
        MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnContinue());
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
