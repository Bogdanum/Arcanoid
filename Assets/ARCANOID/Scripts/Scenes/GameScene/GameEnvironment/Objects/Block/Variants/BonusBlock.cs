using System;
using UnityEngine;

public class BonusBlock : DestructibleBlock
{
    [SerializeField] private SpriteComponent bonusSpriteComponent;
    public event Action OnDestroyed;

    public void SetBonusType(BlockType blockType, Sprite droppableBonusSprite = null)
    {
        Type = blockType;
        bool hasDroppableBonusSprite = droppableBonusSprite != null;
        bonusSpriteComponent.SetActive(hasDroppableBonusSprite);
        if (hasDroppableBonusSprite)
        {
            bonusSpriteComponent.SetSprite(droppableBonusSprite);
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        bonusSpriteComponent.Disable();
        OnDestroyed?.Invoke();
    }

    public override void OnDespawned()
    {
        base.OnDespawned();
        OnDestroyed = null;
    }

    protected override void OnCompleteDestroyParticles()
    {
        MessageBus.RaiseEvent<IBlockLifecycleHandler>(handler => handler.OnBlockDestroyed(this));
    }
}
