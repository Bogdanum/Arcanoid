using UnityEngine;

public class PlayerPlatformController : MonoBehaviour, IStartLevelHandler
{
    [SerializeField] private GameBounds gameBounds;
    [SerializeField] private Platform platform;
    [SerializeField] private PlayerPlatformSettings settings;

    private void Awake()
    {
        Init();
    }

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    private void Init()
    {
        platform.Init(settings.TargetPositionAccuracy, gameBounds.GetGameBoundarySizeX());
        platform.RefreshParameters(settings.InitialSpeed, settings.InitialSize);
        SpawnBall();  // <-- test
    }
    
    public void OnLevelStarted()
    {
        SpawnBall();
    }

    private void SpawnBall()
    {
        MessageBus.RaiseEvent<IMainBallLifecycleHandler>(handler => handler.OnCreateNewBallOnPlatform(platform.SpawnPoint));
    }
}
