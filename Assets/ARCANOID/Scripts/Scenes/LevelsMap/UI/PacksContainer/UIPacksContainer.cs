using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIPacksContainer : MonoBehaviour
{
    private PackButtonsFactory _factory;
    private Dictionary<string, PackButton> _packButtons;

    [Inject]
    public void Init(PackButtonsFactory factory)
    {
        _factory = factory;
    }

    public void RefreshContainer(Dictionary<string, LevelPackInfo> packInfos)
    {
        if (_packButtons == null)
        {
            CreatePackButtons(packInfos);
        }
        foreach (var id in packInfos.Keys)
        {
            bool isDefaultView = !packInfos[id].IsOpened;
            _packButtons[id].SetupView(packInfos[id], isDefaultView);
        }
    }

    private void CreatePackButtons(Dictionary<string, LevelPackInfo> packInfos)
    {
        _packButtons = new Dictionary<string, PackButton>();

        foreach (var id in packInfos.Keys)
        {
            var packButton = _factory.Create();
            packButton.transform.SetDefaultParams(transform);
            _packButtons.Add(id, packButton);
        }
    }
}
