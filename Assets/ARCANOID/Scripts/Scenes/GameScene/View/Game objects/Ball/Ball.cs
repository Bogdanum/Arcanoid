using UnityEngine;

public class Ball : PoolItem, IPauseHandler
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BallPhysics ballPhysics;

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
        spriteRenderer.sprite = _ballSettings.DefaultSprite;
        Damage = _ballSettings.Damage;
    }

    public override void OnDespawned()
    {
        MessageBus.Unsubscribe(this);
        base.OnDespawned();
        ballPhysics.DisablePhysics();
    }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void ReturnToDefaultSprite() => ChangeSprite(_ballSettings.DefaultSprite);

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
