using UnityEngine;

public class CellsGrid
{
    private readonly FieldSizeController _sizeController;
    private readonly BlockPropertiesParser _propertiesParser;
    private readonly CellsVisualization _cellsVisualization;
    private readonly Vector2 _startGridPos;
    private readonly float _margin;
    public CellOnGameField[,] CellsOnGameField { get; private set; }
    public Transform BlocksParent { get; private set; }
    public int RowCount { get; private set; }
    public int ColCount { get; private set; }
    public Vector2 CellSize { get; private set; }

    public CellsGrid(FieldSizeController sizeController, CellsVisualization cellsVisualization, Transform blocksParent)
    {
        _sizeController = sizeController;
        _startGridPos = _sizeController.StartGridPosition;
        _margin = _sizeController.CellsMargin;
        _cellsVisualization = cellsVisualization;
        BlocksParent = blocksParent;
        _propertiesParser = new BlockPropertiesParser();
    }
    
    public void Create(LevelData<TileProperties> levelData)
    {
        RowCount = levelData.RowCount;
        ColCount = levelData.ColCount;
        CellSize = _sizeController.CalculateCellSizeByResolution(ColCount);
        SetCellsParametersByTiles(levelData.Data);
        _cellsVisualization.Init(CellsOnGameField, RowCount, ColCount, CellSize);
    }

    private void SetCellsParametersByTiles(TileProperties[,] data)
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
                var blockProps = _propertiesParser.ParseBlockPropertiesFromTileData(data[row, col]);
                CellsOnGameField[row, col] = new CellOnGameField(currentCellPosition, blockProps);
                
                currentCellPosition.x += rightStep;
            }
            currentCellPosition.x = positionX;
            currentCellPosition.y += downStep;
        }
    }
}