using UnityEngine;
using Zenject;

public class LevelPacksManagerInstaller : MonoInstaller
{
    [SerializeField] private LevelPacksManager levelPacksManager;
    [SerializeField] private TextAsset tilesetFile;
    [SerializeField] private JsonTokens jsonTokens;

    public override void InstallBindings()
    {
        var managerInstance = Container.InstantiatePrefabForComponent<LevelPacksManager>(levelPacksManager);
        Container.Bind<LevelPacksManager>().FromInstance(managerInstance).AsSingle();
        managerInstance.Init(jsonTokens, tilesetFile);
    }
}
