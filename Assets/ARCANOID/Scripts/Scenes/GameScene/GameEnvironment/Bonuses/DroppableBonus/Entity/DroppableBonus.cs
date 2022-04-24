using UnityEngine;

public class DroppableBonus : PoolItem
{
    [SerializeField] private BonusPhysics physics;
    [SerializeField] private DroppableBonusRenderer mainRenderer;
    [SerializeField] private BlockCollider bonusCollider;

    private DroppableBonusSettings.Settings _settings;
    private IBonusEffectReproducer _effectReproducer;
    private float _limitPosY;

    public void SetParams(BonusId bonusId, DroppableBonusSettings settings, IBonusEffectReproducer effectReproducer)
    {
        _effectReproducer = effectReproducer;
        _limitPosY = settings.LimitPositionY;
        _settings = settings.GetDroppableBonusSettings(bonusId);
        physics.Init(_settings.gravityScale);
        mainRenderer.Init(_settings.bonusSprite, _settings.particlesColor);
    }

    public override void OnSpawned()
    {
        base.OnSpawned();
        bonusCollider.OnTriggerEnter += ExecuteEffect;
    }

    public override void OnDespawned()
    {
        base.OnDespawned();
        bonusCollider.OnTriggerEnter -= ExecuteEffect;
    }
 
    private void ExecuteEffect(Collider2D otherCollider)
    {
        Transform parent = otherCollider.transform.parent;
        if (parent.TryGetComponent(out Platform _))
        {
            _effectReproducer.Reproduce();
            BackToPool();
        }
    }

    private void Update()
    {
        if (transform.position.y > _limitPosY) return;
        BackToPool();
    }

    private void BackToPool()
    {
        MessageBus.RaiseEvent<IBonusLifecycleHandler>(handler => handler.ReturnToPool(this));
    }
}