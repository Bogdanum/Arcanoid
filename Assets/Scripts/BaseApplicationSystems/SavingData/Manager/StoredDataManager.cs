using UnityEngine;

public class StoredDataManager : Singleton<StoredDataManager>
{
    private const string PROVIDER_CONFIG_PATH = "Configurations/UserData/StorageProvider"; 
    private StorageProvider _storageProvider;

    protected override void Init()
    {
        _storageProvider = Resources.Load<StorageProvider>(PROVIDER_CONFIG_PATH);
    }

    public GameData GetGameData() => LoadData();
    
    public void SaveLanguage(LanguagesEnums.Language language)
    {
        var data = LoadData();
        data.Language = language;
        SaveData(data);
    }
    
    private GameData LoadData()
    {
        return _storageProvider.GetStorage().Load();
    }

    private void SaveData(GameData data)
    {
        _storageProvider.GetStorage().Save(data);
    }
}
