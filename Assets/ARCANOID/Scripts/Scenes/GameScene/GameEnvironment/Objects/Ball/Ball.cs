using UnityEngine;

public class Ball : PoolItem, IPauseHandler
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BallPhysics ballPhysics;
    [SerializeField] private ParticleSystem ballParticleSystem;

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
        var visualSettings = _ballSettings.BallVisualSettings;
        spriteRenderer.sprite = visualSettings.defaultSprite;
        Damage = _ballSettings.Damage;
        SetupParticlesColor(visualSettings.firstParticleColor, visualSettings.secondParticleColor);
    }

    public override void OnDespawned()
    {
        MessageBus.Unsubscribe(this);
        base.OnDespawned();
        ballPhysics.DisablePhysics();
    }

    private void SetupParticlesColor(Color first, Color second)
    {
        var settings = ballParticleSystem.main;
        var settingsStartColor = settings.startColor;
        settingsStartColor.mode = ParticleSystemGradientMode.TwoColors;
        settingsStartColor.colorMin = first;
        settingsStartColor.colorMax = second;
        settings.startColor = settingsStartColor;
    }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void ReturnToDefaultSprite() => ChangeSprite(_ballSettings.BallVisualSettings.defaultSprite);

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
