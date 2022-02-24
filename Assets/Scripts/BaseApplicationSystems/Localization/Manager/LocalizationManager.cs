using UnityEngine;

public class LocalizationManager : Singleton<LocalizationManager>
{
    private const string PARSER_CONFIG_PATH = "Configurations/Localization/CsvParserConfig";
    private ITranslationsStorage _translationsStorage;

    protected override void Init()
    {
        var parserConfig = LoadParserConfig();
        _translationsStorage = new TranslationsStorage(parserConfig, StoredDataManager.Instance);
    }

    private LanguageParserConfig LoadParserConfig()
    {
        return Resources.Load<LanguageParserConfig>(PARSER_CONFIG_PATH);
    }
    
    protected override void OnApplicationQuit() => SaveCurrentLanguage();

    private void OnApplicationPause(bool status) => SaveCurrentLanguage();

    private void SaveCurrentLanguage()
    {
        _translationsStorage.SaveCurrentLanguage();
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
