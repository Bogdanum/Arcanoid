using UnityEngine;

public class IntrablockBonusSpawner : IBlockSpawner
{
    private readonly PoolsManager _poolsManager;

    public IntrablockBonusSpawner(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
    }
    
    public Block Spawn(BlockProperties properties, Vector3 position, Vector3 scale, Transform parent)
    {
        var block = _poolsManager.GetItem<BonusBlock>(position, scale, Quaternion.identity, parent);
        block.SetInitialParams(properties.ParamsID);
        block.SetBonusType(properties.Type);
        block.OnDestroyed += () => MessageBus.RaiseEvent<IBonusLifecycleHandler>(handler =>
            handler.StartIntrablockBonusAction(properties.BonusId, position));
        return block;
    }
}
