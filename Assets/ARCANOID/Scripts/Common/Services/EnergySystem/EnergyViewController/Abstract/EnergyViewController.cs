using TMPro;
using UnityEngine;
using Zenject;

public abstract class EnergyViewController : MonoBehaviour
{
    [SerializeField] protected TMP_Text energyProgress;
    [SerializeField] protected TMP_Text timer;
    protected EnergyManager _energyManager;
    protected bool _isRestoreProcessActive;

    [Inject]
    private void Init(EnergyManager energyManager)
    {
        _energyManager = energyManager;
    }
    
    protected virtual void OnEnable()
    {
        _energyManager.OnEnergyChanged += OnEnergyChanged;
    }

    protected virtual void OnDisable()
    {
        _energyManager.OnEnergyChanged -= OnEnergyChanged;
    }

    private void Update()
    {
        if (_isRestoreProcessActive)
        {
            var currentRestoreInterval = _energyManager.GetCurrentRestoreInterval();
            var minutes = currentRestoreInterval.Minutes;
            var seconds = currentRestoreInterval.Seconds;
            timer.text = string.Format("{0:d2}:{1:d2}", minutes, seconds);
        }
    }

    protected abstract void OnEnergyChanged();
}
