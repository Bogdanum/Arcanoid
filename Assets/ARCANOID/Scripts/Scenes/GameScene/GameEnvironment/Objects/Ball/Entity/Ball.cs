using UnityEngine;

public class Ball : PoolItem, IPauseHandler
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D rageBallCollider;
    [SerializeField] private BallPhysics ballPhysics;
    [SerializeField] private BallParticleSystem ballParticleSystem;

    private BallBaseSettings _ballSettings;
    private float _velocity;
    
    public int Damage { get; private set; }

    public void Init(BallBaseSettings settings)
    {
        _ballSettings = settings;
    }
    
    public override void OnSpawned()
    {
        MessageBus.Subscribe(this);
        base.OnSpawned();
        Damage = _ballSettings.Damage;
        SetDefaultParams();
        var visualSettings = _ballSettings.BallVisualSettings;
        ballParticleSystem.SetupParticlesColor(visualSettings.firstParticleColor, visualSettings.secondParticleColor);
    }

    public override void OnDespawned()
    {
        MessageBus.Unsubscribe(this);
        base.OnDespawned();
        ballPhysics.DisablePhysics();
        ballPhysics.OnDespawned();
    }
    
    public void SetDefaultParams()
    {
        rageBallCollider.isTrigger = false;
        spriteRenderer.sprite = _ballSettings.BallVisualSettings.defaultSprite;
        ballParticleSystem.PlayNormalParticles();
    }

    public void SetRageParams()
    {
        rageBallCollider.isTrigger = true;
        spriteRenderer.sprite = _ballSettings.BallVisualSettings.rageSprite;
        ballParticleSystem.PlayRageParticles();
    }

    public void SetVelocity(float velocity)
    {
        _velocity = velocity;
        ballPhysics.SetVelocity(velocity);
    }

    public void PushBall(Vector2 direction)
    {
        Vector2 velocityVector = direction.normalized * _velocity;
        ballPhysics.StartMovement(velocityVector);
    }

    public void OnGamePaused() => ballPhysics.DisablePhysics();

    public void OnGameResumed()
    {
        if (ballPhysics.IsMoving)
        {
            ballPhysics.EnablePhysics();
        }
    }
}
