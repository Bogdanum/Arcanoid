using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

public class JsonLevelParser : ILevelParser<LevelData<TileProperties>>
{
    private readonly LevelTokens _levelTokens;
    private readonly Dictionary<int, TileProperties> _tilePropsStorage;
    
    public JsonLevelParser(string tilesetString, JsonTokens jsonTokens)
    {
        _levelTokens = jsonTokens.LevelTokens;
        ITilesetParser<TileProperties> tilesetParser = new JsonTilesetParser(jsonTokens.TilesetTokens);
        _tilePropsStorage = tilesetParser.ParseTileMap(tilesetString);
    }
    
    public LevelData<TileProperties> ParseLevelFromString(string level)
    {
        var levelObject = JObject.Parse(level);
        int colCount = (int) levelObject[_levelTokens.Width];
        int rowCount = (int) levelObject[_levelTokens.Height];
        TileProperties[,] layerData = GatLayerData(levelObject, colCount, rowCount);
        
        return new LevelData<TileProperties>(colCount, rowCount, layerData);
    }

    private TileProperties[,] GatLayerData(JObject levelObject, int colCount, int rowCount)
    {
        var layer = (JObject) levelObject[_levelTokens.Layers][0];
        var dataArray = layer[_levelTokens.Data].Select(token => (int)token).ToArray();
        var layerData = new TileProperties[rowCount, colCount];

        for (int row = 0; row < rowCount; row++)
        {
            int line = row * colCount;
            for (int col = 0; col < colCount; col++)
            {
                int id = dataArray[line + col];
                layerData[row, col] = _tilePropsStorage[id].Copy();
            }
        }
        return layerData;
    }
}
