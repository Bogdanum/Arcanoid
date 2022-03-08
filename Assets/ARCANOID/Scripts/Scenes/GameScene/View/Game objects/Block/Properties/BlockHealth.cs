using UnityEngine;

[System.Serializable]
public class BlockHealth
{
    [SerializeField] private int defaultHealth = 3;
    [SerializeField] private Sprite[] cracksSteps;

    public int DefaultHealth => defaultHealth;
    private int _quantityRatio;
    
    public void Init()
    {
        _quantityRatio = (defaultHealth / cracksSteps.Length) + 1;
    }

    public Sprite GetCracksByHealth(int healthPoints)
    {
        int cracksStep = healthPoints / _quantityRatio;
        return cracksStep >= 0 ? cracksSteps[cracksStep] : cracksSteps[0];
    }
}
