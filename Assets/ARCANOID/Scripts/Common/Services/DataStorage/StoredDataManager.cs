using UnityEngine;

public class StoredDataManager : MonoBehaviour
{
    private StorageProvider _storageProvider;

    public void Init(StorageProvider storageProvider)
    {
        _storageProvider = storageProvider;
    }

    public T GetSavedData<T>(IStoredData defaultData) where T : IStoredData => LoadData<T>(defaultData);
    
    public void SaveLanguage(LanguagesEnums.Language language)
    {
        var data = LoadData<InterfaceParamsData>(new InterfaceParamsData());
        data.Language = language;
        SaveData(data);
    }
    
    private T LoadData<T>(IStoredData defaultData) where T : IStoredData
    {
        return _storageProvider.GetStorage<T>().Load(defaultData);
    }

    private void SaveData<T>(T data) where T : IStoredData
    {
        _storageProvider.GetStorage<T>().Save(data);
    }
}
