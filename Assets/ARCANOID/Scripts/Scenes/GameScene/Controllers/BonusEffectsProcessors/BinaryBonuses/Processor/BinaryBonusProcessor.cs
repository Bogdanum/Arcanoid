using System;
using System.Collections;
using UnityEngine;

public class BinaryBonusProcessor : MonoBehaviour
{
    private BinaryBonusProcessorConfig _config;
    
    private float _currentValue;
    public event Action<float> OnCurrentValueChange;

    public void Init(BinaryBonusProcessorConfig config)
    {
        _config = config;
    }
    
    public void Launch(BinaryBonusDirection direction)
    {
        float value = _config.GetValueWithDirectedStep(direction, _currentValue);
        StartCoroutine(EffectProcess(value));
    }

    private IEnumerator EffectProcess(float value)
    {
        _currentValue += value;
        OnCurrentValueChange?.Invoke(_currentValue);
        yield return new WaitForSeconds(_config.BonusEffectTime);
        EndEffect(value);
    }

    private void EndEffect(float value)
    {
        _currentValue -= value;
        var endEffectValue = _config.CheckOutOfBounds(_currentValue);
        OnCurrentValueChange?.Invoke(endEffectValue);
    }

    public void Stop()
    {
        StopAllCoroutines();
        _currentValue = 0;
        OnCurrentValueChange?.Invoke(_currentValue);
    }
}
