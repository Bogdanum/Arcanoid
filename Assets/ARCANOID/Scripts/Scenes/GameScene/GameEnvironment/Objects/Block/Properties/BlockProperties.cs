
public class BlockProperties
{
    public BlockType Type { get; }
    public BlockRendererParamsID ParamsID { get; }
    public int CustomHealth { get; }
    public BonusId BonusId { get; }
    
    public BlockProperties(BlockType type, BlockRendererParamsID paramsID, int customHealth = 0, BonusId bonusId = BonusId.SimpleBomb)
    {
        Type = type;
        ParamsID = paramsID;
        CustomHealth = customHealth;
        BonusId = bonusId;
    }
}
