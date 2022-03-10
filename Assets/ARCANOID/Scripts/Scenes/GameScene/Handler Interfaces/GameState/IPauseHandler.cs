public interface IPauseHandler : ISubscriber
{
     void OnGamePaused();
     void OnGameResumed();
}
