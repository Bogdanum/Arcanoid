using UnityEngine;

public class PlayerPlatformController : MonoBehaviour
{
    [SerializeField] private GameBounds gameBounds;
    [SerializeField] private Platform platform;
    [SerializeField] private PlayerPlatformSettings settings;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        platform.Init(settings.TargetPositionAccuracy, gameBounds.GetGameBoundarySizeX());
        platform.RefreshParameters(settings.InitialSpeed, settings.InitialSize);
    }
}
