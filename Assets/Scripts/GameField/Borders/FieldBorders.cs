using UnityEngine;

public class FieldBorders : MonoBehaviour
{
    [SerializeField] private FieldSettings fieldSettings;
    [SerializeField] private GameBounds gameBounds;

    private Vector2 _gameFieldScale;
    private Transform _cachedFieldTransform;

    public void Init()
    {
        _cachedFieldTransform = transform;
        SetFieldScale();
        UpdateFieldScaleBySettings();
        UpdateFieldPositionBySettings();
    }

    private void SetFieldScale()
    {
        var halfSize = gameBounds.CameraOrthographicSize;
        _gameFieldScale = new Vector2(halfSize * gameBounds.CameraAspect, halfSize);
    }
    
    private void UpdateFieldScaleBySettings()
    {
        Vector2 scale = _gameFieldScale;
        scale.y -= scale.y * fieldSettings.HeaderOffset;
        _cachedFieldTransform.localScale = scale;
    }
    
    private void UpdateFieldPositionBySettings()
    {
        Vector3 pos = _cachedFieldTransform.position;
        pos.y -= _gameFieldScale.y * fieldSettings.HeaderOffset;
        _cachedFieldTransform.position = pos;
    }
}
