using System;
using UnityEngine;
using Zenject;

public class LosePopup : BasePopup, ILocalGameStateHandler
{
    [SerializeField] private EnergyViewWithSliderController energyViewController;
    [SerializeField] private UniversalButtonWithEnergy restartButton;
    [SerializeField] private UniversalButtonWithEnergy secondChanceButton;
    private EnergyManager _energyManager;
    private int _energyToRestart;
    private int _energyToSecondChance;
    private bool _isSecondChanceUsed;
    private bool _isPlayingAnimation;

    [Inject]
    public void Init(EnergyManager energyManager)
    {
        MessageBus.Subscribe(this);
        _energyManager = energyManager;
        InitButtonsCost();
    }

    private void OnEnable()
    {
        _energyManager.OnEnergyChanged += CheckEnergy;
        secondChanceButton.SetActive(!_isSecondChanceUsed);
        CheckEnergy();
    }

    private void OnDisable() => _energyManager.OnEnergyChanged -= CheckEnergy;

    
    private void InitButtonsCost()
    {
        _energyToRestart = _energyManager.GetEnergyActionValue(ActionWithEnergy.RestartGame);
        _energyToSecondChance = _energyManager.GetEnergyActionValue(ActionWithEnergy.SecondChance);
        
        restartButton.SetCost(_energyToRestart);
        secondChanceButton.SetCost(_energyToSecondChance);
    }

    private void CheckEnergy()
    {
        CheckEnergyToRestart();
        CheckEnergyToSecondChance();
    }

    private void CheckEnergyToRestart()
    {
        if (_energyManager.IsEnoughEnergy(_energyToRestart))
        {
            restartButton.Unlock();
            return;
        }
        restartButton.Lock();
    }
    
    private void CheckEnergyToSecondChance()
    {
        if (_energyManager.IsEnoughEnergy(_energyToSecondChance))
        {
            secondChanceButton.Unlock();
            return;
        }
        secondChanceButton.Lock();
    }

    public void OnRestartButtonClicked()
    {
        if (_isPlayingAnimation) return;
        
        _energyManager.RemoveEnergy(ActionWithEnergy.RestartGame);
        energyViewController.StopAnimation();
        _isPlayingAnimation = true;
        energyViewController.PlaySliderAnimation(() =>
        {
            _isPlayingAnimation = false;
            MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnRestartGame());
        });
    }

    public void OnSecondChanceButtonClicked()
    {
        if (_isPlayingAnimation) return;
        
        _isSecondChanceUsed = true;
        _isPlayingAnimation = true;
        _energyManager.RemoveEnergy(ActionWithEnergy.SecondChance);
        energyViewController.StopAnimation();
        energyViewController.PlaySliderAnimation(() =>
        {
            _isPlayingAnimation = false;
            MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnContinue()); 
        });
    }

    protected override void OnAppeared(Action onAppeared = null)
    {
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGamePaused());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputBlock());
    }

    public void OnStartGame() => _isSecondChanceUsed = false;
    public void OnPrepare(){}
    public void OnContinueGame(){}
    public void OnEndGame(){}
}