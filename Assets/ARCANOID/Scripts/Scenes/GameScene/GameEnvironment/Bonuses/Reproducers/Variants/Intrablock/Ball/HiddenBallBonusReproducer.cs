using UnityEngine;

public class HiddenBallBonusReproducer : IIntrablockEffectReproducer
{
    private Vector2 _blockPosition;

    public void Init(Vector2 blockPosition)
    {
        _blockPosition = blockPosition;
    }
    
    public void Reproduce()
    {
        MessageBus.RaiseEvent<IHiddenBallBonusHandler>(handler => handler.OnActivateHiddenBallBonus(_blockPosition));
    }
}
