using System.Collections.Generic;
using System.Linq;

public class SpecificObjectPool
{
     private AbstractPoolItemFactory _factory;
     private Queue<PoolItem> _objectsQueue;

     public SpecificObjectPool(AbstractPoolItemFactory factory)
     {
          _factory = factory;
          _objectsQueue = new Queue<PoolItem>();
     }
     
     public void Resize(int initialQuantity = 5)
     {
          for (int i = 0; i < initialQuantity; i++)
          {
               var item = _factory.CreateItem();
               item.OnDespawned();
               _objectsQueue.Enqueue(item);
          }
     }

     public void ReturnToPool(PoolItem objectToReturn)
     {
          objectToReturn.OnDespawned();
          objectToReturn.transform.SetParent(_factory.FactoryTransform);
          _objectsQueue.Enqueue(objectToReturn);
     }

     public PoolItem Get()
     {
          if (!_objectsQueue.Any())
          {
               Resize(1);
          }
          var item = _objectsQueue.Dequeue();
          item.OnSpawned();
          return item;
     }
}
