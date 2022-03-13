using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerController
{   
    private readonly PoolsManager _poolsManager;
    private readonly Dictionary<BlockType, IBlockSpawner> _spawners;
    private readonly Dictionary<Type, List<Block>> _blocks;
    
    public BlockSpawnerController(PoolsManager poolsManager, Dictionary<BlockType, IBlockSpawner> spawners)
    {
        _poolsManager = poolsManager;
        _spawners = spawners;
        _blocks = new Dictionary<Type, List<Block>>();
    }

    public Block GetSpawnedBlock(BlockProperties properties, Vector3 position, Vector3 scale, Transform parent)
    {
        var block = _spawners[properties.Type].Spawn(properties, position, scale, parent);
        AddToDictionary(block);
        return block;
    }

    private void AddToDictionary(Block block)
    {
        Type type = block.GetType();
        if (!_blocks.ContainsKey(type))
        {
            _blocks.Add(type, new List<Block>());
        }
        _blocks[type].Add(block);
    }

    public void DestroyConcreteBlock<T>(T block) where T : Block
    {
        _poolsManager.ReturnItemToPool(block);
        Type blockType = typeof(T);
        _blocks[blockType].Remove(block);
    }

    public void ClearBlocks()
    {
        foreach (var blockType in _blocks.Keys)
        {
            foreach (var block in _blocks[blockType])
            {
                _poolsManager.ReturnItemToPool(blockType, block);
            }
        }
        _blocks.Clear();
    }
}
