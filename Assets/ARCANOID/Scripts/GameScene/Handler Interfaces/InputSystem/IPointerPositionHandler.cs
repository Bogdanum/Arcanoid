using UnityEngine;

public interface IPointerPositionHandler : ISubscriber
{
     void OnUpdatePointerPosition(Vector3 position);
}
