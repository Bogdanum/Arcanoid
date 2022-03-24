public interface IExplosionProcessor : ILocalGameStateHandler
{
    void LaunchExplosion(GridBlockFinder gridBlockFinder);
}
