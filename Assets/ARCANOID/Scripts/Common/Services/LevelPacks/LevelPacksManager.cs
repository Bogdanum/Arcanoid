using UnityEngine;

public class LevelPacksManager : MonoBehaviour
{
    private ILevelParser<LevelData<TileProperties>> _levelParser;

    public void Init(JsonTokens jsonTokens, TextAsset tilesetFile)
    {
        _levelParser = new JsonLevelParser(tilesetFile.text, jsonTokens);
    }

    public LevelData<TileProperties> GetCurrentLevelData()
    {
        var testLevelFile = Resources.Load<TextAsset>("testLevel");
        if (testLevelFile == null)
        {
            Debug.LogError("Missing test map!");
        }
        return _levelParser.ParseLevelFromString(testLevelFile.text);
    }
}
