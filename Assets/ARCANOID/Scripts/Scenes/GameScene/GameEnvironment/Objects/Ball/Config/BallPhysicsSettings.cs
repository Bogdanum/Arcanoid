using UnityEngine;

[CreateAssetMenu(fileName = "BallPhysicsSettings", menuName = "GameObjectsConfiguration/Ball/BallPhysicsSettings")]
public class BallPhysicsSettings : ScriptableObject
{
     [SerializeField] private float initialVelocity = 5;
     [SerializeField] private float maxVelocity = 10;
     [SerializeField] private float velocityIncreaseStep = 0.5f;
     [Space(10)]
     [Header("REBOUND SETTINGS")]
     [Space(10)]
     [SerializeField, Range(5, 90)] private float minReboundAngle = 25;
     [SerializeField, Range(1, 50)] private float reboundAngleMultiplier = 15;
     [SerializeField, Range(0, 20)] private int maxNumberOfSharpRebounds = 2;

     public float InitialVelocity => initialVelocity;
     public float MaxVelocity => maxVelocity;
     public float VelocityIncreaseStep => velocityIncreaseStep;
     public float MinReboundAngle => minReboundAngle;
     public float ReboundAngleMultiplier => reboundAngleMultiplier;
     public int MaxNumberOfSharpRebounds => maxNumberOfSharpRebounds;
}
