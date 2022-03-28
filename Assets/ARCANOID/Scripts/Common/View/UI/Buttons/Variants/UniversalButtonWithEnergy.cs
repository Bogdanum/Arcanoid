using TMPro;
using UnityEngine;

public class UniversalButtonWithEnergy : UniversalButton
{
    [SerializeField] private GameObject locker;
    [SerializeField] private TMP_Text[] energyCostLabels;

    public void SetCost(int cost)
    {
        foreach (var label in energyCostLabels)
        {
            label.text = cost.ToString();
        } 
    }

    public void Lock() => locker.SetActive(true);
    
    public void Unlock() => locker.SetActive(false);
}
