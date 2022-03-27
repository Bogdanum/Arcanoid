using UnityEngine;

public static class MonoBehaviourExtensions
{
     public static void SetActive(this MonoBehaviour mono, bool state)
     {
          mono.gameObject.SetActive(state);
     }

     public static Vector3 Position(this MonoBehaviour mono)
     {
          return mono.transform.position;
     }
}
