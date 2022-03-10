
public interface ILocalGameStateHandler : ISubscriber
{
    void OnPrepare();
    void OnStartGame();
    void OnContinueGame();
    void OnEndGame();
}
