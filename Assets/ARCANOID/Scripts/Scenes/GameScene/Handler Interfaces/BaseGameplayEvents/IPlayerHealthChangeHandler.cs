
public interface IPlayerHealthChangeHandler : ISubscriber
{
    void OnAddHealth();
    void OnRemoveHealth(bool isBallDestroyed);
}
