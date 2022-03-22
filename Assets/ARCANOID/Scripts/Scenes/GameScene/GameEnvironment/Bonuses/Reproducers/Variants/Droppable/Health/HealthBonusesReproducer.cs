public class HealthBonusesReproducer : IBonusEffectReproducer
{
    private readonly BinaryBonusDirection _direction;
    
    public HealthBonusesReproducer(BinaryBonusDirection direction)
    {
        _direction = direction;
    }
    
    public void Reproduce()
    {
        MessageBus.RaiseEvent<IHealthBonusHandler>(handler => handler.OnActivateHealthBonus(_direction));
    }
}
