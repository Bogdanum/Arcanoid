using UnityEngine;

[CreateAssetMenu(fileName = "BonusName", menuName = "GameObjectsConfiguration/Bonuses/Effects/Create New BinaryEffect config")]
public class BinaryBonusProcessorConfig : ScriptableObject
{
    [SerializeField, Min(0)] private float bonusEffectTime = 3f;
    [SerializeField, Min(0)] private float effectStep;
    [SerializeField] private float min;
    [SerializeField] private float max;

    public float BonusEffectTime => bonusEffectTime;

    public float CheckOutOfBounds(float value)
    {
        return Mathf.Min(Mathf.Max(value, min), max);
    }

    public float GetValueWithDirectedStep(BinaryBonusDirection direction, float currentValue)
    {
        float directedStep = effectStep * (int)direction;
        float valueWithStep = currentValue + directedStep;
        if (valueWithStep > max)
        {
            directedStep = max - currentValue;
        }
        else if (valueWithStep < min)
        {
            directedStep = min - currentValue;
        }
        return directedStep;
    }
}
