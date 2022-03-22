using UnityEngine;

public class PlatformSpeedBonusStateController : MonoBehaviour, IPlatformSpeedHandler, ILocalGameStateHandler
{
    [SerializeField] private BinaryBonusProcessor bonusProcessor;
    private PlayerPlatformController _platformController; 
    
    public void Init(PlayerPlatformController platformController, BinaryBonusProcessorConfig config)
    {
        _platformController = platformController;
        bonusProcessor.Init(config);
        bonusProcessor.OnCurrentValueChange += ChangePlatformSpeed;
    }
    
    private void OnEnable() => MessageBus.Subscribe(this);

    private void OnDisable()
    {
        MessageBus.Unsubscribe(this);
        bonusProcessor.OnCurrentValueChange -= ChangePlatformSpeed;
    }

    private void ChangePlatformSpeed(float speed)
    {
        _platformController.ChangeSpeed(speed);
    }

    public void OnStartPlatformSpeedBonus(BinaryBonusDirection direction)
    {
        bonusProcessor.Launch(direction);
    }

    public void OnPrepare() => bonusProcessor.Stop();
    public void OnContinueGame() => bonusProcessor.Stop();
    public void OnStartGame() {}
    public void OnEndGame() {}
}
