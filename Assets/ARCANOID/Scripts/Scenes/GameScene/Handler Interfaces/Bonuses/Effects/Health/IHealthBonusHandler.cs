public interface IHealthBonusHandler : ISubscriber
{
    void OnActivateHealthBonus(BinaryBonusDirection direction);
}
