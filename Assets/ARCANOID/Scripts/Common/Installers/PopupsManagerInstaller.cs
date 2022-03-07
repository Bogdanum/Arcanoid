using UnityEngine;
using Zenject;

public class PopupsManagerInstaller : MonoInstaller
{
    [SerializeField] private PopupsManager popupsManager;
    [SerializeField] private PopupsContainer containerPrefab;

    public override void InstallBindings()
    {
        var container = Container.InstantiatePrefabForComponent<PopupsContainer>(containerPrefab);
        var manager = Container.InstantiatePrefabForComponent<PopupsManager>(popupsManager);
        Container.Bind<PopupsManager>().FromInstance(manager).AsSingle().NonLazy();
        manager.Init(container);
    }
}
