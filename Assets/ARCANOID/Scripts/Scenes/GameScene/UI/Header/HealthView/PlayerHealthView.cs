using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private HorizontalLayoutGroup parentLayoutGroup;
    private HealthViewGridConfig _config;
    private BallUI[] _ballsIcons;
    private Vector2 _containerSize;

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
                _ballsIcons[i].transform.localScale = Vector3.one;
            }
        }
        _containerSize = GetContainerSize();
        RefreshCellSizes(_config.InitHealthCount);
    }

    private Vector2 GetContainerSize()
    {
        var parentRect = (RectTransform) parentLayoutGroup.transform;
        var parentSize = parentRect.rect.size;
        parentSize.x -= parentLayoutGroup.spacing + parentLayoutGroup.padding.left + parentLayoutGroup.padding.right;
        parentSize.y -= parentLayoutGroup.padding.top + parentLayoutGroup.padding.bottom;
        return new Vector2(parentSize.x / 2, parentSize.y);
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
        RefreshCellSizes(currentHeartID + 1);
        _ballsIcons[currentHeartID].Show(_config.DurationOfAppearance);
    }
    
    public void RemoveHeart(int heartIdToRemove)
    {
        _ballsIcons[heartIdToRemove].Hide(_config.DurationOfAppearance, () =>
        {
            RefreshCellSizes(heartIdToRemove);
        });
    }

    private void RefreshCellSizes(int healthCount)
    {
        if (_ballsIcons.Length < 1) return;

        float cellWidth = _containerSize.x / healthCount;
        float side = cellWidth > _containerSize.y ? _containerSize.y : cellWidth;
        grid.cellSize = new Vector2(side, side);
    }
}
