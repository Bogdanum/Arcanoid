using UnityEngine;

public interface IMainBallLifecycleHandler : ISubscriber
{
    void OnCreateNewBallOnPlatform(Transform platform);
    void OnDestroyBall(Ball ball);
}
