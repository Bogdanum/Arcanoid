using UnityEngine;

public interface ISimpleBombBonusHandler : ISubscriber
{
    void OnExplode(Vector2 bombPosition);
}
