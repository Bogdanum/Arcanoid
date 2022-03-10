using UnityEngine;

public class OrthographicCameraConstWidth : MonoBehaviour
{
    [SerializeField] private Vector2 defaultResolution = new Vector2(720, 1280);
    [SerializeField, Range(0f, 1f)] private float widthOrHeight = 0;
    [SerializeField] private Camera componentCamera;
    
    private float _initialSize;
    private float _targetAspect;

    private void Start()
    {
        _initialSize = componentCamera.orthographicSize;
        _targetAspect = defaultResolution.x / defaultResolution.y;
    }

    private void Update()
    {
        float constantWidthSize = _initialSize * (_targetAspect / componentCamera.aspect);
        componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, widthOrHeight);
    }
}
