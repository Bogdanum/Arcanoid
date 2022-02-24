using UnityEngine;
using UnityEngine.UI;

public class LocalizedUiText : MonoBehaviour, ILanguageChangeListener
{
    [SerializeField] protected Text label;
    [SerializeField] protected string translationID = "translation_error";

    private void OnEnable()
    {
        MessageBus.Subscribe(this);
        UpdateTranslation();
    }

    private void OnDisable()
    {
        MessageBus.Unsubscribe(this);
    }

    protected void UpdateTranslation()
    {
        OnLanguageChanged();
    }
    public virtual void OnLanguageChanged()
    {
        label.text = LocalizationManager.Instance.GetTranslation(translationID);
    }
}
