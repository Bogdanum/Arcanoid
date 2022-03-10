using UnityEngine;

[CreateAssetMenu(fileName = "BallPhysicsSettings", menuName = "GameObjectsConfiguration/Ball/BallPhysicsSettings")]
public class BallPhysicsSettings : ScriptableObject
{
     [SerializeField] private float initialVelocity = 5;
     [SerializeField] private float maxVelocity = 10;
     [SerializeField] private float velocityIncreaseStep = 0.5f;

     public float InitialVelocity => initialVelocity;
     public float MaxVelocity => maxVelocity;
     public float VelocityIncreaseStep => velocityIncreaseStep;
}
