using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergyFxConfig", menuName = "UI/EnergyAnimation/EnergyFxConfig")]
public class EnergyFxConfig : ScriptableObject
{
    [SerializeField] private Vector2 fxEntitySize;
    [SerializeField] private Ease moveEaseType;
    [SerializeField] private float entityFlightDuration;
    [SerializeField] private int entitiesCount;
    [SerializeField] private float delayBetweenEntities;
    [SerializeField] private float maxDistanceBetweenEntities;
    [SerializeField] private float maxRotationAngle;

    public Vector2 FxEntitySize => fxEntitySize;
    public Ease MoveEaseType => moveEaseType;
    public float EntityFlightDuration => entityFlightDuration;
    public int EntitiesCount => entitiesCount;
    public float DelayBetweenEntities => delayBetweenEntities;
    public float MaxDistanceBetweenEntities => maxDistanceBetweenEntities;
    public float MaxRotationAngle => maxRotationAngle;
}
