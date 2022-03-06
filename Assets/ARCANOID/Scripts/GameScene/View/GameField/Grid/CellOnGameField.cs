using UnityEngine;

public class CellOnGameField
{
    public Vector2 Position { get; }
    public BlockProperties BlockProperties { get; }

    public CellOnGameField(Vector2 position, BlockProperties blockProperties)
    {
        Position = position;
        BlockProperties = blockProperties;
    }
}
