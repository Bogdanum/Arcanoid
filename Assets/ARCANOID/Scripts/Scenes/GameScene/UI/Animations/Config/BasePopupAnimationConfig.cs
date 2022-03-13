using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "BasePopupAnimationConfig", menuName = "UI/Base/BasePopupAnimationConfig")]
public class BasePopupAnimationConfig : ScriptableObject
{
    [SerializeField] private float fadeInDuration = 1;
    [SerializeField] private float fadeOutDuration = 1;
    [SerializeField] private Ease fadeInEase = Ease.Linear;
    [SerializeField] private Ease fadeOutEase = Ease.Linear;
    [SerializeField] private float fadeInDelay = 1;

    public float FadeInDuration => fadeInDuration;
    public float FadeOutDuration => fadeOutDuration;
    public Ease FadeInEase => fadeInEase;
    public Ease FadeOutEase => fadeOutEase;
    public float FadeInDelay => fadeInDelay;
}
