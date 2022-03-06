using System;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D collider;
    public event Action onTriggerEnter;
    public event Action<Collider2D> onCollisionEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onCollisionEnter?.Invoke(collision.collider);
    }

    public void Enable() => collider.enabled = true;
    public void Disable() => collider.enabled = false;
    public void SetTrigger(bool isTrigger) => collider.isTrigger = isTrigger;
}
