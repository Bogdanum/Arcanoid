using UnityEngine;

public interface IBlockSpawner
{ 
    BlockType BlockType { get; }
    Block Spawn(BlockProperties properties, Vector3 position, Vector3 scale, Transform parent);
}
