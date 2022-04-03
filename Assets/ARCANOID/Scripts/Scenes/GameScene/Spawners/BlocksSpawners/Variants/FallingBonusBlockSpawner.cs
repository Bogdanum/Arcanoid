using UnityEngine;

public class FallingBonusBlockSpawner : IBlockSpawner
{
    private readonly PoolsManager _poolsManager;
    private readonly DroppableBonusSettings _droppableBonusSettings;

    public FallingBonusBlockSpawner(PoolsManager poolsManager, DroppableBonusSettings droppableBonusSettings)
    {
        _poolsManager = poolsManager;
        _droppableBonusSettings = droppableBonusSettings;
    }
    
    public Block Spawn(BlockProperties properties, Vector3 position, Vector3 scale, Transform parent)
    {
        var block = _poolsManager.GetItem<BonusBlock>(position, scale, Quaternion.identity, parent);
        block.SetInitialParams(properties.ParamsID);
        block.SetBonusType(properties.Type, _droppableBonusSettings.GetSprite(properties.BonusId));
        block.OnDestroyed += () => MessageBus.RaiseEvent<IBonusLifecycleHandler>(handler =>
                handler.SpawnDroppableBonus(properties.BonusId, position));
        return block;
    }
}
