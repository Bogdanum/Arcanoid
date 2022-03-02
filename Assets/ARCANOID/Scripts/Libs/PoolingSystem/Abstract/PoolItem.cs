using UnityEngine;

public abstract class PoolItem : MonoBehaviour
{
     public virtual void OnSpawned()
     {
          gameObject.SetActive(true);
     }

     public virtual void OnDespawned()
     {
          gameObject.SetActive(false);
     }
}
