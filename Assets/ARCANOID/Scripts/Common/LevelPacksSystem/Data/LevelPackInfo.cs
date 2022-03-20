public class LevelPackInfo
{
    public bool IsOpened { get; set; }
    public bool IsLast { get; set; }
    public bool IsRepassed { get; set; }
    public int CurrentLevel { get; set; }
    public int CompletedLevels { get; set; }
    public LevelPack Pack { get; set; }
}
