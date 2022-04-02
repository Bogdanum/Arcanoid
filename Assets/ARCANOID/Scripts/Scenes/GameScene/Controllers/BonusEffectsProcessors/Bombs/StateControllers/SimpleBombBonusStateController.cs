using UnityEngine;

public class SimpleBombBonusStateController : MonoBehaviour, ISimpleBombBonusHandler
{
    [SerializeField] private SimpleBombExplosionProcessor explosionProcessor;
    private GridOfBlocks _gridOfBlocks;

    public void Init(GridOfBlocks gridOfBlocks, BombBonusConfig config)
    {
        MessageBus.Subscribe(this);
        _gridOfBlocks = gridOfBlocks;
        explosionProcessor.Init(config);
    }

    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnExplode(Vector2 bombPosition)
    {
        var blocksInExplosionRadiusFinder = new FinderNeighborsWithinRadius(bombPosition, _gridOfBlocks);
        explosionProcessor.LaunchExplosion(blocksInExplosionRadiusFinder);
    }
}
