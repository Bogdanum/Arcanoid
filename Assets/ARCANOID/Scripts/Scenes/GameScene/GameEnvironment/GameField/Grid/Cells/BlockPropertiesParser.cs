public class BlockPropertiesParser
{
    public BlockProperties ParseBlockPropertiesFromTileData(TileProperties tileProperties)
    {
        return new BlockProperties
        (
            (BlockType)tileProperties.TileType, 
            (BlockRendererParamsID)tileProperties.TileRenderer,
            (BonusId)tileProperties.TileBonus
        );
    }
}