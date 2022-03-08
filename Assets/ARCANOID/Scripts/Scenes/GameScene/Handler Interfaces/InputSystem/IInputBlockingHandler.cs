
public interface IInputBlockingHandler : ISubscriber
{
    void OnInputActivation();
    void OnInputBlock();
}
