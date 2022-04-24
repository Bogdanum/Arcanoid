using System;

public class BlockFactory : PoolItemFactory<Block, BlocksDesignProperties>
{
    public override Type PoolItemType => ItemPrefab.GetType();

    public override PoolItem CreateItem()
    {
        var block = Instantiate(ItemPrefab, _factoryTransform);
        block.Init(ItemSettings);
        return block;
    }
}
