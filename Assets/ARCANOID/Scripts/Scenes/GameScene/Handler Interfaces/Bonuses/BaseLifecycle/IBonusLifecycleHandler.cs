using UnityEngine;

public interface IBonusLifecycleHandler : ISubscriber
{
    void SpawnBonusObject(BonusId bonusId, Vector3 position);
    
    void ReturnToPool(DroppableBonus droppableBonus);
}
