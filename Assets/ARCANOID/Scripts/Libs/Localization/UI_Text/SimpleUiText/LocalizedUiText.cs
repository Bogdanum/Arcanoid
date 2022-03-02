using System;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedUiText : MonoBehaviour, ILanguageChangeListener
{
    [SerializeField] private Text label;
    [SerializeField] private string translationID = "translation_error";
    [SerializeField] private TextWithValueParams textWithValueParams;
    private string _insertedValue;

    private void OnEnable()
    {
        MessageBus.Subscribe(this);
        UpdateTranslation();
    }

    private void OnDisable()
    {
        MessageBus.Unsubscribe(this);
    }
    
    public void ChangeTranslationID(string newID)
    {
        translationID = newID;
        UpdateTranslation();
    }
    
    public void InsertNumber(string insertedValue)
    {
        _insertedValue = insertedValue;
        OnLanguageChanged();
    }

    private void UpdateTranslation()
    {
        OnLanguageChanged();
    }
    public void OnLanguageChanged()
    {
        var translate = LocalizationManager.Instance.GetTranslation(translationID);
        if (textWithValueParams.IsTextWithValue)
        {
            label.text = String.Format(textWithValueParams.Format, translate, _insertedValue);
        }
        else
        {
            label.text = translate;
        }
    }
}

