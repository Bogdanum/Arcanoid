using System;

public class DroppableBonusFactory : PoolItemFactory<DroppableBonus, DroppableBonusSettings>
{
    public override Type PoolItemType => ItemPrefab.GetType();
    
    public override PoolItem CreateItem()
    {
        var bonus = Instantiate(ItemPrefab, _factoryTransform);
        return bonus;
    }
}
