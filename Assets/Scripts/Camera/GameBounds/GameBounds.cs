using UnityEngine;

public class GameBounds : MonoBehaviour
{
    [SerializeField] private Camera worldCamera;

    public float GetGameBoundarySizeX()
    {
        return worldCamera.orthographicSize * 2 * worldCamera.aspect;
    }
}
