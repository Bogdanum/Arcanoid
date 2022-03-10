using UnityEngine;
using Zenject;

public class LocalizationManager : MonoBehaviour
{
    private ITranslationsStorage _translationsStorage;
    [Inject] private StoredDataManager _storedDataManager;

    public void Init(LanguageParserConfig parserConfig)
    {
        _translationsStorage = new TranslationsStorage(parserConfig, _storedDataManager);
    }

    private void OnApplicationQuit() => SaveCurrentLanguage();

    private void OnApplicationPause(bool status) => SaveCurrentLanguage();

    private void SaveCurrentLanguage()
    {
        _translationsStorage?.SaveCurrentLanguage();
    }

    private void Start() => RaiseUpdateLanguageEvent();

    public LanguagesEnums.Language GetCurrentLanguage()
    {
        return _translationsStorage.GetCurrentLanguage();
    }

    public string GetTranslation(string itemID)
    {
        return _translationsStorage.GetTranslation(itemID);
    }

    public void SetCurrentLanguage(LanguagesEnums.Language language)
    {
        _translationsStorage.SetLanguage(language);
        RaiseUpdateLanguageEvent();
    }

    private void RaiseUpdateLanguageEvent()
    {
        MessageBus.RaiseEvent<ILanguageChangeListener>(listener => listener.OnLanguageChanged());
    }
}
