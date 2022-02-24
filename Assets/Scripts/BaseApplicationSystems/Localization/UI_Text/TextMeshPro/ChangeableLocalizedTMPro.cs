
public class ChangeableLocalizedTMPro : LocalizedTMPro
{
    public void ChangeTranslationID(string newID)
    {
        translationID = newID;
        UpdateTranslation();
    }
}
