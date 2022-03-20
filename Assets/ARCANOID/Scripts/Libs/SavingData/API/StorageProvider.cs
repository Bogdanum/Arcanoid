using UnityEngine;

[CreateAssetMenu(fileName = "StorageProvider", menuName = "Data/Storage/StorageProvider")]
public class StorageProvider : ScriptableObject
{
    [SerializeField, Space(10)] 
    private StorageLocation storageLocation = StorageLocation.BinaryFile;

    public IDataStorage<T> GetStorage<T>() where T : IStoredData
    {
        switch (storageLocation)
        {
            case StorageLocation.BinaryFile:
            {
                return new BinaryDataStorage<T>();
            }
            default: 
                return new BinaryDataStorage<T>();
        }
    }
}

