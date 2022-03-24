using UnityEngine;
using Zenject;

public class BombExplosionsVFXController : MonoBehaviour, ISimpleBombBonusHandler
{
    [SerializeField] private Transform container;
    private PoolsManager _poolsManager;
    private BlockSpawnerController _blockSpawnerController;
    
    [Inject]
    public void Init(PoolsManager poolsManager, BlockSpawnerController blockSpawnerController)
    {
        _poolsManager = poolsManager;
        _blockSpawnerController = blockSpawnerController;
    }

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnExplode(Vector2 bombPosition)
    {
        var explosion = _poolsManager.GetItem<BombExplosionParticles>(bombPosition, container);
        explosion.transform.localScale = _blockSpawnerController.GetCurrentBlockScale();
        explosion.Play();
        explosion.OnComplete = () => OnCompleteExplosion(explosion);
    }

    private void OnCompleteExplosion(BombExplosionParticles particles)
    {
        _poolsManager.ReturnItemToPool(particles);
    }
}
