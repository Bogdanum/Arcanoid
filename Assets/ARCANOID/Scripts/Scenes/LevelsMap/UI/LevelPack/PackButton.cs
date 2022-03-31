using TMPro;
using UnityEngine;

public class PackButton : PoolItem
{
    [SerializeField] private PackButtonView view;
    [SerializeField] private GameObject energyLocker;
    [SerializeField] private TMP_Text energyText;
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
        view.SetBackgroundImage(isDefault ? _defaultParams.background : packParams.PackButtonBackground);
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

    public void SetupLocker(int cost) => energyText.text = cost.ToString();
    public void Lock() => energyLocker.SetActive(true);
    public void Unlock() => energyLocker.SetActive(false);

    private void OnPressed(string packID)
    {
        _uiController.OnPackClicked(packID);
    }
}
