using UnityEngine;

public class LineTntBonusStateController : MonoBehaviour, ILineTntBonusHandler
{
    [SerializeField] private ChainBombExplosionProcessor explosionProcessor;
    private GridOfBlocks _gridOfBlocks;

    public void Init(GridOfBlocks gridOfBlocks, BombBonusConfig config)
    {
        MessageBus.Subscribe(this);
        _gridOfBlocks = gridOfBlocks;
        explosionProcessor.Init(config);
    }

    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnExplode(Vector2 tntPosition, LineTntDirection lineDirection)
    {
        var linearChainBlockFinder = new LinearChainFinder(tntPosition, _gridOfBlocks, lineDirection);
        MessageBus.RaiseEvent<IDestroyedBlockInChainHandler>(handler => handler.OnBlockDestroyedOnPosition(tntPosition));
        explosionProcessor.LaunchExplosion(linearChainBlockFinder);
    }
}
