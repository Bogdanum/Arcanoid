using UnityEngine;

public class BedrockSpawner : IBlockSpawner
{
    private readonly PoolsManager _poolsManager;
    
    public BedrockSpawner(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
    }

    public Block Spawn(BlockProperties properties, Vector3 position, Vector3 scale, Transform parent)
    {
        var block = _poolsManager.GetItem<Bedrock>(position, scale, Quaternion.identity, parent);
        block.SetInitialParams();
        return block;
    }
}
