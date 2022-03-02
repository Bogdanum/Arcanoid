using System;

public class BallFactory : PoolItemFactory<Ball, BallBaseSettings>
{
    public override Type PoolItemType => typeof(Ball);
    public override PoolItem CreateItem<T>()
    {
        var ball = Instantiate(ItemPrefab, _factoryTransform);
        ball.Init(ItemSettings);
        return ball;
    }
}
