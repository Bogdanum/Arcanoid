using UnityEngine;

public class ColorChainTntStateController : MonoBehaviour, IChainColorTntBonusHandler
{
    [SerializeField] private ChainBombExplosionProcessor explosionProcessor;
    private GridOfBlocks _gridOfBlocks;

    public void Init(BombBonusConfig config, GridOfBlocks gridOfBlocks)
    {
        MessageBus.Subscribe(this);
        _gridOfBlocks = gridOfBlocks;
        explosionProcessor.Init(config);
    }

    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnExplode(Vector2 tntPosition)
    {
        var colorChainFinder = new ColorChainFinder(tntPosition, _gridOfBlocks);
        MessageBus.RaiseEvent<IDestroyedBlockInChainHandler>(handler => handler.OnBlockDestroyedOnPosition(tntPosition));
        explosionProcessor.LaunchExplosion(colorChainFinder);
    }
}
