using System.Collections.Generic;
using System.Linq;
using ModestTree;

public class ProgressSaveProvider
{
    private readonly StoredDataManager _storedDataManager;
    private StoredGameProgress _storedGameProgress;
    
    public bool IsSaveExistsOnStart { get; private set; }

    public ProgressSaveProvider(StoredDataManager storedDataManager ,Dictionary<string, LevelPack> packs)
    {
        _storedDataManager = storedDataManager;
        IsSaveExistsOnStart = _storedDataManager.SaveExists<StoredGameProgress>();

        if (IsSaveExistsOnStart)
        {
            _storedGameProgress = _storedDataManager.GetSavedData<StoredGameProgress>(new StoredGameProgress());
            CheckFirstPackSave(packs);
            CheckLastPackSave(packs);
            return;
        }
        OpenDefaultPack(packs);
    }

    private void CheckFirstPackSave(Dictionary<string, LevelPack> packs)
    {
        var packID = packs.First().Key;
        if (!StoredProgressContainsKey(packID))
        {
            var openPacks = _storedGameProgress.PacksProgress.Where(packs => packs.Value.IsOpen);
            IsSaveExistsOnStart = openPacks.Any(pack => packs.ContainsKey(pack.Key));
            OpenPack(packID);
        }
    }
    
    private void CheckLastPackSave(Dictionary<string, LevelPack> packs)
    {
        var completedPacksIds = GetCompletedPacksIds();
        var packIds = packs.Keys.ToArray();
        foreach (var packID in completedPacksIds)
        {
            int nextPackIndex = packIds.IndexOf(packID) + 1;
            if (nextPackIndex > 0 && nextPackIndex < packIds.Length)
            {
                string nextPackID = packIds[nextPackIndex];
                if (StoredProgressContainsKey(nextPackID)) return;
                
                OpenPack(nextPackID);
            }
        }
    }

    private void OpenDefaultPack(Dictionary<string, LevelPack> packs)
    {
        _storedGameProgress = new StoredGameProgress
        {
            PacksProgress = new Dictionary<string, StoredGameProgress.PackProgressState>()
        };
        OpenPack(packs.Keys.First());
    }

    public void OpenPack(string packID)
    {
        if (!StoredProgressContainsKey(packID))
        {
            var packState = new StoredGameProgress.PackProgressState
            {
                IsOpen = true,
                IsComplete = false,
                CurrentLevel = 0
            };
            _storedGameProgress.PacksProgress.Add(packID, packState);
        }
    }

    private string[] GetCompletedPacksIds()
    {
        return _storedGameProgress.PacksProgress
            .Where(pack => pack.Value.IsComplete)
            .Select(pack => pack.Key).ToArray();
    }

    public bool StoredProgressContainsKey(string packID)
    {
        return _storedGameProgress.PacksProgress.ContainsKey(packID);
    }

    public void SaveProgress()
    {
        _storedDataManager.SaveProgress(_storedGameProgress);
        IsSaveExistsOnStart = _storedDataManager.SaveExists<StoredGameProgress>();
    }
    
    public bool IsPackOpen(string packID)
    {
        return _storedGameProgress.PacksProgress[packID].IsOpen;
    }
        
    public bool IsPackComplete(string packID)
    {
        return _storedGameProgress.PacksProgress[packID].IsComplete;
    }

    public int GetCurrentLevel(string packID)
    {
        return _storedGameProgress.PacksProgress[packID].CurrentLevel;
    }

    public void CompletePack(string packID)
    {
        _storedGameProgress.PacksProgress[packID].IsComplete = true;
    }
        
    public void SetCurrentLevel(string packID, int level)
    {
        _storedGameProgress.PacksProgress[packID].CurrentLevel = level;
    }
}
