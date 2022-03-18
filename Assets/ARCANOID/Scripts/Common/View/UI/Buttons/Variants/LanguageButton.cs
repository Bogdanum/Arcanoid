using UnityEngine;
using UnityEngine.Events;

public class LanguageButton : AnimatedButton
{
    [SerializeField] private Vector3 onClickScale;
    [SerializeField] private float animationTime;
    [SerializeField] private TweenScaler scaler;
    [SerializeField] private LanguagesEnums.Language language;
    [SerializeField] private UnityEvent<LanguagesEnums.Language> onSelectLanguage;

    protected override void OnPointerDown()
    {
        scaler.DoScale(onClickScale, animationTime);
    }

    protected override void ReturnToNormalAnim()
    {
        scaler.DoScale(Vector3.one, animationTime);
    }

    protected override void ExecuteClickEvent()
    {
        if (PointerEnter && interactable)
        {
            onSelectLanguage?.Invoke(language);
        }
    }
}
