using UnityEngine;

public class FinderNeighborsWithinRadius : GridBlockFinder
{
    public FinderNeighborsWithinRadius(Vector2 bombPosition, GridOfBlocks gridOfBlocks) : base(bombPosition, gridOfBlocks) { }

    protected override void FillBlocksToDestroySet()
    {
        foreach (var direction in DirectionsInGridHelper._allDirections)
        {
            var currentPosition = _normalizedBombPosition + direction;
            if (IsInGridRange(currentPosition))
            {
                var block = _blocksGrid[currentPosition.x, currentPosition.y];
                if (block != null)
                {
                    AddToDestroySet(block);
                }
            }
        }
        HasNextBlocks = false;
    }
}
