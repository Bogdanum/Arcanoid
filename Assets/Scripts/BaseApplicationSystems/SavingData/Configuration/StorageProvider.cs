using UnityEngine;

[CreateAssetMenu(fileName = "StorageProvider", menuName = "Storage/StorageProvider")]
public class StorageProvider : ScriptableObject
{
    [SerializeField, Space(10)] 
    private StorageEnums.StorageLocation storageLocation;
    
    public IStorage GetStorage()
    {
        switch (storageLocation)
        {
            case StorageEnums.StorageLocation.PlayerPrefs:
            {
                return new PlayerPrefsStorage();
            }
            default: 
                return new PlayerPrefsStorage();
        }
    }
}

