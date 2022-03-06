using UnityEngine;

public class FieldSizeController : MonoBehaviour
{
    [SerializeField] private Camera currentCamera;
    [SerializeField] private FieldSettings fieldOffsetsSettings;
    [SerializeField] private bool isSquareBlocks = true;
    
    public Vector2 StartGridPosition { get; private set; }
    public float CellsMargin => fieldOffsetsSettings.CellsMargin;

    public void Init()
    {
        float headerOffset = Screen.height * fieldOffsetsSettings.HeaderOffset;
        float sideOffset = Screen.width * fieldOffsetsSettings.SideOffset;
        float gameFieldHeight = Screen.height - headerOffset;
        SetStartGridPosition(gameFieldHeight, sideOffset);
    }

    private void SetStartGridPosition(float gameFieldHeight, float sideOffset)
    {
        Vector2 screenStartPosition = new Vector2(sideOffset, gameFieldHeight);
        StartGridPosition = currentCamera.ScreenToWorldPoint(screenStartPosition);
    }

    public Vector2 CalculateCellSizeByResolution(int numberOfColumns)
    {
        float viewSizeY = 2 * currentCamera.orthographicSize;
        Vector2 viewSize = new Vector2(viewSizeY * currentCamera.aspect, viewSizeY);
        float viewSizeX_correct = viewSize.x - viewSize.x * fieldOffsetsSettings.SideOffset * 2;
        float cellSizeX = (viewSizeX_correct - (numberOfColumns - 1) * CellsMargin) / numberOfColumns;
        float cellSizeY = isSquareBlocks ? cellSizeX : cellSizeX / fieldOffsetsSettings.RectBlockAspect;
        return new Vector2(cellSizeX, cellSizeY);
    }
}
