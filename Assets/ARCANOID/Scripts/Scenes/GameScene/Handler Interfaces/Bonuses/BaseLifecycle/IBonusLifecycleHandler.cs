using UnityEngine;

public interface IBonusLifecycleHandler : ISubscriber
{
    void SpawnDroppableBonus(BonusId bonusId, Vector3 position);
    void StartIntrablockBonusAction(BonusId bonusId, Vector2 position);
    void ReturnToPool(DroppableBonus droppableBonus);
}
