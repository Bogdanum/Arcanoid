using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    private Dictionary<Type, SpecificObjectPool> _allPools;

    public void Init(SpecificPoolsSettings settings)
    {
        _allPools = new Dictionary<Type, SpecificObjectPool>();
        CreatePools(settings);
    }

    private void CreatePools(SpecificPoolsSettings settings)
    {
        foreach (var poolSettings in settings.AllPoolsSettings)
        {
            var factoryPrefab = poolSettings.factory;
            var factory = Instantiate(factoryPrefab, transform);
            factory.Init(poolSettings, factory.transform);
            
            var pool = new SpecificObjectPool(factory);
            pool.Resize(poolSettings.initialCount);
            _allPools.Add(factory.PoolItemType, pool);
        }
    }

    public T GetItem<T>(Vector3 position, Vector3 scale, Quaternion rotation, Transform parent = null) where T : PoolItem
    {
        return GetInitializedItemFromPool<T>(position, scale, rotation, parent);
    }

    public T GetItem<T>(Vector3 position, Transform parent = null) where T : PoolItem
    {
        return GetInitializedItemFromPool<T>(position, Vector3.one, Quaternion.identity, parent);
    }

    public void ReturnItemToPool<T>(T item) where T : PoolItem
    {
        var itemType = typeof(T);
        _allPools[itemType].ReturnToPool(item);
    }

    public void ReturnItemToPool<T>(Type itemType, T item) where T : PoolItem
    {
        _allPools[itemType].ReturnToPool(item);
    }

    private T GetInitializedItemFromPool<T>(Vector3 position, Vector3 scale, Quaternion rotation, Transform parent = null) where T : PoolItem
    {
        Type itemType = typeof(T);
        var item = _allPools[itemType].Get();
        var itemTransform = item.transform;
        itemTransform.position = position;
        itemTransform.localScale = scale;
        itemTransform.rotation = rotation;
        itemTransform.SetParent(parent);
        return item as T;
    }
}
