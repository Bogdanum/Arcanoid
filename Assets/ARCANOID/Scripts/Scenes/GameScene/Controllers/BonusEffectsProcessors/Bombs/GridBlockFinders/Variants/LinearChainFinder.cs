using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinearChainFinder : GridBlockFinder
{
    private readonly Dictionary<Vector2Int, Vector2Int> _positionDirectionMap;

    public LinearChainFinder(Vector2 bombPosition, GridOfBlocks gridOfBlocks, LineTntDirection lineDirection) : base(bombPosition, gridOfBlocks)
    {
        _positionDirectionMap = DirectionsInGridHelper.GetInitialPositionDirectionMap(NormalizedBombPosition, lineDirection);
    }

    protected override void FillBlocksToDestroySet()
    {
        HasNextBlocks = false;
        var initialPosDirMap = _positionDirectionMap.ToList();
        _positionDirectionMap.Clear();
        
        foreach (var pair in initialPosDirMap)
        {
            var position = pair.Key;
            if (IsInGridRange(position))
            {
                HasNextBlocks = true;
                Block block = BlocksGrid[position.x, position.y];
                if (block != null)
                {
                    AddToDestroySet(block);
                }
                _positionDirectionMap.Add(position + pair.Value, pair.Value);
            }
        }
    }
}
