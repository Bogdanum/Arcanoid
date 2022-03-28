public class BallsOnSceneVelocityController : IComplexityIncreaseHandler, ILocalGameStateHandler
{
    private readonly BallPhysicsSettings _ballPhysicsSettings;
    private readonly BallsOnSceneContainer _container;
    private float _currentBallsVelocity;
    private float _additionalVelocity;
    
    public BallsOnSceneVelocityController(BallPhysicsSettings ballPhysicsSettings, BallsOnSceneContainer container)
    {
        MessageBus.Subscribe(this);
        _ballPhysicsSettings = ballPhysicsSettings;
        _container = container;
        _currentBallsVelocity = ballPhysicsSettings.InitialVelocity;
    }

    ~BallsOnSceneVelocityController() => MessageBus.Unsubscribe(this);
    
    public void ChangeAdditionalVelocity(float additionalVelocity)
    {
        _additionalVelocity = additionalVelocity;
        UpdateBallsVelocity();
    }
    
    public void OnIncreasingComplexity()
    {
        if (_currentBallsVelocity >= _ballPhysicsSettings.MaxVelocity) return;

        _currentBallsVelocity += _ballPhysicsSettings.VelocityIncreaseStep;
        UpdateBallsVelocity();
    }
    
    public void UpdateBallsVelocity()
    {
        float velocity = _currentBallsVelocity + _additionalVelocity;
        var ballsList = _container.GetBallsOnSceneList();
        ballsList.ForEach(ball => ball.SetVelocity(velocity));
    }

    public void OnPrepare()
    {
        _currentBallsVelocity = _ballPhysicsSettings.InitialVelocity;
    }

    public void OnStartGame(){}

    public void OnContinueGame(){}

    public void OnEndGame(){}
}