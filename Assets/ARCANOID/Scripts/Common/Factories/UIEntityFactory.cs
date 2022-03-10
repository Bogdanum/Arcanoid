using System;

public class UIEntityFactory : PoolItemFactory<PoolableUIEntity, BaseUIEntitiesSettings>
{
    public override PoolItem CreateItem<T>()
    {
        var entity = Instantiate(ItemPrefab, _factoryTransform);
        entity.Init();
        return entity;
    }

    public override Type PoolItemType => ItemPrefab.GetType();
}
