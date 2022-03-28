using System;

[Serializable]
public class SavedEnergyProgress : IStoredData
{
    public int Energy { get; set; }
    public DateTime SaveTime { get; set; }
    public float RecoveryProgress { get; set; }
}
