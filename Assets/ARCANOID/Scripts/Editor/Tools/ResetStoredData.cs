using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ResetStoredData : Editor
{
    [MenuItem("Game/StoredData/Reset progress")]
    public static void ResetProgress()
    {
        ResetData(typeof(StoredGameProgress), "Game progress removed!");
    }

    [MenuItem("Game/StoredData/Reset energy save")]
    public static void ResetEnergySave()
    {
        ResetData(typeof(SavedEnergyProgress), "Saved energy removed!");
    }

    private static void ResetData(Type dataType, string message)
    {
        var fileName = $"{dataType}.dat";
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
            Debug.Log(message);
        }
    }
}
