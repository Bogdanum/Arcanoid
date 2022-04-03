using UnityEngine;

[CreateAssetMenu(fileName = "JsonTokens", menuName = "Data/LevelPacks/JsonTokens", order = 3)]
public class JsonTokens : ScriptableObject
{
    [SerializeField] private LevelTokens levelTokens;
    [SerializeField] private TilesetTokens tilesetTokens;

    public LevelTokens LevelTokens => levelTokens;
    public TilesetTokens TilesetTokens => tilesetTokens;
}

[System.Serializable]
public struct LevelTokens
{
    public string Layers;
    public string Data;
    public string Width;
    public string Height;
}

[System.Serializable]
public struct TilesetTokens
{
    public string TilesKey;
    public string TileId;
    public string TileProperties;
    public string PropertyName;
    public string PropertyValue;
    public string TypeName;
    public string SpriteName;
    public string BonusName;
}