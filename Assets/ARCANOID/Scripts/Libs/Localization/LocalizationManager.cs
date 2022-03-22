using System;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public event Action OnLanguageChanged;
    private ITranslationsStorage _translationsStorage;

    public void Init(LanguageParserConfig parserConfig, StoredDataManager storedDataManager)
    {
        _translationsStorage = new TranslationsStorage(parserConfig, storedDataManager);
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
        OnLanguageChanged?.Invoke();
    }
}
