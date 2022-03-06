using UnityEngine;

public abstract class Block : PoolItem
{
    [SerializeField] protected BlockCollider collider;
    [SerializeField] protected SpriteComponent mainSpriteRenderer;
    [SerializeField] protected BlockParticleSystem particleSystem;
    protected bool Destroyed;
    
    public BlockType Type { get; protected set; }
    public abstract void Init(BlocksDesignProperties designProps);

    public override void OnSpawned()
    {
        base.OnSpawned();
        Destroyed = false;
    }

    public abstract void Destroy();
}
