using System;
using UnityEngine;

public class Platform : MonoBehaviour, IPauseHandler
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlatformMovement platformMovement;
    [SerializeField] private PlatformCollider platformCollider;
    [SerializeField] private Transform spawnPoint;
    
    private float _gameBoundarySize;
    public Transform SpawnPoint => spawnPoint;

    public void Init(float targetPosAccuracy, float gameBoundarySize)
    {
        platformMovement.Init(targetPosAccuracy, gameBoundarySize);
        platformCollider.Init();
        _gameBoundarySize = gameBoundarySize;
    }

    public void RefreshParameters(float speed, float size)
    {
        SetNewSpeed(speed);
        SetNewSize(size);
        platformMovement.RefreshParameters();
        StopAllCoroutines();
    }

    public void SetNewSize(float size)
    {
        platformMovement.SetNewPhysicsSize(size);

        var newSize = _gameBoundarySize * size;
        platformCollider.SetNewSize(newSize);
        spriteRenderer.size = new Vector2(newSize, spriteRenderer.size.y);
    }

    public void SetNewSpeed(float speed)
    {
        platformMovement.SetNewSpeed(speed);
    }

    public void BackToInitialPosition(Action onComplete = null)
    {
        StartCoroutine(platformMovement.BackToInitialPosition(onComplete));
    }

    public void LockControl() => platformMovement.LockControl();
    public void OnGamePaused() => LockControl();

    public void OnGameResumed() => platformMovement.UnlockControl();
}
