using UnityEngine;

[CreateAssetMenu(fileName = "LevelPack", menuName = "Data/LevelPacks/Create new pack", order = 1)]
public class LevelPack : ScriptableObject
{
    [SerializeField] private string packID;
    [SerializeField] private TextAsset[] levels;

    [Header("VISUAL PARAMS"), Space(20)] 
    
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite gameBackground;
    [SerializeField] private Sprite packButtonBackground;
    [SerializeField] private Color fontColor;

    public string PackID => packID;
    public int Count => levels.Length;
    public Sprite Icon => icon;
    public Sprite GameBackground => gameBackground;
    public Sprite PackButtonBackground => packButtonBackground;
    public Color FontColor => fontColor;
    public TextAsset GetLevel(int id) => levels[id];
}
