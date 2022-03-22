using UnityEngine;

public class BallSpeedBonusStateController : MonoBehaviour, IBallSpeedBonusHandler, ILocalGameStateHandler
{
    [SerializeField] private BinaryBonusProcessor bonusProcessor;
    private BallsOnSceneController _ballsOnSceneController;

    public void Init(BallsOnSceneController ballsOnSceneController, BinaryBonusProcessorConfig config)
    {
        bonusProcessor.Init(config);
        _ballsOnSceneController = ballsOnSceneController;
        MessageBus.Subscribe(this);
        bonusProcessor.OnCurrentValueChange += ChangeBallsVelocity;
    }

    private void OnDisable()
    {
        MessageBus.Unsubscribe(this);
        bonusProcessor.OnCurrentValueChange -= ChangeBallsVelocity;
    }

    public void OnActivateBallSpeedBonus(BinaryBonusDirection direction)
    {
        bonusProcessor.Launch(direction);
    }

    private void ChangeBallsVelocity(float velocity)
    {
        _ballsOnSceneController.ChangeAdditionalVelocity(velocity);
    }
    
    public void OnPrepare() => bonusProcessor.Stop();
    public void OnContinueGame() => bonusProcessor.Stop();
    public void OnStartGame(){}
    public void OnEndGame(){}
}
