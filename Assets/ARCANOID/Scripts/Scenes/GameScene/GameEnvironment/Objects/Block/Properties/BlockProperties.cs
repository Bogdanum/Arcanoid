
public class BlockProperties
{
    public BlockType Type { get; }
    public BlockRendererParamsID ParamsID { get; }
    public int CustomHealth { get; }
    
    public BlockProperties(BlockType type, BlockRendererParamsID paramsID, int customHealth = 0)
    {
        Type = type;
        ParamsID = paramsID;
        CustomHealth = customHealth;
    }
}
