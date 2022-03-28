using UnityEngine;
using Zenject;

public class EnergyManagerInstaller: MonoInstaller
{
    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private EnergySystemConfig config;

    public override void InstallBindings()
    {
        Container.Bind<EnergyManager>().FromInstance(energyManager).AsSingle().NonLazy();

        var storedDataManager = Container.Resolve<StoredDataManager>();
        config.Init();
        energyManager.Init(config, storedDataManager, new ContainerOfApples());
    }
}
