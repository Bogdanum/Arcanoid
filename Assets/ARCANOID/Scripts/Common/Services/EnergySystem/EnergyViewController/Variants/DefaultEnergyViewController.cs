public class DefaultEnergyViewController : EnergyViewController
{
    protected override void OnEnable()
    {
        base.OnEnable();
        OnEnergyChanged();
    }

    protected override void OnEnergyChanged()
    {
        var energyState = _energyManager.GetCurrentEnergyState();
        energyProgress.text = $"{energyState.Energy.ToString()}/{energyState.Max.ToString()}";
        _isRestoreProcessActive = !energyState.IsFull;
        timer.SetActive(_isRestoreProcessActive);
    }
}