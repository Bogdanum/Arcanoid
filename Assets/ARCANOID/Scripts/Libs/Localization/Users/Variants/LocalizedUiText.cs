using System;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedUiText : LocalizedText
{
    [SerializeField] private Text label;
    
    protected override void RefreshLabel()
    {
        var translate = _localizationManager.GetTranslation(translationID);
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

