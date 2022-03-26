using System.IO;
using UnityEditor;
using UnityEngine;

public class ResetStoredData : Editor
{
    [MenuItem("Game/StoredData/Reset progress")]
    public static void ResetProgress()
    {
        var fileName = $"{typeof(StoredGameProgress)}.dat";
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
            Debug.Log("Game progress removed!");
        }
    }
}
