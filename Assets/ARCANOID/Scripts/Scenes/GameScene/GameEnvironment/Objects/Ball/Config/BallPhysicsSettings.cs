using System;
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
     [SerializeField] private ReboundParams verticalNormal;
     [SerializeField] private ReboundParams horizontalNormal;

     public float InitialVelocity => initialVelocity;
     public float MaxVelocity => maxVelocity;
     public float VelocityIncreaseStep => velocityIncreaseStep;
     public ReboundParams VerticalNormal => verticalNormal;
     public ReboundParams HorizontalNormal => horizontalNormal;
     
     [Serializable]
     public struct ReboundParams
     {
          public float minReboundAngle;
          public float reboundAngleMultiplier;
     }
}
