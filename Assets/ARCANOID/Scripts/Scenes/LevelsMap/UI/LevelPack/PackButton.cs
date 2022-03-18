using UnityEngine;

public class PackButton : PoolItem
{
    [SerializeField] private PackButtonView view;
    private DefaultPackButtonVisualParams _defaultParams;
    private LevelsMapUIController _uiController;

    public void Init(DefaultPackButtonVisualParams defaultParams, LevelsMapUIController uiController)
    {
        _defaultParams = defaultParams;
        _uiController = uiController;
    }

    public void SetupView(LevelPackInfo packInfo , bool isDefault = true)
    {
        var packParams = packInfo.Pack;
        view.SetInteractable(!isDefault);
        view.SetBackgroundImage(isDefault ? _defaultParams.background : packParams.Background);
        view.SetPackIcon(isDefault ? _defaultParams.icon : packParams.Icon);
        view.SetFontColor(isDefault ? _defaultParams.fontColor : packParams.FontColor);
        view.SetBackgroundColor(isDefault ? _defaultParams.bgColor : Color.white);
        view.SetPackName(isDefault ? _defaultParams.translationID : packParams.PackID);
        view.UpdateLevelsLabel(isDefault ? 0 : packInfo.CompletedLevels, isDefault ? (object) "?" : packParams.Count);
        if (!isDefault)
        {
            view.SetCallback(() => OnPressed(packParams.PackID));
        }
    }

    private void OnPressed(string packID)
    {
        _uiController.OnPackClicked(packID);
    }
}
