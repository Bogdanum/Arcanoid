using UnityEngine;

public class PlayerPrefsStorage : IStorage
{
    private const string DATA_KEY = "GameData";
    
    public void Save(GameData gameData)
    {
        string data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(DATA_KEY, data);
        PlayerPrefs.Save();
    }

    public GameData Load()
    {
        GameData gameData = new GameData();

        if (PlayerPrefs.HasKey(DATA_KEY))
        {
            string data = PlayerPrefs.GetString(DATA_KEY);
            gameData = JsonUtility.FromJson<GameData>(data);
        }
        return gameData;
    }
}
