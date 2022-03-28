using UnityEngine;

public interface IChainColorTntBonusHandler : ISubscriber
{
    void OnExplode(Vector2 tntPosition);
}
