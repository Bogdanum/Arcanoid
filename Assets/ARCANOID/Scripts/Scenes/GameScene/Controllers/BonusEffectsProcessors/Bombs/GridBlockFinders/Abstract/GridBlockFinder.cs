using System.Collections.Generic;
using UnityEngine;

public abstract class GridBlockFinder
{
    protected readonly Block[,] _blocksGrid;
    protected readonly Vector2Int _normalizedBombPosition;
    private HashSet<Block> _blocksToDestroy;
    private readonly GridOfBlocks _gridOfBlocks;
    private readonly int _colCount;
    private readonly int _rowCount;
    public virtual bool HasNextBlocks { get; protected set; }

    public GridBlockFinder(Vector2 bombPosition, GridOfBlocks gridOfBlocks)
    {
        HasNextBlocks = true;
        _gridOfBlocks = gridOfBlocks;
        _blocksGrid = _gridOfBlocks.GetGrid();
        _colCount = _blocksGrid.GetUpperBound(1);
        _rowCount = _blocksGrid.GetUpperBound(0);
        _normalizedBombPosition = _gridOfBlocks.GetNormalizedBlockPosition(bombPosition);
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
        _gridOfBlocks.Remove(block);
    }

    protected abstract void FillBlocksToDestroySet();
}
