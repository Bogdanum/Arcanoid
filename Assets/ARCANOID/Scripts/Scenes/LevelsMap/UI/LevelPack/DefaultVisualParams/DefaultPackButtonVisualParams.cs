using UnityEngine;

[CreateAssetMenu(fileName = "DefaultPackButtonVisualParams", menuName = "UI/Packs/DefaultPackButtonVisualParams")]
public class DefaultPackButtonVisualParams : ScriptableObject
{
    public Color fontColor;
    public Color bgColor;
    public Sprite background;
    public Sprite icon;
    public string translationID;
}
