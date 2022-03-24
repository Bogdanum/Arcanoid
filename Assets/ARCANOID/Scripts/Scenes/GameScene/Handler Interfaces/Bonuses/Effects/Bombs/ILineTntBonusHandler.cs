using UnityEngine;

public interface ILineTntBonusHandler : ISubscriber
{
    void OnExplode(Vector2 tntPosition, LineTntDirection lineDirection);
}
