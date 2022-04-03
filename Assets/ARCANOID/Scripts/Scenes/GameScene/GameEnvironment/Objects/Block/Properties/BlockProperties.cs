
public class BlockProperties
{
    public BlockType Type { get; }
    public BlockRendererParamsID ParamsID { get; }
    public BonusId BonusId { get; }
    
    public BlockProperties(BlockType type, BlockRendererParamsID paramsID, BonusId bonusId = BonusId.SimpleBomb)
    {
        Type = type;
        ParamsID = paramsID;
        BonusId = bonusId;
    }
}