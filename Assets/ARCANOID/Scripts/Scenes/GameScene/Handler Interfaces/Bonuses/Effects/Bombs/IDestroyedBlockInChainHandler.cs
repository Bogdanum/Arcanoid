using UnityEngine;

public interface IDestroyedBlockInChainHandler : ISubscriber
{
    void OnBlockDestroyedOnPosition(Vector3 position);
}
