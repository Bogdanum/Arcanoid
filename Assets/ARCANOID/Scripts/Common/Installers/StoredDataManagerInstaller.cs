using UnityEngine;
using Zenject;

public class StoredDataManagerInstaller : MonoInstaller
{
    [SerializeField] private StoredDataManager storedDataManager;
    [SerializeField] private StorageProvider storageProvider;
    
    public override void InstallBindings()
    {
        Container.Bind<StoredDataManager>().FromInstance(storedDataManager).AsSingle().NonLazy();
        storedDataManager.Init(storageProvider);
    }
}