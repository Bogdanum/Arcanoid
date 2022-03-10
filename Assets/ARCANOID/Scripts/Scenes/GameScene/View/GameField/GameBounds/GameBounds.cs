using UnityEngine;

public class GameBounds : MonoBehaviour
{
    [SerializeField] private Camera worldCamera;

    public float CameraOrthographicSize => worldCamera.orthographicSize;
    public float CameraAspect => worldCamera.aspect;
    
    public float GetGameBoundarySizeX()
    {
        return worldCamera.orthographicSize * 2 * worldCamera.aspect;
    }
}
