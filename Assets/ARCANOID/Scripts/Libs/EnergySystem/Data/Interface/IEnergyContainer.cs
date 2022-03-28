public interface IEnergyContainer
{
    event System.Action OnEnergyChanged;

    void Init(EnergySystemConfig config, int startEnergy);
    bool IsFull();
    bool IsEnough(int value);
    void Add(int value);
    void AddOverLimit(int value);
    void Remove(int value);
    EnergyState GetEnergyState();
}