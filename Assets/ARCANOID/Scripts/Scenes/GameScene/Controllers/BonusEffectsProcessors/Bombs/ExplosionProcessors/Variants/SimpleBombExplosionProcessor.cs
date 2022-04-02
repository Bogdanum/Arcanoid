using System.Collections;
using UnityEngine;

public class SimpleBombExplosionProcessor : MonoBehaviour, IExplosionProcessor, IPauseHandler
{
    private BombBonusConfig _config;
    private bool _isPaused;

    public void Init(BombBonusConfig config)
    {
        MessageBus.Subscribe(this);
        _config = config;
    }

    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void LaunchExplosion(GridBlockFinder gridBlockFinder)
    {
        StartCoroutine(ExplosionProcess(gridBlockFinder));
    }

    private IEnumerator ExplosionProcess(GridBlockFinder gridBlockFinder)
    {
        while (_isPaused) yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(_config.Delay);

        foreach (var block in gridBlockFinder.GetNextSetToDestroy())
        {
            var typeOfBlock = block.Type;
            if (!_config.CanBeDestroyed(typeOfBlock))
            {
                continue;
            }
            if (typeOfBlock == BlockType.Bedrock)
            {
                block.Destroy();
            } else
            {
                DoDamageOrAddToDestroySet(block);
            }
        }
    }

    private void DoDamageOrAddToDestroySet(Block block)
    {
        var destructibleBlock = (DestructibleBlock)block;
        int damage = _config.Damage;
        destructibleBlock.TakeDamage(damage);
    }

    public void OnPrepare() => StopAllCoroutines();
    public void OnGamePaused() => _isPaused = true;
    public void OnGameResumed() => _isPaused = false;
    public void OnStartGame(){}
    public void OnContinueGame(){}
    public void OnEndGame(){}
}
