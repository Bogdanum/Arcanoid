using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class EnergyFxAnimator : MonoBehaviour
{
    [SerializeField] private EnergyFxConfig energyFxConfig;
    [SerializeField] private Transform fxContainer;
    [SerializeField] private RectTransform target;
    private PoolsManager _poolsManager;
    private bool _isPlaing;

    [Inject]
    public void Init(PoolsManager poolsManager)
    {
        _poolsManager = poolsManager;
    }
    
    public void Play(Vector3 startPosition, TweenCallback onComplete = null)
    {
        if (_isPlaing) return;
        
        StartCoroutine(AnimationProcess(startPosition, onComplete));
    }

    private IEnumerator AnimationProcess(Vector3 startPosition, TweenCallback onComplete = null)
    {
        _isPlaing = true;
        for (int i = 0; i < energyFxConfig.EntitiesCount; i++)
        {
            yield return new WaitForSeconds(energyFxConfig.DelayBetweenEntities);

            var randomStartPosition = GetRandomStartPosition(startPosition);
            var fxEntity = _poolsManager.GetItem<AppleFxEntity>(randomStartPosition);
            if (fxEntity == null) continue;
            
            fxEntity.SetRectParams(randomStartPosition, energyFxConfig.FxEntitySize, fxContainer);
            fxEntity.Play(target, energyFxConfig, () => ReturnToPool(fxEntity));
        }
        onComplete?.Invoke();
        _isPlaing = false;
    }

    private Vector2 GetRandomStartPosition(Vector2 startPosition)
    {
        var maxDistance = energyFxConfig.MaxDistanceBetweenEntities;
        var distance = new Vector2
        (
            Random.Range(-maxDistance, maxDistance),
            Random.Range(-maxDistance, maxDistance)
        );
        return startPosition + distance;
    }

    private void ReturnToPool(AppleFxEntity fxEntity)
    {
        _poolsManager.ReturnItemToPool(fxEntity);
    }
}