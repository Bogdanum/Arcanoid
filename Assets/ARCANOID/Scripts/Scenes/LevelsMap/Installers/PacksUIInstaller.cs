using UnityEngine;
using Zenject;

public class PacksUIInstaller : MonoInstaller
{
    [SerializeField] private PackButton prefab;
    [SerializeField] private DefaultPackButtonVisualParams defaultPackParams;
    [SerializeField] private LevelsMapUIController levelsMapUIController;

    public override void InstallBindings()
    {
        Container.Bind<PackButton>().FromInstance(prefab);
        Container.Bind<DefaultPackButtonVisualParams>().FromInstance(defaultPackParams).AsSingle();
        Container.Bind<LevelsMapUIController>().FromInstance(levelsMapUIController).AsSingle();
        Container.BindFactory<PackButton, PackButtonsFactory>().FromFactory<CustomPackButtonsFactory>();
    }
}
