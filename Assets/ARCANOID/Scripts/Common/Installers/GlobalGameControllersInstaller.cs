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
        Container.Bind<PauseController>().FromNew().AsSingle().WithArguments(popupsManager);
        Container.Bind<GameResultController>().FromNew().AsSingle().WithArguments(popupsManager).NonLazy();
    }
}
