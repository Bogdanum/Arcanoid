using UnityEngine;

[CreateAssetMenu(fileName = "StorageProvider", menuName = "Storage/StorageProvider")]
public class StorageProvider : ScriptableObject
{
    [SerializeField, Space(10)] 
    private StorageEnums.StorageLocation storageLocation;
    [SerializeField] private string binaryFileName = "GameData.dat";
    
    public IGameDataStorage GetStorage()
    {
        switch (storageLocation)
        {
            case StorageEnums.StorageLocation.PlayerPrefs:
            {
                return new PlayerPrefsGameDataStorage();
            }
            case StorageEnums.StorageLocation.BinaryFile:
            {
                return new BinaryGameDataStorage(binaryFileName);
            }
            default: 
                return new PlayerPrefsGameDataStorage();
        }
    }
}

