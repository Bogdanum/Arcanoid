using UnityEngine;

[CreateAssetMenu(fileName = "BonusName", menuName = "GameObjectsConfiguration/Bonuses/Effects/Create New HiddenBallBonus config")]
public class HiddenBallBonusConfig : ScriptableObject
{
    [SerializeField] private bool randomBallsDirection;
    [SerializeField] private Vector2 defaultDirection = Vector2.down;
    [SerializeField, Min(1)] private int ballsCount = 1;

    public bool RandomBallsDirection => randomBallsDirection;
    public Vector2 DefaultDirection => defaultDirection;
    public int BallsCount => ballsCount;
}
