using System;
using UnityEngine;
using Zenject;

public class BonusesEffectsInstaller : MonoInstaller
{
    [SerializeField] private PlatformBonusesSettings platformBonusesSettings;
    
    public override void InstallBindings()
    {
        InitPlatformBonusesControllers();
    }

    private void InitPlatformBonusesControllers()
    {
        var platformSizeBonusStateController = platformBonusesSettings.platformSizeBonusStateController;
        platformSizeBonusStateController.Init(platformBonusesSettings.platformController, platformBonusesSettings.sizeConfig);
        var platformSpeedBonusStateController = platformBonusesSettings.platformSpeedBonusStateController;
        platformSpeedBonusStateController.Init(platformBonusesSettings.platformController, platformBonusesSettings.speedConfig);
    }

    [Serializable]
    internal class PlatformBonusesSettings
    {
        public PlatformSizeBonusStateController platformSizeBonusStateController;
        public PlayerPlatformController platformController;
        public BinaryBonusProcessorConfig sizeConfig;
        public PlatformSpeedBonusStateController platformSpeedBonusStateController;
        public BinaryBonusProcessorConfig speedConfig;
    }
}
