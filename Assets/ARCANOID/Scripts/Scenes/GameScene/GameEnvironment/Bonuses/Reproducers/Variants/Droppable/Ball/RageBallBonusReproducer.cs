public class RageBallBonusReproducer : IBonusEffectReproducer
{
    public void Reproduce()
    {
        MessageBus.RaiseEvent<IRageBallBonusHandler>(handler => handler.OnActivateRage());
    }
}
