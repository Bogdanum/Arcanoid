using System;
using UnityEngine;
using Zenject;

public class VictoryPopup : BasePopup, IPackActionHandler
{
    [SerializeField] private PackProgressView packProgressView;
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

    protected override void PrepareToShow()
    {
        packProgressView.OnPrepareView();
    }
    
    public void OnChoosingAnotherPack()
    {
        InitProgressView();
    }

    private void InitProgressView()
    {
        _cachedPackInfo = _levelPacksManager.GetCurrentPackInfo();
        if (_cachedPackInfo == null) return;
        packProgressView.SetPackIcon(_cachedPackInfo.Pack.Icon);
        packProgressView.SetNextPackName(_cachedPackInfo.Pack.PackID);
        packProgressView.InitProgressValues(_cachedPackInfo.CurrentLevel , _cachedPackInfo.Pack.Count + 1);
    }

    protected override void OnAppeared(Action onAppeared = null)
    {
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGamePaused());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputBlock());
        onAppeared?.Invoke();
        
        var currentPackInfo = _levelPacksManager.GetCurrentPackInfo();
        int levelsCount = _cachedPackInfo.Pack.Count;
        packProgressView.UpdateButtonLevel(currentPackInfo.CurrentLevel);
        if (_cachedPackInfo == currentPackInfo)
        {
            _lastOrRepassedPack = _cachedPackInfo.IsLast || _cachedPackInfo.IsRepassed;
            if (_lastOrRepassedPack)
            {
                packProgressView.UpdateProgressAnimate(levelsCount + 1, null);
            } else
            {
                packProgressView.UpdateProgressAnimate(currentPackInfo.CurrentLevel, null);
            } 
            return;
        }
        packProgressView.UpdateProgressAnimate(levelsCount + 1, InitProgressView);
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
