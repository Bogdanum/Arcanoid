using System;
using UnityEngine;
using Zenject;

public class BonusesEffectsInstaller : MonoInstaller
{
    [SerializeField] private PlatformBonusSettings platformBonusSettings;
    
    public override void InstallBindings()
    {
        var platformSizeBonusStateController = platformBonusSettings.platformSizeBonusStateController;
        platformSizeBonusStateController.Init(platformBonusSettings.platformController, platformBonusSettings.config);
    }
    
    [Serializable]
    internal class PlatformBonusSettings
    {
        public PlatformSizeBonusStateController platformSizeBonusStateController;
        public PlayerPlatformController platformController;
        public BinaryBonusProcessorConfig config;
    }
}
