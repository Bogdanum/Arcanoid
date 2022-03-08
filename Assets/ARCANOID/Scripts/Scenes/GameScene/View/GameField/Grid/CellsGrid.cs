using UnityEngine;

public class CellsGrid
{
    public CellOnGameField[,] CellsOnGameField { get; private set; }
    public int RowCount { get; private set; }
    public int ColCount { get; private set; }
    public Vector2 CellSize { get; private set; }
    
    private FieldSizeController _sizeController;
    private CellsVisualization _cellsVisualization;
    private Transform _blocksParent;
    private Vector2 _startGridPos;
    private float _margin;

    public CellsGrid(FieldSizeController sizeController, CellsVisualization cellsVisualization, Transform blocksParent)
    {
        _sizeController = sizeController;
        _startGridPos = _sizeController.StartGridPosition;
        _margin = _sizeController.CellsMargin;
        _cellsVisualization = cellsVisualization;
        _blocksParent = blocksParent;
    }
    
    // test (change rowCount & colCount to current level params)
    public void Create(int rowCount, int colCount)
    {
        RowCount = rowCount;
        ColCount = colCount;
        CellSize = _sizeController.CalculateCellSizeByResolution(ColCount);
        SetCellsParametersByTiles();
        _cellsVisualization.Init(CellsOnGameField, rowCount, colCount, CellSize);
    }

    private void SetCellsParametersByTiles()
    {
        CellsOnGameField = new CellOnGameField[RowCount, ColCount];

        float positionX = _startGridPos.x + CellSize.x / 2;
        float positionY = _startGridPos.y - CellSize.y / 2;
        var currentCellPosition = new Vector2(positionX, positionY);

        float rightStep = CellSize.x + _margin;
        float downStep = -(CellSize.y + _margin);

        for (int row = 0; row < RowCount; row++)
        {
            for (int col = 0; col < ColCount; col++)
            {
                // change test to real props
                var testProps = new BlockProperties(BlockType.Simple, BlockRendererParamsID.Stone);
                CellsOnGameField[row, col] = new CellOnGameField(currentCellPosition, testProps);
                
                currentCellPosition.x += rightStep;
            }
            currentCellPosition.x = positionX;
            currentCellPosition.y += downStep;
        }
    }

    public void SendCreateBlocksRequest()
    {
        for (int row = 0; row < RowCount; row++)
        {
            for (int col = 0; col < ColCount; col++)
            {
                var currentCell = CellsOnGameField[row, col];
                if (currentCell.BlockProperties.Type != BlockType.Empty)
                {
                    CreateBlockOnCell(currentCell);
                }
            }
        }
    }

    private void CreateBlockOnCell(CellOnGameField cell)
    {
        if (cell.BlockProperties.Type != BlockType.Empty)
        {
            MessageBus.RaiseEvent<IBlockLifecycleHandler>(handler =>
                    handler.OnGetBlockParams(cell.Position, CellSize, _blocksParent, cell.BlockProperties));
        }
    }
}
