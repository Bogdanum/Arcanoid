using System;
using UnityEngine;
using Zenject;

public class BonusesEffectsInstaller : MonoInstaller
{
    [SerializeField] private PlatformBonusesSettings platformBonusesSettings;
    [SerializeField] private BallBonusesSettings ballBonusesSettings;
    [SerializeField] private BombsSettings bombsSettings;
    
    public override void InstallBindings()
    {
        InitPlatformBonusesControllers();
        InitBallBonusesControllers();
        InitBombsControllers();
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
        var ballSpeedBonusController = ballBonusesSettings.ballSpeedBonusStateController;
        ballSpeedBonusController.Init(ballBonusesSettings.ballsOnSceneController, ballBonusesSettings.speedConfig);
        Container.Bind<HiddenBallBonusProcessor>().FromNew().AsSingle().WithArguments(ballBonusesSettings.ballsOnSceneController, ballBonusesSettings.hiddenBallBonusConfig).NonLazy();
    }
    
    private void InitBombsControllers()
    {
        var simpleBombBonusStateController = bombsSettings.simpleBombBonusStateController;
        bombsSettings.simpleBombConfig.Init();
        simpleBombBonusStateController.Init(bombsSettings.gridOfBlocks, bombsSettings.simpleBombConfig);
        var lineTntBonusStateController = bombsSettings.lineTntBonusStateController;
        bombsSettings.lineBombConfig.Init();
        lineTntBonusStateController.Init(bombsSettings.gridOfBlocks, bombsSettings.lineBombConfig);
    }

    [Serializable]
    internal class PlatformBonusesSettings
    {
        public PlatformSizeBonusStateController platformSizeBonusStateController;
        public PlayerPlatformController platformController;
        [Header("Size bonus")]
        public BinaryBonusProcessorConfig sizeConfig;
        [Header("Speed bonus")]
        public PlatformSpeedBonusStateController platformSpeedBonusStateController;
        public BinaryBonusProcessorConfig speedConfig;
    }
    
    [Serializable]
    internal class BallBonusesSettings
    {
        public BallsOnSceneController ballsOnSceneController;
        public BlocksOnSceneController blocksOnSceneController;
        [Header("Rage bonus")]
        public RageBallBonusStateController rageBallBonusStateController;
        public SimpleTemporaryBonusConfig rageBallConfig;
        [Header("Speed bonus")] 
        public BallSpeedBonusStateController ballSpeedBonusStateController;
        public BinaryBonusProcessorConfig speedConfig;
        [Header("HiddenBall bonus")] 
        public HiddenBallBonusConfig hiddenBallBonusConfig;
    }
    
    [Serializable]
    internal class BombsSettings
    {
        public GridOfBlocks gridOfBlocks;
        [Header("Simple Bomb")] 
        public SimpleBombBonusStateController simpleBombBonusStateController;
        public BombBonusConfig simpleBombConfig;
        [Header("Line Bomb")] 
        public LineTntBonusStateController lineTntBonusStateController;
        public BombBonusConfig lineBombConfig;
    }
}
