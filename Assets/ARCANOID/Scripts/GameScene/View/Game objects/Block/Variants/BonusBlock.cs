using UnityEngine;

public class BonusBlock : DestructibleBlock
{
    [SerializeField] private SpriteComponent bonusSpriteComponent;

    public void SetBonusType(BlockType blockType)
    {
        Type = blockType;
    }
    
    public override void Destroy()
    {
        base.Destroy();
        bonusSpriteComponent.Disable();
    }

    protected override void OnCompleteDestroyParticles()
    {
        MessageBus.RaiseEvent<IBlockLifecycleHandler>(handler => handler.OnBlockDestroyed(this));
    }
}
