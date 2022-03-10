using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryDataStorage<T> : IDataStorage<T> where T : IStoredData
{
    private string _fileName;
    private string _dataPath;

    public BinaryDataStorage()
    {
        _fileName = $"{typeof(T)}.dat";
        _dataPath = Path.Combine(Application.persistentDataPath, _fileName);
    }

    public void Save(T data)
    {
        using (FileStream file = File.Create(_dataPath))
        {
            new BinaryFormatter().Serialize(file, data);
        }
    }

    public T Load(IStoredData defaultData)
    {
        if (File.Exists(_dataPath))
        {
            using (FileStream file = File.Open(_dataPath, FileMode.Open))
            {
                return (T) new BinaryFormatter().Deserialize(file);
            }
        }
        return (T)defaultData;
    }
}
