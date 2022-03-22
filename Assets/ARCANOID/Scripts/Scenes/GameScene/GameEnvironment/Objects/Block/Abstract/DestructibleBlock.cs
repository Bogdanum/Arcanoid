using System.Collections;
using UnityEngine;

public abstract class DestructibleBlock : Block
{
    [SerializeField] private BlockCracksRenderer cracksRenderer;

    private BlocksDesignProperties _designProps;
    private int _healthPoints;

    public override void Init(BlocksDesignProperties designProps)
    {
        _designProps = designProps;
        mainSpriteRenderer.RefreshScale();
        cracksRenderer.Init(_designProps.BlockHealth);
    }

    public virtual void SetInitialParams(BlockRendererParamsID paramsID, int customHealth)
    {
        blockCollider.Enable();
        _healthPoints = customHealth > 0 ? customHealth : _designProps.BlockHealth.DefaultHealth;
        _designProps.Init(_healthPoints);
        cracksRenderer.Refresh();
        var rendererParams = _designProps.GetBlockRendererParamsByID(paramsID);
        mainSpriteRenderer.SetSprite(rendererParams.mainSprite);
        blockParticleSystem.SetColor(rendererParams.mainColor, rendererParams.accentColor);
        blockParticleSystem.SetSize(transform.localScale);

        MessageBus.RaiseEvent<IBlockLifecycleHandler>(handler => handler.OnDestructibleBlockSpawned());
    }

    public override void OnSpawned()
    {
        base.OnSpawned();
        blockCollider.onCollisionEnter += OnHitByBall;
        blockCollider.OnTriggerEnter += OnOtherEnter;
    }

    public override void OnDespawned()
    {
        base.OnDespawned();
        DisableTriggerColliderState();
        blockCollider.onCollisionEnter -= OnHitByBall;
        blockCollider.OnTriggerEnter -= OnOtherEnter;
    }

    private void OnHitByBall(Collider2D other)
    {
        var parent = other.transform.parent;
        if (parent.TryGetComponent(out Ball ball))
        {
            TakeDamage(ball.Damage);
        }
    }

    private void OnOtherEnter(Collider2D other)
    {
        var parent = other.transform.parent;
        if (parent.TryGetComponent(out Ball _))
        {
            Destroy();
        }
    }
    
    private void TakeDamage(int damage)
    {
        _healthPoints -= damage;
        if (_healthPoints < 1)
        {
            Destroy();
        }
        cracksRenderer.ShowCracksByHealth(_healthPoints);
    }
    
    public override void Destroy()
    {
        if (Destroyed) return;

        Destroyed = true;
        StartCoroutine(PlayParticlesAndDestroy());
        MessageBus.RaiseEvent<IBlockLifecycleHandler>(handler => handler.OnPlayingBlockDestructionParticles(this));
    }

    private IEnumerator PlayParticlesAndDestroy()
    {
        DisableView();
        yield return blockParticleSystem.Play();
        OnCompleteDestroyParticles();
    }

    private void DisableView()
    {
        mainSpriteRenderer.Disable();
        blockCollider.Disable();
        cracksRenderer.Disable();
    }

    protected abstract void OnCompleteDestroyParticles();

    public void SetTriggerColliderState() => blockCollider.SetTrigger(true);
    public void DisableTriggerColliderState() => blockCollider.SetTrigger(false);
}