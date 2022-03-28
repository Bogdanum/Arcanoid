using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlockSpawnerControllerInstaller : MonoInstaller
{
    [SerializeField] private DroppableBonusSettings droppableBonusSettings;
    
    public override void InstallBindings()
    {
        droppableBonusSettings.Init();
        var poolsManager = Container.TryResolve<PoolsManager>();
        if (poolsManager == null)
        {
            Debug.Log("[Installer] Missing PoolsManager", this);   
        }
        var spawners = CreateSpawners(poolsManager);
        Container.Bind<BlockSpawnerController>().FromNew().AsSingle().WithArguments(poolsManager, spawners);
    }

    private Dictionary<BlockType, IBlockSpawner> CreateSpawners(PoolsManager poolsManager)
    {
        return new Dictionary<BlockType, IBlockSpawner>
        {
            { BlockType.Simple, new SimpleBlockSpawner(poolsManager) },
            { BlockType.Bedrock, new BedrockSpawner(poolsManager) },
            { BlockType.FallingBonus, new FallingBonusBlockSpawner(poolsManager, droppableBonusSettings) },
            { BlockType.IntrablockBonus, new IntrablockBonusSpawner(poolsManager) },
        };
    }
}
