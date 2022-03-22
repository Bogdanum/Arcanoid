using UnityEngine;

public class PlatformSizeBonusStateController : MonoBehaviour, IPlatformSizeHandler, ILocalGameStateHandler
{
    [SerializeField] private BinaryBonusProcessor bonusProcessor;
    private PlayerPlatformController _platformController;

    public void Init(PlayerPlatformController platformController, BinaryBonusProcessorConfig config)
    {
        _platformController = platformController;
        bonusProcessor.Init(config);
        bonusProcessor.OnCurrentValueChange += ResizePlatform;
    }

    private void OnEnable()
    {
        MessageBus.Subscribe(this);
    }

    private void OnDisable()
    {
        MessageBus.Unsubscribe(this);
        bonusProcessor.OnCurrentValueChange -= ResizePlatform;
    }

    private void ResizePlatform(float value)
    {
        _platformController.ResizePerStep(value);
    }

    public void OnStartPlatformSizeBonus(BinaryBonusDirection direction)
    {
        bonusProcessor.Launch(direction);
    }

    public void OnPrepare() => bonusProcessor.Stop();
    public void OnContinueGame() => bonusProcessor.Stop();
    public void OnStartGame() {}
    public void OnEndGame() {}
}
