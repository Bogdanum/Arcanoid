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
public class BallVisualSettings
{
    public Sprite defaultSprite;
    public Color firstParticleColor;
    public Color secondParticleColor;
}
