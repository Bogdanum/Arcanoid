using UnityEngine;

public class VictoryPopup : BasePopup
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject continueButtonLocker;

    public void OnContinueButtonClicked()
    {
        MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnStartGame());
    }
    
    public void LockContinueButton()
    {
        continueButton.SetActive(false);
        continueButtonLocker.SetActive(true);
    }

    public void UnlockContinueButton()
    {
        continueButtonLocker.SetActive(false);
        continueButton.SetActive(true);
    }
}
