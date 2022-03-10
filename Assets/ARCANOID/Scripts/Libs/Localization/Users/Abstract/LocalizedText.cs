using UnityEngine;
using Zenject;

public abstract class LocalizedText : MonoBehaviour, ILanguageChangeListener
{
    [SerializeField] protected string translationID = "translation_error";
    [SerializeField] protected TextWithValueParams textWithValueParams;
    [Inject] protected LocalizationManager _localizationManager;

    protected string _insertedValue;

    protected virtual void OnEnable()
    {
        MessageBus.Subscribe(this);

        if (_localizationManager != null) RefreshLabel();
    }

    protected virtual void OnDisable()
    {
        MessageBus.Unsubscribe(this);
    }
    
    public void ChangeTranslationID(string newID)
    {
        translationID = newID;
        RefreshLabel();
    }
    
    public void InsertNumber(string insertedValue)
    {
        _insertedValue = insertedValue;
        RefreshLabel();
    }

    public void OnLanguageChanged()
    {
        RefreshLabel();
    }

    protected abstract void RefreshLabel();
}
