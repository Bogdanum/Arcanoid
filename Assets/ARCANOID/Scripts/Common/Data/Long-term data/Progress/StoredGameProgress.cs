using System.Collections.Generic;

[System.Serializable]
public class StoredGameProgress : IStoredData
{
    public Dictionary<string, PackProgressState> PacksProgress { get; set; }
    
    [System.Serializable]
    public class PackProgressState
    {
        public bool IsOpen { get; set; }
        public bool IsComplete { get; set; }
        public int CurrentLevel { get; set; }
    }
}
