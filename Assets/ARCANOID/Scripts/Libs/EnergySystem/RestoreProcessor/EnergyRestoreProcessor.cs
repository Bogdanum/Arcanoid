using System;
using System.Collections;
using UnityEngine;

public class EnergyRestoreProcessor : MonoBehaviour
{
    public event Action OnRestoreComplete;
    private EnergySystemConfig _config;
    private DateTime _nextRestoreTime;
    private bool _hasOfflineProgress;
    private bool _isRestoreActive;

    public void Init(EnergySystemConfig config)
    {
        _config = config;
    }

    public void StartRestoring()
    {
        if (_isRestoreActive) return;

        StartCoroutine(RestoreProcess());
    }

    private IEnumerator RestoreProcess()
    {
        _isRestoreActive = true;

        if (_nextRestoreTime != default && _hasOfflineProgress)
        {
            _nextRestoreTime = _nextRestoreTime.AddSeconds(_config.TimeToRestoreStep);
            _hasOfflineProgress = false;
        }
        else
        {
            _nextRestoreTime = DateTime.Now.AddSeconds(_config.TimeToRestoreStep);
        }

        yield return new WaitWhile(() => GetCurrentRestoreInterval().TotalSeconds > 0);
        
        _isRestoreActive = false;
        OnRestoreComplete?.Invoke();
    }

    public void StopRestoring()
    {
        StopCoroutine(RestoreProcess());
        _isRestoreActive = false;
    }

    public float GetCurrentRestoreStepProgress()
    {
        if (!_isRestoreActive) return 0;

        float remainingSeconds = (float)GetCurrentRestoreInterval().TotalSeconds;
        return remainingSeconds / _config.TimeToRestoreStep;
    }

    public TimeSpan GetCurrentRestoreInterval() => _nextRestoreTime.Subtract(DateTime.Now);

    public int GetOfflineEnergy(SavedEnergyProgress savedEnergyProgress)
    {
        var timeIntervalAfterSaving = DateTime.Now.Subtract(savedEnergyProgress.SaveTime);
        var remainingTimerSeconds = savedEnergyProgress.RecoveryProgress * _config.TimeToRestoreStep;
        var timerProgress = _config.TimeToRestoreStep - remainingTimerSeconds;
        var elapsedTimeAfterSaving = timeIntervalAfterSaving.TotalSeconds + timerProgress;
        var energyValue = _config.EnergyPerStep * elapsedTimeAfterSaving / _config.TimeToRestoreStep;
        _nextRestoreTime = DateTime.Now.AddSeconds(-energyValue % 1 * _config.TimeToRestoreStep);
        _hasOfflineProgress = true;
        return (int)energyValue;
    }
}