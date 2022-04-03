using System;
using System.Collections.Generic;
using UnityEngine;

public class GridOfBlocks : MonoBehaviour
{
    private Block[,] _grid;
    private Dictionary<Vector2, Vector2Int> _normalizedBlockLayouts;

    public void Fill(CellsGrid cellsGrid)
    {
        _grid = new Block[cellsGrid.ColCount, cellsGrid.RowCount];
        _normalizedBlockLayouts = new Dictionary<Vector2, Vector2Int>();

        for (int row = 0; row < cellsGrid.RowCount; row++)
        {
            for (int col = 0; col < cellsGrid.ColCount; col++)
            {
                var pos = cellsGrid.CellsOnGameField[row, col].Position;
                _normalizedBlockLayouts.Add(pos, new Vector2Int(col, row));
            }
        }
    }
    
    public void Add(Vector2 blockPosition, Block block)
    {
        var normalizedPos = _normalizedBlockLayouts[blockPosition];
        _grid[normalizedPos.x, normalizedPos.y] = block;
    }

    public void Remove(Block block)
    {
        Vector2 blockPos = block.transform.position;
        var normBlockPos = _normalizedBlockLayouts[blockPos];
        _grid[normBlockPos.x, normBlockPos.y] = null;
    }

    public Block[,] GetGrid() => _grid;

    public Vector2Int GetNormalizedBlockPosition(Vector2 blockPosition) => _normalizedBlockLayouts[blockPosition];
    
    #if UNITY_EDITOR

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (var block in _grid)
            {
                block?.Destroy();
            }
        }
    }

#endif
}
