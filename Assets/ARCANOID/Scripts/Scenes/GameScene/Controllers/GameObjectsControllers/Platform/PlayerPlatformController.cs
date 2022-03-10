using UnityEngine;

public class PlayerPlatformController : MonoBehaviour, ILocalGameStateHandler
{
    [SerializeField] private GameBounds gameBounds;
    [SerializeField] private Platform platform;
    [SerializeField] private PlayerPlatformSettings settings;
    [SerializeField] private ValueChangeAnimation valueAnim;

    private float _prevPlatformSize;

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
    }

    public void OnStartGame() => SpawnBall();
    
    public void OnContinueGame()
    {
        platform.BackToInitialPosition(SpawnBall);
    }
    
    private void SpawnBall()
    {
        MessageBus.RaiseEvent<IMainBallLifecycleHandler>(handler => handler.OnCreateNewBallOnPlatform(platform.SpawnPoint));
    }

    public void OnPrepare()
    {
        valueAnim.Stop();
        platform.RefreshParameters(settings.InitialSpeed, settings.InitialSize);
        _prevPlatformSize = settings.InitialSize;
    }

    public void IncreaseSize(float value)
    {
        float newSize = settings.InitialSize + value;
        valueAnim.Play(_prevPlatformSize, newSize, platform.SetNewSize);
        _prevPlatformSize = settings.InitialSize;
    }

    public void IncreaseSpeed(float value)
    {
        float newSpeed = settings.InitialSpeed + value;
        platform.SetNewSpeed(newSpeed);
    }

    public void OnEndGame()
    {
        platform.BackToInitialPosition(() => platform.LockControl());
        valueAnim.Stop();
    }
}
