using System;
using UnityEngine;
public class LocalizedTMProWithValue : LocalizedTMPro
{
    private const string defaultFormat = "{0}: {1}";
    
    [SerializeField] private string format = defaultFormat;
    private string _insertedValue;

    public void InsertNumber(string insertedValue)
    {
        _insertedValue = insertedValue;
        OnLanguageChanged();
    }

    public override void OnLanguageChanged()
    {
        string translatedText = LocalizationManager.Instance.GetTranslation(translationID);
        label.text = String.Format(format, translatedText, _insertedValue);
    }
}
