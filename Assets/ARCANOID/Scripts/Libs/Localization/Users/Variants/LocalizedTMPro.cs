using System;
using TMPro;
using UnityEngine;

public class LocalizedTMPro : LocalizedText
{
    [SerializeField] private TMP_Text label;
    
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
