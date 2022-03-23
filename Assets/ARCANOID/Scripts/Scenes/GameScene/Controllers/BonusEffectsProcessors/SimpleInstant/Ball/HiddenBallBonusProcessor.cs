using UnityEngine;

public class HiddenBallBonusProcessor : IHiddenBallBonusHandler
{
    private readonly HiddenBallBonusConfig _config;
    private readonly BallsOnSceneController _ballsOnSceneController;

    public HiddenBallBonusProcessor(BallsOnSceneController ballsOnSceneController, HiddenBallBonusConfig config)
    {
        MessageBus.Subscribe(this);
        _ballsOnSceneController = ballsOnSceneController;
        _config = config;
    }

    ~HiddenBallBonusProcessor() => MessageBus.Unsubscribe(this);
    
    public void OnActivateHiddenBallBonus(Vector2 bonusPosition)
    {
        for (int i = 0; i < _config.BallsCount; i++)
        {
            Vector2 direction = GetDirection();
            _ballsOnSceneController.CreateBallAtPositionAndPushInDirection(bonusPosition, direction);
        }
    }

    private Vector2 GetDirection()
    {
        if (!_config.RandomBallsDirection) return _config.DefaultDirection;

        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        var direction = new Vector2(x, y);
        return direction == Vector2.zero ? _config.DefaultDirection : direction;
    }
}
