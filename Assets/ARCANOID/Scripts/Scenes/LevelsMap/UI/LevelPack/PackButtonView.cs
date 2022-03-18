using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackButtonView : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image packIcon;
    [SerializeField] private LocalizedTMPro packName;
    [SerializeField] private TMP_Text level;
    [SerializeField] private PackButtonListener buttonListener;

    public void SetInteractable(bool state) => buttonListener.SetInteractable(state);
    
    public void SetFontColor(Color color)
    {
        packName.Color = color;
        level.color = color;
    }
    
    public void SetBackgroundColor(Color color) => background.color = color;

    public void SetPackIcon(Sprite sprite) => packIcon.sprite = sprite;

    public void SetBackgroundImage(Sprite sprite)
    {
        background.sprite = sprite;
        background.type = Image.Type.Tiled;
    }

    public void SetPackName(string translationKey) => packName.ChangeTranslationID(translationKey);
    
    public void SetCallback(Action callback)
    { 
        if (buttonListener.hasCallback) return;
        
        buttonListener.OnClick += callback;
        buttonListener.hasCallback = true;
    }

    public void UpdateLevelsLabel(object current, object last)
    {
        level.text = $"{current}/{last}";
    }
}
