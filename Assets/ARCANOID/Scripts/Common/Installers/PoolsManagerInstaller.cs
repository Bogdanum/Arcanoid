using UnityEngine;
using Zenject;

public class PoolsManagerInstaller : MonoInstaller
{
    [SerializeField] private PoolsManager poolsManager;
    [SerializeField] private SpecificPoolsSettings settings;

    public override void InstallBindings()
    {
        var poolsManagerInstance = Container.InstantiatePrefabForComponent<PoolsManager>(poolsManager);
        Container.Bind<PoolsManager>().FromInstance(poolsManagerInstance).AsSingle().NonLazy();
        poolsManagerInstance.Init(settings);
    }
}