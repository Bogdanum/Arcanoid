using UnityEngine;

public class SimpleBlockSpawner : IBlockSpawner
{
    public BlockType BlockType => BlockType.Simple;
    private readonly PoolsManager _poolsManager;
    
    public SimpleBlockSpawner(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
    }
    
    public Block Spawn(BlockProperties properties, Vector3 position, Vector3 scale, Transform parent)
    {
        var block = _poolsManager.GetItem<SimpleBlock>(position, scale, Quaternion.identity, parent);
        block.SetInitialParams(properties.ParamsID, properties.CustomHealth);
        return block;
    }
}
