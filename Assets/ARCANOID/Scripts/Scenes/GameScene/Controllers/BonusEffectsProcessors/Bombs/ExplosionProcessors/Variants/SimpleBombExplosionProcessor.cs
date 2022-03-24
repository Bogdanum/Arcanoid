using System.Collections;
using UnityEngine;

public class SimpleBombExplosionProcessor : MonoBehaviour, IExplosionProcessor
{
    private GridOfBlocks _gridOfBlocks;
    private BombBonusConfig _config;

    public void Init(GridOfBlocks gridOfBlocks, BombBonusConfig config)
    {
        _gridOfBlocks = gridOfBlocks;
        _config = config;
    }
    
    public void LaunchExplosion(GridBlockFinder gridBlockFinder)
    {
        StartCoroutine(ExplosionProcess(gridBlockFinder));
    }

    private IEnumerator ExplosionProcess(GridBlockFinder gridBlockFinder)
    {
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
    public void OnStartGame(){}
    public void OnContinueGame(){}
    public void OnEndGame(){}
}
