using UnityEngine;

public class StoredDataManager : MonoBehaviour
{
    private StorageProvider _storageProvider;

    public void Init(StorageProvider storageProvider)
    {
        _storageProvider = storageProvider;
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
