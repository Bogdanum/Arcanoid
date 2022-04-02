public class BlocksCreatorProvider
{
    private readonly CellsGrid _cellsGrid;

    public BlocksCreatorProvider(CellsGrid cellsGrid)
    {
        _cellsGrid = cellsGrid;
    }
    
    public void SendCreateBlocksRequest()
    {
        for (int row = 0; row < _cellsGrid.RowCount; row++)
        {
            for (int col = 0; col < _cellsGrid.ColCount; col++)
            {
                var currentCell = _cellsGrid.CellsOnGameField[row, col];
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
                handler.OnGetBlockParams(cell.Position, _cellsGrid.CellSize, _cellsGrid.BlocksParent, cell.BlockProperties));
        }
    }
}
