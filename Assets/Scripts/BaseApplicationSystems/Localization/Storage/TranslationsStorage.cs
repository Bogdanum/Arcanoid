using System.Collections.Generic;

public class TranslationsStorage : ITranslationsStorage
{
    private Dictionary<string, List<string>> _translationsDictionary;
    private ILanguageParser _languageParser;
    private LanguagesEnums.Language _currentLanguage;
    private StoredDataManager _storedDataManager;

    public TranslationsStorage(LanguageParserConfig parserConfig, StoredDataManager storedDataManager)
    {
        _translationsDictionary = new Dictionary<string, List<string>>();
        _storedDataManager = storedDataManager;
        _currentLanguage = GetCurrentLanguage();
        CreateParser(parserConfig);
        _translationsDictionary = GetTranslationsDictionary();
    }

    private void CreateParser(LanguageParserConfig parserConfig)
    {
        CsvParserConfig config = (CsvParserConfig) parserConfig;
        _languageParser = new CsvParser(config);
    }
    
    private Dictionary<string, List<string>> GetTranslationsDictionary()
    {
        return _languageParser.GetTranslationDictionary();
    }

    public string GetTranslation(string itemID)
    {
        var values = _translationsDictionary[itemID];
        int id = (int)_currentLanguage;
        return values[id];
    }

    public LanguagesEnums.Language GetCurrentLanguage()
    {
        GameData gameData = _storedDataManager.GetGameData();
        _currentLanguage = gameData.Language;
        return _currentLanguage;
    }
    
    public void SaveCurrentLanguage()
    {
        _storedDataManager.SaveLanguage(_currentLanguage);
    }

    public void SetLanguage(LanguagesEnums.Language language)
    {
        _currentLanguage = language;
        SaveCurrentLanguage();
    }
}
