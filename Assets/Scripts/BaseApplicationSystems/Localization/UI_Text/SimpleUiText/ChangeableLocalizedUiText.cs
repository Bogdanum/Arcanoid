public class ChangeableLocalizedUiText : LocalizedUiText
{
    public void ChangeTranslationID(string newID)
    {
        translationID = newID;
        UpdateTranslation();
    }
}
