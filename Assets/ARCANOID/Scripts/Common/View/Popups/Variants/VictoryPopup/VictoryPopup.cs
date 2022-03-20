using System;
using UnityEngine;
using Zenject;

public class VictoryPopup : BasePopup, IPackActionHandler
{
    [SerializeField] private PackProgressView packProgressView;
    private LevelPacksManager _levelPacksManager;
    private SceneLoader _sceneLoader;
    private LevelPackInfo _cachedPackInfo;
    private bool _lastOrRepassedPack;

    [Inject]
    public void Initialize(LevelPacksManager levelPacksManager, SceneLoader sceneLoader)
    {
        MessageBus.Subscribe(this);
        _levelPacksManager = levelPacksManager;
        _sceneLoader = sceneLoader;
        InitProgressView();
    }
    
    public override void PrepareToShow()
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
    
    public override void OnAppeared(Action onAppeared)
    {
        var currentPackInfo = _levelPacksManager.GetCurrentPackInfo();
        int levelsCount = currentPackInfo.Pack.Count;
        packProgressView.UpdateButtonLevel(currentPackInfo.CurrentLevel);
        if (_cachedPackInfo == currentPackInfo)
        {
            _lastOrRepassedPack = _cachedPackInfo.IsLast || _cachedPackInfo.IsRepassed;
            if (_lastOrRepassedPack)
            {
                packProgressView.UpdateProgressAnimate
                    (
                        levelsCount + 1, 
                        null, 
                        () => onAppeared?.Invoke()
                    );
            } else
            {
                packProgressView.UpdateProgressAnimate
                    (
                        currentPackInfo.CurrentLevel, 
                        null, 
                        () => onAppeared?.Invoke()
                    );
            }
            return;
        }
        packProgressView.UpdateProgressAnimate
            (
                levelsCount + 1, 
                InitProgressView, 
                () => onAppeared?.Invoke()
            );
    }

    public void OnContinueButtonClicked()
    {
        if (_lastOrRepassedPack)
        {
            MessageBus.RaiseEvent<IClearGameFieldHandler>(handler => handler.OnClearGameField());
            _sceneLoader.LoadScene(Scene.LevelSelection);
        } else
        {
            MessageBus.RaiseEvent<IGlobalGameStateHandler>(handler => handler.OnStartGame());
        }
    }
}
