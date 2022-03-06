
public class BlockProperties
{
    public BlockType Type { get; }
    public BlockRendererParamsID ParamsID { get; }
    
    public BlockProperties(BlockType type, BlockRendererParamsID paramsID)
    {
        Type = type;
        ParamsID = paramsID;
    }
}
