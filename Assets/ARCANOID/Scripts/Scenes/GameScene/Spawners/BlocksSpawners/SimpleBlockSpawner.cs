using UnityEngine;

public class SimpleBlockSpawner
{
    private PoolsManager _poolsManager;

    public SimpleBlockSpawner(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
    }

    public SimpleBlock Spawn(BlockRendererParamsID paramsID, Vector3 position, Vector3 scale, Transform parent)
    {
        var block = _poolsManager.GetItem<SimpleBlock>(position, scale, Quaternion.identity, parent);
        block.SetInitialParams(paramsID);
        return block;
    }
}
