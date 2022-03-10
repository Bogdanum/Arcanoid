public class SimpleBlock : DestructibleBlock
{
    public BlockRendererParamsID ParamsID { get; private set; }

    public override void Init(BlocksDesignProperties designProps)
    {
        base.Init(designProps);
        Type = BlockType.Simple;
    }

    public override void SetInitialParams(BlockRendererParamsID paramsID)
    {
        base.SetInitialParams(paramsID);
        ParamsID = paramsID;
    }

    protected override void OnCompleteDestroyParticles()
    {
        MessageBus.RaiseEvent<IBlockLifecycleHandler>(handler => handler.OnBlockDestroyed(this));
    }
}
