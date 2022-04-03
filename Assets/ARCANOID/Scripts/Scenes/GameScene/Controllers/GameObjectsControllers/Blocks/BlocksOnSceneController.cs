using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlocksOnSceneController : MonoBehaviour, IBlockLifecycleHandler, IClearGameFieldHandler, ILocalGameStateHandler
{
    [SerializeField] private GridOfBlocks gridOfBlocks;
    [SerializeField] private ProgressSliderCounter progressSliderCounter;
    private BlockSpawnerController _blockSpawnerController;
    private int _blocksOnSceneCount;
    
    [Inject]
    public void Init(BlockSpawnerController blockSpawnerController)
    {
        _blockSpawnerController = blockSpawnerController;
    }

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnDestructibleBlockSpawned() => _blocksOnSceneCount++;

    public void OnGetBlockParams(Vector3 position, Vector3 size, Transform parent, BlockProperties properties)
    {
        var block = _blockSpawnerController.GetSpawnedBlock(properties, position, size, parent);
        gridOfBlocks.Add(position, block);
    }

    public void OnPlayingBlockDestructionParticles(Block block)
    {
        _blocksOnSceneCount--;
        progressSliderCounter.UpdateProgress(_blocksOnSceneCount);
        gridOfBlocks.Remove(block);
        IncreaseGameComplexity();
        
        if (_blocksOnSceneCount < 1)
        {
            MessageBus.RaiseEvent<IGameResultHandler>(handler => handler.OnVictory());
        }
    }

    private void IncreaseGameComplexity()
    {
        MessageBus.RaiseEvent<IComplexityIncreaseHandler>(handler => handler.OnIncreasingComplexity());
    }
    
    public void OnBlockDestroyed<T>(T block) where T : Block
    {
        _blockSpawnerController.DestroyConcreteBlock(block);
        gridOfBlocks.Remove(block);
    }

    public void OnClearGameField()
    {
        _blockSpawnerController.ClearBlocks();
        _blocksOnSceneCount = 0;
        progressSliderCounter.ResetProgressBar();
    }

    public void OnStartGame()
    {
        progressSliderCounter.InitProgressBar(0, _blocksOnSceneCount);
    }
    
    public void OnPrepare() {}
    public void OnContinueGame() {}
    public void OnEndGame() {}
}
