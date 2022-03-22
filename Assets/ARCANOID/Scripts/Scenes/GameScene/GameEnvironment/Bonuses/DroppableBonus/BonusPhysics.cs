using UnityEngine;

public class BonusPhysics : MonoBehaviour, IPauseHandler
{
    [SerializeField] private Rigidbody2D bonusRigidbody;

    public void Init(float gravityScale)
    {
        MessageBus.Subscribe(this);
        bonusRigidbody.gravityScale = gravityScale;
        OnGameResumed();
    }
    
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnGamePaused()
    {
        bonusRigidbody.simulated = false;
    }

    public void OnGameResumed()
    {
        bonusRigidbody.simulated = true;
    }
}
