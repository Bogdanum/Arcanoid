using System;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D targetCollider;
    public event Action onTriggerEnter;
    public event Action<Collider2D> OnTriggerEnter;
    public event Action<Collider2D> onCollisionEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter?.Invoke();
        OnTriggerEnter?.Invoke(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onCollisionEnter?.Invoke(collision.collider);
    }

    public void Enable() => targetCollider.enabled = true;
    public void Disable() => targetCollider.enabled = false;
    public void SetTrigger(bool isTrigger) => targetCollider.isTrigger = isTrigger;
}
