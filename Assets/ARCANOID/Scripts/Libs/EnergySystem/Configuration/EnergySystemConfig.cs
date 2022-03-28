using UnityEngine;

[CreateAssetMenu(fileName = "Energy Config", menuName = "Energy System/New Energy Config")]
public class EnergySystemConfig : ScriptableObject
{
    [SerializeField] private float timeToRestoreStep;
    [SerializeField] private int energyPerStep;
    [SerializeField] private int maxEnergy;
    [SerializeField] private EnergyActionList energyActionList;
    
    public float TimeToRestoreStep => timeToRestoreStep;
    public int EnergyPerStep => energyPerStep;
    public int MaxEnergy => maxEnergy;

    public void Init() => energyActionList.Init();
    
    public int GetEnergyValue(ActionWithEnergy actionType)
    {
        return energyActionList.GetEnergyValue(actionType);
    }
}
