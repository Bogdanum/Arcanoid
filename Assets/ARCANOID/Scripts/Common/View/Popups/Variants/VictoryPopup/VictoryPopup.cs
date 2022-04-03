using System;
using UnityEngine;
using Zenject;

public class VictoryPopup : BaseAnimatedPopup, IPackActionHandler
{
    [SerializeField] private PackProgressViewController packProgressViewController;
    private LevelPacksManager _levelPacksManager;
    private SceneLoader _sceneLoader;
    private EnergyManager _energyManager;
    private LevelPackInfo _cachedPackInfo;
    private bool _lastOrRepassedPack;

    [Inject]
    public void Initialize(LevelPacksManager levelPacksManager, SceneLoader sceneLoader, EnergyManager energyManager)
    {
        MessageBus.Subscribe(this);
        _levelPacksManager = levelPacksManager;
        _sceneLoader = sceneLoader;
        _energyManager = energyManager;
        InitProgressView();
    }

    private void OnDisable() => InitProgressView();

    public void OnChoosingAnotherPack()
    {
        InitProgressView();
    }

    private void InitProgressView()
    {
        _cachedPackInfo = _levelPacksManager.GetCurrentPackInfo();
        if (_cachedPackInfo == null) return;
        packProgressViewController.InitSizes();
        packProgressViewController.SetPackIcon(_cachedPackInfo.Pack.Icon);
        packProgressViewController.SetNextPackName(_cachedPackInfo.Pack.PackID);
        packProgressViewController.InitProgressValues(_cachedPackInfo.CurrentLevel , _cachedPackInfo.Pack.Count + 1);
    }

    protected override void OnAppeared(Action onAppeared = null)
    {
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGamePaused());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputBlock());
        onAppeared?.Invoke();
        
        var currentPackInfo = _levelPacksManager.GetCurrentPackInfo();
        int levelsCount = _cachedPackInfo.Pack.Count;
        _lastOrRepassedPack = _cachedPackInfo.IsLast || _cachedPackInfo.IsRepassed;
        packProgressViewController.UpdateButtonLevel(currentPackInfo.CurrentLevel, _lastOrRepassedPack);
        if (_cachedPackInfo == currentPackInfo)
        {
            if (_lastOrRepassedPack)
            {
                packProgressViewController.UpdateProgressAnimate(levelsCount + 1, PlayCompletePackAnimation);
            } else
            {
                packProgressViewController.UpdateProgressAnimate(currentPackInfo.CurrentLevel, AddEnergyForWinning);
            } 
            return;
        }
        packProgressViewController.UpdateProgressAnimate(levelsCount + 1, PlayCompletePackAnimation);
    }

    private void PlayCompletePackAnimation()
    {
        _cachedPackInfo = _levelPacksManager.GetCurrentPackInfo();
        packProgressViewController.PlayCompletePackAnimation(_cachedPackInfo.Pack.Icon, () =>
        {
            packProgressViewController.SetNextPackName(_cachedPackInfo.Pack.PackID);
            AddEnergyForWinning();
        });
    }
    
    private void AddEnergyForWinning()
    {
        _energyManager.AddEnergyForAction(ActionWithEnergy.Victory);
    }

    public void OnContinueButtonClicked()
    {
        if (_lastOrRepassedPack)
        {
            MessageBus.RaiseEvent<IClearGameFieldHandler>(handler => handler.OnClearGameField());
            _sceneLoader.LoadScene(Scene.LevelSelection);
        } else
        {
            _energyManager.RemoveEnergy(ActionWithEnergy.StartGame);
            MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnStartGame());
        }
    }
}
