using UnityEngine;
using Zenject;

public class PoolsManagerInstaller : MonoInstaller
{
    [SerializeField] private PoolsManager poolsManager;

    public override void InstallBindings()
    {
        Container.Bind<PoolsManager>().FromInstance(poolsManager).AsSingle().NonLazy();
    }
}