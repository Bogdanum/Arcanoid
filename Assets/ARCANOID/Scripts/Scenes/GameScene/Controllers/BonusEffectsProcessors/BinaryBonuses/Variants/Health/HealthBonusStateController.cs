using UnityEngine;

public class HealthBonusStateController : MonoBehaviour, IHealthBonusHandler
{
    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnActivateHealthBonus(BinaryBonusDirection direction)
    {
        if (direction == BinaryBonusDirection.Increase)
        {
            MessageBus.RaiseEvent<IPlayerHealthChangeHandler>(handler => handler.OnAddHealth());
        } else
        {
            MessageBus.RaiseEvent<IPlayerHealthChangeHandler>(handler => handler.OnRemoveHealth(false));
        }
    }
}
