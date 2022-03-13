using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    private HealthViewGridConfig _config;
    private BallUI[] _ballsIcons;

    public void Init(HealthViewGridConfig config, PoolsManager poolsManager)
    {
        _config = config;
        if (_ballsIcons != null)
        {
            ReturnAllBallsToPool(poolsManager);
        }
        _ballsIcons = new BallUI[_config.MaxHealthCount];
        for (int i = 0; i < _config.MaxHealthCount; i++)
        {
            _ballsIcons[i] = poolsManager.GetItem<BallUI>(Vector3.zero, transform);
            if (i < _config.InitHealthCount)
            {
                _ballsIcons[i].Show();
            }
        }
        SetGridSize(_config.InitHealthCount);
    }

    private void ReturnAllBallsToPool(PoolsManager poolsManager)
    {
        foreach (var ball in _ballsIcons)
        {
            poolsManager.ReturnItemToPool(ball);
        }
    }

    public void AddHealth(int currentHeartID)
    {
        SetGridSize(currentHeartID + 1);
        _ballsIcons[currentHeartID].Show(_config.DurationOfAppearance);
    }
    
    public void RemoveHeart(int heartIdToRemove)
    {
        _ballsIcons[heartIdToRemove].Hide(_config.DurationOfAppearance);
        SetGridSize(heartIdToRemove);
    }
    
    private void SetGridSize(int visibleHeartsCount)
    {
        var gridSizes = _config.GetGridSizes();
        foreach (var kv in gridSizes)
        {
            if (visibleHeartsCount < kv.Key)
            {
                grid.cellSize = new Vector2(gridSizes[kv.Key], gridSizes[kv.Key]);
                return;
            }
        }
        grid.cellSize = new Vector2(5, 5);
    }

}
