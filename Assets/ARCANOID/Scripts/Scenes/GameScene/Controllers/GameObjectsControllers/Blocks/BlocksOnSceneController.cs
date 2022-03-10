using UnityEngine;
using Zenject;

public class BlocksOnSceneController : MonoBehaviour, IBlockLifecycleHandler, IClearGameFieldHandler
{
    [SerializeField] private GridOfBlocks gridOfBlocks;
    
    [Inject] private PoolsManager _poolsManager;
    private BlockSpawnerController _spawner;
    private int _blocksOnSceneCount;

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _spawner = new BlockSpawnerController(_poolsManager);
    }

    public void OnDestructibleBlockSpawned() => _blocksOnSceneCount++;

    public void OnGetBlockParams(Vector3 position, Vector3 size, Transform parent, BlockProperties properties)
    {
        var block = _spawner.GetSpawnedBlock(properties, position, size, parent);
        gridOfBlocks.Add(position, block);
    }

    public void OnPlayingBlockDestructionParticles(Block block)
    {
        _blocksOnSceneCount--;
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
        _spawner.DestroyConcreteBlock(block);
        gridOfBlocks.Remove(block);
    }

    public void OnClearGameField()
    {
        _spawner.ClearBlocks();
        _blocksOnSceneCount = 0;
    }
}
