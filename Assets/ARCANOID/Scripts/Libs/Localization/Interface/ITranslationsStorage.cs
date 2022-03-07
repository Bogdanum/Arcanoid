public interface ITranslationsStorage
{
    LanguagesEnums.Language GetCurrentLanguage();
    string GetTranslation(string itemID);
    void SaveCurrentLanguage();
    void SetLanguage(LanguagesEnums.Language language);
}
