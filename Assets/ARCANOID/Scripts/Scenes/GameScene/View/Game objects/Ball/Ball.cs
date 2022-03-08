using UnityEngine;

public class Ball : PoolItem
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
        base.OnSpawned();
        spriteRenderer.sprite = _ballSettings.DefaultSprite;
        Damage = _ballSettings.Damage;
    }

    public override void OnDespawned()
    {
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
}
