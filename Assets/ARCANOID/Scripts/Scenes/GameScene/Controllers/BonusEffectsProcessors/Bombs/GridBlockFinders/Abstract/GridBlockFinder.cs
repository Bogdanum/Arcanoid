using System.Collections.Generic;
using UnityEngine;

public abstract class GridBlockFinder
{
    protected readonly Block[,] BlocksGrid;
    protected readonly Vector2Int NormalizedBombPosition;
    private HashSet<Block> _blocksToDestroy;
    private readonly int _colCount;
    private readonly int _rowCount;
    public virtual bool HasNextBlocks { get; protected set; } = true;

    protected GridBlockFinder(Vector2 bombPosition, GridOfBlocks gridOfBlocks)
    {
        var blocksOnScene = gridOfBlocks;
        BlocksGrid = blocksOnScene.GetGrid();
        _colCount = BlocksGrid.GetUpperBound(1);
        _rowCount = BlocksGrid.GetUpperBound(0);
        NormalizedBombPosition = blocksOnScene.GetNormalizedBlockPosition(bombPosition);
    }
    
    protected bool IsInGridRange(Vector2Int position)
    {
        bool inRangeX = position.x >= 0 && position.x <= _rowCount;
        bool inRangeY = position.y >= 0 && position.y <= _colCount;
        return inRangeX && inRangeY;
    }

    public HashSet<Block> GetNextSetToDestroy()
    {
        _blocksToDestroy = new HashSet<Block>();
        FillBlocksToDestroySet();
        return _blocksToDestroy;
    }

    protected void AddToDestroySet(Block block)
    {
        _blocksToDestroy.Add(block);
    }

    protected abstract void FillBlocksToDestroySet();
}
