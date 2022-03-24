using UnityEngine;

public class ChainColorTntReproducer : IIntrablockEffectReproducer
{
    private Vector2 _bombPosition;

    public void Init(Vector2 bombPosition)
    {
        _bombPosition = bombPosition;
    }
    
    public void Reproduce()
    {
        MessageBus.RaiseEvent<IChainColorTntBonusHandler>(handler => handler.OnExplode(_bombPosition));
    }
}
