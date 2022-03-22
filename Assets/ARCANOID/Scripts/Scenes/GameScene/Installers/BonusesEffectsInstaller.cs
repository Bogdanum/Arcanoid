using System;
using UnityEngine;
using Zenject;

public class BonusesEffectsInstaller : MonoInstaller
{
    [SerializeField] private PlatformBonusesSettings platformBonusesSettings;
    [SerializeField] private BallBonusesSettings ballBonusesSettings;
    
    public override void InstallBindings()
    {
        InitPlatformBonusesControllers();
        InitBallBonusesControllers();
    }
    
    private void InitPlatformBonusesControllers()
    {
        var platformSizeBonusStateController = platformBonusesSettings.platformSizeBonusStateController;
        platformSizeBonusStateController.Init(platformBonusesSettings.platformController, platformBonusesSettings.sizeConfig);
        var platformSpeedBonusStateController = platformBonusesSettings.platformSpeedBonusStateController;
        platformSpeedBonusStateController.Init(platformBonusesSettings.platformController, platformBonusesSettings.speedConfig);
    }
    
    private void InitBallBonusesControllers()
    {
        var rageBallBonusController = ballBonusesSettings.rageBallBonusStateController;
        rageBallBonusController.Init(ballBonusesSettings.ballsOnSceneController, ballBonusesSettings.blocksOnSceneController, ballBonusesSettings.rageBallConfig);
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
    
    [Serializable]
    internal class BallBonusesSettings
    {
        public BallsOnSceneController ballsOnSceneController;
        public BlocksOnSceneController blocksOnSceneController;
        public RageBallBonusStateController rageBallBonusStateController;
        public SimpleTemporaryBonusConfig rageBallConfig;
    }
}
