using UnityEngine;

[CreateAssetMenu(fileName = "BallBaseSettings", menuName = "GameObjectsConfiguration/Ball/BallBaseSettings")]
public class BallBaseSettings : ScriptableObject
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private int damage;

    public Sprite DefaultSprite => defaultSprite;
    public int Damage => damage;
}
