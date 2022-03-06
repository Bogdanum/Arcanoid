using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerController
{   
    private readonly PoolsManager _poolsManager;
    private readonly Dictionary<Type, List<Block>> _blocks;

    public BlockSpawnerController(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
        _blocks = new Dictionary<Type, List<Block>>();
    }

    public Block GetSpawnedBlock(BlockProperties properties, Vector3 position, Vector3 scale, Transform parent)
    {
        switch (properties.Type)
        {
            case BlockType.Simple:
            {
                var block = new SimpleBlockSpawner(_poolsManager).Spawn(properties.ParamsID, position, scale, parent);
                AddToDictionary(block);
                return block;
            }
            case BlockType.Bedrock:
            {
                var block = new BedrockSpawner(_poolsManager).Spawn(position, scale, parent);
                AddToDictionary(block);
                return null;
            }
            default: return null;
        }
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
