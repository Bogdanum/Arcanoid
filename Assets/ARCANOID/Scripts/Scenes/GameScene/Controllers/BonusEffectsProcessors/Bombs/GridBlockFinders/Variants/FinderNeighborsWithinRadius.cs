using UnityEngine;

public class FinderNeighborsWithinRadius : GridBlockFinder
{
    public FinderNeighborsWithinRadius(Vector2 bombPosition, GridOfBlocks gridOfBlocks) : base(bombPosition, gridOfBlocks) { }

    protected override void FillBlocksToDestroySet()
    {
        foreach (var direction in DirectionsInGridHelper.AllDirections)
        {
            var currentPosition = NormalizedBombPosition + direction;
            if (IsInGridRange(currentPosition))
            {
                var block = BlocksGrid[currentPosition.x, currentPosition.y];
                if (block != null)
                {
                    AddToDestroySet(block);
                }
            }
        }
        HasNextBlocks = false;
    }
}
