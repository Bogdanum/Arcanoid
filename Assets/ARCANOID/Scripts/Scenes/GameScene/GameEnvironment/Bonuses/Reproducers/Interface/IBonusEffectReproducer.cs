using UnityEngine;

public interface IBonusEffectReproducer
{
    void Reproduce();
}

public interface IIntrablockEffectReproducer : IBonusEffectReproducer
{
    void Init(Vector2 blockPosition);
}