using UnityEngine;

public abstract class PoolItemFactory<P, S> : AbstractPoolItemFactory where P : PoolItem where S : ScriptableObject
{
    protected P ItemPrefab;
    protected S ItemSettings;
    protected Transform _factoryTransform;
    public override Transform FactoryTransform => _factoryTransform;
    public override void Init(SpecificPoolSettings settings, Transform factoryTransform)
    {
        ItemPrefab = settings.itemPrefab as P;
        ItemSettings = settings.itemSettings as S;
        _factoryTransform = factoryTransform;
    }
}
