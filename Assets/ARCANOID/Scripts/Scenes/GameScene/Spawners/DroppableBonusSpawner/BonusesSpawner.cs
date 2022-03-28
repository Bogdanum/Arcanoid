using System.Collections.Generic;
using UnityEngine;

public class BonusesSpawner
{
    private readonly List<DroppableBonus> _bonusesOnScene;
    private readonly PoolsManager _poolsManager;
    private readonly DroppableBonusSettings _settings;
    private readonly Dictionary<BonusId, IBonusEffectReproducer> _effectReproducers;

    public BonusesSpawner(PoolsManager poolsManager, DroppableBonusSettings settings, Dictionary<BonusId, IBonusEffectReproducer> effectReproducers)
    {
        _effectReproducers = effectReproducers;
        _poolsManager = poolsManager;
        _settings = settings;
        _bonusesOnScene = new List<DroppableBonus>();
    }

    public void SpawnDroppableBonus(BonusId bonusId, Vector3 position, Transform parent)
    {
        var bonus = _poolsManager.GetItem<DroppableBonus>(position, parent);
        bonus.SetParams(bonusId, _settings, _effectReproducers[bonusId]);
        _bonusesOnScene.Add(bonus);
    }

    public IBonusEffectReproducer GetEffectReproducer(BonusId bonusId, Vector2 position)
    {
        var reproducer = _effectReproducers[bonusId] as IIntrablockEffectReproducer;
        reproducer.Init(position);
        return reproducer;
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
