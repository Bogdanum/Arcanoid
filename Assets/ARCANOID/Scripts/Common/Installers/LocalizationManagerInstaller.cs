using UnityEngine;
using Zenject;

public class LocalizationManagerInstaller : MonoInstaller
{
    [SerializeField] private LocalizationManager localizationManager;
    [SerializeField] private LanguageParserConfig languageParserConfig;
    
    public override void InstallBindings()
    {
        var localizationManagerInstance = Container.InstantiatePrefabForComponent<LocalizationManager>(localizationManager);
        Container.Bind<LocalizationManager>().FromInstance(localizationManagerInstance).AsSingle().NonLazy();
        localizationManagerInstance.Init(languageParserConfig);
    }
}