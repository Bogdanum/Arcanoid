using System;
using UnityEngine;

public abstract class AbstractPoolItemFactory : MonoBehaviour
{
    public abstract void Init(SpecificPoolSettings settings, Transform factoryTransform);
    public abstract Transform FactoryTransform { get; }
    public abstract Type PoolItemType { get; }
    public abstract PoolItem CreateItem();
}
