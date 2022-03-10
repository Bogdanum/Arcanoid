using UnityEngine;
using Zenject;

public class StoredDataManagerInstaller : MonoInstaller
{
    [SerializeField] private StoredDataManager storedDataManager;
    [SerializeField] private StorageProvider storageProvider;
    
    public override void InstallBindings()
    {
        var storedDataManagerInstance = Container.InstantiatePrefabForComponent<StoredDataManager>(storedDataManager);
        storedDataManagerInstance.Init(storageProvider);
        Container.Bind<StoredDataManager>().FromInstance(storedDataManagerInstance).AsSingle().NonLazy();
    }
}