using System;
using System.Collections;
using UnityEngine;

public class SimpleTemporaryBonusProcessor : MonoBehaviour
{
    private SimpleTemporaryBonusConfig _config;
    public event Action OnEffectEnded;
    private float _bonusEffectTime;
    public bool IsBonusActive { get; private set; }

    public void Init(SimpleTemporaryBonusConfig config)
    {
        _config = config;
    }

    public void Activate()
    {
        _bonusEffectTime += _config.EffectTime;
        if (!IsBonusActive)
        {
            StartCoroutine(EffectTimer());
        }
    }

    private IEnumerator EffectTimer()
    {
        IsBonusActive = true;
        float time = 0;
        while (time < _bonusEffectTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        EndEffect();
    }

    private void EndEffect()
    {
        IsBonusActive = false;
        _bonusEffectTime = 0;
        OnEffectEnded?.Invoke();
    }

    public void ForceEnd()
    {
        if (IsBonusActive)
        {
            StopAllCoroutines();
            EndEffect();
        }
    }
}
