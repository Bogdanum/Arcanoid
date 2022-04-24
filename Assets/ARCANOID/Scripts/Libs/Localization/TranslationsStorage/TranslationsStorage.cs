using System.Collections.Generic;

public class TranslationsStorage : ITranslationsStorage
{
    private Dictionary<string, string> _translationsDictionary;
    private ILanguageParser _languageParser;
    private LanguagesEnums.Language _currentLanguage;
    private readonly LanguageParserConfig _parserConfig;
    private readonly StoredDataManager _storedDataManager;

    public TranslationsStorage(LanguageParserConfig parserConfig, StoredDataManager storedDataManager)
    {
        _translationsDictionary = new Dictionary<string, string>();
        _storedDataManager = storedDataManager;
        _currentLanguage = GetCurrentLanguage();
        _parserConfig = parserConfig;
        CreateParser(parserConfig);
    }

    private void CreateParser(LanguageParserConfig parserConfig)
    {
        JsonParserConfig config = (JsonParserConfig) parserConfig;
        config.Init();
        _languageParser = new JsonTranslationParser(config, _currentLanguage);
        _translationsDictionary = GetTranslationsDictionary();
    }

    private Dictionary<string, string> GetTranslationsDictionary()
    {
        return _languageParser.GetTranslationDictionary();
    }

    public string GetTranslation(string itemID)
    {
        return _translationsDictionary[itemID];
    }

    public LanguagesEnums.Language GetCurrentLanguage()
    {
        InterfaceParamsData interfaceParamsData = _storedDataManager.GetSavedData<InterfaceParamsData>(new InterfaceParamsData());
        _currentLanguage = interfaceParamsData.Language;
        return _currentLanguage;
    }
    
    public void SetLanguage(LanguagesEnums.Language language)
    {
        if (language == _currentLanguage) return;
        
        _currentLanguage = language;
        CreateParser(_parserConfig);
        SaveCurrentLanguage();
    }
    
    public void SaveCurrentLanguage()
    {
        _storedDataManager.SaveLanguage(_currentLanguage);
    }
}
