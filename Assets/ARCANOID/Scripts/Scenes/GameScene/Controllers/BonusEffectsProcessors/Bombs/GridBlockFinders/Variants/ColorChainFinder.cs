using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorChainFinder : GridBlockFinder
{
    private List<Vector2Int> _directions;
    private List<Vector2Int> _positionsPreviousChain;
    private Dictionary<BlockRendererParamsID, List<SimpleBlock>> _setOfBlocksById;
    private Dictionary<SimpleBlock, Vector2Int> _blocksPositions;
    public override bool HasNextBlocks => true;

    public ColorChainFinder(Vector2 bombPosition, GridOfBlocks gridOfBlocks) : base(bombPosition, gridOfBlocks)
    {
        InitCollections();
        FindSimpleBlocks();
    }

    private void InitCollections()
    {
        _directions = DirectionsInGridHelper.WasdDirections;
        _positionsPreviousChain = new List<Vector2Int>();
        _positionsPreviousChain.Add(NormalizedBombPosition);
        _setOfBlocksById = new Dictionary<BlockRendererParamsID, List<SimpleBlock>>();
        _blocksPositions = new Dictionary<SimpleBlock, Vector2Int>();
    }
    
    private void FindSimpleBlocks()
    {
        var availableBlocks = new Dictionary<Vector2Int, BlockRendererParamsID>();
        foreach (var direction in _directions)
        {
            var currentBlockPosition = NormalizedBombPosition + direction;
            if (IsInGridRange(currentBlockPosition))
            {
                var block = GetBlockOnPosition(currentBlockPosition);
                if (block == null) continue;

                AddBlockToDictionary(block, currentBlockPosition);
                availableBlocks.Add(currentBlockPosition, block.ParamsID);
            }
        }
        foreach (var blockRecord in availableBlocks)
        {
            FindAvailableBlocksById(blockRecord.Key, blockRecord.Value);
        }
    }
    
    private SimpleBlock GetBlockOnPosition(Vector2Int currentBlockPosition)
    {
        return BlocksGrid[currentBlockPosition.x, currentBlockPosition.y] as SimpleBlock;
    }
    
    private void AddBlockToDictionary(SimpleBlock block, Vector2Int currentBlockPosition)
    { 
       var id = block.ParamsID;
       if (!_setOfBlocksById.ContainsKey(id))
       {
           _setOfBlocksById.Add(id, new List<SimpleBlock>());
       } 
       _setOfBlocksById[id].Add(block);
       
       if (_blocksPositions.ContainsKey(block)) return;
       _blocksPositions.Add(block, currentBlockPosition);
    }
    
    private void FindAvailableBlocksById(Vector2Int blockPosition, BlockRendererParamsID blockRendererId)
    {
        foreach (var direction in _directions)
        {
            var currentBlockPosition = direction + blockPosition;
            if (IsInGridRange(currentBlockPosition))
            {
                var block = GetBlockOnPosition(currentBlockPosition);
                if (block == null) continue;
                if (IsBlockMatchesId(block, blockRendererId))
                {
                    AddBlockToDictionary(block, currentBlockPosition);
                    FindAvailableBlocksById(currentBlockPosition, blockRendererId);
                }
            }
        }
    }

    private bool IsBlockMatchesId(SimpleBlock block, BlockRendererParamsID blockRendererId)
    {
        return block.ParamsID == blockRendererId && !_blocksPositions.Keys.Contains(block);
    }

    protected override void FillBlocksToDestroySet()
    {
        if (!_setOfBlocksById.Keys.Any()) return;

        BlockRendererParamsID bestId = FindBestAvailableParamsId();
        var positionsCurrentChain = new List<Vector2Int>();
        foreach (var block in _setOfBlocksById[bestId])
        {
            var blockPosition = _blocksPositions[block];
            var blockOnPosition = BlocksGrid[blockPosition.x, blockPosition.y];
            if (blockOnPosition == null) continue;

            foreach (var previousPosition in _positionsPreviousChain)
            {
                if (IsChainContinuous(blockPosition, previousPosition))
                {
                    AddToDestroySet(block);
                    positionsCurrentChain.Add(blockPosition);
                }
            }
        }
        _positionsPreviousChain = positionsCurrentChain;
    }

    private BlockRendererParamsID FindBestAvailableParamsId()
    {
        var currentId = BlockRendererParamsID.Dirt;
        int counter = 0;
        foreach (var rendererVariant in _setOfBlocksById)
        {
            int numberOfBlocksById = rendererVariant.Value.Count;
            if (counter < numberOfBlocksById)
            {
                currentId = rendererVariant.Key;
                counter = numberOfBlocksById;
            }
        }
        return currentId;
    }
    
    private bool IsChainContinuous(Vector2Int blockPosition, Vector2Int previousPosition)
    {
        int absDifferenceX = Mathf.Abs(previousPosition.x - blockPosition.x); 
        int absDifferenceY = Mathf.Abs(previousPosition.y - blockPosition.y);
        return absDifferenceX + absDifferenceY < 2;
    }
}
