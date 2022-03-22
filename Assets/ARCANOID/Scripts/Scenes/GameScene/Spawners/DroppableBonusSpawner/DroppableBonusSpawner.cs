using System.Collections.Generic;
using UnityEngine;

public class DroppableBonusSpawner
{
    private readonly List<DroppableBonus> _bonusesOnScene;
    private readonly PoolsManager _poolsManager;
    private readonly DroppableBonusSettings _settings;
    private readonly Dictionary<BonusId, IBonusEffectReproducer> _effectReproducers;

    public DroppableBonusSpawner(PoolsManager poolsManager, DroppableBonusSettings settings, Dictionary<BonusId, IBonusEffectReproducer> effectReproducers)
    {
        _effectReproducers = effectReproducers;
        _poolsManager = poolsManager;
        _settings = settings;
        _bonusesOnScene = new List<DroppableBonus>();
    }

    public void Spawn(BonusId bonusId, Vector3 position, Transform parent)
    {
        var bonus = _poolsManager.GetItem<DroppableBonus>(position, parent);
        bonus.SetParams(bonusId, _settings, _effectReproducers[bonusId]);
        _bonusesOnScene.Add(bonus);
    }

    public void Destroy(DroppableBonus droppableBonus)
    {
        _poolsManager.ReturnItemToPool(droppableBonus);
        _bonusesOnScene.Remove(droppableBonus);
    }

    public void ClearAll()
    {
        _bonusesOnScene.ForEach(_poolsManager.ReturnItemToPool);
        _bonusesOnScene.Clear();
    }
}
