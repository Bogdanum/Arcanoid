using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class JsonTilesetParser : ITilesetParser<TileProperties>
{
    private readonly TilesetTokens tilesetTokens;
    
    public JsonTilesetParser(TilesetTokens tilesetTokens)
    {
        this.tilesetTokens = tilesetTokens;
    }
    
    public Dictionary<int, TileProperties> ParseTileMap(string mapString)
    {
        var tileMap = new Dictionary<int, TileProperties>();
        tileMap.Add(0, new TileProperties { TileType = 0 });
        
        JObject tileMapObj = JObject.Parse(mapString);
        var tilesArray = (JArray)tileMapObj[tilesetTokens.TilesKey];

        if (tilesArray != null)
        {
            foreach (var tile in tilesArray)
            {
                int tileID = (int) tile[tilesetTokens.TileId] + 1;
                var propsArray = (JArray) tile[tilesetTokens.TileProperties];
                TileProperties currentTileProps = GeneratePropsFromPropsArray(propsArray);
                tileMap.Add(tileID, currentTileProps);
            }
        }
        return tileMap;
    }

    private TileProperties GeneratePropsFromPropsArray(JArray propsArray)
    {
        var tileProps = new TileProperties();
        foreach (var property in propsArray)
        {
            string propName = (string) property[tilesetTokens.PropertyName];
            int propValue = (int) property[tilesetTokens.PropertyValue];

            if (propName == tilesetTokens.TypeName) 
            { 
                tileProps.TileType = propValue;
            }
            else if (propName == tilesetTokens.SpriteName)
            {
                tileProps.TileRenderer = propValue;
            }
            else if (propName == tilesetTokens.BonusName)
            {
                tileProps.TileBonus = propValue;
            }
        }
        return tileProps;
    }
}
