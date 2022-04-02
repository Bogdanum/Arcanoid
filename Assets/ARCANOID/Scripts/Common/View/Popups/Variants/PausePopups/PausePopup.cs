using UnityEngine;
using Zenject;

public class PausePopup : BaseAnimatedPopup
{
    [SerializeField] private UniversalButtonWithEnergy restartButton;
    private SceneLoader _sceneLoader;
    private EnergyManager _energyManager;

    [Inject]
    public void Init(SceneLoader sceneLoader, EnergyManager energyManager)
    {
        _sceneLoader = sceneLoader;
        _energyManager = energyManager;
        SetRestartCost();
    }

    private void OnEnable()
    {
        _energyManager.OnEnergyChanged += CheckEnergy;
        CheckEnergy();
    }

    private void OnDisable() => _energyManager.OnEnergyChanged -= CheckEnergy;

    private void SetRestartCost()
    {
        var cost = _energyManager.GetEnergyActionValue(ActionWithEnergy.RestartGame);
        restartButton.SetCost(cost);
    }
    
    private void CheckEnergy()
    {
        var energyToRestart = _energyManager.GetEnergyActionValue(ActionWithEnergy.RestartGame);
        if (_energyManager.IsEnoughEnergy(energyToRestart))
        {
            restartButton.Unlock();
        }
        else
        {
            restartButton.Lock();
        }
    }
    
    public void OnResumeClicked()
    {
        MessageBus.RaiseEvent<IPausePopupButtonsHandler>(handler => handler.OnResumeButtonClicked());
    }

    public void OnRestartClicked()
    {
        MessageBus.RaiseEvent<IPausePopupButtonsHandler>(handler => handler.OnRestartButtonClicked());
    }

    public void BackToLevelsMap()
    {
        MessageBus.RaiseEvent<IClearGameFieldHandler>(handler => handler.OnClearGameField());
        _sceneLoader.LoadScene(Scene.LevelSelection, Hide);
    }
}
