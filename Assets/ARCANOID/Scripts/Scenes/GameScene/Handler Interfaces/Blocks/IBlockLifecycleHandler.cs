using UnityEngine;

public interface IBlockLifecycleHandler : ISubscriber
{
    void OnDestructibleBlockSpawned();
    void OnGetBlockParams(Vector3 position, Vector3 size, Transform parent, BlockProperties properties);
    void OnPlayingBlockDestructionParticles(Block block);
    void OnBlockDestroyed<T>(T block) where T : Block;
}