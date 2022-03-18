public class LevelData<TilePropsType>
{
    public int ColCount { get; }
    public int RowCount { get; }
    public TilePropsType[,] Data { get; }
    
    public LevelData(int colCount, int rowCount, TilePropsType[,] data)
    {
        ColCount = colCount;
        RowCount = rowCount;
        Data = data;
    }
}
