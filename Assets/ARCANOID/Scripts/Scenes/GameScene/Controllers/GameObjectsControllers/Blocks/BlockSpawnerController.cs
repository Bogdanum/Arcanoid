using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockSpawnerController
{   
    private readonly PoolsManager _poolsManager;
    private readonly Dictionary<BlockType, IBlockSpawner> _spawners;
    private readonly Dictionary<Type, List<Block>> _blocks;
    private Vector3 _currentBlockScale;
    
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
        _currentBlockScale = scale;
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

    public List<T> GetBlocks<T>() where T : Block
    {
        var blockType = typeof(T);
        if (_blocks.ContainsKey(blockType))
        {
            return _blocks[blockType].Select(block => block as T).ToList();
        }
        return null;
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

    public Vector3 GetCurrentBlockScale() => _currentBlockScale;
}
