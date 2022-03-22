using UnityEngine;

[CreateAssetMenu(fileName = "BallBaseSettings", menuName = "GameObjectsConfiguration/Ball/BallBaseSettings")]
public class BallBaseSettings : ScriptableObject
{
    [SerializeField] private BallVisualSettings ballVisualSettings;
    [SerializeField] private int damage;
    
    public int Damage => damage;
    public BallVisualSettings BallVisualSettings => ballVisualSettings;
}

[System.Serializable]
public struct BallVisualSettings
{
    public Sprite defaultSprite;
    public Sprite rageSprite;
    public Color firstParticleColor;
    public Color secondParticleColor;
}
