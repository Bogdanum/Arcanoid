public interface IBallSpeedBonusHandler : ISubscriber
{
    void OnActivateBallSpeedBonus(BinaryBonusDirection direction);
}