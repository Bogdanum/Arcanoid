using UnityEngine;
using Zenject;

public class LocalizationManagerInstaller : MonoInstaller
{
    [SerializeField] private LocalizationManager localizationManager;
    [SerializeField] private LanguageParserConfig languageParserConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<LocalizationManager>().FromInstance(localizationManager).AsSingle();
        var storedDataManager = Container.Resolve<StoredDataManager>();
        localizationManager.Init(languageParserConfig, storedDataManager);
    }
}