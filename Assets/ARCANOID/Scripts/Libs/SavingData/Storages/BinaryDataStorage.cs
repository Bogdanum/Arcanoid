using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryDataStorage<T> : IDataStorage<T> where T : IStoredData
{
    private readonly string _dataPath;

    public BinaryDataStorage()
    {
        var fileName = $"{typeof(T)}.dat";
        _dataPath = Path.Combine(Application.persistentDataPath, fileName);
    }

    public bool SaveExists() => File.Exists(_dataPath);

    public void Save(T data)
    {
        var binFormatter = new BinaryFormatter();
        var file = File.Create(_dataPath);
        binFormatter.Serialize(file, data);
        file.Close();
    }

    public T Load(IStoredData defaultData)
    {
        if (File.Exists(_dataPath))
        {
            var binFormatter = new BinaryFormatter();
            var file = File.Open(_dataPath, FileMode.Open);
            var data = (T) binFormatter.Deserialize(file);
            file.Close();
            return data;
        }
        return (T)defaultData;
    }

    public void ResetData()
    {
        if (SaveExists())
        {
            File.Delete(_dataPath);
            Debug.Log("Data reset complete!");
        }
    }
}
