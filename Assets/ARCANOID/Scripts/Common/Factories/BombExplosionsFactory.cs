using System;

public class BombExplosionsFactory : PoolItemFactory<BombExplosionParticles, BaseUIEntitiesSettings>
{
    public override Type PoolItemType => ItemPrefab.GetType();
    public override PoolItem CreateItem()
    {
        var particles = Instantiate(ItemPrefab, _factoryTransform);
        return particles;
    }
}
