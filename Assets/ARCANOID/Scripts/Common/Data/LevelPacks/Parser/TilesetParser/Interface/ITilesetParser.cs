using System.Collections.Generic;

public interface ITilesetParser<TileProps>
{
     Dictionary<int, TileProps> ParseTileMap(string mapString);
}
