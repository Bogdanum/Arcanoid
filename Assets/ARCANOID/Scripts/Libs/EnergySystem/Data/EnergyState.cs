public class EnergyState
{
    public bool IsFull { get; }
    public int Energy { get; }
    public int Max { get; }

    public EnergyState(int energyEnergy, int maxEnergy)
    {
        IsFull = energyEnergy >= maxEnergy;
        Energy = energyEnergy;
        Max = maxEnergy;
    }
}
