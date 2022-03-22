using UnityEngine;

public class RageBallBonusStateController : MonoBehaviour, IRageBallBonusHandler, ISpawnBallHandler, ILocalGameStateHandler
{
    [SerializeField] private SimpleTemporaryBonusProcessor bonusProcessor;
    private BallsOnSceneController _ballsOnSceneController;
    private BlocksOnSceneController _blocksOnSceneController;

    public void Init(BallsOnSceneController ballsOnSceneController, BlocksOnSceneController blocksOnSceneController, SimpleTemporaryBonusConfig config)
    {
        bonusProcessor.Init(config);
        _blocksOnSceneController = blocksOnSceneController;
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
        ballsOnScene.ForEach(ball => ball.SetRageVisualParams());
        var simpleBlocks = _blocksOnSceneController.GetBlocksOnSceneList<SimpleBlock>();
        var bonusBlocks = _blocksOnSceneController.GetBlocksOnSceneList<BonusBlock>();
        simpleBlocks?.ForEach(block => block.SetTriggerColliderState());
        bonusBlocks?.ForEach(block => block.SetTriggerColliderState());
    }

    private void OnEndEffectTime()
    {
        var ballsOnScene = _ballsOnSceneController.GetBallsOnSceneList();
        ballsOnScene.ForEach(ball => ball.SetDefaultVisualParams());
        var simpleBlocks = _blocksOnSceneController.GetBlocksOnSceneList<SimpleBlock>();
        var bonusBlocks = _blocksOnSceneController.GetBlocksOnSceneList<BonusBlock>();
        simpleBlocks?.ForEach(block => block.DisableTriggerColliderState());
        bonusBlocks?.ForEach(block => block.DisableTriggerColliderState());
    }

    public void OnSpawnBallOnScene(Ball ball)
    {
        if (bonusProcessor.IsBonusActive)
        {
            ball.SetRageVisualParams();
        }
    }
    
    public void OnPrepare() => bonusProcessor.ForceEnd();
    public void OnContinueGame() => bonusProcessor.ForceEnd();
    public void OnStartGame(){}
    public void OnEndGame(){}
}
