using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PopupsManagerInstaller : MonoInstaller
{
    [SerializeField] private PopupsManager popupsManager;
    [SerializeField] private PopupsContainer containerPrefab;
    [SerializeField] private BasePopup[] popups;

    public override void InstallBindings()
    {
        var container = Container.InstantiatePrefabForComponent<PopupsContainer>(containerPrefab);
        var popupsList = new Dictionary<Type, BasePopup>();
        for (int i = 0; i < popups.Length; i++)
        { 
            var popup = Container.InstantiatePrefabForComponent<BasePopup>(popups[i], container.transform);
            popup.Initialize();
            popupsList.Add(popup.GetType(), popup);
        }
        container.Init(popupsList);
        var manager = Container.InstantiatePrefabForComponent<PopupsManager>(popupsManager);
        Container.Bind<PopupsManager>().FromInstance(manager).AsSingle().NonLazy();
        manager.Init(container);
    }
}