using System;
using UnityEngine;

public class ContainerOfApples : IEnergyContainer
{
    public event Action OnEnergyChanged;
    private EnergySystemConfig _config;
    private int _applesCount;

    public void Init(EnergySystemConfig config, int startEnergy)
    {
        _config = config;
        _applesCount = startEnergy;
    }

    public bool IsFull() => _applesCount >= _config.MaxEnergy;

    public bool IsEnough(int value) => _applesCount >= value;

    public void Add(int value)
    {
        _applesCount += value;
        _applesCount = Mathf.Min(_applesCount, _config.MaxEnergy);
        OnEnergyChanged?.Invoke();
    }

    public void AddOverLimit(int value)
    {
        _applesCount += value;
        OnEnergyChanged?.Invoke();
    }

    public void Remove(int value)
    {
        _applesCount -= value;
        _applesCount = Mathf.Max(_applesCount, 0);
        OnEnergyChanged?.Invoke();
    }

    public EnergyState GetEnergyState() => new EnergyState(_applesCount, _config.MaxEnergy);
}
