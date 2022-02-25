using System.Collections.Generic;

public class TranslationsStorage : ITranslationsStorage
{
    private Dictionary<string, string> _translationsDictionary;
    private ILanguageParser _languageParser;
    private LanguageParserConfig _parserConfig;
    private LanguagesEnums.Language _currentLanguage;
    private StoredDataManager _storedDataManager;

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
        GameData gameData = _storedDataManager.GetGameData();
        _currentLanguage = gameData.Language;
        return _currentLanguage;
    }
    
    public void SetLanguage(LanguagesEnums.Language language)
    {
        _currentLanguage = language;
        CreateParser(_parserConfig);
        SaveCurrentLanguage();
    }
    
    public void SaveCurrentLanguage()
    {
        _storedDataManager.SaveLanguage(_currentLanguage);
    }
}
