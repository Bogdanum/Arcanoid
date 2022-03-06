public interface ILanguageChangeListener : ISubscriber
{
    void OnLanguageChanged(LocalizationManager localizationManager);
}
