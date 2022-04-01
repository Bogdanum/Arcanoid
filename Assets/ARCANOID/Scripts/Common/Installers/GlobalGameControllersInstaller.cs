using UnityEngine;
using Zenject;

public class GlobalGameControllersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var popupsManager = Container.TryResolve<PopupsManager>();
        if (popupsManager == null)
        {
            Debug.Log("[Installer] Missing PopupsManager!");
        }
        var levelPacksManager = Container.Resolve<LevelPacksManager>();
        var energyManager = Container.Resolve<EnergyManager>();
        Container.Bind<PauseController>().FromNew().AsSingle().WithArguments(popupsManager, energyManager);
        Container.Bind<GameResultController>().FromNew().AsSingle().WithArguments(popupsManager, levelPacksManager).NonLazy();
    }
}
