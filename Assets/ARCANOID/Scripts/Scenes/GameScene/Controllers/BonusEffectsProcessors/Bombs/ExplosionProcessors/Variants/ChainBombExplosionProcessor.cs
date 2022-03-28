using System.Collections;
using UnityEngine;

public class ChainBombExplosionProcessor : MonoBehaviour, IExplosionProcessor, IPauseHandler
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
        while (gridBlockFinder.HasNextBlocks)
        {
            while (_isPaused) yield return new WaitForEndOfFrame();
            
            yield return new WaitForSeconds(_config.Delay);
            
            foreach (var block in gridBlockFinder.GetNextSetToDestroy())
            {
                if (_config.CanBeDestroyed(block.Type))
                {
                    MessageBus.RaiseEvent<IDestroyedBlockInChainHandler>(handler => handler.OnBlockDestroyedOnPosition(block.Position()));
                    block.Destroy();
                }
            }
        } 
    }

    public void OnPrepare() => StopAllCoroutines();
    public void OnGamePaused() => _isPaused = true;
    public void OnGameResumed() => _isPaused = false;
    public void OnStartGame(){}
    public void OnContinueGame(){}
    public void OnEndGame(){}
}
