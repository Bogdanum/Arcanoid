using UnityEngine;

public class CellsVisualization : MonoBehaviour
{
    [SerializeField] private Color gizmosColor = Color.cyan;
    private CellOnGameField[,] _cellsOnGameField;
    private int _rows;
    private int _cols;
    private Vector2 _cellsSize;
    private bool _active;
    
    public void Init(CellOnGameField[,] cellsOnGameField, int rows, int cols, Vector2 cellsSize)
    {
        _cellsOnGameField = cellsOnGameField;
        _rows = rows;
        _cols = cols;
        _cellsSize = cellsSize;
        _active = true;
    }

    private void OnDrawGizmos()
    {
        if (!_active) return;

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                var cell = _cellsOnGameField[row, col];
                Gizmos.color = gizmosColor;
                Gizmos.DrawWireCube(cell.Position, _cellsSize);
            }
        }
    }
}
