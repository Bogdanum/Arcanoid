
public interface IGameResultHandler : ISubscriber
{
    void OnVictory();
    void OnLose();
}
