using UnityEngine;

public interface IPointerPositionHandler : ISubscriber
{
     void OnUpdatePointerPosition(Vector3 position);
     void OnUpdateHoldingState(bool isHolding);
}
