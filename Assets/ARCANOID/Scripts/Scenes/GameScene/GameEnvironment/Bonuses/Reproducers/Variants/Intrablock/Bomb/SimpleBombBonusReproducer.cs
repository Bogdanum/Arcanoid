using UnityEngine;

public class SimpleBombBonusReproducer : IIntrablockEffectReproducer
{
    private Vector2 _bombPosition;

    public void Init(Vector2 bombPosition)
    {
        _bombPosition = bombPosition;
    }
    
    public void Reproduce()
    {
        MessageBus.RaiseEvent<ISimpleBombBonusHandler>(handler => handler.OnExplode(_bombPosition));
    }
}