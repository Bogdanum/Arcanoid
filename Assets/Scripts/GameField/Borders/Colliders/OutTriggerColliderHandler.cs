using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OutTriggerColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform parent = other.transform.parent;
        if (parent.TryGetComponent(out Ball ball))
        {
            MessageBus.RaiseEvent<IMainBallLifecycleHandler>(handler => handler.OnDestroyBall(ball));
        }
    }
}
