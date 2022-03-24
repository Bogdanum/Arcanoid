using System.Collections;
using UnityEngine;

public class ChainBombExplosionProcessor : MonoBehaviour, IExplosionProcessor
{
    private BombBonusConfig _config;

    public void Init(BombBonusConfig config)
    {
        _config = config;
    }
    
    public void LaunchExplosion(GridBlockFinder gridBlockFinder)
    {
        StartCoroutine(ExplosionProcess(gridBlockFinder));
    }

    private IEnumerator ExplosionProcess(GridBlockFinder gridBlockFinder)
    {
        while (gridBlockFinder.HasNextBlocks)
        {
            yield return new WaitForSeconds(_config.Delay);
            
            foreach (var block in gridBlockFinder.GetNextSetToDestroy())
            {
                if (_config.CanBeDestroyed(block.Type))
                {
                    block.Destroy();
                }
            }
        } 
    }
    
    public void OnPrepare() => StopAllCoroutines();
    public void OnStartGame(){}
    public void OnContinueGame(){}
    public void OnEndGame(){}
}
