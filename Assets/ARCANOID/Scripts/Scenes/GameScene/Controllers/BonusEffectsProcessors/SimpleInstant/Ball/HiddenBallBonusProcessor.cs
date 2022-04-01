using UnityEngine;

public class HiddenBallBonusProcessor : MonoBehaviour, IHiddenBallBonusHandler
{
    private HiddenBallBonusConfig _config;
    private BallsOnSceneController _ballsOnSceneController;

    public void Init(BallsOnSceneController ballsOnSceneController, HiddenBallBonusConfig config)
    {
        MessageBus.Subscribe(this);
        _ballsOnSceneController = ballsOnSceneController;
        _config = config;
    }

    private void OnDisable() => MessageBus.Unsubscribe(this);
    
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

        int x = Random.Range(-1, 1);
        int y = Random.Range(-1, 1);
        var direction = new Vector2(x, y);
        return direction == Vector2.zero ? _config.DefaultDirection : direction;
    }
}
