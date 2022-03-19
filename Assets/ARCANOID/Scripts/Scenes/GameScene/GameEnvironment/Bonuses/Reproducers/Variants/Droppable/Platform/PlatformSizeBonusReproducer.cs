public class PlatformSizeBonusReproducer : IBonusEffectReproducer
{
    private readonly BinaryBonusDirection _direction;

    public PlatformSizeBonusReproducer(BinaryBonusDirection direction)
    {
        _direction = direction;
    }

    public void Reproduce()
    {
        MessageBus.RaiseEvent<IPlatformSizeHandler>(handler => handler.OnStartPlatformSizeBonus(_direction));
    }
}
