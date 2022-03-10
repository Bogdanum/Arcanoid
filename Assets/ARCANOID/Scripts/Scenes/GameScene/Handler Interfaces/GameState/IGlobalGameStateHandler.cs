public interface IGlobalGameStateHandler : ISubscriber
{
     void OnStartGame();
     void OnRestartGame();
     void OnContinue();
}
