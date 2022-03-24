using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BonusesCreatorsInstaller : MonoInstaller
{
    [SerializeField] private DroppableBonusSettings droppableBonusSettings;
    
    public override void InstallBindings()
    {
        var poolsManager = Container.Resolve<PoolsManager>();
        droppableBonusSettings.Init();
        var effectReproducers = CreateReproducers();
        Container.Bind<BonusesSpawner>().FromNew().AsSingle().WithArguments(poolsManager, droppableBonusSettings, effectReproducers);
    }

    private Dictionary<BonusId, IBonusEffectReproducer> CreateReproducers()
    {
        return new Dictionary<BonusId, IBonusEffectReproducer>
        {
            { BonusId.PlatformSizeIncrease, new PlatformSizeBonusReproducer(BinaryBonusDirection.Increase) },
            { BonusId.PlatformSizeDecrease, new PlatformSizeBonusReproducer(BinaryBonusDirection.Decrease) },
            { BonusId.PlatformAcceleration , new PlatformSpeedBonusReproducer(BinaryBonusDirection.Increase) },
            { BonusId.PlatformDeceleration , new PlatformSpeedBonusReproducer(BinaryBonusDirection.Decrease) },
            { BonusId.SourceOfLife, new HealthBonusesReproducer(BinaryBonusDirection.Increase) },
            { BonusId.BlackLabel, new HealthBonusesReproducer(BinaryBonusDirection.Decrease) },
            { BonusId.FireBall, new RageBallBonusReproducer() },
            { BonusId.BallAcceleration, new BallSpeedBonusReproducer(BinaryBonusDirection.Increase) },
            { BonusId.BallDeceleration, new BallSpeedBonusReproducer(BinaryBonusDirection.Decrease) },
            { BonusId.HiddenBall, new HiddenBallBonusReproducer() },
            { BonusId.SimpleBomb, new SimpleBombBonusReproducer() },
            { BonusId.VerticalBomb, new LineTntBonusReproducer(LineTntDirection.Vertical) },
            { BonusId.HorizontalBomb, new LineTntBonusReproducer(LineTntDirection.Horizontal) },
            { BonusId.ChainColorBomb, new ChainColorTntReproducer() }
        };
    }
}
