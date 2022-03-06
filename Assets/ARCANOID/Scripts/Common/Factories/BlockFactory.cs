using System;
using UnityEngine;

public class BlockFactory : PoolItemFactory<Block, BlocksDesignProperties>
{
    public override Type PoolItemType => ItemPrefab.GetType();

    public override void Init(SpecificPoolSettings settings, Transform factoryTransform)
    {
        base.Init(settings, factoryTransform);
        ItemSettings.Init();
    }

    public override PoolItem CreateItem<T>()
    {
        var block = Instantiate(ItemPrefab, _factoryTransform);
        block.Init(ItemSettings);
        return block;
    }
}
