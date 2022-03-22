public class BallSpeedBonusReproducer : IBonusEffectReproducer
{
    private readonly BinaryBonusDirection _direction;

    public BallSpeedBonusReproducer(BinaryBonusDirection direction)
    {
        _direction = direction;
    }
    
    public void Reproduce()
    {
        MessageBus.RaiseEvent<IBallSpeedBonusHandler>(handler => handler.OnActivateBallSpeedBonus(_direction));
    }
}
