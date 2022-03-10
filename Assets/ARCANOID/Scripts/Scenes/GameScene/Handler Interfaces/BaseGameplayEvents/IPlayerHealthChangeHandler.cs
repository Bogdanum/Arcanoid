
public interface IPlayerHealthChangeHandler : ISubscriber
{
    void OnAddHealth();
    void OnRemoveHealth();
}
