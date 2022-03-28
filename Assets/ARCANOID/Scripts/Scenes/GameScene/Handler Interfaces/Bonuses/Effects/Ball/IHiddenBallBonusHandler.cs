using UnityEngine;

public interface IHiddenBallBonusHandler : ISubscriber
{
    void OnActivateHiddenBallBonus(Vector2 bonusPosition);
}
