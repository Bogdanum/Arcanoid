using UnityEngine;

public class BedrockSpawner
{
    private PoolsManager _poolsManager;

    public BedrockSpawner(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
    }

    public Bedrock Spawn(Vector3 position, Vector3 scale, Transform parent)
    {
        var block = _poolsManager.GetItem<Bedrock>(position, scale, Quaternion.identity, parent);
        block.SetInitialParams();
        return block;
    }
}
