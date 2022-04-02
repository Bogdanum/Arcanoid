using UnityEngine;

public class RageBallBonusStateController : MonoBehaviour, IRageBallBonusHandler, ISpawnBallHandler, ILocalGameStateHandler
{
    [SerializeField] private SimpleTemporaryBonusProcessor bonusProcessor;
    private BallsOnSceneController _ballsOnSceneController;

    public void Init(BallsOnSceneController ballsOnSceneController, SimpleTemporaryBonusConfig config)
    {
        bonusProcessor.Init(config);
        _ballsOnSceneController = ballsOnSceneController;
        MessageBus.Subscribe(this);
        bonusProcessor.OnEffectEnded += OnEndEffectTime;
    }

    private void OnDisable()
    {
        MessageBus.Unsubscribe(this);
        bonusProcessor.OnEffectEnded -= OnEndEffectTime;
    }

    public void OnActivateRage()
    {
        bonusProcessor.Activate();
        var ballsOnScene = _ballsOnSceneController.GetBallsOnSceneList();
        ballsOnScene.ForEach(ball => ball.SetRageParams());
    }

    private void OnEndEffectTime()
    {
        var ballsOnScene = _ballsOnSceneController.GetBallsOnSceneList();
        ballsOnScene.ForEach(ball => ball.SetDefaultParams());
    }

    public void OnSpawnBallOnScene(Ball ball)
    {
        if (bonusProcessor.IsBonusActive)
        {
            ball.SetRageParams();
        }
    }
    
    public void OnPrepare() => bonusProcessor.ForceEnd();
    public void OnContinueGame() => bonusProcessor.ForceEnd();
    public void OnStartGame(){}
    public void OnEndGame(){}
}
