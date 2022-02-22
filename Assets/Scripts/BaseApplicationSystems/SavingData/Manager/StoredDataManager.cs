using UnityEngine;

public class StoredDataManager : Singleton<StoredDataManager>
{
    [SerializeField] private StorageProvider storageProvider;

    public GameData GetGameData() => LoadData();
    
    public void SaveLanguage(SystemLanguage language)
    {
        var data = LoadData();
        data.Language = language;
        SaveData(data);
    }
    
    private GameData LoadData()
    {
        return storageProvider.GetStorage().Load();
    }

    private void SaveData(GameData data)
    {
        storageProvider.GetStorage().Save(data);
    }
}
