using UnityEngine;

public class ReboundDirectionCalculator
{
     private readonly Rigidbody2D _rigidbody2D;
     private readonly Transform _ballTransform;
     private readonly BallPhysicsSettings _ballPhysicsSettings;
     private float _velocity;

     public ReboundDirectionCalculator(Rigidbody2D rigidbody2D, Transform ballTransform, BallPhysicsSettings ballPhysicsSettings)
     {
          _rigidbody2D = rigidbody2D;
          _ballTransform = ballTransform;
          _ballPhysicsSettings = ballPhysicsSettings;
     }

     public void UpdateVelocity(float velocity) => _velocity = velocity;
     
     public void OnCollisionEnter2D(Collision2D other)
     {
          if (other.collider.TryGetComponent(out PlatformCollider _)) return;
          
          CalculateReboundAngle(Vector2.up, _ballPhysicsSettings.VerticalNormal , true);
          var reboundPerpendicular = _ballTransform.position.x < 0f ? Vector2.up : Vector2.right; 
          CalculateReboundAngle(reboundPerpendicular, _ballPhysicsSettings.HorizontalNormal, false);
     }

     private void CalculateReboundAngle(Vector2 reboundPerpendicular, BallPhysicsSettings.ReboundParams reboundParams , bool verticalCalc)
     {
          var perpendicular = verticalCalc ? Vector2.right : Vector2.up;
          float contactAngle = GetContactAngle(perpendicular, out float signX, out Vector3 outDirection);
          if (contactAngle < reboundParams.minReboundAngle)
          {
               var signY = Mathf.Sign(Vector2.Dot(outDirection, reboundPerpendicular));
               var targetAngle = reboundParams.reboundAngleMultiplier * signX * signY;
               var targetQuaternion = Quaternion.Euler(0f, 0f, targetAngle);
               _rigidbody2D.velocity = targetQuaternion * outDirection * _velocity;
          }
     }

     private float GetContactAngle(Vector2 perpendicular, out float signX, out Vector3 outDirection)
     {
          outDirection = _rigidbody2D.velocity.normalized;
          signX = Mathf.Sign(Vector2.Dot(outDirection, perpendicular));
          return Vector2.Angle(outDirection, perpendicular * signX);
     }
}
