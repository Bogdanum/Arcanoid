using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryGameDataStorage : IGameDataStorage
{
    private string _dataPath;

    public BinaryGameDataStorage(string dataFileName)
    {
        _dataPath = Path.Combine(Application.persistentDataPath, dataFileName);
    }
    public void Save(GameData gameData)
    {
        using (FileStream file = File.Create(_dataPath))
        {
            new BinaryFormatter().Serialize(file, gameData);
        }
    }

    public GameData Load()
    {
        GameData gameData = new GameData();

        if (File.Exists(_dataPath))
        {
            using (FileStream file = File.Open(_dataPath, FileMode.Open))
            {
                gameData = (GameData) new BinaryFormatter().Deserialize(file);
            }
        }
        return gameData;
    }
}
