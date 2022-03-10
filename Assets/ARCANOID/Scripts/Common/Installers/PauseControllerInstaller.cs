using UnityEngine;
using Zenject;

public class PauseControllerInstaller : MonoInstaller
{
    [SerializeField] private PauseController prefab;

    public override void InstallBindings()
    {
        var pauseControllerInstance = Container.InstantiatePrefabForComponent<PauseController>(prefab);
        Container.Bind<PauseController>().FromInstance(pauseControllerInstance).AsSingle().NonLazy();
    }
}
