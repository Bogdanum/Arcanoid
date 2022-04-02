using System.Collections.Generic;
using UnityEngine;

public static class DirectionsInGridHelper
{
    public static readonly List<Vector2Int> AllDirections = new List<Vector2Int>()
    {
        Vector2Int.up, 
        Vector2Int.left, 
        Vector2Int.right,
        Vector2Int.down, 
        new Vector2Int(-1, 1),
        new Vector2Int(1, 1),
        new Vector2Int(-1, -1),
        new Vector2Int(1, -1)
    };

    public static readonly List<Vector2Int> WasdDirections = new List<Vector2Int>()
    {
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.down
    };

    public static Dictionary<Vector2Int, Vector2Int> GetInitialPositionDirectionMap(Vector2Int blockPosition, LineTntDirection lineDirection)
    {
        if (lineDirection == LineTntDirection.Vertical)
        {
            return new Dictionary<Vector2Int, Vector2Int>()
            {
                { blockPosition + Vector2Int.up, Vector2Int.up },
                { blockPosition + Vector2Int.down, Vector2Int.down }
            };
        }
        return new Dictionary<Vector2Int, Vector2Int>()
        {
                { blockPosition + Vector2Int.right, Vector2Int.right },
                { blockPosition + Vector2Int.left, Vector2Int.left }
        };
    }
}

public enum LineTntDirection
{
    Vertical,
    Horizontal
}