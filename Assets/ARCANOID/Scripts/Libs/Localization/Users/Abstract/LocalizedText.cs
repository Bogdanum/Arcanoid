using UnityEngine;
using Zenject;

public abstract class LocalizedText : MonoBehaviour
{
    [SerializeField] protected string translationID = "translation_error";
    [SerializeField] protected TextWithValueParams textWithValueParams;
    protected LocalizationManager _localizationManager;
    protected string _insertedValue;
    public virtual Color Color { get; set; }

    [Inject]
    public void Init(LocalizationManager localizationManager)
    {
        _localizationManager = localizationManager;
    }
    
    protected virtual void OnEnable()
    {
        _localizationManager.OnLanguageChanged += RefreshLabel;
        if (_localizationManager != null)
        {
            RefreshLabel();
        }
        else
        {
            Debug.Log("Missing Localization manager: " + gameObject.name);
        }
    }

    protected virtual void OnDisable()
    {
        _localizationManager.OnLanguageChanged -= RefreshLabel;
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

    public void SetInsertNumberActive(bool state)
    {
        textWithValueParams.IsTextWithValue = state;
    }

    protected abstract void RefreshLabel();
}
