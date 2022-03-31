using System;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public event Action OnEnergyChanged;
    [SerializeField] private EnergyRestoreProcessor energyRestoreProcessor;
    private EnergySystemConfig _config;
    private StoredDataManager _storedDataManager;
    private SavedEnergyProgress _savedEnergyProgress;
    private IEnergyContainer _energyContainer;

    public void Init(EnergySystemConfig config, StoredDataManager storedDataManager, IEnergyContainer energyContainer)
    {
        _config = config;
        _storedDataManager = storedDataManager;
        _energyContainer = energyContainer;

        LoadLastSavedEnergy();
        energyRestoreProcessor.Init(_config);
        _energyContainer.Init(_config, _savedEnergyProgress.Energy);
        GetOfflineEnergy();
    }

    private void OnEnable()
    {
        _energyContainer.OnEnergyChanged += OnEnergyUpdated;
        energyRestoreProcessor.OnRestoreComplete += AddEnergyAfterRestoring;
        OnEnergyUpdated();
    }

    private void OnDisable()
    {
        _energyContainer.OnEnergyChanged -= OnEnergyUpdated;
        energyRestoreProcessor.OnRestoreComplete -= AddEnergyAfterRestoring;
    }

    private void LoadLastSavedEnergy()
    {
        var defaultEnergyProgress = new SavedEnergyProgress()
        {
            SaveTime = DateTime.Now,
            Energy = _config.MaxEnergy
        };
        _savedEnergyProgress = _storedDataManager.GetSavedData<SavedEnergyProgress>(defaultEnergyProgress);
    }
    
    private void GetOfflineEnergy()
    {
        if (_energyContainer.IsFull()) return;

        int offlineEnergy = energyRestoreProcessor.GetOfflineEnergy(_savedEnergyProgress);
        _energyContainer.Add(offlineEnergy);
    }
    
    private void OnEnergyUpdated()
    {
        OnEnergyChanged?.Invoke();
        if (_energyContainer.IsFull())
        {
            energyRestoreProcessor.StopRestoring();
            return;
        }
        energyRestoreProcessor.StartRestoring();
    }
    
    private void AddEnergyAfterRestoring()
    {
        _energyContainer.Add(_config.EnergyPerStep);
    }

    public void AddEnergyForAction(ActionWithEnergy action)
    {
        int energy = GetEnergyActionValue(action);
        _energyContainer.AddOverLimit(energy);
    }

    public void RemoveEnergy(ActionWithEnergy action)
    {
        int energy = GetEnergyActionValue(action);
        if (IsEnoughEnergy(energy))
        {
            _energyContainer.Remove(energy);
        }
    }

    public EnergyState GetCurrentEnergyState() => _energyContainer.GetEnergyState();

    public bool IsEnoughEnergy(int energy) => _energyContainer.IsEnough(energy);

    public int GetEnergyActionValue(ActionWithEnergy action) => _config.GetEnergyValue(action);

    public TimeSpan GetCurrentRestoreInterval() => energyRestoreProcessor.GetCurrentRestoreInterval();

    private void OnApplicationPause(bool pauseStatus) => SaveEnergyState();
    private void OnApplicationQuit() => SaveEnergyState();

    private void SaveEnergyState()
    {
        var currentEnergyState = GetCurrentEnergyState();
        _savedEnergyProgress.Energy = currentEnergyState.Energy;
        _savedEnergyProgress.RecoveryProgress = energyRestoreProcessor.GetCurrentRestoreStepProgress();
        _savedEnergyProgress.SaveTime = DateTime.Now;
        _storedDataManager.SaveEnergyProgress(_savedEnergyProgress);
    }
    
    #if UNITY_EDITOR
    
    [EditorButton("Add 3 energy")]
    public void AddEnergy()
    {
        _energyContainer.AddOverLimit(3);
    }
    
    [EditorButton("Remove 3 energy")]
    public void RemoveEnergy()
    {
        _energyContainer.Remove(3);
    }

    #endif
}