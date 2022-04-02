using System;
using UnityEngine;
using Zenject;

public class BombExplosionsVFXController : MonoBehaviour, ISimpleBombBonusHandler, IDestroyedBlockInChainHandler
{
    [SerializeField] private Transform container;
    [SerializeField] private Settings settings;
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
        PlayExplosionParticles(bombPosition, settings.simpleTntExplosionColor);
    }
    public void OnBlockDestroyedOnPosition(Vector3 position)
    {
        PlayExplosionParticles(position, settings.chainTntExplosionColor);
    }

    private void PlayExplosionParticles(Vector3 position, Color color)
    {
        var explosion = _poolsManager.GetItem<BombExplosionParticles>(position, container);
        explosion.transform.localScale = _blockSpawnerController.GetCurrentBlockScale();
        explosion.SetColor(color);
        explosion.Play();
        explosion.OnComplete = () => OnCompleteExplosion(explosion);
    }

    private void OnCompleteExplosion(BombExplosionParticles particles)
    {
        _poolsManager.ReturnItemToPool(particles);
    }
    
    [Serializable]
    private class Settings
    {
        public Color simpleTntExplosionColor;
        public Color chainTntExplosionColor;
    }
}
