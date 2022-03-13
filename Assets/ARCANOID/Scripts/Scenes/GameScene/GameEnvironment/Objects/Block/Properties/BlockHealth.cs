using UnityEngine;

[System.Serializable]
public class BlockHealth
{
    [SerializeField] private int defaultHealth = 3;
    [SerializeField] private Sprite[] cracksSteps;

    public int DefaultHealth => defaultHealth;
    private int last;
    private float _quantityRatio;
    
    public void Init(int customBlockHealth = 0)
    {
        float initHealth = customBlockHealth > 0 ? customBlockHealth : defaultHealth;
        _quantityRatio = initHealth / cracksSteps.Length + 1;
        last = cracksSteps.Length - 1;
    }

    public Sprite GetCracksByHealth(int healthPoints)
    {
        int cracksStep = (int)(healthPoints / _quantityRatio);
        cracksStep = cracksStep >= last ? last : cracksStep;
        return cracksStep >= 0 ? cracksSteps[cracksStep] : cracksSteps[0];
    }
}
