using System;
using UnityEngine;
using Zenject;

public class BonusesEffectsInstaller : MonoInstaller
{
    [SerializeField] private PlatformBonusesSettings platformBonusesSettings;
    
    public override void InstallBindings()
    {
        var platformSizeBonusStateController = platformBonusesSettings.platformSizeBonusStateController;
        platformSizeBonusStateController.Init(platformBonusesSettings.platformController, platformBonusesSettings.config);
    }
    
    [Serializable]
    internal class PlatformBonusesSettings
    {
        public PlatformSizeBonusStateController platformSizeBonusStateController;
        public PlayerPlatformController platformController;
        public BinaryBonusProcessorConfig config;
    }
}
