public class PlatformSpeedBonusReproducer : IBonusEffectReproducer
{
    private readonly BinaryBonusDirection _direction;

    public PlatformSpeedBonusReproducer(BinaryBonusDirection direction)
    {
        _direction = direction;
    }
    
    public void Reproduce()
    {
        MessageBus.RaiseEvent<IPlatformSpeedHandler>(handler => handler.OnStartPlatformSpeedBonus(_direction));
    }
}
