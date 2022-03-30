using DG.Tweening;
using UnityEngine;

public class EnergyViewWithSliderController : EnergyViewController
{
    [SerializeField] private ProgressSliderView slider;
    [SerializeField] private ValueChangeAnimation valueAnimation;
    private EnergyState _previousEnergyState;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        _previousEnergyState = _energyManager.GetCurrentEnergyState();
        OnEnergyChanged();
        SetupSlider();
    }

    protected override void OnEnergyChanged()
    {
        PlaySliderAnimation();
    }

    public void PlaySliderAnimation(TweenCallback onComplete = null)
    {
        var energyState = _energyManager.GetCurrentEnergyState();
        onComplete ??= () => _previousEnergyState = energyState;
        valueAnimation.Play(_previousEnergyState.Energy, energyState.Energy, UpdateProgress, onComplete);
    }

    private void UpdateProgress(float energy)
    {
        slider.UpdateProgress(energy);
        int currentEnergy = (int)energy;
        energyProgress.text = $"{currentEnergy.ToString()}/{_previousEnergyState.Max.ToString()}";
        _isRestoreProcessActive = currentEnergy < _previousEnergyState.Max;
        timer.SetActive(_isRestoreProcessActive);
    }
    
    private void SetupSlider()
    {
        slider.SetMaxValue(_previousEnergyState.Max);
        slider.UpdateProgress(_previousEnergyState.Energy);
    }

    public void StopAnimation() => valueAnimation.Stop();
}
