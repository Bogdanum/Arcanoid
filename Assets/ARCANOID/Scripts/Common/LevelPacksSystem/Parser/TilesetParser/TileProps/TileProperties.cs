
public class TileProperties
{
    public int TileType { get; set; }
    public int TileRenderer { get; set; }
    public int TileBonus { get; set; }
    public int CustomHealth { get; set; }
    
    public TileProperties Copy() => (TileProperties)MemberwiseClone();
}
