using UnityEngine;

public interface IBlockSpawner
{
    Block Spawn(BlockProperties properties, Vector3 position, Vector3 scale, Transform parent);
}
