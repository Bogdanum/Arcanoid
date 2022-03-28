using System.Collections;
using UnityEngine;

public class SimpleBombExplosionProcessor : MonoBehaviour, IExplosionProcessor, IPauseHandler
{
    private GridOfBlocks _gridOfBlocks;
    private BombBonusConfig _config;
    private bool _isPaused;

    public void Init(GridOfBlocks gridOfBlocks, BombBonusConfig config)
    {
        MessageBus.Subscribe(this);
        _gridOfBlocks = gridOfBlocks;
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
        if (destructibleBlock.DamageLessThanLife(damage))
        {
            _gridOfBlocks.Add(block.transform.position, block);
        }
        destructibleBlock.TakeDamage(damage);
    }

    public void OnPrepare() => StopAllCoroutines();
    public void OnGamePaused() => _isPaused = true;
    public void OnGameResumed() => _isPaused = false;
    public void OnStartGame(){}
    public void OnContinueGame(){}
    public void OnEndGame(){}
}
