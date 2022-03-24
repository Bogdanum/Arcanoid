using System.Collections.Generic;
using UnityEngine;

public static class DirectionsInGridHelper
{
    public static readonly List<Vector2Int> _allDirections = new List<Vector2Int>()
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

    public static readonly List<Vector2Int> _wasdDirections = new List<Vector2Int>()
    {
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.down
    };
}
