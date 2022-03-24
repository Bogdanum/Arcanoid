using UnityEngine;

public class LineTntBonusReproducer : IIntrablockEffectReproducer
{
    private readonly LineTntDirection _lineDirection;
    private Vector2 _tntPosition;

    public LineTntBonusReproducer(LineTntDirection lineDirection)
    {
        _lineDirection = lineDirection;
    }
    
    public void Init(Vector2 blockPosition)
    {
        _tntPosition = blockPosition;
    }
    
    public void Reproduce()
    {
        MessageBus.RaiseEvent<ILineTntBonusHandler>(handler => handler.OnExplode(_tntPosition, _lineDirection));
    }
}
